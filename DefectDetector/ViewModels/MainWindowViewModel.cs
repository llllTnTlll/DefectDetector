using DefectDetector.Helper;
using DefectDetector.Model;
using DefectDetector.Views.components;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Net.Sockets;

namespace DefectDetector.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public Socket ClientSocket { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion("LeftListRegion", typeof(MenuList));

            // 尝试连接至python服务
            SocketHelper socketHelper = new SocketHelper("127.0.0.1", 50001);
            ClientSocket = socketHelper.Connect();
        }



    }
}
