using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectDetector.Model
{
    public class HistoryItemModel:BindableBase
    {
        // 预制棒Id
        private int _itemId;
        public int ItemId
        {
            get { return _itemId; }
            set { SetProperty(ref _itemId, value); }
        }

        // 检测时间
        private DateTime _entryTime;
        public DateTime EntryTime
        {
            get { return _entryTime; }
            set { SetProperty(ref _entryTime, value); }
        }

        // 检测员
        private string _inspector;
        public string Inspector
        {
            get { return _inspector; }
            set { SetProperty(ref _inspector, value); }
        }

        // 瑕疵预测框
        private ObservableCollection<BoxItem> _boxes;
        public ObservableCollection<BoxItem> Boxes
        {
            get { return _boxes; }
            set { SetProperty(ref _boxes, value); }
        }

    }
}
