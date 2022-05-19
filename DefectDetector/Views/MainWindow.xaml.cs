using System;
using System.Windows;

namespace DefectDetector.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hide_Completed(object sender, EventArgs e)
        {
            toolBar.Margin = new Thickness(0, -50, 0, 50);
        }

        private void Show_Completed(object sender, EventArgs e)
        {
            toolBar.Margin = new Thickness(0, 0, 0, 0);
        }
    }
}
