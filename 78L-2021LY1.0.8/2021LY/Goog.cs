using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static gts.mc;

/// <summary>
/// 固高板卡类   
/// </summary>
namespace _2021LY
{
    class Goog : ControlCard
    {
        //gts.mc.TTrapPrm trapPrm;
        static gts.mc.TJogPrm jog;

        /// <summary>
        /// 初始化
        /// </summary>
        public override bool Initial_Card(out string msg)
        {
            msg = "初始化成功！";
            try
            {
                if (!CommUtil.CheckState(gts.mc.GT_Open(0, 1))) // 打开运动控制器
                {
                    msg = "打开运动控制器失败！";
                    return false;
                }
                if (!CommUtil.CheckState(Reset())) // 复位运动控制器
                {
                    msg = "复位运动控制器失败！";
                    return false;
                }
                if (!CommUtil.CheckState(gts.mc.GT_LoadConfig("GT8000.cfg"))) // 下载控制器配置文件
                {
                    msg = "控制器配置文件打开失败！";
                    return false;
                }

                if (!CommUtil.CheckState(Clear_Sts(1, 4)))// 清除1~4轴的轴状态
                {
                    msg = "清除1~4轴的轴状态失败！";
                    return false;
                }

                if (CommParam.One_Extend_Car)
                {
                    gts.mc.GT_OpenExtMdl("gts.dll");
                    //加载扩展模块配置文件
                    gts.mc.GT_LoadExtConfig("mdl.cfg");
                }

                return true;
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Initial_Card 初始化卡 操作异常 ", x);
                //Logger.Recod_Log_File("Initial_Card 初始化卡操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }

        }

        public static void Close()
        {
            gts.mc.GT_Close();
        }



        /// <summary>
        /// 读取轴状态   axis 起始轴号，正整数。  status 32 位轴状态字 (512代表轴已经使能) 。
        /// </summary>
        public static void Get_Status(short axis, out int status)
        {
            try
            {
                uint pClock;
                gts.mc.GT_GetSts(axis, out status, 1, out pClock);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Goog.Get_Status 读取轴状态  操作异常 ", x);
               // Logger.Recod_Log_File("Goog.Get_Status 读取轴状态 操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }
        /// <summary>
        /// 读取轴当前位置，即编码器位置  axis 轴号  如果获取距离，需要乘以脉冲对应距离的系数
        /// </summary>
        public static double Get_AxisPos(short axis)
        {
            uint pClock;
            double encpos = 0;
            try
            {
                gts.mc.GT_GetEncPos(axis, out encpos, 1, out pClock);
                return encpos;
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Goog.Get_AxisPos 读取轴当前位置  操作异常 ", x);
               // Logger.Recod_Log_File("Goog.Get_AxisPos 读取轴当前位置 操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }


        /// <summary>
        /// 轴去使能
        /// </summary>
        public static short Axis_Off(short aixs)
        {
            return gts.mc.GT_AxisOff(aixs);
        }

        /// <summary>
        /// 单轴使能
        /// </summary>
        public static short Axis_On(short aixs)
        {
            try
            {
                return gts.mc.GT_AxisOn(aixs);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Goog.Axis_On 单轴使能操作异常 操作异常 ", x);
               // Logger.Recod_Log_File("Goog.Axis_On 单轴使能操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }

        /// <summary>
        /// 接通伺服使能
        /// </summary>
        public static void All_Axis_On()
        {
            for (short i = 1; i <= CommParam.Use_Axis; i++)
            {
                Goog.Axis_On(i);
            }
        }

        /// <summary>
        /// 断开伺服使能
        /// </summary>
        public static void All_Axis_Off()
        {
            for (short i = 1; i <= CommParam.Use_Axis; i++)
            {
                Goog.Axis_Off(i);
            }
        }






        /// <summary>
        /// 清除轴状态     short aixs:起始轴号。正整数 ,short count  轴数
        /// </summary>
        public static short Clear_Sts(short aixs, short count)
        {
            try
            {
                return gts.mc.GT_ClrSts(aixs, count);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Goog.Clear_Sts 清除轴状态 操作异常 ", x);
               // Logger.Recod_Log_File("Goog.Clear_Sts 清除轴状态操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }
        /// <summary>
        ///位置清零
        /// </summary>
        public static void Zero_Pos()
        {
            try
            {
                gts.mc.GT_ZeroPos(1, 8);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Goog.Zero_Pos 位置清零  操作异常 ", x);
               // Logger.Recod_Log_File("Goog.Zero_Pos 位置清零 操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }

        /// <summary>
        /// 复位
        /// </summary>
        public static short Reset()
        {
            try
            {
                return gts.mc.GT_Reset();
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Goog.Reset 复位 操作异常 ", x);
               // Logger.Recod_Log_File("Goog.Reset 复位 操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }
        /// <summary>
        /// 1234轴是否运动   true 运动中   false没有运动
        /// </summary>
        public static bool Axis_Is_Run()
        {
            int times = 0;

            if (CommParam.X_Status == CommParam.AxisRunning || CommParam.Y_Status == CommParam.AxisRunning || CommParam.Z_Status == CommParam.AxisRunning)
            {
                return true;
            }
            else
            {
                while (true)  
                {
                    if (CommParam.X_Status != CommParam.AxisRunning && CommParam.Y_Status != CommParam.AxisRunning && CommParam.Z_Status != CommParam.AxisRunning)
                    {
                        times++;
                        Thread.Sleep(10);
                        if (times >= 5)
                        {
                            times = 0;
                            return false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// jog运动
        ///  axis轴号1开始  vel目标速度  acc加速度  dec减速度  direction方向（1或-1） 
        /// </summary>

        public static void Jog_Action(short axis, double vel, double acc, double dec, short direction)
        {
            try
            {
                gts.mc.GT_PrfJog(axis);//设置为jog模式    
                jog.acc = acc;
                jog.dec = dec;
                gts.mc.GT_SetJogPrm(axis, ref jog);//设置jog运动参数
                gts.mc.GT_SetVel(axis, direction * vel);//设置目标速度
                gts.mc.GT_Update(1 << (axis - 1));//更新轴运动
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Goog.Jog_Action jog运动  操作异常 ", x);
                //Logger.Recod_Log_File("Goog.Jog_Action jog运动 操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }

        /// <summary>
        /// 绝对位置运动（回零后）20210723 pass
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="velP">速度</param>
        /// <param name="acc">加速度</param>
        /// <param name="dec">减速度</param>
        /// <param name="posP">目标位置</param>
        /// <returns></returns>
        public static void P2PAbs(short axis, int posP,double Vel, double Acc, double Dec,int smoothTimeA )  //单轴绝对位置点位运动（轴号，速度，加速度，平滑时间，位置）
        {
            short sRtn = 0;//返回值
            gts.mc.TTrapPrm trap;
            sRtn += gts.mc.GT_PrfTrap(axis);     //设置为点位运动，模式切换需要停止轴运动。
            //若返回值为 1：若当前轴在规划运动，请调用GT_Stop停止运动再调用该指令。
            sRtn += gts.mc.GT_GetTrapPrm(axis, out trap);       /*读取点位运动参数（不一定需要）。若返回值为 1：请检查当前轴是否为 Trap 模式
                                                                    若不是，请先调用 GT_PrfTrap 将当前轴设置为 Trap 模式。*/
            trap.acc = Acc;              //单位pulse/ms2
            trap.dec = Acc;              //单位pulse/ms2
            trap.velStart = 0;           //起跳速度，默认为0。
            trap.smoothTime = (short)smoothTimeA;         //平滑时间，使加减速更为平滑。范围[0,50]单位ms。，

            sRtn += gts.mc.GT_SetTrapPrm(axis, ref trap);//设置点位运动参数。

            sRtn += gts.mc.GT_SetVel(axis, Vel);        //设置目标速度
            sRtn += gts.mc.GT_SetPos(axis, posP);        //设置目标位置
            sRtn += gts.mc.GT_Update(1 << (axis - 1));   //更新轴运动

            // 等待运动完成
            short run;
            // 坐标系的缓存区剩余空间查询变量
            int segment;
            // 坐标系的缓存区剩余空间查询变量
            int space;
            //sRtn = gts.mc.GT_CrdStatus(1, out run, out segment, 0);
            //do
            //{
            //    // 查询坐标系1的FIFO的插补运动状态
            //    sRtn = gts.mc.GT_CrdStatus(
            //    1,  // 坐标系是坐标系1
            //    out run,  // 读取插补运动状态
            //    out segment,  // 读取当前已经完成的插补段数
            //    0); // 查询坐标系1的FIFO0缓存区
            //        // 坐标系在运动, 查询到的run的值为1
            //} while (run == 1);

            uint pClock;        //时钟信号
            int pStatus;        //轴状态字
            double prfPos;      //规划脉冲
            do
            {
                sRtn = gts.mc.GT_GetSts(axis, out pStatus, 1, out pClock);
            } while ((pStatus & 0x0400) != 0);

            if (sRtn == 0)
            {
               // return true;
            }
            else
            {
               // return false;
            }
        }

        /// <summary>
        /// 点位运动 绝对位置
        /// axis:轴号
        /// pos:步长,(正整数。 目标速度。单位：pulse/ms。)
        /// vel:目标速度（正数，单位：pulse/ms2。）
        ///acc:加速度（正数，单位：pulse/ms2。），
        /// dec:减速度(正数，单位：pulse/ms2。未设置减速度时，默认减速度和 加速度相同。)
        /// smoothTime:平滑时间（正整数，取值范围：[0, 50]，单位 ms。平滑时间的数值越大，加减速过程越平稳。）
        /// velStart:起跳速度。正数，单位：pulse/ms。默认值为 0。
        /// </summary>
        public static void PrfTrap_Action(short axis, int pos, double vel, double acc, double dec, int smoothTime)
        {
            try
            {
                gts.mc.TTrapPrm trap;
                gts.mc.GT_PrfTrap(axis);
                trap.acc = acc;
                trap.dec = dec;
                trap.smoothTime =  Convert.ToInt16(smoothTime);
                trap.velStart = 0;//起跳速度
                gts.mc.GT_SetTrapPrm(axis, ref trap);//设置点位运动参数
                gts.mc.GT_SetVel(axis, vel);//设置目标速度
                gts.mc.GT_SetPos(axis, pos);//设置目标位置
                gts.mc.GT_Update(1 << (axis - 1));//更新轴运动
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Goog.PrfTrap_Action 点位运动 绝对位置  操作异常 ", x);
                //Logger.Recod_Log_File("Goog.PrfTrap_Action 点位运动 绝对位置 操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }
        /// <summary>
        /// 停止轴运动   axis轴号
        /// </summary>
        public static void Stop_Axis(short axis)
        {
            try
            {
                gts.mc.GT_Stop(1 << (axis - 1), 0);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Goog.Stop_Axis 停止轴运动  操作异常 ", x);
                //Logger.Recod_Log_File("Goog.Stop_Axis 停止轴运动 操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }

        public static void Stop_All_Axis()
        {
            for (short i = 1; i < CommParam.Use_Axis + 1; i++)
            {
                gts.mc.GT_Stop(1 << (i - 1), 0);
                //  Console.WriteLine(1 << (i - 1));
            }
        }


        /// <summary>
        /// 建立XY轴的2维的1号坐标系  ；synVelMax  最大速度  ；synAccMax 加速度，evenTime最小速度
        /// </summary>
        /// 不允许多个规划轴映射到相同坐标系 的相同坐标轴, 即8个轴的轴号在profile1~8只能出现一次
        /// 也不允许把相同规划轴对应到不同的坐标系
        /// 即三维坐标系，4个轴只有1号坐标系
        /// 8轴1-4为一个坐标系  5-8可以为另外一个坐标系 ，2个坐标系同时存在
        /// 部分参数写死，后期根据实际情况优化   
        public static void Create_XY_Cooreinate(double synVelMax, double synAccMax, short evenTime, short crd)
        {

            short sRtn;
            gts.mc.TCrdPrm crdPrm;//// TCrdPrm结构体变量，该结构体定义了坐标系
            crdPrm.dimension = 3;                // 建立二维的坐标系      坐标系的维数。取值范围：[1, 4]。
            crdPrm.synVelMax = synVelMax;                      // 坐标系的最大合成速度是: 500 pulse/ms   ：(0, 32767)。
            crdPrm.synAccMax = synAccMax;                        // 坐标系的最大合成加速度是: 2 pulse/ms^2   ：(0, 32767)。
            crdPrm.evenTime = evenTime;                         // 坐标系的最小匀速时间为0          ：(0, 32767)。
            crdPrm.profile1 = 1;                       // 规划器1对应到X轴                       
            crdPrm.profile2 = 2;                       // 规划器2对应到Y轴
            crdPrm.profile3 = 3;                       // 规划器3对应到Z轴
            crdPrm.profile4 = 0;
            crdPrm.profile5 = 0;
            crdPrm.profile6 = 0;
            crdPrm.profile7 = 0;
            crdPrm.profile8 = 0;
            crdPrm.setOriginFlag = 1;                    // 需要设置加工坐标系原点位置
            crdPrm.originPos1 = 0;                     // 加工坐标系原点位置在(0,0,0)，即与机床坐标系原点重合
            crdPrm.originPos2 = 0;
            crdPrm.originPos3 = 0;
            crdPrm.originPos4 = 0;
            crdPrm.originPos5 = 0;
            crdPrm.originPos6 = 0;
            crdPrm.originPos7 = 0;
            crdPrm.originPos8 = 0;
            try
            {
                sRtn = gts.mc.GT_SetCrdPrm(crd, ref crdPrm);//建立1号坐标系，为插补提供坐标系。将在坐标系内描述的运动通过映射关系映射到相应的规划轴上
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Goog.Create_XY_Cooreinate 建立XY轴的2维的1号坐标系  操作异常 ", x);
               // Logger.Recod_Log_File("Goog.Create_XY_Cooreinate 建立XY轴的2维的1号坐标系 操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }

        /// <summary>
        ///  清除插补缓存区内的插补数据  ;
        ///crd: 坐标系号。正整数，取值范围：[1, 2]。
        ///fifo: 所要清除的插补缓存区号。正整数，取值范围：[0, 1]，默认值为：0
        /// </summary>

        public static short Crd_Clear(short crd, short fifo)
        {
            try
            {

                //sRtn = gts.mc.GT_CrdClear(1, 0);
                return gts.mc.GT_CrdClear(crd, fifo);
            }
            catch (Exception x)
            {
                LogHelper.WriteErrorLog("Goog.Crd_Clear 清除插补缓存区内的插补数据  操作异常 ", x);
               // Logger.Recod_Log_File("Goog.Crd_Clear 清除插补缓存区内的插补数据 操作异常" + x.Message + "\r\n x.StackTrace" + x.StackTrace);
                throw x;
            }
        }

        /// <summary>
        /// 缓存区指令，二维XY直线插补
        /// crd :坐标系号。正整数，取值范围：[1, 2]。 
        /// x :插补段 x 轴终点坐标值。取值范围：[-1073741823, 1073741823]，单位：pulse。
        ///  y: 插补段 y 轴终点坐标值。取值范围：[-1073741823, 1073741823]，单位：pulse。
        /// synVel :插补段的目标合成速度。取值范围：(0, 32767)，单位：pulse/ms。
        ///synAcc: 插补段的合成加速度。取值范围：(0, 32767)，单位：pulse/ms²。
        ///velEnd:插补段的终点速度。取值范围：[0, 32767)，单位：pulse/ms。该值只有在没有使用前瞻预处理功能时才有意义，否则该值无效。默认值为：0。
        ///fifo: 插补缓存区号。取值范围：[0, 1]，默认值为：0。
        /// </summary>
        /// <returns></returns>
        public static short XY_Line(short crd, int x, int y, double synVel, double synAcc, double velEnd, short fifo)
        {
            try
            {
                // 向缓存区写入第一段插补数据
                return gts.mc.GT_LnXY(
                crd,// 1, 该插补段的坐标系是坐标系1
                x, y,// 20000, 0,	该插补段的终点坐标(200000, 0)
                synVel,//10,	 该插补段的目标速度：100pulse/ms
                synAcc,//0.1,	 插补段的加速度：0.1pulse/ms^2
                velEnd,//0,		 终点速度为0
                fifo);// 0);      向坐标系1的FIFO0缓存区传递该直线插补数据

            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(" Goog.XY_Line 新增二维XY直线插补 操作异常 ", e);
               // Logger.Recod_Log_File("Goog.XY_Line 新增二维XY直线插补 操作异常" + e.Message + "\r\n x.StackTrace" + e.StackTrace);
                throw e;
            }
        }

      
        /// <summary>
        /// 直线插补 pass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void GT800_LnXY(short Card, int x, int y, double Vel, double Acc, double Dec, short fifo, double synVelMax, double synAccMax, short evenTime)
        {
            gts.mc.TCrdPrm crdPrm;
            //***********************************************************************
            //建立了一个二维坐标系
            //***********************************************************************
            short sRtn;// 指令返回值变量
            //short cardNum = 0;
            crdPrm = new gts.mc.TCrdPrm();
            crdPrm.dimension = 2; // 坐标系为二维坐标系
            crdPrm.synVelMax = synVelMax; // 最大合成速度：500pulse/ms
            crdPrm.synAccMax = synAccMax;  // 最大加速度：1pulse/ms^2
            crdPrm.evenTime = evenTime;  // 最小匀速时间：50ms
            crdPrm.profile1 = 1; // 规划器1对应到X轴
            crdPrm.profile2 = 2; // 规划器2对应到Y轴           
            crdPrm.setOriginFlag = 1; // 表示需要指定坐标系的原点坐标的规划位置
            //crdPrm.originPos1 = 100; // 坐标系的原点坐标的规划位置为（100, 100）
            //crdPrm.originPos2 = 100;
            sRtn = gts.mc.GT_SetCrdPrm(1, ref crdPrm);


            // 坐标系运动状态查询变量
            short run;
            // 坐标系的缓存区剩余空间查询变量
            int segment;

            // 即将把数据存入坐标系1的FIFO0中，所以要首先清除此缓存区中的数据
            sRtn = gts.mc.GT_CrdClear(Card, 0);
            sRtn = gts.mc.GT_CrdClear(1, 0);
            // 向缓存区写入第一段插补数据
            sRtn = gts.mc.GT_LnXY(
                1,				// 该插补段的坐标系是坐标系1
                x, y,		    // 该插补段的终点坐标(200000, 0)
                Vel,		    // 该插补段的目标速度：100pulse/ms
                Acc,			    // 插补段的加速度：0.1pulse/ms^2
                Dec,				// 终点速度为0
                0);             // 向坐标系1的FIFO0缓存区传递该直线插补数据

            // 启动坐标系1的FIFO0的插补运动
            sRtn = gts.mc.GT_CrdStart(1, 0);
            // 等待运动完成
            sRtn = gts.mc.GT_CrdStatus(1, out run, out segment, 0);
            do
            {
                // 查询坐标系1的FIFO的插补运动状态
                sRtn = gts.mc.GT_CrdStatus(
                1,              // 坐标系是坐标系1
                out run,        // 读取插补运动状态
                out segment,    // 读取当前已经完成的插补段数
                0);             // 查询坐标系1的FIFO0缓存区
                                // 坐标系在运动, 查询到的run的值为1
            } while (run == 1);
        }

        /// <summary>
        /// XYZ三维直线插补。  xinzeng
        /// </summary>
        public static short XYZ_Line(short crd, int x, int y, int z, double synVel, double synAcc, double velEnd, short fifo)
        {
            try
            {
                // 向缓存区写入第一段插补数据
                return gts.mc.GT_LnXYZ(
                crd,// 1, 该插补段的坐标系是坐标系1
                x, y, z,// 20000, 0,	该插补段的终点坐标(200000, 0)
                synVel,//10,	 该插补段的目标速度：100pulse/ms
                synAcc,//0.1,	 插补段的加速度：0.1pulse/ms^2
                velEnd,//0,		 终点速度为0
                fifo);// 0);      向坐标系1的FIFO0缓存区传递该直线插补数据

            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(" Goog.XYZ_Line 新增三维XYZ直线插补 操作异常 ", e);
               // Logger.Recod_Log_File("Goog.XYZ_Line 新增三维XYZ直线插补 操作异常" + e.Message + "\r\n x.StackTrace" + e.StackTrace);
                throw e;
            }
        }

        /// <summary>
        /// 缓存区内数字量 IO 输出设置指令。
        /// crd 坐标系号。正整数，取值范围：[1, 2]。
        ///doMask从 bit0 ~bit15 按位表示指定的数字量输出是否有操作。0：该路数字量输出无操作。1：该路数字量输出有操作。
        ///doValue 从 bit0 ~bit15 按位表示指定的数字量输出的值。
        ///fifo 插补缓存区号。正整数，取值范围：[0, 1]，默认值为：0。
        /// </summary>
        public short Buffer_IO(short crd, short doMask, short doValue, short fifo)
        {
            try
            {
                return gts.mc.GT_BufIO(
                     1,                // 坐标系是坐标系1
                     (ushort)gts.mc.MC_GPO,        // 数字量输出类型：通用输出
                     (ushort)doMask,//0xffff,           // bit0~bit15全部都输出
                      (ushort)doValue, //0x55,          // 输出的数值为0x55
                    fifo);// 0);               // 向坐标系1的FIFO0缓存区传递该数字量输出

            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog("Goog.Buffer_IO 缓存区内数字量 IO 输出设置指令 操作异常 ", e);
               // Logger.Recod_Log_File("Goog.Buffer_IO 缓存区内数字量 IO 输出设置指令 操作异常" + e.Message + "\r\n x.StackTrace" + e.StackTrace);
                throw e;
            }

        }



        /// <summary>
        ///缓存区指令。  XY 平面圆弧插补。使用圆心描述方法描述圆弧 或者圆
        /// </summary>
        /// <param name="crd">坐标系号。正整数，取值范围：[1, 2]。</param>
        /// <param name="x">圆弧插补 x 轴的终点坐标值。取值范围：[-1073741823, 1073741823]，单位：pulse。</param>
        /// <param name="y">圆弧插补 y 轴的终点坐标值。取值范围：[-1073741823, 1073741823]，单位：pulse。</param>
        /// <param name="xCenter">圆弧插补的圆心 x 方向相对于起点位置的偏移量。</param>
        /// <param name="yCenter">圆弧插补的圆心 y 方向相对于起点位置的偏移量。</param>
        /// <param name="circleDir">圆弧的旋转方向 0：顺时针圆弧 1：逆时针圆弧。</param>
        /// <param name="synVel">插补段的目标合成速度。取值范围：(0, 32767)，单位：pulse/ms。</param>
        /// <param name="synAcc">插补段的合成加速度。取值范围：(0, 32767)，单位：pulse/ms2。</param>
        /// <param name="velEnd">插补段的终点速度。取值范围：[0, 32767)，单位：pulse/ms。该值只有在没有使用前瞻预处理功能时才有意义，否则该值无效。默认值为：0。</param>
        /// <param name="fifo">插补缓存区号。正整数，取值范围：[0, 1]，默认值为：0。</param>
        /// <returns></returns>
        public short Arc_XYC(short crd, int x, int y, double xCenter, double yCenter,
            short circleDir, double synVel, double synAcc, double velEnd = 0, short fifo = 0)
        {
            try
            {
                return gts.mc.GT_ArcXYC(
              crd,// 1,                  坐标系是坐标系1
              x, y,// 20000, 0,            该圆弧的终点坐标(200000, 0)
              xCenter, yCenter,// - 10000, 0,            圆弧插补的圆心相对于起点位置的偏移量(-100000, 0)
              circleDir,// 0,                    该圆弧是顺时针圆弧
              synVel,// 5,                   该插补段的目标速度：100pulse/ms
              synAcc,//  0.1,                 该插补段的加速度：0.1pulse/ms^2
              velEnd,// 0,                   终点速度为0
              fifo);// 0);					 向坐标系1的FIFO0缓存区传递该直线插补数据
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog("Goog.Arc_XYC   XY 平面圆弧插补指令 操作异常 ", e);
                //Logger.Recod_Log_File("Goog.Arc_XYC   XY 平面圆弧插补指令  操作异常" + e.Message + "\r\n x.StackTrace" + e.StackTrace);
                throw e;
            }
        }


        /// <summary>
        /// XY 平面圆弧插补。使用半径描述方法描述圆弧。 
        /// </summary>
        /// <param name="crd">坐标系号。正整数，取值范围：[1, 2]</param>
        /// <param name="x">圆弧插补 x 轴的终点坐标值。取值范围：[-1073741823, 1073741823]，单位：pulse。</param>
        /// <param name="y">圆弧插补 y 轴的终点坐标值。取值范围：[-1073741823, 1073741823]，单位：pulse。</param>
        /// <param name="radius">圆弧插补的圆弧半径值。取值范围：[-1073741823, 1073741823]，单位：pulse。</param>
        /// 半径为正时，表示圆弧为小于等于 180°圆弧。   
        /// 半径为负时，表示圆弧为大于 180°圆弧。
        /// 半径描述方式不能用来描述整圆
        /// <param name="circleDir">圆弧的旋转方向。 0：顺时针圆弧。 1：逆时针圆弧。</param>
        /// <param name="synVel">插补段的目标合成速度。取值范围：(0, 32767)，单位：pulse/ms。</param>
        /// <param name="synAcc">插补段的合成加速度。取值范围：(0, 32767)，单位：pulse/ms2。</param>
        /// <param name="velEnd">插补段的终点速度。取值范围：[0, 32767)，单位：pulse/ms。该值只有在没有使用 前瞻预处理功能时才有意义，否则该值无效。默认值为</param>
        /// <param name="fifo">插补缓存区号。正整数，取值范围：[0, 1]，默认值为：0。</param>
        /// <returns></returns>
        public static short Arc_XYR(short crd, int x, int y, double radius, short circleDir, double synVel,
            double synAcc, double velEnd = 0, short fifo = 0)
        {
            try
            {
                // 向缓存区写入第三段插补数据，该段数据是以半径描述方法描述了一个1/4圆弧
                return gts.mc.GT_ArcXYR(
                   crd,// 1,					// 坐标系是坐标系1
                   x, y,// 0, 20000,			// 该圆弧的终点坐标(0, 200000)
                   radius,// 20000,				// 半径：200000pulse
                   circleDir,// 1,					// 该圆弧是逆时针圆弧
                   synVel,// 5,					// 该插补段的目标速度：100pulse/ms
                   synAcc,//0.1,					// 该插补段的加速度：0.1pulse/ms^2
                   velEnd,// 0,					// 终点速度为0
                   fifo);// 0);                 // 向坐标系1的FIFO0缓存区传递该直线插补数据
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog("Goog.Arc_XYR   XY 平面圆弧插补指令  操作异常 ", e);
               // Logger.Recod_Log_File("Goog.Arc_XYR   XY 平面圆弧插补指令  操作异常" + e.Message + "\r\n x.StackTrace" + e.StackTrace);
                throw e;
            }

        }


        /// <summary>
        /// 启动坐标系1的FIFO0的插补运动
        /// mask :从 bit0~bit1 按位表示需要启动的坐标系。bit0 对应坐标系 1，bit1 对应坐标系 2。 0：不启动该坐标系，1：启动该坐标系。
        ///option:从 bit0 ~bit1 按位表示坐标系需要启动的缓存区的编号。
        ///bit0 对应坐标系 1，bit1 对应坐标系 2。 0：启动坐标系中 FIFO0 的运动，1：启动坐标系中 FIFO1 的运动。
        /// </summary>
        public static short Crd_Start(short mask, short option)
        {
            try
            {
                // 启动坐标系1的FIFO0的插补运动
                return gts.mc.GT_CrdStart(mask, option);// (1, 0);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog("Goog.Crd_Start  启动坐标系1的FIFO0的插补运动 操作异常 ", e);
              //  Logger.Recod_Log_File("Goog.Crd_Start  启动坐标系1的FIFO0的插补运动  操作异常" + e.Message + "\r\n x.StackTrace" + e.StackTrace);
                throw e;
            }
        }



        /// <summary>
        ///   回原点  search_home  搜索距离；axis 轴号 ;acc 加速度，dec 减速度，vel目标速度，home_offset偏移距离
        /// </summary>
        public static void GoHome(short axis, int search_home, double acc, double dec, double vel, int home_offset)
        {

           // LogHelper.WriteErrorLog("Goog.GoHome   search回原点参数   axis:" + axis + " search_home:" + search_home + "  acc:" + acc + "  dec:" + dec + "  vel:" + vel + "  home_offset:" + home_offset);

            gts.mc.TTrapPrm trapPrm;
            short sRtn;
            // 启动Home捕获
            sRtn = gts.mc.GT_SetCaptureMode(axis, gts.mc.CAPTURE_HOME);
            // 切换到点位运动模式
            sRtn = gts.mc.GT_PrfTrap(axis);
            // 读取点位模式运动参数
            sRtn = gts.mc.GT_GetTrapPrm(axis, out trapPrm);
            trapPrm.acc = acc;
            trapPrm.dec = dec;
            // 设置点位模式运动参数
            sRtn = gts.mc.GT_SetTrapPrm(axis, ref trapPrm);
            // 设置点位模式目标速度，即回原点速度
            sRtn = gts.mc.GT_SetVel(axis, vel);
            // 设置点位模式目标位置，即原点搜索距离
            sRtn = gts.mc.GT_SetPos(axis, search_home);
            // 启动运动
            sRtn = gts.mc.GT_Update(1 << (axis - 1));
            short capture = 0;
            int pos = 0;
            uint clk;//时钟参数
            while (true)
            {
                gts.mc.GT_GetCaptureStatus(axis, out capture, out pos, 1, out clk);

                if (capture == 1)
                {
                    break;
                }
            }

            int p = pos + home_offset;
            // Console.WriteLine("总偏移量："+pos + "****偏移：" + home_offset+ "int p:"+p);
            // 运动到"捕获位置+偏移量"
            sRtn = gts.mc.GT_SetPos(axis, p);
            // 在运动状态下更新目标位置
            sRtn = gts.mc.GT_Update(1 << (axis - 1));



        }
        /// <summary>
        /// 轴回零
        /// </summary>
        public void Auto_GoHome(short axis, int search_home, double vel, double acc, int home_offset)
        {
            short rtn;
            try
            {
                Clear_Sts(axis, 1);
                Console.WriteLine("Auto_GoHome -回原点参数： axis={0},  search_home={1},  vel={2},  acc={3},  home_offset={4}", axis, search_home, vel, acc, home_offset);
                rtn = gts.mc.GT_Home(axis, search_home, vel, acc, home_offset);// (2, -200000, 5, 0.25, 3000);	// Home回零模式
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog("Goog.Auto_GoHome   自动回原点   操作异常 ", e);
               // Logger.Recod_Log_File("Goog.Auto_GoHome   自动回原点    操作异常" + e.Message + "\r\n x.StackTrace" + e.StackTrace);
                throw e;
            }
        }

        

        /// <summary>
        ///  crd, 坐标系号；  delayTime延时时间。取值范围[0, 16383]，单位ms；fifo插补缓存区号。正整数，取值范围：[0, 1]，默认值为：0。
        /// </summary>
        public static short DelayTime(short crd, ushort delayTime, short fifo)
        {
            try
            {
                return gts.mc.GT_BufDelay(
                   crd,               // 坐标系是坐标系1
                   delayTime,         // 延时400ms
                   fifo);             // 向坐标系1的FIFO0缓存区传递该延时
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog("Goog.DelayTime   插补缓存指令，延时  操作异常 ", e);
               // Logger.Recod_Log_File("Goog.DelayTime   插补缓存指令，延时    操作异常" + e.Message + "\r\n x.StackTrace" + e.StackTrace);
                throw e;
            }
        }

        /// <summary>
        /// 按位设置数字 IO 输出状态
        /// </summary>
        /// <param name="doType">指定数字 IO 类型  MC_ENABLE：驱动器使能。MC_CLEAR：报警清除。MC_GPO：通用输出。</param>
        /// <param name="doIndex">输出 IO 的索引。doType=MC_ENABLE 时：[1, 8]。doType=MC_CLEAR 时：[1, 8]。doType=MC_GPO 时：[1, 16]。</param>
        /// <param name="value">设置数字 IO 输出电平。1 表示高电平，0 表示低电平</param>
        public static void SetDoBit(short doType, short doIndex, short value)
        {
            try
            {
                gts.mc.GT_SetDoBit(doType, doIndex, value);

            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog("Goog.SetDoBit   按位设置数字 IO 输出状态     操作异常 ", e);
              //  Logger.Recod_Log_File("Goog.SetDoBit   按位设置数字 IO 输出状态    操作异常" + e.Message + "\r\n x.StackTrace" + e.StackTrace);
                throw e;
            }
        }
        /// <summary>
        /// 读取数字 DO 输出状态
        /// </summary>
        /// <param name="doType">指定数字 IO 类型  MC_ENABLE：驱动器使能。MC_CLEAR：报警清除。MC_GPO：通用输出。</param>
        /// <param name="value"> 1 表示高电平，0 表示低电平。</param>
        public static void GetDo(short doType, out int value)
        {
            try
            {
                gts.mc.GT_GetDo(doType, out value);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog("Goog.GetDo  读取数字 DO 输出状态     操作异常 ", e);
               // Logger.Recod_Log_File("Goog.GetDo  读取数字 DO 输出状态    操作异常" + e.Message + "\r\n x.StackTrace" + e.StackTrace);
                throw e;
            }
        }

        /// <summary>
        /// 读取数字 Di 输出状态    
        /// </summary>
        /// <param name="doType">指定数字 IO 类型。    gts.mc.MC_GPI
        /// MC_LIMIT_POSITIVE(该宏定义为 0)：正限位。
        /// MC_LIMIT_NEGATIVE(该宏定义为 1)：负限位。
        ///MC_ALARM(该宏定义为 2)：驱动报警。
        ///MC_HOME(该宏定义为 3)：原点开关。
        ///MC_GPI(该宏定义为 4)：通用输入。
        ///MC_ARRIVE(该宏定义为 5)：电机到位信号。
        ///MC_MPG(该宏定义为 6)：手轮 MPG 轴选和倍率信号（24V 电平输入）。</param>
        /// <param name="value">数字 IO 输入状态，按位指示 IO 输入电平(根据配置工具 di 的 reverse 值不同而不同)。
        /// 当 reverse=0 时，1 表示高电平，0 表示低电平。
        ///当 reverse = 1 时，1 表示低电平，0 表示高电平。</param>
        public static void GetDi(short diType, out int value)
        {
            try
            {
                gts.mc.GT_GetDi(diType, out value);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog("Goog.GetDi  读取数字 Di 输出状态    操作异常 ", e);
               // Logger.Recod_Log_File("Goog.GetDi  读取数字 Di 输出状态    操作异常" + e.Message + "\r\n x.StackTrace" + e.StackTrace);
                throw e;
            }
        }

        //static Thread Single_Point_t;


        //public static void Single_Point_XY(int x, int y, int z)
        //{

        //    var Zb_Run1_Task = Task.Factory.StartNew(() =>
        //    {
        //        Go_Single_Poin(x, y, z);
        //    });


        //    //Thread Single_Point_t = new Thread(() => Go_Single_Poin(x, y, z));
        //    //Single_Point_t.IsBackground = true;
        //    //Single_Point_t.Start();


        //    //short crd = 1;//坐标系号
        //    //short fifo = 0;

        //    //Goog.Create_XY_Cooreinate(CommParam.ZBX_SynVelMax, CommParam.ZBX_SynAccMax, CommParam.ZBX_EvenTime, crd);

        //    //Goog.Crd_Clear(crd, fifo);
        //    //bool tag = true;

        //    //if (z != 0)//Z轴回零，则先走Z轴,Z轴完成，再其他轴
        //    //{
        //    //    Goog.PrfTrap_Action(CommParam.AxisZ, 0, CommParam.CB_Vel, CommParam.CB_Acc, CommParam.CB_Dec, CommParam.CB_SmoothTime);

        //    //    Thread.Sleep(10);//睡眠10ms 再检测Z轴是否停止
        //    //    while (tag)
        //    //    {
        //    //        if (CommParam.Z_Status != CommParam.AxisRunning)
        //    //            tag = false;
        //    //    }
        //    //}
        //    //Goog.XY_Line(crd, x, y, CommParam.CB_Vel, CommParam.CB_Acc, 0, fifo);
        //    //Goog.Crd_Start(1, 0);

        //    //bool Ztag = true;
        //    //while (Ztag)
        //    //{
        //    //    if (CommParam.X_Status != CommParam.AxisRunning && CommParam.Y_Status != CommParam.AxisRunning)
        //    //        Ztag = false;
        //    //}
        //    //if (z != 0)
        //    //{
        //    //    Goog.PrfTrap_Action(CommParam.AxisZ, z, CommParam.CB_Vel, CommParam.CB_Acc, CommParam.CB_Dec, CommParam.CB_SmoothTime);
        //    //}

        //    //Console.WriteLine("完成");

        //}
        /// <summary>
        /// 在轨迹中单个点位插补运动
        /// 当Z轴不为零，先Z轴回0，然后运行XY插补点位，最后运行Z轴点位
        /// 当Z轴为零，直接运行XY插补点位，最后运行Z轴点位
        /// </summary>
        public static void Go_Single_Poin(int x, int y, int z)
        {

            short crd = 1;//坐标系号
            short fifo = 0;

            while (true)
            {
                if (CommParam.X_Status != CommParam.AxisRunning && CommParam.Y_Status != CommParam.AxisRunning && CommParam.Z_Status != CommParam.AxisRunning)
                {
                    if (z != 0)//此处Z轴是否需要回零，可以根据具体情况加入判断
                    {
                        Goog.PrfTrap_Action(CommParam.AxisZ, 0, CommParam.CB_Vel, CommParam.CB_Acc, CommParam.CB_Dec, CommParam.CB_SmoothTime);
                    }
                    break;
                }
            }

            Thread.Sleep(CommParam.sleeptime);
            while (true && !CommParam.Stop_Buttom_On)
            {
                if (CommParam.Z_Status != CommParam.AxisRunning)
                {

                    Goog.Create_XY_Cooreinate(CommParam.ZBX_SynVelMax, CommParam.ZBX_SynAccMax, CommParam.ZBX_EvenTime, crd);
                    Goog.Crd_Clear(crd, fifo);
                    Goog.XY_Line(crd, x, y, CommParam.CB_Vel, CommParam.CB_Acc, 0, fifo);


                    Goog.Crd_Start(1, 0);
                    break;
                }
            }

            Thread.Sleep(CommParam.sleeptime);
            while (true && !CommParam.Stop_Buttom_On)
            {
                if (z == 0)
                {
                    break;
                }
                else if (CommParam.X_Status != CommParam.AxisRunning && CommParam.Y_Status != CommParam.AxisRunning)
                {
                    Goog.PrfTrap_Action(CommParam.AxisZ, z, CommParam.CB_Vel, CommParam.CB_Acc, CommParam.CB_Dec, CommParam.CB_SmoothTime);
                    break;
                }
            }

            Thread.Sleep(CommParam.sleeptime);
            while (true && !CommParam.Stop_Buttom_On)
            {
                if (CommParam.Z_Status != CommParam.AxisRunning)
                {
                    CommParam.XYZ_Second = true;
                    // CommParam.XYZ_First = true;
                    break;
                }
            }
        }
        public static void Go_Single_Poin_dui_Dao(int x, int y, int z)
        {

            short crd = 1;//坐标系号
            short fifo = 0;

            while (true)
            {
                Thread.Sleep(50);
                if (CommParam.X_Status != CommParam.AxisRunning && CommParam.Y_Status != CommParam.AxisRunning && CommParam.Z_Status != CommParam.AxisRunning)
                {
                    if (z != 0)//此处Z轴是否需要回零，可以根据具体情况加入判断
                    {
                        Goog.PrfTrap_Action(CommParam.AxisZ, 0, CommParam.CB_Vel, CommParam.CB_Acc, CommParam.CB_Dec, CommParam.CB_SmoothTime);
                    }
                    break;
                }
            }

            double Vel=double.Parse( CommParam.Dd_Speed_Vel)  ;//对左刀坐标点合集
            double Acc = double.Parse(CommParam.Ddr_Speed_Acc ) ;//对右刀坐标点合集
            double End = double.Parse(CommParam.Ddr_Speed_End ) ;//对左刀坐标点合集

            Thread.Sleep(CommParam.sleeptime);
            while (true && !CommParam.Stop_Buttom_On)
            {
                if (CommParam.Z_Status != CommParam.AxisRunning)
                {

                    Goog.Create_XY_Cooreinate(CommParam.ZBX_SynVelMax, CommParam.ZBX_SynAccMax, CommParam.ZBX_EvenTime, crd);
                    Goog.Crd_Clear(crd, fifo);
                    Goog.XY_Line(crd, x, y, Vel, Acc, 0, fifo);


                    Goog.Crd_Start(1, 0);
                    break;
                }
            }

            Thread.Sleep(CommParam.sleeptime);
            while (true && !CommParam.Stop_Buttom_On)
            {
                if (z == 0)
                {
                    break;
                }
                else if (CommParam.X_Status != CommParam.AxisRunning && CommParam.Y_Status != CommParam.AxisRunning)
                {
                    Goog.PrfTrap_Action(CommParam.AxisZ, z, CommParam.CB_Vel, CommParam.CB_Acc, CommParam.CB_Dec, CommParam.CB_SmoothTime);
                    break;
                }
            }

            Thread.Sleep(CommParam.sleeptime);
            while (true && !CommParam.Stop_Buttom_On)
            {
                if (CommParam.Z_Status != CommParam.AxisRunning)
                {
                    CommParam.XYZ_duidao = true;
                    // CommParam.XYZ_First = true;
                    break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="se">起点终点状态位   1 起点  3 终点</param>
        public static void Go_Single_Lint(int x, int y, int z, int se, double xy_vel, double zrun_vel)
        {
            short crd = 1;
            short fifo = 0;

            //1、如果是起点  则Z轴先回提刀空跑位置
            if (se == CommParam.Path_Start)
            {
                while (true)
                {
                    if (CommParam.X_Status != CommParam.AxisRunning && CommParam.Y_Status != CommParam.AxisRunning && CommParam.Z_Status != CommParam.AxisRunning)
                    {
                       // Goog.PrfTrap_Action(CommParam.AxisZ, Convert.ToInt32(CommParam.Z_Up), zrun_vel, CommParam.CB_Acc, CommParam.CB_Dec, CommParam.CB_SmoothTime);
                        Goog.P2PAbs(CommParam.AxisZ, Convert.ToInt32(CommParam.Z_Up), zrun_vel, CommParam.CB_Acc, CommParam.CB_Dec, CommParam.CB_SmoothTime);
                        break;
                    }
                }
            }

            //2、Z轴提升到位，则开始XY走到对应位置

            Thread.Sleep(CommParam.sleeptime);
            while (true && !CommParam.Stop_Buttom_On && CommParam.auto_Run_On)
            {
                if (CommParam.Z_Status != CommParam.AxisRunning)
                {
                    //Goog.Create_XY_Cooreinate(CommParam.ZBX_SynVelMax, CommParam.ZBX_SynAccMax, CommParam.ZBX_EvenTime, crd);
                    Goog.Crd_Clear(crd, fifo);
                    Goog.GT800_LnXY(crd, x, y, xy_vel, CommParam.CB_Acc, 0, fifo, CommParam.ZBX_SynVelMax, CommParam.ZBX_SynAccMax, CommParam.ZBX_EvenTime);
                    Goog.Crd_Start(1, 0);
                    break;
                }
            }

            //3、XY轴到位，走Z轴
            Thread.Sleep(CommParam.sleeptime);
            while (true && !CommParam.Stop_Buttom_On&&CommParam.auto_Run_On)
            {
                if (se == CommParam.Path_End)//如果是终点，则提升Z轴到提刀空跑的位置
                {
                    Goog.P2PAbs(CommParam.AxisZ, Convert.ToInt32(CommParam.Z_Up), zrun_vel, CommParam.CB_Acc, CommParam.CB_Dec, CommParam.CB_SmoothTime);
                    break;
                }
                else if (CommParam.X_Status != CommParam.AxisRunning && CommParam.Y_Status != CommParam.AxisRunning)
                {
                    Goog.P2PAbs(CommParam.AxisZ, z, zrun_vel, CommParam.CB_Acc, CommParam.CB_Dec, CommParam.CB_SmoothTime);
                    break;
                }
            }

            //4、Z轴到位
            Thread.Sleep(CommParam.sleeptime);
            while (true && !CommParam.Stop_Buttom_On)
            {
                if (CommParam.Z_Status != CommParam.AxisRunning)
                {
                    //开启切割主轴
                    CommParam.XYZ_Second = true;
                    break;
                }
            }
        }
        /// <summary>
        /// true 信号接通    false未接通
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool gpi_status(int num)
        {
            int lGpiValue;
            Goog.GetDi(gts.mc.MC_GPI, out lGpiValue);
            if ((lGpiValue & (1 << num)) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /**************第一块扩展模块为数字量********************/
        /// <summary>
        /// 设置第一个扩展模块第index路输出为低电平，指示灯亮   index取值（0~15）
        /// </summary>

        public static short SetExtDo_On(short index)
        {
            return gts.mc.GT_SetExtIoBit(0, index, 0);
        }

        /// <summary>
        /// 设置第一个扩展模块第index路输出为高电平，指示灯灭  index取值（0~15）
        /// </summary>

        public static short SetExtDo_Off(short index)
        {
            return gts.mc.GT_SetExtIoBit(0, index, 1);
        }

        /// <summary>
        /// 获取第一个扩展模块第index个输入的状态   index取值（0~15）  true 为接通   false为断开
        /// </summary>
        public static bool GetExtIoBit(short index)
        {
            short rtn;
            ushort value = 2;
            //ushort value;
            //获取第一个模块的输入状态   根据固高文档，扩展模块初始化后，状态都为1，接通后状态为0  
            rtn = gts.mc.GT_GetExtIoBit(0, index, out value);
            if (value == 0)// value接通为0  断开为1
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取第一个扩展模块输入的状态
        /// </summary>
        public static void GetGetExtIoValue(out ushort value)
        {
            //获取第一个数字量模块的输入状态
            gts.mc.GT_GetExtIoValue(0, out value);
        }

        /// <summary>
        /// 关闭扩展模块
        /// </summary>
        public static void GT_CloseExtM()
        {
            gts.mc.GT_CloseExtMdl();
        }

        /**************第一块扩展模块为数字量********************/



        #region 回零
        /*举例：线程内单轴回零*/

        /// <summary>
        /// 回零参数设置与启动
        /// </summary>
        /// <param name="axisNum"></param>
        public static void btn_homeStart(short axis)
        {
            short axisNum = 0;
            Thread threadHome;  //回零线程
            //short cardNo = 0;   //卡号为0
            //short axis = 1;     //回零轴
            gts.mc.THomePrm tHomePrm = new gts.mc.THomePrm();   //回零参数
            threadHome = new Thread(() =>
            {
                short sRtn = 0;
                gts.mc.THomeStatus pHomeStatus;//使用home回零或home+index回零，若轴停止在home点则需要先移开home点在开启回零。由实际情况确认。
                sRtn = gts.mc.GT_ClrSts( axis, axisNum);//回零前先清除状态
                sRtn = gts.mc.GT_ZeroPos( axis, 1);//清除规划和实际位置
                sRtn = gts.mc.GT_GetHomePrm( axis, out tHomePrm);//读取回零参数
                tHomePrm.mode = 11;     //限位加home回零方式 宏定义11 
                tHomePrm.searchHomeDistance = 0;    //搜索Home距离，0表示最大距离搜索
                tHomePrm.searchIndexDistance = 0;   //搜索index距离，0表示最大距离搜索
                tHomePrm.moveDir = 1;       //回零方向 1为正 -1为负
                tHomePrm.indexDir = 1;           //搜索index方向 1为正 -1为负
                tHomePrm.velHigh = 20;              //寻找限位速度
                tHomePrm.velLow = 5;                //寻找home、index速度
                tHomePrm.smoothTime = 10;           //平滑时间，运动加减速平滑
                tHomePrm.acc = 1;               //加速度
                tHomePrm.dec = 1;               //减速度
                tHomePrm.escapeStep = 8000;    //限位回零后方式时第一次找到限位反向移动距离
                tHomePrm.homeOffset = 0;        //原点偏移 = 0
                sRtn = gts.mc.GT_GoHome( axis, ref tHomePrm);//启动SmartHome回原点
                do
                {
                    sRtn = gts.mc.GT_GetHomeStatus( axis, out pHomeStatus);//获取回原点状态
                }
                while (pHomeStatus.run == 1 & pHomeStatus.stage == 100); // 等待搜索原点停止run和stage来判断是否回零完成。
                Thread.Sleep(1000);     //等待电机完全停止，时间由电机调试效果确定也可采用到位判断
                sRtn = gts.mc.GT_ZeroPos( axis, 1);  //回零完成手动清零设为原点
            })
            { IsBackground = true };
            threadHome.Start();
        }

        /// <summary>
        /// 回零状态检测判
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //mc.THomeStatus tHomeStatus = new mc.THomeStatus();
            //sRtn = mc.GT_GetHomeStatus(cardNo, axis, out tHomeStatus);
            //homeRunning = tHomeStatus.run;//回零状态：0已停止运动 1正在运动
            //homeStage = tHomeStatus.stage;//0-100 对应宏定义，请查阅固高手册可以查询回零过程，stage = 100表示回零成功。
            //homeError = tHomeStatus.error;//回零错误代码请查阅固高手册可以查询回零错误，0表示回零过程无错误
        }


        //复位方法
        /// <summary>
        /// 固高运动Smart Home限位回零
        /// </summary>
        public void Auto_GoHome2()
        {

            for (short i = 1; i <= 3; i++)
            {
                //打开使能
                //gts.mc.GT_AxisOn(i);
                //限位为低电平触发,这是个雷区，开启以后只能运动一个轴
                //GTS.GT_LmtSns(_cardNum,3);
                //编码器不取反，保证规划位置与实际位置方向一致
                //GTS.GT_EncSns(_cardNum,1);
                //设置回原点参数
                gts.mc.THomePrm prm = new gts.mc.THomePrm();
                //获取home参数
                gts.mc.GT_GetHomePrm(i, out prm);
                //回原点模式
                prm.mode = 10;
                //搜索原点时的运动方向，1正向，0负方向
                prm.moveDir = -1;
                //搜索index的运动方向，1正向，0负方向
                //prm.indexDir = 1;
                // 设置捕获沿：0-下降沿，1-上升沿 
                prm.edge = 0;
                //用于设置触发器：取值-1 和[1,8]，i
                //-1 表示 使用的触发器和轴号对应，其它值表示使 用其它轴的触发器，
                //触发器用于实现高速 硬件捕获，默认设置为-1 即可 
                prm.triggerIndex = i;
                //回原点运动的高速速度
                prm.velHigh = 50;
                //回原点运动的低速速度
                prm.velLow = 5;
                //回原点运动的加速度
                prm.acc = 0.1;
                // 回原点运动的减速
                prm.dec = 0.1;
                // 回原点运动的平滑时间：取值[0,50]，单位：ms，
                //具体含义与 GTS 系列控制器点位运动相似
                prm.smoothTime = 25;
                // 设定的搜索 Home 的搜索范围，0 表示 搜索距离为 805306368 
                prm.searchHomeDistance = 0;
                // 设定的搜索 Index 的搜索范围， 0 表示 搜索距离为 805306368 
                prm.searchIndexDistance = 0;
                // 采用“限位回原点” 方式时，反方向离开 限位的脱离步长 
                prm.escapeStep = 5000;
                //没有限位开关则取消限位,取消限位信息
                //GTS.GT_LmtsOn(_cardNum, i, 0);
                //清除轴状态
                gts.mc.GT_ClrSts(i, 1);
                //
                gts.mc.GT_ZeroPos(i, 1);
                //启动自动回原点
                gts.mc.GT_GoHome(i, ref prm);
                gts.mc.THomeStatus status = new gts.mc.THomeStatus();
                do
                {
                    gts.mc.GT_GetHomeStatus(i, out status);
                    int num = status.run;
                } while (Convert.ToBoolean(status.run));
            }
        }

        //复位
        private void ResetTest()
        {
            short sRtn;
            Reset();
            sRtn = gts.mc.GT_ZeroPos( 1, 1);
            sRtn = gts.mc.GT_ZeroPos( 2, 1);
            sRtn = gts.mc.GT_ZeroPos( 3, 1);
        }

        public static void Auto_GoHomeTest(short i)
        {
            
            //打开使能
            //gts.mc.GT_AxisOn(i);
            //限位为低电平触发,这是个雷区，开启以后只能运动一个轴
            //GTS.GT_LmtSns(_cardNum,3);
            //编码器不取反，保证规划位置与实际位置方向一致
            //GTS.GT_EncSns(_cardNum,1);
            //设置回原点参数
            gts.mc.THomePrm prm = new gts.mc.THomePrm();
            //获取home参数
            gts.mc.GT_GetHomePrm(i, out prm);
            //回原点模式
            prm.mode = 10;
            //搜索原点时的运动方向，1正向，0负方向
            prm.moveDir = -1;
            //搜索index的运动方向，1正向，0负方向
            //prm.indexDir = 1;
            // 设置捕获沿：0-下降沿，1-上升沿 
            prm.edge = 0;
            //用于设置触发器：取值-1 和[1,8]，i
            //-1 表示 使用的触发器和轴号对应，其它值表示使 用其它轴的触发器，
            //触发器用于实现高速 硬件捕获，默认设置为-1 即可 
            prm.triggerIndex = i;
            //回原点运动的高速速度
            prm.velHigh = 50;
            //回原点运动的低速速度
            prm.velLow = 5;
            //回原点运动的加速度
            prm.acc = 0.1;
            // 回原点运动的减速
            prm.dec = 0.1;
            // 回原点运动的平滑时间：取值[0,50]，单位：ms，
            //具体含义与 GTS 系列控制器点位运动相似
            prm.smoothTime = 25;
            // 设定的搜索 Home 的搜索范围，0 表示 搜索距离为 805306368 
            prm.searchHomeDistance = 0;
            // 设定的搜索 Index 的搜索范围， 0 表示 搜索距离为 805306368 
            prm.searchIndexDistance = 0;
            // 采用“限位回原点” 方式时，反方向离开 限位的脱离步长 
            prm.escapeStep = 50000;
            //没有限位开关则取消限位,取消限位信息
            //GTS.GT_LmtsOn(_cardNum, i, 0);
            //清除轴状态
            gts.mc.GT_ClrSts(i, 1);
            //
            gts.mc.GT_ZeroPos(i, 1);
            //启动自动回原点
            gts.mc.GT_GoHome(i, ref prm);
            gts.mc.THomeStatus status = new gts.mc.THomeStatus();
            do
            {
                gts.mc.GT_GetHomeStatus(i, out status);
                int num = status.run;
            } while (Convert.ToBoolean(status.run));
            
        }

        #region 点位运动
        /// <summary>
        /// 点位运动
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="acc">加速度 </param>
        /// <param name="dec">减速度</param>
        /// <param name="vel">目标速度</param>
        /// <param name="velStart">起跳速度 默认值为 0</param>
        /// <param name="smooth">平滑时间 [0, 50]</param>
        /// <param name="pos">目标位置</param>
        public void PointMovement(short axis, double acc, double dec, double vel = 50, double velStart = 0, short smooth = 25, int pos = 0)
        {
            int sts;
            uint pClock;
            TTrapPrm _trapPrm;
            //清除轴的报警和限位
            gts.mc.GT_ClrSts( axis, 1);
            //伺服使能
            gts.mc.GT_AxisOn( axis);
            //设置点位模式
            gts.mc.GT_PrfTrap( axis);
            gts.mc.GT_GetTrapPrm( axis, out _trapPrm);
            _trapPrm.acc = acc;//加速度
            _trapPrm.dec = dec;//减速度
            _trapPrm.velStart = velStart;//
            _trapPrm.smoothTime = smooth;
            gts.mc.GT_SetTrapPrm( axis, ref _trapPrm);
            gts.mc.GT_SetPos( axis, pos);
            gts.mc.GT_SetVel( axis, vel);
            gts.mc.GT_Update( (1 << (axis - 1)));
            do
            {
                // 读取AXIS轴的状态
                gts.mc.GT_GetSts( axis, out sts, 1, out pClock);
                /*  // 读取AXIS轴的规划位置
                  GT_GetPrfPos(_cardNum, axis,, 1,out pClock);*/

            } while (Convert.ToBoolean(sts & 0x400));// 等待AXIS轴规划停止
            gts.mc.GT_Stop( axis, axis);

        }
        #endregion

        #endregion

    }
}
