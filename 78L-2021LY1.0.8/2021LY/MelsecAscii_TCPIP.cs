
using HslCommunication;
using HslCommunication.Profinet.Melsec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021LY
{
    /// <summary>
    /// Ascii方式
    /// </summary>
    public class MelsecAscii_TCPIP
    {
        private MelsecMcAsciiNet melsec_net = null;//Ascii方式

        #region 建立通信
        /// <summary>
        /// 建立通信
        /// </summary>
        /// <returns>连接成功返回TRUE，连接失败返回FALSE</returns>
        public bool ConnectServer_TCP(string IpAddress, string Port)//"192.168.1.2",4899
        {
            bool isSuccess = false;
            try
            {
                melsec_net = new MelsecMcAsciiNet();
                
                melsec_net.IpAddress = IpAddress;
                melsec_net.Port = int.Parse(Port);

                melsec_net.ConnectTimeOut = 2000; // 网络连接的超时时间
                melsec_net.NetworkNumber = 0x00;  // 网络号
                melsec_net.NetworkStationNumber = 0x00; // 网络站号

                this.melsec_net.ConnectClose();
                OperateResult operateResult = this.melsec_net.ConnectServer();
                isSuccess = operateResult.IsSuccess;
            }
            catch (Exception)
            {

                return false;
            }
            return isSuccess;
        }

        public void Dispos_Server_TCP()
        {
            try { melsec_net.Dispose(); }
            catch
            { }
           

        }
        #endregion

            #region 设置 X Y M B 状态
        public void Write_X_ON(string X)
        {
            X = X.ToUpper();
            switch (X)
            {
                case "X0":
                    melsec_net.Write("X000", new bool[] { true });
                    break;
                case "X1":
                    melsec_net.Write("X001", new bool[] { true });
                    break;
                case "X2":
                    melsec_net.Write("X002", new bool[] { true });
                    break;
                case "X3":
                    melsec_net.Write("X003", new bool[] { true });
                    break;
                case "X4":
                    melsec_net.Write("X004", new bool[] { true });
                    break;
                case "X5":
                    melsec_net.Write("X005", new bool[] { true });
                    break;
                case "X6":
                    melsec_net.Write("X006", new bool[] { true });
                    break;
                case "X7":
                    melsec_net.Write("X007", new bool[] { true });
                    break;
                case "X8":
                    melsec_net.Write("X008", new bool[] { true });
                    break;
                case "X9":
                    melsec_net.Write("X009", new bool[] { true });
                    break;
                case "X10":
                    melsec_net.Write("X00A", new bool[] { true });
                    break;
                case "X11":
                    melsec_net.Write("X00B", new bool[] { true });
                    break;
                case "X12":
                    melsec_net.Write("X00C", new bool[] { true });
                    break;
                case "X13":
                    melsec_net.Write("X00D", new bool[] { true });
                    break;
                case "X14":
                    melsec_net.Write("X00E", new bool[] { true });
                    break;
                case "X15":
                    melsec_net.Write("X00F", new bool[] { true });
                    break;

                case "X16":
                    melsec_net.Write("X010", new bool[] { true });
                    break;
                case "X17":
                    melsec_net.Write("X011", new bool[] { true });
                    break;
                case "X18":
                    melsec_net.Write("X012", new bool[] { true });
                    break;
                case "":
                    melsec_net.Write("X013", new bool[] { true });
                    break;
                case "X19":
                    melsec_net.Write("X014", new bool[] { true });
                    break;
                case "X20":
                    melsec_net.Write("X015", new bool[] { true });
                    break;
                case "X21":
                    melsec_net.Write("X016", new bool[] { true });
                    break;
                case "X22":
                    melsec_net.Write("X017", new bool[] { true });
                    break;
                case "X23":
                    melsec_net.Write("X018", new bool[] { true });
                    break;
                case "X24":
                    melsec_net.Write("X019", new bool[] { true });
                    break;
                case "X25":
                    melsec_net.Write("X01A", new bool[] { true });
                    break;
                case "X26":
                    melsec_net.Write("X01B", new bool[] { true });
                    break;
                case "X27":
                    melsec_net.Write("X01C", new bool[] { true });
                    break;
                case "X28":
                    melsec_net.Write("X01D", new bool[] { true });
                    break;
                case "X29":
                    melsec_net.Write("X01E", new bool[] { true });
                    break;
                case "X30":
                    melsec_net.Write("X01F", new bool[] { true });
                    break;
                default:
                    break;
            }
        }
        public void Write_X_OFF(string X)
        {
            X = X.ToUpper();
            switch (X)
            {
                case "X0":
                    melsec_net.Write("X000", new bool[] { false });
                    break;
                case "X1":
                    melsec_net.Write("X001", new bool[] { false });
                    break;
                case "X2":
                    melsec_net.Write("X002", new bool[] { false });
                    break;
                case "X3":
                    melsec_net.Write("X003", new bool[] { false });
                    break;
                case "X4":
                    melsec_net.Write("X004", new bool[] { false });
                    break;
                case "X5":
                    melsec_net.Write("X005", new bool[] { false });
                    break;
                case "X6":
                    melsec_net.Write("X006", new bool[] { false });
                    break;
                case "X7":
                    melsec_net.Write("X007", new bool[] { false });
                    break;
                case "X8":
                    melsec_net.Write("X008", new bool[] { false });
                    break;
                case "X9":
                    melsec_net.Write("X009", new bool[] { false });
                    break;
                case "X10":
                    melsec_net.Write("X00A", new bool[] { false });
                    break;
                case "X11":
                    melsec_net.Write("X00B", new bool[] { false });
                    break;
                case "X12":
                    melsec_net.Write("X00C", new bool[] { false });
                    break;
                case "X13":
                    melsec_net.Write("X00D", new bool[] { false });
                    break;
                case "X14":
                    melsec_net.Write("X00E", new bool[] { false });
                    break;
                case "X15":
                    melsec_net.Write("X00F", new bool[] { false });
                    break;

                case "X16":
                    melsec_net.Write("X010", new bool[] { false });
                    break;
                case "X17":
                    melsec_net.Write("X011", new bool[] { false });
                    break;
                case "X18":
                    melsec_net.Write("X012", new bool[] { false });
                    break;
                case "":
                    melsec_net.Write("X013", new bool[] { false });
                    break;
                case "X19":
                    melsec_net.Write("X014", new bool[] { false });
                    break;
                case "X20":
                    melsec_net.Write("X015", new bool[] { false });
                    break;
                case "X21":
                    melsec_net.Write("X016", new bool[] { false });
                    break;
                case "X22":
                    melsec_net.Write("X017", new bool[] { false });
                    break;
                case "X23":
                    melsec_net.Write("X018", new bool[] { false });
                    break;
                case "X24":
                    melsec_net.Write("X019", new bool[] { false });
                    break;
                case "X25":
                    melsec_net.Write("X01A", new bool[] { false });
                    break;
                case "X26":
                    melsec_net.Write("X01B", new bool[] { false });
                    break;
                case "X27":
                    melsec_net.Write("X01C", new bool[] { false });
                    break;
                case "X28":
                    melsec_net.Write("X01D", new bool[] { false });
                    break;
                case "X29":
                    melsec_net.Write("X01E", new bool[] { false });
                    break;
                case "X30":
                    melsec_net.Write("X01F", new bool[] { false });
                    break;
                default:
                    break;
            }
        }

        public void Write_Y_ON(string Y)
        {
            Y = Y.ToUpper();
            switch (Y)
            {
                case "Y0":
                    melsec_net.Write("Y000", new bool[] { true });
                    break;
                case "Y1":
                    melsec_net.Write("Y001", new bool[] { true });
                    break;
                case "Y2":
                    melsec_net.Write("Y002", new bool[] { true });
                    break;
                case "Y3":
                    melsec_net.Write("Y003", new bool[] { true });
                    break;
                case "Y4":
                    melsec_net.Write("Y004", new bool[] { true });
                    break;
                case "Y5":
                    melsec_net.Write("Y005", new bool[] { true });
                    break;
                case "Y6":
                    melsec_net.Write("Y006", new bool[] { true });
                    break;
                case "Y7":
                    melsec_net.Write("Y007", new bool[] { true });
                    break;
                case "Y8":
                    melsec_net.Write("Y008", new bool[] { true });
                    break;
                case "Y9":
                    melsec_net.Write("Y009", new bool[] { true });
                    break;
                case "Y10":
                    melsec_net.Write("Y00A", new bool[] { true });
                    break;
                case "Y11":
                    melsec_net.Write("Y00B", new bool[] { true });
                    break;
                case "Y12":
                    melsec_net.Write("Y00C", new bool[] { true });
                    break;
                case "Y13":
                    melsec_net.Write("Y00D", new bool[] { true });
                    break;
                case "Y14":
                    melsec_net.Write("Y00E", new bool[] { true });
                    break;
                case "Y15":
                    melsec_net.Write("Y00F", new bool[] { true });
                    break;

                case "Y16":
                    melsec_net.Write("Y010", new bool[] { true });
                    break;
                case "Y17":
                    melsec_net.Write("Y011", new bool[] { true });
                    break;
                case "Y18":
                    melsec_net.Write("Y012", new bool[] { true });
                    break;
                case "":
                    melsec_net.Write("Y013", new bool[] { true });
                    break;
                case "Y19":
                    melsec_net.Write("Y014", new bool[] { true });
                    break;
                case "Y20":
                    melsec_net.Write("Y015", new bool[] { true });
                    break;
                case "Y21":
                    melsec_net.Write("Y016", new bool[] { true });
                    break;
                case "Y22":
                    melsec_net.Write("Y017", new bool[] { true });
                    break;
                case "Y23":
                    melsec_net.Write("Y018", new bool[] { true });
                    break;
                case "Y24":
                    melsec_net.Write("Y019", new bool[] { true });
                    break;
                case "Y25":
                    melsec_net.Write("Y01A", new bool[] { true });
                    break;
                case "Y26":
                    melsec_net.Write("Y01B", new bool[] { true });
                    break;
                case "Y27":
                    melsec_net.Write("Y01C", new bool[] { true });
                    break;
                case "Y28":
                    melsec_net.Write("Y01D", new bool[] { true });
                    break;
                case "Y29":
                    melsec_net.Write("Y01E", new bool[] { true });
                    break;
                case "Y30":
                    melsec_net.Write("Y01F", new bool[] { true });
                    break;
                default:
                    break;
            }
        }
        public void Write_Y_OFF(string Y)
        {
            Y = Y.ToUpper();
            switch (Y)
            {
                case "Y0":
                    melsec_net.Write("Y000", new bool[] { false });
                    break;
                case "Y1":
                    melsec_net.Write("Y001", new bool[] { false });
                    break;
                case "Y2":
                    melsec_net.Write("Y002", new bool[] { false });
                    break;
                case "Y3":
                    melsec_net.Write("Y003", new bool[] { false });
                    break;
                case "Y4":
                    melsec_net.Write("Y004", new bool[] { false });
                    break;
                case "Y5":
                    melsec_net.Write("Y005", new bool[] { false });
                    break;
                case "Y6":
                    melsec_net.Write("Y006", new bool[] { false });
                    break;
                case "Y7":
                    melsec_net.Write("Y007", new bool[] { false });
                    break;
                case "Y8":
                    melsec_net.Write("Y008", new bool[] { false });
                    break;
                case "Y9":
                    melsec_net.Write("Y009", new bool[] { false });
                    break;
                case "Y10":
                    melsec_net.Write("Y00A", new bool[] { false });
                    break;
                case "Y11":
                    melsec_net.Write("Y00B", new bool[] { false });
                    break;
                case "Y12":
                    melsec_net.Write("Y00C", new bool[] { false });
                    break;
                case "Y13":
                    melsec_net.Write("Y00D", new bool[] { false });
                    break;
                case "Y14":
                    melsec_net.Write("Y00E", new bool[] { false });
                    break;
                case "Y15":
                    melsec_net.Write("Y00F", new bool[] { false });
                    break;

                case "Y16":
                    melsec_net.Write("Y010", new bool[] { false });
                    break;
                case "X17":
                    melsec_net.Write("Y011", new bool[] { false });
                    break;
                case "Y18":
                    melsec_net.Write("Y012", new bool[] { false });
                    break;
                case "":
                    melsec_net.Write("Y013", new bool[] { false });
                    break;
                case "Y19":
                    melsec_net.Write("Y014", new bool[] { false });
                    break;
                case "Y20":
                    melsec_net.Write("Y015", new bool[] { false });
                    break;
                case "Y21":
                    melsec_net.Write("Y016", new bool[] { false });
                    break;
                case "Y22":
                    melsec_net.Write("Y017", new bool[] { false });
                    break;
                case "Y23":
                    melsec_net.Write("Y018", new bool[] { false });
                    break;
                case "Y24":
                    melsec_net.Write("Y019", new bool[] { false });
                    break;
                case "Y25":
                    melsec_net.Write("Y01A", new bool[] { false });
                    break;
                case "Y26":
                    melsec_net.Write("Y01B", new bool[] { false });
                    break;
                case "Y27":
                    melsec_net.Write("Y01C", new bool[] { false });
                    break;
                case "Y28":
                    melsec_net.Write("Y01D", new bool[] { false });
                    break;
                case "Y29":
                    melsec_net.Write("Y01E", new bool[] { false });
                    break;
                case "Y30":
                    melsec_net.Write("Y01F", new bool[] { false });
                    break;
                default:
                    break;
            }
        }

        public void Write_M_ON(string M)
        {
            M = M.ToUpper();
            switch (M)
            {
                case "M0":
                    melsec_net.Write("M000", new bool[] { true });
                    break;
                case "M1":
                    melsec_net.Write("M001", new bool[] { true });
                    break;
                case "M2":
                    melsec_net.Write("M002", new bool[] { true });
                    break;
                case "M3":
                    melsec_net.Write("M003", new bool[] { true });
                    break;
                case "M4":
                    melsec_net.Write("M004", new bool[] { true });
                    break;
                case "M5":
                    melsec_net.Write("M005", new bool[] { true });
                    break;
                case "M6":
                    melsec_net.Write("M006", new bool[] { true });
                    break;
                case "M7":
                    melsec_net.Write("M007", new bool[] { true });
                    break;
                case "M8":
                    melsec_net.Write("M008", new bool[] { true });
                    break;
                case "M9":
                    melsec_net.Write("M009", new bool[] { true });
                    break;
                case "M10":
                    melsec_net.Write("M00A", new bool[] { true });
                    break;
                case "M11":
                    melsec_net.Write("M00B", new bool[] { true });
                    break;
                case "M12":
                    melsec_net.Write("M00C", new bool[] { true });
                    break;
                case "M13":
                    melsec_net.Write("M00D", new bool[] { true });
                    break;
                case "M14":
                    melsec_net.Write("M00E", new bool[] { true });
                    break;
                case "M15":
                    melsec_net.Write("M00F", new bool[] { true });
                    break;

                case "M16":
                    melsec_net.Write("M010", new bool[] { true });
                    break;
                case "M17":
                    melsec_net.Write("M011", new bool[] { true });
                    break;
                case "M18":
                    melsec_net.Write("M012", new bool[] { true });
                    break;
                case "":
                    melsec_net.Write("M013", new bool[] { true });
                    break;
                case "M19":
                    melsec_net.Write("M014", new bool[] { true });
                    break;
                case "M20":
                    melsec_net.Write("M015", new bool[] { true });
                    break;
                case "M21":
                    melsec_net.Write("M016", new bool[] { true });
                    break;
                case "M22":
                    melsec_net.Write("M017", new bool[] { true });
                    break;
                case "M23":
                    melsec_net.Write("M018", new bool[] { true });
                    break;
                case "M24":
                    melsec_net.Write("M019", new bool[] { true });
                    break;
                case "M25":
                    melsec_net.Write("M01A", new bool[] { true });
                    break;
                case "M26":
                    melsec_net.Write("M01B", new bool[] { true });
                    break;
                case "M27":
                    melsec_net.Write("M01C", new bool[] { true });
                    break;
                case "M28":
                    melsec_net.Write("M01D", new bool[] { true });
                    break;
                case "M29":
                    melsec_net.Write("M01E", new bool[] { true });
                    break;
                case "M30":
                    melsec_net.Write("M01F", new bool[] { true });
                    break;
                default:
                    break;
            }
        }
        public void Write_M_OFF(string M)
        {
            M = M.ToUpper();
            switch (M)
            {
                case "M0":
                    melsec_net.Write("M000", new bool[] { false });
                    break;
                case "M1":
                    melsec_net.Write("M001", new bool[] { false });
                    break;
                case "M2":
                    melsec_net.Write("M002", new bool[] { false });
                    break;
                case "M3":
                    melsec_net.Write("M003", new bool[] { false });
                    break;
                case "M4":
                    melsec_net.Write("M004", new bool[] { false });
                    break;
                case "M5":
                    melsec_net.Write("M005", new bool[] { false });
                    break;
                case "M6":
                    melsec_net.Write("M006", new bool[] { false });
                    break;
                case "M7":
                    melsec_net.Write("M007", new bool[] { false });
                    break;
                case "M8":
                    melsec_net.Write("M008", new bool[] { false });
                    break;
                case "M9":
                    melsec_net.Write("M009", new bool[] { false });
                    break;
                case "M10":
                    melsec_net.Write("M00A", new bool[] { false });
                    break;
                case "M11":
                    melsec_net.Write("M00B", new bool[] { false });
                    break;
                case "M12":
                    melsec_net.Write("M00C", new bool[] { false });
                    break;
                case "M13":
                    melsec_net.Write("M00D", new bool[] { false });
                    break;
                case "M14":
                    melsec_net.Write("M00E", new bool[] { false });
                    break;
                case "M15":
                    melsec_net.Write("M00F", new bool[] { false });
                    break;

                case "M16":
                    melsec_net.Write("M010", new bool[] { false });
                    break;
                case "M17":
                    melsec_net.Write("M011", new bool[] { false });
                    break;
                case "M18":
                    melsec_net.Write("M012", new bool[] { false });
                    break;
                case "":
                    melsec_net.Write("M013", new bool[] { false });
                    break;
                case "M19":
                    melsec_net.Write("M014", new bool[] { false });
                    break;
                case "M20":
                    melsec_net.Write("M015", new bool[] { false });
                    break;
                case "M21":
                    melsec_net.Write("M016", new bool[] { false });
                    break;
                case "M22":
                    melsec_net.Write("M017", new bool[] { false });
                    break;
                case "M23":
                    melsec_net.Write("M018", new bool[] { false });
                    break;
                case "M24":
                    melsec_net.Write("M019", new bool[] { false });
                    break;
                case "M25":
                    melsec_net.Write("M01A", new bool[] { false });
                    break;
                case "M26":
                    melsec_net.Write("M01B", new bool[] { false });
                    break;
                case "M27":
                    melsec_net.Write("M01C", new bool[] { false });
                    break;
                case "M28":
                    melsec_net.Write("M01D", new bool[] { false });
                    break;
                case "M29":
                    melsec_net.Write("M01E", new bool[] { false });
                    break;
                case "M30":
                    melsec_net.Write("M01F", new bool[] { false });
                    break;
                default:
                    break;
            }
        }

        public void Write_B_ON(string B)
        {
            B = B.ToUpper();
            switch (B)
            {
                case "B0":
                    melsec_net.Write("B000", new bool[] { true });
                    break;
                case "B1":
                    melsec_net.Write("B001", new bool[] { true });
                    break;
                case "B2":
                    melsec_net.Write("B002", new bool[] { true });
                    break;
                case "B3":
                    melsec_net.Write("B003", new bool[] { true });
                    break;
                case "B4":
                    melsec_net.Write("B004", new bool[] { true });
                    break;
                case "B5":
                    melsec_net.Write("B005", new bool[] { true });
                    break;
                case "B6":
                    melsec_net.Write("B006", new bool[] { true });
                    break;
                case "B7":
                    melsec_net.Write("B007", new bool[] { true });
                    break;
                case "B8":
                    melsec_net.Write("B008", new bool[] { true });
                    break;
                case "B9":
                    melsec_net.Write("B009", new bool[] { true });
                    break;
                case "B10":
                    melsec_net.Write("B00A", new bool[] { true });
                    break;
                case "B11":
                    melsec_net.Write("B00B", new bool[] { true });
                    break;
                case "B12":
                    melsec_net.Write("B00C", new bool[] { true });
                    break;
                case "B13":
                    melsec_net.Write("B00D", new bool[] { true });
                    break;
                case "B14":
                    melsec_net.Write("B00E", new bool[] { true });
                    break;
                case "B15":
                    melsec_net.Write("B00F", new bool[] { true });
                    break;

                case "B16":
                    melsec_net.Write("B010", new bool[] { true });
                    break;
                case "B17":
                    melsec_net.Write("B011", new bool[] { true });
                    break;
                case "B18":
                    melsec_net.Write("B012", new bool[] { true });
                    break;
                case "":
                    melsec_net.Write("B013", new bool[] { true });
                    break;
                case "B19":
                    melsec_net.Write("B014", new bool[] { true });
                    break;
                case "B20":
                    melsec_net.Write("B015", new bool[] { true });
                    break;
                case "B21":
                    melsec_net.Write("B016", new bool[] { true });
                    break;
                case "B22":
                    melsec_net.Write("B017", new bool[] { true });
                    break;
                case "B23":
                    melsec_net.Write("B018", new bool[] { true });
                    break;
                case "B24":
                    melsec_net.Write("B019", new bool[] { true });
                    break;
                case "B25":
                    melsec_net.Write("B01A", new bool[] { true });
                    break;
                case "B26":
                    melsec_net.Write("B01B", new bool[] { true });
                    break;
                case "B27":
                    melsec_net.Write("B01C", new bool[] { true });
                    break;
                case "B28":
                    melsec_net.Write("B01D", new bool[] { true });
                    break;
                case "B29":
                    melsec_net.Write("B01E", new bool[] { true });
                    break;
                case "B30":
                    melsec_net.Write("B01F", new bool[] { true });
                    break;
                default:
                    break;
            }
        }
        public void Write_B_OFF(string B)
        {
            B = B.ToUpper();
            switch (B)
            {
                case "B0":
                    melsec_net.Write("B000", new bool[] { false });
                    break;
                case "B1":
                    melsec_net.Write("B001", new bool[] { false });
                    break;
                case "B2":
                    melsec_net.Write("B002", new bool[] { false });
                    break;
                case "B3":
                    melsec_net.Write("B003", new bool[] { false });
                    break;
                case "B4":
                    melsec_net.Write("B004", new bool[] { false });
                    break;
                case "B5":
                    melsec_net.Write("B005", new bool[] { false });
                    break;
                case "B6":
                    melsec_net.Write("B006", new bool[] { false });
                    break;
                case "B7":
                    melsec_net.Write("B007", new bool[] { false });
                    break;
                case "B8":
                    melsec_net.Write("B008", new bool[] { false });
                    break;
                case "B9":
                    melsec_net.Write("B009", new bool[] { false });
                    break;
                case "B10":
                    melsec_net.Write("B00A", new bool[] { false });
                    break;
                case "B11":
                    melsec_net.Write("B00B", new bool[] { false });
                    break;
                case "B12":
                    melsec_net.Write("B00C", new bool[] { false });
                    break;
                case "B13":
                    melsec_net.Write("B00D", new bool[] { false });
                    break;
                case "B14":
                    melsec_net.Write("B00E", new bool[] { false });
                    break;
                case "B15":
                    melsec_net.Write("B00F", new bool[] { false });
                    break;

                case "B16":
                    melsec_net.Write("B010", new bool[] { false });
                    break;
                case "B17":
                    melsec_net.Write("B011", new bool[] { false });
                    break;
                case "B18":
                    melsec_net.Write("B012", new bool[] { false });
                    break;
                case "":
                    melsec_net.Write("B013", new bool[] { false });
                    break;
                case "B19":
                    melsec_net.Write("B014", new bool[] { false });
                    break;
                case "B20":
                    melsec_net.Write("B015", new bool[] { false });
                    break;
                case "B21":
                    melsec_net.Write("B016", new bool[] { false });
                    break;
                case "B22":
                    melsec_net.Write("B017", new bool[] { false });
                    break;
                case "B23":
                    melsec_net.Write("B018", new bool[] { false });
                    break;
                case "B24":
                    melsec_net.Write("B019", new bool[] { false });
                    break;
                case "B25":
                    melsec_net.Write("B01A", new bool[] { false });
                    break;
                case "B26":
                    melsec_net.Write("B01B", new bool[] { false });
                    break;
                case "B27":
                    melsec_net.Write("B01C", new bool[] { false });
                    break;
                case "B28":
                    melsec_net.Write("B01D", new bool[] { false });
                    break;
                case "B29":
                    melsec_net.Write("B01E", new bool[] { false });
                    break;
                case "B30":
                    melsec_net.Write("B01F", new bool[] { false });
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 读取 X Y M B 状态
        public bool[] ReadBool_X(string StartAdd, ushort num)
        {
            bool[] XaddData = melsec_net.ReadBool(StartAdd, num).Content;            // 读取X是否通，十六进制地址
            return XaddData;
        }
        public bool[] ReadBool_Y(string StartAdd, ushort num)
        {
            bool[] XaddData = melsec_net.ReadBool(StartAdd, num).Content;            // 读取Y是否通，十六进制地址
            return XaddData;
        }
        public bool[] ReadBool_M(string StartAdd, ushort num)
        {
            bool[] MaddData = melsec_net.ReadBool(StartAdd, num).Content;            // 读取M100是否通，十进制地址
            return MaddData;
        }
        public bool[] ReadBool_B(string StartAdd, ushort num)
        {
            bool[] BaddData = melsec_net.ReadBool(StartAdd, num).Content;            // 读取B1A0是否通，十六进制地址
            return BaddData;
        }
        #endregion

        #region 操作D寄存器数据类型
        public void Write_D_short(string D, short Data)//("D1000", (short)1234);
        {
            melsec_net.Write(D, Data);

        }
        public short Read_D_short(string D)//("D1000");
        {
            short ReaData = melsec_net.ReadInt16(D).Content;
            return ReaData;
        }
        public void Write_D_ushort(string D, ushort Data)//("D1000", (ushort)1234);
        {
            melsec_net.Write(D, Data);

        }
        public ushort Read_D_ushortt(string D)//("D1000");
        {
            ushort ReaData = melsec_net.ReadUInt16(D).Content;
            return ReaData;
        }
        public void Write_D_int(string D, int Data)//("D1000", (int)1234);
        {
            melsec_net.Write(D, Data);

        }
        public int Read_D_int(string D)//("D1000");
        {
            int ReaData = melsec_net.ReadInt32(D).Content;
            return ReaData;
        }
        public void Write_D_uint(string D, uint Data)//("D1000", (int)1234);
        {
            melsec_net.Write(D, Data);

        }
        public uint Read_D_uint(string D)//("D1000");
        {
            uint ReaData = melsec_net.ReadUInt32(D).Content;
            return ReaData;
        }
        public void Write_D_float(string D, float Data)//("D1000", (float)1234);
        {
            melsec_net.Write(D, Data);

        }
        public float Read_D_float(string D)//("D1000");
        {
            float ReaData = melsec_net.ReadFloat(D).Content;
            return ReaData;
        }
        public void Write_D_long(string D, long Data)//("D1000", 123.456d)
        {
            melsec_net.Write(D, Data);

        }
        public long Read_D_long(string D)//("D1000");
        {
            long ReaData = melsec_net.ReadInt64(D).Content;
            return ReaData;
        }
        public void Write_D_ulong(string D, ulong Data)//("D1000", 123.456d)
        {
            melsec_net.Write(D, Data);

        }
        public ulong Read_D_ulong(string D)//("D1000");
        {
            ulong ReaData = melsec_net.ReadUInt64(D).Content;
            return ReaData;
        }
        public void Write_D_double(string D, double Data)//("D1000", 123456661235123534L); 
        {
            melsec_net.Write(D, Data);

        }
        public double Read_D_double(string D)//("D1000");
        {
            double ReaData = melsec_net.ReadDouble(D).Content;
            return ReaData;
        }

        public void Write_D_string(string D, string Data)//("D1000", "K123456789"); 
        {
            melsec_net.Write(D, Data);

        }
        public string Read_D_string(string D, ushort linth)//("D1000", 10)
        {
            string ReaData = melsec_net.ReadString(D, linth).Content;
            return ReaData;
        }

        #endregion

        #region 其他操作方式
        /// <summary>
        /// bit写入
        /// </summary>
        /// <param name="bitName">软原件名称</param>
        /// <param name="bitState">设置状态</param>
        /// <returns>执行成功返回0，执行失败返回-1</returns>
        public int writeBit(string bitName, bool bitState)
        {
            bool[] values = new bool[0];
            if (bitState)
            {
                values = new bool[]
                {
                    true
                };
            }
            else
            {
                values = new bool[1];
            }
            OperateResult operateResult = this.melsec_net.Write(bitName, values);
            bool isSuccess = operateResult.IsSuccess;
            int result;
            if (isSuccess)
            {
                result = 0;
            }
            else
            {
                result = -1;
            }
            return result;
        }

        /// <summary>
        /// bit读取
        /// </summary>
        /// <param name="bitName">软原件名称</param>
        /// <returns>返回所读的软元件状态</returns>
        public bool readBit(string bitName)
        {
            OperateResult<bool[]> operateResult = this.melsec_net.ReadBool(bitName, 1);
            return operateResult.IsSuccess;
        }

        /// <summary>
        /// 写入short类型数据
        /// </summary>
        /// <param name="memoryAddress">存储地址</param>
        /// <param name="data">需要写入的数据</param>
        /// <returns>执行OK返回0，失败返回-1</returns>
        public int writeShortDate(string memoryAddress, short data)
        {
            short[] values = new short[]
            {
                data
            };
            OperateResult operateResult = this.melsec_net.Write(memoryAddress, values);
            bool isSuccess = operateResult.IsSuccess;
            int result;
            if (isSuccess)
            {
                result = 0;
            }
            else
            {
                result = -1;
            }
            return result;
        }

        /// <summary>
        /// 读取short类型数据
        /// </summary>
        /// <param name="memoryAddress">存储地址</param>
        /// <returns>执行成功返回读取结果,失败返回99999999</returns>
        public int readShortDate(string memoryAddress)
        {
            OperateResult<short[]> operateResult = this.melsec_net.ReadInt16(memoryAddress, 1);
            bool isSuccess = operateResult.IsSuccess;
            int result;
            if (isSuccess)
            {
                result = (int)operateResult.Content[0];
            }
            else
            {
                result = 99999999;
            }
            return result;
        }

        /// <summary>
        /// 写入int类型数据
        /// </summary>
        /// <param name="memoryAddress">存储地址</param>
        /// <param name="data">写入数据</param>
        /// <returns>执行成功返回0，失败返回-1</returns>
        public int writeIntDate(string memoryAddress, int data)
        {
            int[] values = new int[]
            {
                data
            };
            OperateResult operateResult = this.melsec_net.Write(memoryAddress, values);
            bool isSuccess = operateResult.IsSuccess;
            int result;
            if (isSuccess)
            {
                result = 0;
            }
            else
            {
                result = -1;
            }
            return result;
        }

        /// <summary>
        /// 读取int类型数据
        /// </summary>
        /// <param name="memoryAddress">读取地址</param>
        /// <returns>执行成功返回读取结果，失败返回9999999999</returns>
        public long readIntDate(string memoryAddress)
        {
            OperateResult<long[]> operateResult = this.melsec_net.ReadInt64(memoryAddress, 1);
            bool isSuccess = operateResult.IsSuccess;
            long result;
            if (isSuccess)
            {
                result = operateResult.Content[0];
            }
            else
            {
                result = 99999999999L;
            }
            return result;
        }

        /// <summary>
        /// 读多个输入
        /// </summary>
        /// <param name="Imput_X">输入PLC X地址：X1 X2 ...</param>
        /// <param name="quantity">读取数量</param>
        /// <returns></returns>
        public List<bool> ReadMultipleInputs(string Imput_X, ushort quantity)
        {
            List<bool> ReadResult = new List<bool>();
            // X100-X10F读取显示
            OperateResult<bool[]> read = melsec_net.ReadBool(Imput_X, quantity);
            if (read.IsSuccess)
            {
                // 成功读取，True代表通，False代表不通
                var input = read.Content;

                //bool X200 = read.Content[0];
                //bool X201 = read.Content[1];
                //bool X202 = read.Content[2];
                //bool X203 = read.Content[3];
                //bool X204 = read.Content[4];
                //bool X205 = read.Content[5];
                //bool X206 = read.Content[6];
                //bool X207 = read.Content[7];
                //bool X208 = read.Content[8];
                //bool X209 = read.Content[9];
                //bool X20A = read.Content[10];
                //bool X20B = read.Content[11];
                //bool X20C = read.Content[12];
                //bool X20D = read.Content[13];
                //bool X20E = read.Content[14];
                //bool X20F = read.Content[15];
                // 显示

                return ReadResult;
            }
            else
            {
                //失败读取，显示失败信息
                //MessageBox.Show(read.ToMessageShowString());
                return ReadResult;
            }
        }

        /// <summary>
        /// 写多个寄存器
        /// short[] values = new short[5] { 1335, 8765, 1234, 4567, -2563 };
        /// D100为1234,D101为8765,D102为1234,D103为4567,D104为-2563
        /// </summary>
        /// <param name="ImputStart_D">输入开始D寄存器</param>
        /// <param name="values">要写的值 eg:short[] values = new short[5] { 1335, 8765, 1234, 4567, -2563 } D100为1234,D101为8765,D102为1234,D103为4567,D104为-2563</param>
        /// <returns></returns>
        public bool WriteMultipleRegisters_D(string ImputStart_D, short[] values)
        {

            OperateResult write = melsec_net.Write("ImputStart_D", values);
            if (write.IsSuccess)
            {
                //成功写入
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 写字符串
        /// </summary>
        /// <param name="ImputStart_D"></param>
        /// <returns></returns>
        public bool WriteString_D(string ImputStart_D, string WriteStr)
        {

            OperateResult write = melsec_net.Write(ImputStart_D, WriteStr);// 写入100到104共10个字符的字符串
            if (write.IsSuccess)
            {
                //成功写入
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 读字符串
        /// </summary>
        /// <param name="ImputStart_D"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public string ReadString_D(string ImputStart_D, ushort Lenth)
        {

            string ReadStr = melsec_net.ReadString(ImputStart_D, Lenth).Content;// 读取100到104共10个字符的字符串
            return ReadStr;
        }

        private void ReadWrite2()
        {
            bool[] M100 = melsec_net.ReadBool("M100", 1).Content;            // 读取M100是否通，十进制地址
            bool[] X1A0 = melsec_net.ReadBool("X1A0", 1).Content;            // 读取X1A0是否通，十六进制地址
            bool[] Y1A0 = melsec_net.ReadBool("Y1A0", 1).Content;            // 读取Y1A0是否通，十六进制地址
            bool[] B1A0 = melsec_net.ReadBool("B1A0", 1).Content;            // 读取B1A0是否通，十六进制地址
            short short_D1000 = melsec_net.ReadInt16("D1000").Content;   // 读取D1000的short值  ,W3C0,R3C0 效果是一样的
            ushort ushort_D1000 = melsec_net.ReadUInt16("D1000").Content; // 读取D1000的ushort值
            int int_D1000 = melsec_net.ReadInt32("D1000").Content;          // 读取D1000-D1001组成的int数据
            uint uint_D1000 = melsec_net.ReadUInt32("D1000").Content;       // 读取D1000-D1001组成的uint数据
            float float_D1000 = melsec_net.ReadFloat("D1000").Content;    // 读取D1000-D1001组成的float数据
            long long_D1000 = melsec_net.ReadInt64("D1000").Content;       // 读取D1000-D1003组成的long数据
            ulong ulong_D1000 = melsec_net.ReadUInt64("D1000").Content;       // 读取D1000-D1003组成的long数据
            double double_D1000 = melsec_net.ReadDouble("D1000").Content; // 读取D1000-D1003组成的double数据
            string str_D1000 = melsec_net.ReadString("D1000", 10).Content; // 读取D1000-D1009组成的条码数据


            melsec_net.Write("M100", new bool[] { true });                        // 写入M100为通
            melsec_net.Write("Y1A0", new bool[] { true });                        // 写入Y1A0为通
            melsec_net.Write("X1A0", new bool[] { true });                        // 写入X1A0为通
            melsec_net.Write("B1A0", new bool[] { true });                        // 写入B1A0为通
            melsec_net.Write("D1000", (short)1234);                // 写入D1000  short值  ,W3C0,R3C0 效果是一样的
            melsec_net.Write("D1000", (ushort)45678);              // 写入D1000  ushort值
            melsec_net.Write("D1000", 1234566);                    // 写入D1000  int值
            melsec_net.Write("D1000", (uint)1234566);               // 写入D1000  uint值
            melsec_net.Write("D1000", 123.456f);                    // 写入D1000  float值
            melsec_net.Write("D1000", 123.456d);                    // 写入D1000  double值
            melsec_net.Write("D1000", 123456661235123534L);          // 写入D1000  long值
            melsec_net.Write("D1000", "K123456789");                // 写入D1000  string值
        }

        #endregion 
    }
}
