using ActProgType64Lib;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectDetector.ViewModels.mainViews
{
    public class PLCViewModel:BindableBase
    {
        private ActProgType64Class lpcom_ReferencesProgType;

        private int int_result;

        // 文本框提示内容
        private string _textContent;
        public string TextContent
        {
            get { return _textContent; }
            set { SetProperty(ref _textContent, value); }
        }

        // 连接按钮绑定内容
        public DelegateCommand OpenCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }

        public DelegateCommand GetDeviceValueCommand { get; set; }

        public DelegateCommand SetDeviceValueCommand { get; set; }

        public PLCViewModel()
        {
            lpcom_ReferencesProgType = new ActProgType64Class();
            OpenCommand = new DelegateCommand(Open);
            CloseCommand = new DelegateCommand(Close);
            GetDeviceValueCommand = new DelegateCommand(GetDeviceValue);
            SetDeviceValueCommand = new DelegateCommand(SetDeviceValue);
            
        }

        private void SetDeviceValue()
        {
            lpcom_ReferencesProgType.SetDevice("D20", 1000000);
        }

        private void GetDeviceValue()
        {
            lpcom_ReferencesProgType.GetDevice("D20", out int_result);
            TextContent = int_result.ToString();
        }

        private void Close()
        {
            int iReturnCode = lpcom_ReferencesProgType.Close();

            if (iReturnCode == 0)
            {
                TextContent = "退出成功";
            }
            else
            {
                TextContent = "退出失败";
            }
        }

        /// <summary>
        /// 连接到PLC
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void Open()
        {
            // 配置连接参数
            lpcom_ReferencesProgType.ActSourceStationNumber = 1;
            lpcom_ReferencesProgType.ActUnitType = 15;
            lpcom_ReferencesProgType.ActProtocolType = 4;
            lpcom_ReferencesProgType.ActNetworkNumber = 0;
            lpcom_ReferencesProgType.ActStationNumber = 255;
            lpcom_ReferencesProgType.ActUnitNumber = 0;
            lpcom_ReferencesProgType.ActConnectUnitNumber = 0;
            lpcom_ReferencesProgType.ActIONumber = 0;
            lpcom_ReferencesProgType.ActCpuType = 520;
            lpcom_ReferencesProgType.ActPortNumber = 3;
            lpcom_ReferencesProgType.ActBaudRate = 9600;
            lpcom_ReferencesProgType.ActDataBits = 7;
            lpcom_ReferencesProgType.ActParity = 2;
            lpcom_ReferencesProgType.ActStopBits = 0;
            lpcom_ReferencesProgType.ActControl = 8;
            lpcom_ReferencesProgType.ActCpuTimeOut = 0;
            lpcom_ReferencesProgType.ActTimeOut = 10000;
            lpcom_ReferencesProgType.ActSumCheck = 0;
            lpcom_ReferencesProgType.ActPacketType = 1;

            int iReturnCode = lpcom_ReferencesProgType.Open();

            if (iReturnCode == 0)
            {
                TextContent = "连接成功";

            }
            else
            {
                TextContent = "连接失败";
            }
        }


    }
}
