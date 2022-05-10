using DefectDetector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectDetector.Common
{
    public class CoordTransfer
    {
        public static BoxItem Transformer(double xmin, double ymin, double xmax, double ymax, int index)
        {
            double left = xmin;
            double top = ymin;
            double width = xmax - xmin;
            double height = ymax - ymin;
            BoxItem boxItem = new BoxItem() { Left = left, Top = top, Height = height, Width = width, ClsId = index };
            return boxItem;
        }
    }
}
