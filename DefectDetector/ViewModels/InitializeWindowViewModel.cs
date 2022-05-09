using DefectDetector.Helper;
using DefectDetector.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace DefectDetector.ViewModels
{
    public class InitializeWindowViewModel:BindableBase
    {
        private readonly IContainerExtension _container;
        public InitializeWindowViewModel(IContainerExtension container)
        {
            _container = container;

            //检查数据库链接
            TextWord = "  正在连接服务器";
            if (!SQLhelper.IsConnected())Application.Current.Shutdown();
            TextWord = "  连接到检测服务";

            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var initWindow = Application.Current.MainWindow;
                var loginWindow = _container.Resolve<LoginWindow>();
                Application.Current.MainWindow = loginWindow;
                initWindow.Close();
                loginWindow.Show();
            }));
        }

        /// <summary>
        /// 初始化窗体加载显示文本
        /// </summary>
        private string _textWord;
        public string TextWord
        {
            get { return _textWord; }
            set { _textWord = value; RaisePropertyChanged(); }
        }

    }
}
