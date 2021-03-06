using DefectDetector.Common;
using DefectDetector.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DefectDetector.Model
{
    public class BoxItem : BindableBase
    {
        // 瑕疵Box索引
        private int _index;
        public int Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }

        // 左边距
        private double _left;
        public double Left
        {
            get { return _left; }
            set { SetProperty(ref _left, value); }
        }

        // 顶边距
        private double _top;
        public double Top
        {
            get { return _top; }
            set { SetProperty(ref _top, value); }
        }

        // 高度
        private double _height;
        public double Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }

        // 宽度
        private double _width;
        public double Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        // 类别id
        private int clsId;
        public int ClsId
        {
            get { return clsId; }
            set
            {
                clsId = value;
                RaisePropertyChanged(nameof(ClsId));
                RaisePropertyChanged(nameof(ClsName));
                RaisePropertyChanged(nameof(ClsColor));
            }
        }

        public string ClsName
        {
            get
            {
                return MainWindowViewModel.ClsNames[ClsId];
            }
        }

        public Brush ClsColor
        {
            get
            {
                return MainWindowViewModel.ClsBrushes[ClsId];
            }
        }

        public DelegateCommand BoxClickCommand { get; set; }
    }
}
