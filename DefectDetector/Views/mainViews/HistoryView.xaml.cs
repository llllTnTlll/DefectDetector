using DefectDetector.Common;
using DefectDetector.Model;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DefectDetector.Views.mainViews
{
    /// <summary>
    /// HistoryView.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryView : UserControl
    {
        public static double Board_Width { get; set; }
        public static double Board_Height { get; set; }

        private readonly IEventAggregator _eventAggregator;
        public HistoryView(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Board_Width = chart_board.ActualWidth;
            Board_Height = chart_board.ActualHeight;
        }

        private void historyListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HistoryItemModel selectedItem = historyListbox.SelectedItem as HistoryItemModel;
            if (selectedItem != null)
            {
                _eventAggregator.GetEvent<HistorySelectedEvent>().Publish(selectedItem);
            }
        }
    }
}
