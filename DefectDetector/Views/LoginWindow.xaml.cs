using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static DefectDetector.ViewModels.InitializeWindowViewModel;

namespace DefectDetector.Views
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        //窗体关闭
        private void CloseButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //窗体拖动
        private void WindowMove(object sender, MouseEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                Point p = Mouse.GetPosition(this);
                if (p.X >= 400 && p.X <= 700 && p.Y >= 170 && p.Y <= 400)
                {
                    //限定区域内不响应窗体拖动
                }
                else
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                        try { DragMove(); }
                        catch { }
                }

            }));

        }

        private void MinsizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Stop();
            (sender as MediaElement).Play();
        }

        private void MediaElement_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Play();
        }
    }
}
