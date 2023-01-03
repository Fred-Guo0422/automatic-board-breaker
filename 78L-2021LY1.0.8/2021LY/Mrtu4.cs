using _2021LY.Forms;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021LY
{
    class Mrtu4
    {
        //串口参数
        private string portName;
        private int baudRate;
        private Parity parity;
        private int dataBits;
        private StopBits stopBits;

        private SerialPort port;
        private IModbusMaster master;

        public Mrtu4(string _portName, int _baudRate, StopBits _stopBits, Parity _parity, int _dataBits)
        {
            portName = _portName;
            baudRate = _baudRate;
            parity = _parity;
            dataBits = _dataBits;
            stopBits = _stopBits;

            InitSerialPortParameter();

            master = ModbusSerialMaster.CreateRtu(port);
        }

        private SerialPort InitSerialPortParameter()
        {
            port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            port.Open();
            return port;
        }

        public async void _wuint16(ushort startAddress, byte slaveAddress, ushort vla)
        {
            if (vla == 11468 || vla == 10922)
            {
                CommParam.Mrtu_Send = true;
            }
            //Console.WriteLine(startAddress);
            //Console.WriteLine(slaveAddress);
            //Console.WriteLine(vla);
            try
            {

            await master.WriteSingleRegisterAsync(slaveAddress, startAddress, vla);
            }
            catch (Exception ex)
            {

              //  Console.WriteLine(ex.StackTrace);
            }
        }

        public ushort[] _ruint16(ushort startAddress, byte slaveAddress, ushort numberOfPoints)
        {
            ushort[] registerBuffer;
            registerBuffer = master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
            return registerBuffer;
        }

        public void closeCom()
        {
            port.Close();
        }


    }
}
