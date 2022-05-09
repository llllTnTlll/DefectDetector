using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DefectDetector.Helper
{
    public class BindedPasswordHelper
    {
        /// <summary>
        /// 扩展密码框密码可用于绑定的帮助类
        /// </summary>
        #region 指示密码框的密码是否可以绑定的附加属性

        public static bool GetIsPasswordBindingEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPasswordBindingEnabledProperty);
        }

        public static void SetIsPasswordBindingEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPasswordBindingEnabledProperty, value);
        }

        public static readonly DependencyProperty IsPasswordBindingEnabledProperty =
            DependencyProperty.RegisterAttached("IsPasswordBindingEnabled", typeof(bool),
            typeof(BindedPasswordHelper),
            new UIPropertyMetadata(false, OnIsPasswordBindingEnabledChanged));

        private static void OnIsPasswordBindingEnabledChanged(DependencyObject obj,
                                                              DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = obj as PasswordBox;

            if (passwordBox != null)
            {
                passwordBox.PasswordChanged -= PasswordBoxPasswordChanged;

                if ((bool)e.NewValue)
                {
                    passwordBox.PasswordChanged += PasswordBoxPasswordChanged;
                }

            }
        }

        //当密码发生改变时，更新用于绑定的密码
        private static void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;

            if (!string.Equals(GetBindedPassword(passwordBox), passwordBox.Password))
            {
                SetBindedPassword(passwordBox, passwordBox.Password);
            }
        }

        #endregion

        #region 用于绑定的密码的附加属性

        public static string GetBindedPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BindedPasswordProperty);
        }


        public static void SetBindedPassword(DependencyObject obj, string value)
        {
            obj.SetValue(BindedPasswordProperty, value);
        }

        public static readonly DependencyProperty BindedPasswordProperty =
            DependencyProperty.RegisterAttached("BindedPassword", typeof(string),
            typeof(BindedPasswordHelper),
            new UIPropertyMetadata(string.Empty, OnBindedPasswordChanged));

        //当被绑定的密码更新时，更新密码
        private static void OnBindedPasswordChanged(DependencyObject obj,
                                                    DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = obj as PasswordBox;
            if (passwordBox != null && e.NewValue != null && passwordBox.Password != e.NewValue.ToString())
            {
                passwordBox.Password = e.NewValue.ToString();
            }
        }

        #endregion

    }
}
