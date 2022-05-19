using DefectDetector.Common;
using DefectDetector.Helper;
using DefectDetector.Model;
using DefectDetector.Views.components;
using DefectDetector.Views.mainViews;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media;

namespace DefectDetector.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        #region 全局状态指示
        // 指示是否正确连接到服务
        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                RaisePropertyChanged(nameof(IsConnected));
                if(IsConnected && !IsDetecting)
                {
                    CanDetectBtnClick = true;
                }
                else CanDetectBtnClick = false;
            }
        }

        // 是否正在检测
        private bool _isDetecting;
        public bool IsDetecting
        {
            get { return _isDetecting; }
            set
            {
                _isDetecting = value;
                RaisePropertyChanged(nameof(IsDetecting));
                if (IsConnected && !IsDetecting)
                {
                    CanDetectBtnClick = true;
                }
                else CanDetectBtnClick = false;
            }
        }
        #endregion

        #region 主窗体
        // 瑕疵图表画笔
        public static Dictionary<int, Brush> ClsBrushes { get; set; }

        // 瑕疵Id对应表
        public static Dictionary<int, string> ClsNames { get; set; }

        // 图表数据源
        public ObservableCollection<BoxItem> Boxes { get; set; }

        // 预制棒信息属性
        private PreformModel _preformInfo;
        public PreformModel PreformInfo
        {
            get { return _preformInfo; }
            set { SetProperty(ref _preformInfo, value); }
        }

        // 顶部ToolBar 收起/显示
        private bool _isToolBarShow;
        public bool IsToolBarShow
        {
            get { return _isToolBarShow; }
            set { SetProperty(ref _isToolBarShow, value); }
        }

        // 底部状态栏颜色属性
        private SolidColorBrush bottomColor;
        public SolidColorBrush BottomColor
        {
            get { return bottomColor; }
            set
            {
                bottomColor = value;
                RaisePropertyChanged();
            }
        }

        // 底部状态栏提示信息
        private string _statusPrompt;
        public string StatusPrompt
        {
            get { return _statusPrompt; }
            set { SetProperty(ref _statusPrompt, value); }
        }

        // 底部状态栏提示图标
        private string _statusIcon;
        public string StatusIcon
        {
            get { return _statusIcon; }
            set { SetProperty(ref _statusIcon, value); }
        }

        // 通讯用Socket连接
        public Socket ClientSocket { get; private set; }

        // Socket通信结果包
        private ResultPkg _result;
        public ResultPkg Result
        {
            get { return _result; }
            set
            {
                if (_result == value) return;
                _result = value;
                CanReviseBtnClick = true;
                RaisePropertyChanged(nameof(Result));
                OnResultChanged();

            }
        }
        #endregion

        #region ChartView
        // 图表刷新事件
        public delegate void ResultChangedEventHandler();
        public event ResultChangedEventHandler ResultChanged;
        protected virtual void OnResultChanged()
        {
            if (ResultChanged != null)
            {
                CanReviseBtnClick = true;
                ResultChanged.Invoke();
            }
        }

        // 修改结果按钮可用性
        private bool _canReviseBtnClick;
        public bool CanReviseBtnClick
        {
            get { return _canReviseBtnClick; }
            set { SetProperty(ref _canReviseBtnClick, value); }
        }

        // 修改结果按钮绑定命令
        public DelegateCommand ReviseResultCommand { get; private set; }

        // 检测按钮可用性
        private bool _canDetectBtnClick;
        public bool CanDetectBtnClick
        {
            get { return _canDetectBtnClick; }
            set
            {
                _canDetectBtnClick = value;
                RaisePropertyChanged();
            }
        }

        // 检测按钮绑定文本
        private string _detectionBtnText;
        public string DetectionBtnText
        {
            get { return _detectionBtnText; }
            set { SetProperty(ref _detectionBtnText, value); }
        }

        // 被选中瑕疵Box
        private BoxItem _selectedBoxItem;
        public BoxItem SelectedBoxItem
        {
            get { return _selectedBoxItem; }
            set { SetProperty(ref _selectedBoxItem, value); }
        }

        // 检测按钮绑定命令
        public DelegateCommand StartDetectionCommand { get; private set; }
        #endregion

        private readonly IEventAggregator _eventAggregator;
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            // 初始化状态参数
            IsConnected = false;
            IsDetecting = false;
            IsToolBarShow = true;
            CanDetectBtnClick = false;
            CanReviseBtnClick = false;
            DetectionBtnText = "开始检测";


            // 初始化瑕疵图表画笔及Id对应表
            Boxes = new ObservableCollection<BoxItem>();
            ClsBrushes = new Dictionary<int, Brush>();
            ClsNames = new Dictionary<int, string>();

            ClsBrushes.Add(0, new SolidColorBrush(Color.FromRgb(255, 255, 255)));
            ClsBrushes.Add(1, new SolidColorBrush(Color.FromRgb(179, 238, 58)));
            ClsBrushes.Add(2, new SolidColorBrush(Color.FromRgb(255, 215, 0)));
            ClsBrushes.Add(3, new SolidColorBrush(Color.FromRgb(238, 99, 99)));

            ClsNames.Add(0, "_background_");
            ClsNames.Add(1, "rectangle");
            ClsNames.Add(2, "triangle");
            ClsNames.Add(3, "circle");

            //初始化底部状态栏颜色
            BottomColor = App.Current.Resources["PrimaryHueMidBrush"] as SolidColorBrush;

            //为各Region分配初始内容
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion("LeftListRegion", typeof(MenuList));
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(ScreenSaver));

            // 启动连接服务
            StatusPrompt = "   正在连接至检测服务";
            StatusIcon = "HumanGreetingProximity";
            Task connectionTask = new Task(Connect2PyServ);
            connectionTask.Start();

            // 订阅工具栏刷新事件
            eventAggregator.GetEvent<MenuListSlectionChanged>().Subscribe(ToolBarRefresh);

            #region ChartView

            // 订阅Result更新事件
            // 在Result更新时刷新图表控件
            ResultChanged += RefreshChart;

            // 初始化Command
            StartDetectionCommand = new DelegateCommand(StartDetection);
            ReviseResultCommand = new DelegateCommand(ReviseResult);

            // 订阅Box选中事件
            // 在选中时更新SelectedBoxItem
            eventAggregator.GetEvent<BoxSelectedEvent>().Subscribe((BoxItem item) =>
            {
                SelectedBoxItem = item;
            });
            #endregion
        }

        /// <summary>
        /// 刷新ToolBar与MainRegion对应
        /// </summary>
        /// <param name="navigateTo"></param>
        private void ToolBarRefresh(string navigateTo)
        {
            string barName = navigateTo + "Bar";
            //收起工具栏
            IsToolBarShow = false;
            //使用Timer为动画显示效果延时
            //刷新工具栏
            Timer timer = new Timer();
            timer.AutoReset = false;
            timer.Interval = 500;
            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    _regionManager.RequestNavigate("ToolBarRegion", barName);
                }));
                IsToolBarShow = true;
            };
            timer.Enabled = true;
        }

        private void ReviseResult()
        {
            Boxes[SelectedBoxItem.Index] = SelectedBoxItem;
        }

        /// <summary>
        /// 根据Result刷新图表内容
        /// </summary>
        private void RefreshChart()
        {
            ObservableCollection<BoxItem> boxes = new ObservableCollection<BoxItem>();
            int index = 0;
            foreach (List<double> finalbox in Result.Final_boxes)
            {
                double ymin = finalbox[0];
                double xmin = finalbox[1];
                double ymax = finalbox[2];
                double xmax = finalbox[3];
                BoxItem item = CoordTransfer.Transformer(xmin, ymin, xmax, ymax, Result.Final_class_ids[index]);
                item.Index = index;
                item.BoxClickCommand = new DelegateCommand(() =>
                {
                    _eventAggregator.GetEvent<BoxSelectedEvent>().Publish(item);
                });

                boxes.Add(item);
                index += 1;
            }
            //处理输入的数据
            App.Current.Dispatcher.BeginInvoke((Action)delegate ()
            {
                Boxes.Clear();
                foreach (BoxItem box in boxes)
                {
                    Boxes.Add(box);
                }
            });
        }

        /// <summary>
        /// 开始检测 刷新检测列表
        /// </summary>
        private void StartDetection()
        {
            // 确认连接可用
            if (IsConnected && ClientSocket == null)
                return;

            // 若检测到自动保存开启
            if(PreformInfo!=null && (bool)DetectorSettings.Default["ResultAutoSave"] == true)
            {
                // 向预制棒信息表中填充信息
                string sql = String.Format("INSERT Into PreformInfo(IsMarked, DetectionTime, Operator) VALUES({0},'{1}','{2}')",
                    Convert.ToInt32(PreformInfo.IsMarked),PreformInfo.DetectionTime,PreformInfo.Operator);
                SQLhelper.ExecuteNonQuery(sql, 1);
            }
            IsDetecting = true;
            CanReviseBtnClick = false;
            DetectionBtnText = "正在检测";
            PreformInfo = null;

            //获取预测包
            CommandPkg commandPkg = new CommandPkg()
            {
                Command = "predict",
                Params = ""
            };

            Task recv_task = new Task(() => DetectionTaskMethod(commandPkg));
            recv_task.Start();
            recv_task.ContinueWith(t =>
            {
                IsDetecting = false; 
                DetectionBtnText = "开始检测";
                PreformInfo = new PreformModel()
                {
                    IsMarked = false,
                    DetectionTime = DateTime.Now.ToString("G"),
                    Operator = "致远",
                };
            });
        }

        /// <summary>
        /// 尝试连接至python服务
        /// </summary>
        public void Connect2PyServ()
        {
            while (true)
            {
                try
                {
                    SocketHelper socketHelper = new SocketHelper("127.0.0.1", 50001);
                    ClientSocket = socketHelper.Connect();
                    BottomColor = App.Current.Resources["PrimaryHueMidBrush"] as SolidColorBrush;
                    StatusPrompt = "   就绪";
                    StatusIcon = "Check";
                    IsConnected = true;
                    return;
                }
                catch (SocketConnectException)
                {
                    // 设置底部提示条黄色闪烁
                    BottomColor = App.Current.Resources["BottomWarningBrush"] as SolidColorBrush;
                    StatusPrompt = "   正在重连至检测服务";
                    StatusIcon = "LanDisconnect";
                    IsConnected = false;
                    // 尝试重新连接
                }
            }
        }

        /// <summary>
        /// 开始检测按钮线程任务
        /// </summary>
        /// <param name="commandPkg"></param>
        private void DetectionTaskMethod(CommandPkg commandPkg)
        {
            try
            {
                this.Result = SocketHelper.RecvPkg(ClientSocket, commandPkg);
            }
            catch(SocketConnectException)
            {
                // 中断当前Socket连接
                ClientSocket.Close();
                // 重试连接
                Task connectionTask = new Task(Connect2PyServ);
                connectionTask.Start();

            }
        }
    }
}
