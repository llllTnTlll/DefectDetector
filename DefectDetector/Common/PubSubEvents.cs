using DefectDetector.Model;
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

    /// <summary>
    /// 在MenuBar导航离开SettingView时触发
    /// </summary>
    public class SaveReminderEvent: PubSubEvent { }

    /// <summary>
    /// 当MenuList选择变化时触发
    /// </summary>
    public class MenuListSlectionChanged: PubSubEvent<string> { }

    /// <summary>
    /// 历史信息被选中后触发
    /// </summary>
    public class HistorySelectedEvent:PubSubEvent<HistoryItemModel> { }

}
