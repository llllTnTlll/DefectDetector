using DefectDetector.Helper;
using DefectDetector.Views;
using DefectDetector.Views.components;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;

namespace DefectDetector.ViewModels
{
    public class LoginWindowViewModel:BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IContainerExtension _container;
        public LoginWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IContainerExtension container)
        {
            _regionManager = regionManager;
            _container = container;

            _regionManager.RegisterViewWithRegion("InputRegion", typeof(LoginInputRegion));
            eventAggregator.GetEvent<LoginEvent>().Subscribe(OnLoginEventRecved);
        }

        private void OnLoginEventRecved(string obj)
        {
            if (obj == "true")
            {
                IsLogin = obj;
                //使用Timer为动画显示效果延时
                Timer timer = new Timer();
                timer.AutoReset = false;
                timer.Interval = 400;
                timer.Elapsed += Timer_Elapsed;
                timer.Enabled = true;
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //关闭动画结束，关闭窗体
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var loginWindow = Application.Current.MainWindow;
                var mainWindow = _container.Resolve<MainWindow>();
                Application.Current.MainWindow = mainWindow;
                loginWindow.Close();
                mainWindow.Show();
            }));

        }

        private string _isLogin;

        public string IsLogin
        {
            get { return _isLogin; }
            set { _isLogin = value; RaisePropertyChanged();}
        }

    }
}