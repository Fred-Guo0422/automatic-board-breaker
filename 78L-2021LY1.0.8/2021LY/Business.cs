using _2021LY.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
/// 分板机业务逻辑
/*


*/
namespace _2021LY
{
    class Business
    {
        public Thread t;
        public Thread t1;
        public Thread t2;

       

        //进料请求
        public void get_Pcb()
        {

            if (scan_status == 0)
            {
                IOForm.getForm().SetDoOn(7, IOForm.getForm().Exo[6]);
            }

            if (cut_status == 0)
            {
                Goog.SetExtDo_Off(6);
                IOForm.getForm().SetDoOff(10, IOForm.getForm().Exo[9]);
                Thread.Sleep(100);
                IOForm.getForm().SetDoOn(10, IOForm.getForm().Exo[9]);
                IOForm.getForm().SetDoOn(11, IOForm.getForm().Exo[10]);
            }

            CommParam.PLC_bot.Write_D_short("D280", 1);
            m = 0;

                scan_Pcb_status = true;//进料请求线程
                this.t = null;
                this.t = new Thread(new ThreadStart(scan_Pcb));
                this.t.SetApartmentState(ApartmentState.STA);
                this.t.IsBackground = true;
                this.t.Start();

                cut_Pcb_status = true;//分板线程
                this.t1 = null;
                this.t1 = new Thread(new ThreadStart(cut_Pcb));
                this.t1.SetApartmentState(ApartmentState.STA);
                this.t1.IsBackground = true;
                this.t1.Start();

                plate_Back_status = true; //治具回流线程
                this.t2 = null;
                this.t2 = new Thread(new ThreadStart(plate_Back));
                this.t2.SetApartmentState(ApartmentState.STA);
                this.t2.IsBackground = true;
                this.t2.Start();

            CommParam.xianchen_on = true;
                //t = new Thread(() =>
                //{
                //    scan_Pcb_status = true;//918
                //    scan_Pcb();
                //});
                //t.IsBackground = true;
                //t.Start();

                //t1 = new Thread(() =>
                //{
                //    cut_Pcb_status = true;//918
                //    cut_Pcb();
                //});
                //t1.IsBackground = true;
                //t1.Start();


                //t2 = new Thread(() =>
                //{
                //    plate_Back_status = true; //918
                //    plate_Back();
                //});
                //t2.IsBackground = true;
                //t2.Start();

            /* */
        }

        public static bool scan_Pcb_status = false;
        public static bool cut_Pcb_status = false;
        public static bool plate_Back_status = false;
        /*
       分板机 请求进料 写D100值为1 ，上游PLC给板，则把D102值为1   ，送板完成   分板机将D100值为0  ，PLC将D102值为0
       治具下游回流  PLC请求进料 将D104值为1 ，分板机送料 将D106值为1   ，送完， 分板机将D106 值为0 PLC将D104值为0

       下料PLC 请求进料 将D110值为1 ，分板机送料 ，把D112值为1，送完  分板机把D112值为0   下料PLC 将D110值为0
       分板机请求进料 将D114值为1 ，PLC送料把D116值为1，送完  分板机把D114值为0   PLC将D116值为0
        */

        /*
        扫描工位   
        */

        public int m = 0;
        public static short scan_status = 0;// 扫码工位货物状态，0无货 。  1进入扫描工位 。 2扫码正常待分板。 3已发送来板请求
        private void scan_Pcb()
        {
            while (scan_Pcb_status)
            {
                Thread.Sleep(50);
                if (scan_status == 0)
                {
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(1);
                    }//918
                }

                if (scan_status == 0 && !Goog.gpi_status(8) && !Goog.gpi_status(7)) //如果无货，且DI8扫码枪工位进料到位感应、DI7 进料感应接通无信号输入，则请求
                {
                    Goog.SetExtDo_On(10); //DO1 - 10 分板进料请求 开启
                    IOForm.getForm().SetDoOff(8, IOForm.getForm().Exo[7]);// DO7进料电机  启动 
                    scan_status = 1;
                    LogHelper.WriteInfoLog("分板进料__已发送完进料请求，进入扫描工位中");
                }

                if (scan_status == 1)
                {
                    IOForm.getForm().SetDoOff(8, IOForm.getForm().Exo[7]);// DO7进料电机  启动 
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(1);
                    }//918
                }

                if (scan_status == 1 && Goog.gpi_status(8))//DI8扫码枪工位进料到位感应   则停止进料电机   开启扫码枪
                {
                    Goog.SetExtDo_Off(10); //DO1 - 10 分板进料请求 停止

                    //CommParam.PLC_top.Write_D_short("D100", 0);//给D100 写入0，请求成功，开始进料，复位D100
                    Thread.Sleep(800);//感应到后，多走一段时间，具体时长根据现场情况调整
                    IOForm.getForm().SetDoOn(8, IOForm.getForm().Exo[7]);// DO7 停止进料电机
                    LogHelper.WriteInfoLog("分板进料__进料完成，扫码MES通信中");
                    /***开启扫码枪，从mes取验证 ***/

                    var Resutlt = MES.ReadID();// 验证结果 
                    if (Resutlt[0] != "3")//  3  扫描异常     2 小板NG
                    {
                        int a = 0;
                        if (m == 0)
                        {
                            a = 12;
                        }
                        else if (m == 1)
                        {
                            a = 15;
                        }
                        else if (m == 2)
                        {
                            a = 18;
                        }
                        string st = Resutlt[Resutlt.Length - 1] + ":";
                        for (int i = 0; i < Resutlt.Length; i++)
                        {
                            if (Resutlt[i] == "2")
                            {
                                int x = a * 10 + i;
                                CommParam.PLC_bot.Write_D_short("D" + x, 1);
                                LogHelper.WriteInfoLog("分板进料_扫码发送plc,Resutlt=2的板：D" + x);
                                st += i + "-";
                                Thread.Sleep(10);
                            }
                        }
                        Console.WriteLine(st);

                        if (m == 2)
                        {
                            m = 0;
                        }
                        else
                        {
                            m++;
                        }
                        scan_status = 2;
                        LogHelper.WriteInfoLog("分板进料__扫码MES通信完成，待分板");
                    }
                    else
                    {

                        CommParam.Bussiness_Err_Msg = "";
                        CommParam.Saoma = true;
                        DesignForm.getForm().ShowErrorDialog("扫码MES通信异常，请先取板，再点确定");
                        CommParam.Saoma = false;
                        scan_status = 0;
                        LogHelper.WriteDebugLog("分板进料__扫码MES通信异常。扫码MES通信异常，请先取板，再点确定");

                    }

                }
                if (scan_status == 2)
                {
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(1);
                    }//918
                }
            }
            // Console.WriteLine("scan_Pcb 退出");
        }


        //分板工位 单启线程调用
        public Thread PcbRun;
        public Thread checkNif;
        ///分板状态   0无料 1下进料阻挡  2板进分板工位中  3板已进入分板工位 4升顶升气缸 5铣刀分板中 6分板完成顶升气缸降
        /// 7 降到位，可出料  8出料中  9出料完成  10 分板中
        /// 

        public bool err = true;
        public static short cut_status = 0;
        private void cut_Pcb()
        {
            while (cut_Pcb_status)//918
            {
                Thread.Sleep(50);
                if (cut_status == 0) //扫码工位有板待进，分板工位已经空出  则进板  下阻挡 开电机
                { //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(2);
                    }//918
                }
                // Console.WriteLine("cut_status:"+cut_status);
                if (scan_status == 2 && cut_status == 0) //扫码工位有板待进，分板工位已经空出  则进板  下阻挡 开电机
                {
                    LogHelper.WriteInfoLog("分板__降阻挡，准备进板");
                    IOForm.getForm().SetDoOff(7, IOForm.getForm().Exo[6]);// DO6 降进料阻挡气缸
                    cut_status = 1;
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(2);
                    }//918
                }

                if (cut_status == 1 && Goog.gpi_status(10)) //DO6阻挡降且DI10扫码枪工位阻挡气缸下限位 感应到
                {
                    LogHelper.WriteInfoLog("分板__降阻挡完成，开始进板");
                    IOForm.getForm().SetDoOff(8, IOForm.getForm().Exo[7]);// DO7 开扫描工位进料电机
                    IOForm.getForm().SetDoOff(9, IOForm.getForm().Exo[8]);// DO8 开出料工位电机
                    cut_status = 2;
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(2);
                    }//918
                }

                if (cut_status == 2&& Goog.GetExtIoBit(14)) //因为分板工位没有感应光电，所以根据实际情况设置皮带大概运行到位
                {
                    Thread.Sleep(300);
                    IOForm.getForm().SetDoOn(7, IOForm.getForm().Exo[6]);//DO6 升起进料阻挡气缸
                    IOForm.getForm().SetDoOn(8, IOForm.getForm().Exo[7]);// DO7 关扫描工位进料电机
                    IOForm.getForm().SetDoOn(9, IOForm.getForm().Exo[8]);// DO8 关出料工位电机
                    scan_status = 0;
                    LogHelper.WriteInfoLog("分板__进板完成，准备顶起");
                    LogHelper.WriteInfoLog("分板进料__进料工位空出，可以进板");
                    cut_status = 3;
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(2);
                    }//918
                }

                if (cut_status == 3)
                {
                    //板到位，顶升气缸顶起PCB固定
                    //IOForm.getForm().SetDoOff(10, IOForm.getForm().Exo[9]); //DO9 分板顶升气缸
                    Goog.SetExtDo_On(6); //DO1-6 分板顶升气缸 上
                    Thread.Sleep(600); //1秒的时间 完成顶升
                    LogHelper.WriteInfoLog("分板__进板完成，开始顶起");
                    cut_status = 4;
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(2);
                    }//918


                }
                //
                if (cut_status == 4 && !Goog.gpi_status(11) && err)//DI11	分板顶升气缸上限位没有亮，有可能卡板，或者孔位没对好，则报警  因为板硬件包裹需手动处理卡板
                {
                    LogHelper.WriteInfoLog("分板__进板完成，顶起不到位，报警");


                    //CommParam.Bussiness_Err_Msg = "分板顶升气缸没到位。请按复位，调整好治具位置后。再按启动重新启动";

                    LogHelper.WriteDebugLog(CommParam.Bussiness_Err_Msg + "。分板__进板完成，顶起不到位，报警");
                    //DesignForm.getForm().Stop_Auto();

                    err = false;
                    cut_status = 11;//跳到报警
                }

                if (cut_status == 11 && !Goog.gpi_status(11) && !err)//进入报警异常处理
                {
                    CommParam.Ding = true;//报警处理条件
                    bool reset = true;
                    CommParam.Bussiness_Err_Msg = "分板顶升气缸没到位。请按复位，调整好治具位置后。再按启动重新启动";
                    while (true)
                    {
                        if (Goog.gpi_status(2) && reset == true)
                        {
                            Goog.SetExtDo_Off(6);
                            IOForm.getForm().SetDoOff(10, IOForm.getForm().Exo[9]);//顶升气缸下

                            // IOForm.getForm().SetDoOff(7, IOForm.getForm().Exo[6]);//进料气缸下
                            reset = false;
                        }
                        if (Goog.gpi_status(0) && reset == false)
                        {
                            CommParam.Bussiness_Err_Msg = "故障已排除？是否继续分板";

                            IOForm.getForm().SetDoOn(10, IOForm.getForm().Exo[9]);
                            //Goog.SetExtDo_Off(6); //DO1-6 分板顶升气缸 上
                            //  IOForm.getForm().SetDoOn(7, IOForm.getForm().Exo[6]);//进料气缸上

                            break;
                        }
                        Thread.Sleep(20);
                    }
                    cut_status = 3;
                }
                if (cut_status == 4 && Goog.gpi_status(11) && !ddok) //顶升到位，则开始分板
                {
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(2);
                        PcbRun.Abort();
                    }//918
                    err = true;
                    LogHelper.WriteInfoLog("分板__顶起完成，开始启动分板");
                    Start_Cut();
                    LogHelper.WriteInfoLog("分板__分板中");
                    BusinessForm.getForm().totalNum++; //计数
                    if (CommParam.Use_Vision)//如果使用视觉
                    {
                        //调用视觉进行分板

                    }
                    else//否则根据轨迹文件进行分板
                    {

                        /* 执行分板自动运行  */



                        //PcbRun = new Thread(() =>
                        //{
                            DesignForm.getForm().Zb_Run1();//*****************************************************************************
                        //});
                        //PcbRun.IsBackground = true;
                        //PcbRun.Start();

                    }

                   // CommParam.cut_pcb_ing = true;
                    cut_status = 10;
                    LogHelper.WriteInfoLog("分板__分板完成");
                }

                if (cut_status == 10)
                {
                    if (!CommParam.cut_pcb_ing)
                    {
                        stop_Cut();
                        cut_status = 5;
                        CommParam.dui_dao_OK = true;
                    }
                }


                if (CommParam.dui_dao_OK)
                {
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(2);
                        checkNif.Abort();
                    }//918
                   
                    LogHelper.WriteInfoLog("分板__开始对刀");

                    this.checkNif = null;
                    this.checkNif = new Thread(new ThreadStart(dui_Dao));
                    this.checkNif.SetApartmentState(ApartmentState.STA);
                    this.checkNif.IsBackground = true;
                    this.checkNif.Start();

                    //checkNif = new Thread(() =>
                    //    {
                    //        dui_Dao();

                    //    });
                    //    checkNif.IsBackground = true;
                    //    checkNif.Start();
                    CommParam.dui_dao_OK = false;
                }
                
                if (cut_status == 5)
                {
                    LogHelper.WriteInfoLog("分板__分完板，降顶升气缸");
                    Goog.SetExtDo_Off(6); //  复位DO1-6 分板顶升气缸 上
                    Thread.Sleep(100);
                    IOForm.getForm().SetDoOff(10, IOForm.getForm().Exo[9]); // 置位DO9 分板顶升气缸 下
                    IOForm.getForm().SetDoOff(11, IOForm.getForm().Exo[10]); //DO10 分板阻挡气缸 降
                    cut_status = 6;
                    //Thread.Sleep(800);//1秒的时间完成顶升气缸降，时间到，检查下降是否正常
                                       //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(2);
                    }//918
                }
                if (cut_status == 6 && Goog.gpi_status(12))//顶升气缸下降到位 DI12	分板顶升气缸下限位   
                {
                    cut_status = 7;//分板完成
                    LogHelper.WriteInfoLog("分板__降顶升气缸完成，待出料");
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(2);
                    }//918
                }

                //下料PLC 请求进料 将D110值为1 ，分板机送料 ，把D112值为1，送完  分板机把D112值为0   下料PLC 将D110值为0
                if (cut_status == 7)//&& Goog.GetExtIoBit(12)) //分板完成，被请求出料
                {
                    //ssThread.Sleep(2000);
                    short va = CommParam.PLC_bot.Read_D_short("D110");//分板完成，被请求出料
                    LogHelper.WriteInfoLog("分板__监控下料机的下料请求中.....当前D110值为：" + va);
                    Thread.Sleep(500);//1000
                    if (va == 1) // (true)//-
                    {
                        LogHelper.WriteInfoLog("分板__出料中");
                        IOForm.getForm().SetDoOff(9, IOForm.getForm().Exo[8]);// DO8 开出料工位电机
                        CommParam.PLC_bot.Write_D_short("D112", 1);
                        cut_status = 8;
                    }
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(2);
                    }//918
                }
                if (cut_status == 8)
                {
                    IOForm.getForm().SetDoOff(9, IOForm.getForm().Exo[8]);// DO8 开出料工位电机
                }//918

                if (cut_status == 8 && Goog.GetExtIoBit(4))//DI1 - 4   出料感应
                {
                    Thread.Sleep(1500);
                   
                    IOForm.getForm().SetDoOn(11, IOForm.getForm().Exo[10]); //DO10 分板阻挡气缸 升
                    IOForm.getForm().SetDoOn(10, IOForm.getForm().Exo[9]); // 复位DO9 分板顶升气缸 下
                    CommParam.PLC_bot.Write_D_short("D112", 0);
                    cut_status = 0;
                    LogHelper.WriteInfoLog("分板__出料完成，待进板");
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(2);
                    }//918
                }

            }
            // Console.WriteLine("cut_Pcb 退出");
        }
        public bool ddok = false;
        // 返回true则 OK  
        public void dui_Dao()
        {
            CommParam.dui_dao_OK = false;
            string Vel = string.Empty;
            string Acc = string.Empty;
            string End = string.Empty;
            toolSetting.Readspeed(ref Vel, ref Acc, ref End);

            ddok = true;
            string[] left = CommParam.Ddl.Split('_');
            string[] right = CommParam.Ddr.Split('_');

            string b = right[0] + "," + right[1] + "," + right[2] + "," + left[0] + "," + left[1] + "," + left[2] + "," + left[0] + "," + left[1] + ",10";
            //left[0] + "," + left[1] + "," + 5;

            //string b = "23950,-2446,3956,76990,-2446,3956,76990,-2446,5";
            int a = 0;
            for (int i = 0; i < 3; i++)
            {
                string[] xx = b.Split(',');
                int x = Convert.ToInt32(xx[i + a]);
                int y = Convert.ToInt32(xx[i + 1 + a]);
                int z = Convert.ToInt32(xx[i + 2 + a]);
                a += 2;

                //Goog.Go_Single_Poin(x, y, z);
                Goog.Go_Single_Poin_dui_Dao(x, y, z);

                //while (i < 2)
                //{
                if (i == 0 || i == 1)
                {
                    if (!CommParam.auto_Run_On)
                    {

                        break;  //如果停止或者急停，则跳出
                    }
                    if (CommParam.XYZ_duidao)
                    {
                        Thread.Sleep(100);
                        bool o = Goog.gpi_status(15);//接通则 OK
                        bool err = Goog.GetExtIoBit(1);//接通则 NG
                        string mm = "0";
                        if (!o && err)
                        {
                            CommParam.Duidao = true;
                            //IOForm.getForm().ShowErrorDialog_Msg("对刀仪检测失败，请停止自动检查铣刀是否折断，后重新启动");
                            // DesignForm.getForm().Stop_Auto();
                            //DesignForm.getForm().ShowErrorDialog("对刀仪检测失败，请停止自动检查铣刀是否折断");
                            if (i == 0)
                            {
                                CommParam.Bussiness_Err_Msg = "左对刀仪检测失败，请更换铣刀，按提示回原点后再重新自动运行";
                                LogHelper.WriteInfoLog("分板__左对刀失败");
                                LogHelper.WriteDebugLog(CommParam.Bussiness_Err_Msg + " 。分板__左对刀失败");
                                CommParam.Is_Gohome = false;
                                CommParam.XYZ_duidao = false;
                                ddok = false;
                                Thread.Sleep(1400);
                                DesignForm.getForm().Stop_Auto();
                                break;
                            }
                            else if (i == 1)
                            {
                                CommParam.Bussiness_Err_Msg = "右对刀仪检测失败，请更换铣刀，按提示回原点后再重新自动运行";
                                LogHelper.WriteInfoLog("分板__右对刀失败");
                                LogHelper.WriteDebugLog(CommParam.Bussiness_Err_Msg + " 。分板__右对刀失败");
                                CommParam.Is_Gohome = false;
                                CommParam.XYZ_duidao = false;
                                ddok = false;
                                Thread.Sleep(1400);
                                DesignForm.getForm().Stop_Auto();
                                break;
                            }
                        }
                        else
                        {
                            LogHelper.WriteInfoLog("分板__对刀完成");
                            CommParam.XYZ_duidao = false;
                          
                        }
                    }

                   // }
                }
            }

            ddok = false;
           
            DesignForm.getForm().Zb_Run1_StartPoint();
        }


        public static short plate_back = 0;//治具回流状态   0  无货  1 已发请求   2下压完成   3吸尘完毕 
        //4 下压气缸顶起  5 顶起完成   6降阻挡开启回流电机  7出料中
        bool isSend = false;
        //治具回流
        private void plate_Back()
        {
            int one = 0;
            while (plate_Back_status)
            {
                Thread.Sleep(50);
                // Console.WriteLine("plate_back:" + plate_back);
                /*分板机请求进料 将D114值为1 ，PLC送料把D116值为1，送完 分板机把D114值为0   PLC将D116值为0*/
                if (plate_back == 0 && !Goog.GetExtIoBit(4))// && !Goog.GetExtIoBit(5))
                {
                   
                    CommParam.PLC_bot.Write_D_short("D114", 1);
                    Thread.Sleep(1000);
                    if (one == 0) //避免多次日志
                    {
                        LogHelper.WriteInfoLog("治具回流__请求进板，已将下料机D114写入1");
                        one++;
                    }
                    DesignForm.getForm().uiTextBox2.Text = "《治具回流》：等待下料机给料信号D116";
                    short va = CommParam.PLC_bot.Read_D_short("D116");
                    LogHelper.WriteInfoLog("治具回流__请求进板，读取下料机D116，值为：" + va);
                    if (va == 1) //(true)//-
                    {
                        one = 0;
                        Goog.SetExtDo_On(9); //DO1 - 9治具回流电机  启动
                        plate_back = 1;
                        DesignForm.getForm().uiTextBox2.Text = "《治具回流》：等待进料感应器感应有料！";
                    }
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(3);
                    }//918
                }

                if (plate_back == 1 || Goog.GetExtIoBit(5))// DI1 - 5   治具回流进料 感应到
                {
                    LogHelper.WriteInfoLog("治具回流__进板");
                    Goog.SetExtDo_On(9); //DO1 - 9治具回流电机  启动
                    CommParam.PLC_bot.Write_D_short("D114", 0);
                    LogHelper.WriteInfoLog("治具回流__检测到进板，已将下料机D114写入0");
                    Thread.Sleep(1000);//待运行到阻挡
                    Goog.SetExtDo_Off(9); //DO1 - 9治具回流电机 停止
                    //Goog.SetExtDo_On(7);  // DO1 - 7   治具回流下压气缸 下
                    //Thread.Sleep(1500);//待下压完成  
                    plate_back = 2;
                    DesignForm.getForm().uiTextBox2.Text = "《治具回流》：等待上料位允许出料请求！";
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(3);
                    }//918
                }
                //if (plate_back == 2 && Goog.GetExtIoBit(9)) //  DI1 - 9   治具回流下压气缸下限位   下压到位
                //{
                //    LogHelper.WriteInfoLog("治具回流__进板完成,开始吸尘");
                //    Goog.SetExtDo_On(0);//DO1 - 0 吸尘吹气
                //    /*****开启吸尘***/                 //吸尘
                //    Thread.Sleep(2000);//吸尘2秒
                //    Goog.SetExtDo_Off(0);
                //    plate_back = 3;
                //}
                //if (plate_back == 3)
                //{
                //    Goog.SetExtDo_Off(7);  // 复位DO1 - 7   治具回流下压气缸  下
                //    Goog.SetExtDo_On(1); //置位 DO1 - 1     治具回流下压气缸 上
                //    plate_back = 4;
                //}

                //if (plate_back == 4 && Goog.GetExtIoBit(8)) //  DI1-8	治具回流下压气缸上限位 上升到位 
                //{
                //    Goog.SetExtDo_Off(1);  // 复位 DO1 - 1   治具回流下压气缸 上
                //    plate_back = 5;
                //    LogHelper.WriteInfoLog("治具回流__吸尘完成,待出料");
                //}

                /* 治具下游回流 PLC请求进料 将D104值为1 ，分板机送料 将D106值为1   ，送完， 分板机将D106 值为0 PLC将D104值为0*/
                if (plate_back == 2 && Goog.GetExtIoBit(13))  //DI1-13	治具回流出料请求
                {
                    LogHelper.WriteInfoLog("治具回流__开始出料");
                    //IOForm.getForm().SetDoOff(15, IOForm.getForm().Exo[14]); //DO14 治具回流阻挡气缸 下降
                    Goog.SetExtDo_On(9); //DO1 - 9治具回流电机  启动
                    plate_back = 6;
                    DesignForm.getForm().uiTextBox2.Text = "《治具回流》：等待出料感应有料！";
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(3);
                    }
                }

                if (plate_back == 6)
                {
                    Goog.SetExtDo_On(9); //DO1 - 9治具回流电机  启动
                }//918

                if (plate_back == 6 && Goog.GetExtIoBit(11))//&& Goog.GetExtIoBit(7)    DI1 - 7 治具回流阻挡下限位  DI1-11	治具回流出口感应
                {

                    Thread.Sleep(3000);//大概运行时间，已经回流完成，停止电机
                    plate_back = 7;
                    Goog.SetExtDo_Off(9); //DO1 - 9治具回流电机  停止
                    //IOForm.getForm().SetDoOn(15, IOForm.getForm().Exo[14]); //DO14 治具回流阻挡气缸 升起
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(3);
                    }//918
                }
                if (plate_back == 7)//&& Goog.GetExtIoBit(6))// DI1 - 6   治具回流阻挡上限位
                {
                    plate_back = 0;
                    DesignForm.getForm().uiTextBox2.Text = "《治具回流》：出料完成，等待进料感应无料！";
                    LogHelper.WriteInfoLog("治具回流__出料完成");
                    //918
                    if (CommParam.auto_Run_On == false)
                    {
                        Stop_Auto(3);
                    }//918
                }
            }
        }



        public void start_Cut()
        {
            /*检查切割主轴并启动*/
            IOForm.getForm().SetDoOff(13, IOForm.getForm().Exo[12]); //DO12 主轴冷却吹风   开启
            Thread.Sleep(300);//吹风和主轴不可同时启动
            IOForm.getForm().SetDoOff(14, IOForm.getForm().Exo[13]);// DO13 主轴启动 开始旋转
            Thread.Sleep(500);//启动后等半秒开始检测
            bool err = Goog.GetExtIoBit(1);
            Thread.Sleep(40);
            bool run1 = Goog.GetExtIoBit(2);
            Thread.Sleep(40);
            bool run2 = Goog.GetExtIoBit(3);
            if (!err || !run1 || !run2)
            {
                IOForm.getForm().SetDoOn(14, IOForm.getForm().Exo[13]);
                Thread.Sleep(30);
                IOForm.getForm().SetDoOn(13, IOForm.getForm().Exo[12]);

                CommParam.ZhuZhou = true;
                CommParam.Bussiness_Err_Msg = "停止机器，切割旋转主轴异常，检查主轴控制器，后重新启动" + err + run1 + run2;
                LogHelper.WriteDebugLog(CommParam.Bussiness_Err_Msg + "切割旋转主轴异常");
                DesignForm.getForm().Stop_Auto();
            }
            //开启吸尘器
            IOForm.getForm().SetDoOff(16, IOForm.getForm().Exo[15]);

        }


        public void Start_Cut()
        {
            /*检查切割主轴并启动*/
            IOForm.getForm().SetDoOff(13, IOForm.getForm().Exo[12]); //DO12 主轴冷却吹风   开启
            Thread.Sleep(300);//吹风和主轴不可同时启动
            bool left = true;
            bool right = true;
            if (CommParam.left_Knife_Switch)//启动左主轴
            {
                left = Start_left_Knift();
            }
            if (CommParam.right_Knife_Switch)//启动右主轴
            {
                right = Start_right_Knift();
            }

            if (!left || !right)
            {
                string a = "";
                CommParam.ZhuZhou = true;
                if (!left)
                {
                    a += "左主轴";
                }
                if (!right)
                {
                    a += "右主轴";
                }
                CommParam.Bussiness_Err_Msg = "停止机器，切割旋转" + a + "异常，检查主轴控制器，后重新启动";
                LogHelper.WriteDebugLog(CommParam.Bussiness_Err_Msg + "切割旋转主轴异常");
                IOForm.getForm().SetDoOn(13, IOForm.getForm().Exo[12]);
                DesignForm.getForm().Stop_Auto();
            }

            //开启吸尘器
            IOForm.getForm().SetDoOff(16, IOForm.getForm().Exo[15]);
        }

        public bool Start_left_Knift()
        {
            IOForm.getForm().SetDoOff(14, IOForm.getForm().Exo[13]);// DO13 主轴启动 开始旋转
            Thread.Sleep(550);//启动后等半秒开始检测
            bool run1 = Goog.GetExtIoBit(3);
            if (!run1)
            {
                IOForm.getForm().SetDoOn(14, IOForm.getForm().Exo[13]);
                return false;
            }
            return true;
        }
        public bool Start_right_Knift()
        {
            IOForm.getForm().SetDoOff(12, IOForm.getForm().Exo[11]);// DO12 右主轴启动 开始旋转
            Thread.Sleep(550);//启动后等半秒开始检测
            bool run2 = Goog.GetExtIoBit(2);
            if (!run2)
            {
                IOForm.getForm().SetDoOn(12, IOForm.getForm().Exo[11]);
                return false;
            }
            return true;
        }

        public void stop_Cut()
        {
            IOForm.getForm().SetDoOn(12, IOForm.getForm().Exo[11]);
            IOForm.getForm().SetDoOn(14, IOForm.getForm().Exo[13]);
            IOForm.getForm().SetDoOn(13, IOForm.getForm().Exo[12]);
            Thread.Sleep(50);
            IOForm.getForm().SetDoOn(16, IOForm.getForm().Exo[15]);
        }


        void Stop_Auto(int tag)
        {
            if (CommParam.auto_Run_On == false)
            {
                try
                {
                    switch (tag)
                    {
                        case 1:
                            scan_Pcb_status = false;
                            IOForm.getForm().SetDoOn(8, IOForm.getForm().Exo[7]);// DO7进料电机
                            t.Abort();
                            break;
                        case 2:
                            cut_Pcb_status = false;
                            t1.Abort();//主线程
                            checkNif.Abort();//对刀线程
                            PcbRun.Abort();//分板线程
                            break;
                        case 3:
                            plate_Back_status = false;
                            t2.Abort();
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception) { }
            }
        }

        public void stop_xiancheng()
        {
            Stop_Auto(1);
            Stop_Auto(2);
            Stop_Auto(3);
        }
    }
}
