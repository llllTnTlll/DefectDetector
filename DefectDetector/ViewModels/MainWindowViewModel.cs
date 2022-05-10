using DefectDetector.Common;
using DefectDetector.Helper;
using DefectDetector.Model;
using DefectDetector.Views.components;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DefectDetector.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        // 底部状态栏颜色属性
        private SolidColorBrush bottomColor;
        public SolidColorBrush BottomColor
        {
            get { return bottomColor; }
            set
            {
                bottomColor = value;
                RaisePropertyChanged();
            }
        }

        // 底部状态栏提示信息
        private string _statusPrompt;
        public string StatusPrompt
        {
            get { return _statusPrompt; }
            set { SetProperty(ref _statusPrompt, value); }
        }

        // 底部状态栏提示图标
        private string _statusIcon;
        public string StatusIcon
        {
            get { return _statusIcon; }
            set { SetProperty(ref _statusIcon, value); }
        }

        // 通讯用Socket连接
        public Socket ClientSocket { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            //初始化底部状态栏颜色
            BottomColor = App.Current.Resources["PrimaryHueMidBrush"] as SolidColorBrush;

            //为各Region分配初始内容
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion("LeftListRegion", typeof(MenuList));

            // 启动连接服务
            StatusPrompt = "   正在连接至检测服务";
            StatusIcon = "HumanGreetingProximity";
            Task connectionTask = new Task(Connect2PyServ);
            connectionTask.Start();
            

        }

        /// <summary>
        /// 尝试连接至python服务
        /// </summary>
        public void Connect2PyServ()
        {
            while (true)
            {
                try
                {
                    SocketHelper socketHelper = new SocketHelper("127.0.0.1", 50001);
                    ClientSocket = socketHelper.Connect();
                    BottomColor = App.Current.Resources["PrimaryHueMidBrush"] as SolidColorBrush;
                    StatusPrompt = "   就绪";
                    StatusIcon = "Check";
                    return;
                }
                catch (SocketConnectException)
                {
                    // 设置底部提示条黄色闪烁
                    BottomColor = App.Current.Resources["BottomWarningBrush"] as SolidColorBrush;
                    StatusPrompt = "   正在重连至检测服务";
                    StatusIcon = "LanDisconnect";
                    // 尝试重新连接
                }
            }
        }


    }
}
