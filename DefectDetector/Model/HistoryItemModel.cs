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
        private int _preformId;
        public int PreformId
        {
            get { return _preformId; }
            set { SetProperty(ref _preformId, value); }
        }

        // 是否被标记
        private bool _isMarked;
        public bool IsMarked
        {
            get { return _isMarked; }
            set { SetProperty(ref _isMarked, value); }
        }

        // 检测时间
        private DateTime _detectionTime;
        public DateTime DetectionTime
        {
            get { return _detectionTime; }
            set { SetProperty(ref _detectionTime, value); }
        }

        // 检测员
        private string _operator;
        public string Operator
        {
            get { return _operator; }
            set { SetProperty(ref _operator, value); }
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
