using DefectDetector.Common;
using DefectDetector.Helper;
using DefectDetector.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace DefectDetector.ViewModels.components
{
    public class LoginInputRegionViewModel : BindableBase
    {
        private readonly IContainerExtension _container;
        private readonly IEventAggregator _eventAggregator;
        public LoginInputRegionViewModel(IContainerExtension container ,IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;

            this.LoginCommand = new DelegateCommand(() =>
            {
                //判断输入不为空
                if (InputName == null || InputPassword == null)
                {
                    PromptText = "请输入用户名与密码";
                    IsLogin = "";
                    IsLogin = "false";
                    return;
                }
                //执行SQL查询
                string sql = string.Format("SELECT * FROM UserInfo WHERE userName='{0}' and userPassword='{1}'", InputName, InputPassword);
                object result = SQLhelper.ExecuteScalar(sql, 1);
                if (result != null)
                {
                    IsLogin = "";
                    IsLogin = "true";
                    //通知LoginWindow进行关闭动画
                    _eventAggregator.GetEvent<LoginEvent>().Publish(IsLogin);
                }
                else
                {
                    PromptText = "用户名或密码错误";
                    IsLogin = "";
                    IsLogin = "false";
                }
            });
        }
        //用户名
        private string _inputName;
        public string InputName
        {
            get { return _inputName; }
            set { _inputName = value; RaisePropertyChanged(); }
        }
        //密码
        private string _inputPassword;
        public string InputPassword
        {
            get { return _inputPassword; }
            set { _inputPassword = value; RaisePropertyChanged(); }
        }
        //提示文本
        private string _promptText;
        public string PromptText
        {
            get { return _promptText; }
            set { _promptText = value; RaisePropertyChanged(); }
        }
        //登录指示
        private string _isLogin;
        public string IsLogin
        {
            get { return _isLogin; }
            set { _isLogin = value; RaisePropertyChanged(); }
        }

        public DelegateCommand LoginCommand { get; set; }

    }
}
