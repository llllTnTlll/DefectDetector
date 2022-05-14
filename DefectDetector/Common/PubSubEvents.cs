﻿using DefectDetector.Model;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectDetector.Common
{
    public class LoginEvent : PubSubEvent<string> { }

    /// <summary>
    /// 当图表中的瑕疵box被选中时传递BoxItem对象
    /// </summary>
    public class BoxSelectedEvent: PubSubEvent<BoxItem> { }

}
