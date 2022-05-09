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
using Prism.Events;
using static DefectDetector.ViewModels.InitializeWindowViewModel;

namespace DefectDetector.Views
{
    /// <summary>
    /// InitializeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InitializeWindow : Window
    {
        public InitializeWindow()
        {
            InitializeComponent();
        }
    }
}
