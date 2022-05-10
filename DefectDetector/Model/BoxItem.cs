using DefectDetector.ViewModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DefectDetector.Model
{
    public class BoxItem : BindableBase
    {
        private int clsId;

        public double Left { get; set; }
        public double Top { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
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

    }
}
