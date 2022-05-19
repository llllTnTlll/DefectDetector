using DefectDetector.Common;
using DefectDetector.Model;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectDetector.ViewModels.mainViews
{
    public class HistoryViewModel:BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        // 左侧历史列表数据源
        public ObservableCollection<HistoryItemModel> HistoryItems { get; set; }

        // 当前被选中的历史信息块
        private HistoryItemModel _selectedItem;
        public HistoryItemModel SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public HistoryViewModel(IEventAggregator eventAggregator)
        {
            // 订阅列表选中事件
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<HistorySelectedEvent>().Subscribe((item) =>
            {
                SelectedItem = item;
            });

            HistoryItems = new ObservableCollection<HistoryItemModel>();
            ObservableCollection<BoxItem> boxes = new ObservableCollection<BoxItem>();
            BoxItem box1 = new BoxItem() { ClsId = 1, Height = 100, Left = 200, Index = 0, Top = 200, Width = 100 };
            BoxItem box2 = new BoxItem() { ClsId = 2, Height = 10, Left = 10, Index = 0, Top = 10, Width = 10 };
            boxes.Add(box1);
            boxes.Add(box2);
            HistoryItems.Add(new HistoryItemModel() { ItemId = 0, EntryTime = DateTime.Now, Inspector = "张勇", Boxes = boxes });
            HistoryItems.Add(new HistoryItemModel() { ItemId = 0, EntryTime = DateTime.Now, Inspector = "张勇", Boxes = boxes });
        }
    }
}
