using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using Common.Logging;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Teleware.DataCenter.Common;
using Teleware.DataCenter.MapEditor;
using Teleware.DataCenter.MapEditor.Commands;
using Teleware.DataCenter.MapEditor.Common;
using Teleware.DataCenter.MapEditor.EditCommands;
using Teleware.DataCenter.MapEditor.EditCommands.EditControl;
using Teleware.DataCenter.MapEditor.EditTools;
using Teleware.DataCenter.MapEditor.Enums;
using Teleware.DataCenter.MapEditor.Event;
using Teleware.DataCenter.MapEditor.Forms;
using Teleware.DataCenter.MapEditor.Interfaces;
using Teleware.DataCenter.MapViewer.Commands;
using Teleware.DataCenter.MapViewer.Controls;
using Teleware.DataCenter.UI;
using Teleware.OneMap.ArcGIS.Carto;
using Teleware.OneMap.Components;
using Teleware.OneMap.Desktop.Gui;
using Teleware.OneMap.Domain;
using Teleware.OneMap.Infrastructure;
using Teleware.OneMap.Infrastructure.Events;
using Teleware.OneMap.Infrastructure.IoC;
using ApplicationContext = Teleware.OneMap.Applications.ApplicationContext;
using Timer = System.Timers.Timer;

namespace Teleware.DataCenter.MapViewer.Forms
{
    public partial class MapViewerForm : XtraForm, IMapViewer, IEditer
    {
        private readonly IEngineEditor _engineEditor; //arcEngine编辑器
        private IFeatureLayer _currentLayer;
        private readonly IEventAggregator _eventAggregator;
        private readonly IComponentBuilder _componentBuilder;
        private readonly ComponentContext _componentContext;
        private readonly IUnitOfWork _mainUnitOfWork;
        private int _visibleLayerCount ;
        private int _count ;  //防止控件命名冲突
        private BarCheckItem _currentBarItem;//当前按钮；
        private List<esriGeometryType> _geometryTypeList;//当前复制的要素类型列表
        private Timer _timer;
        public event EventHandler<MapLocationChangedEventArgs> CurrentLocationChanged;
        public Action<double> SetMainMapScale { get; set; }

        public MapViewerForm()
        {
            InitializeComponent();

            _mainUnitOfWork = ContainerManager.GetContainer(ContainerNames.Main).Resolve<IUnitOfWork>();
            _componentBuilder = ContainerManager.GetContainer(ContainerNames.Main).Resolve<IComponentBuilder>();
            IServiceProvider serviceProvider = new DelegateServiceLocator(GetServiceByTypeAndName, GetAllServicesByType);
            _componentContext = new ComponentContext(serviceProvider, components);

            BindToMap();
            _engineEditor = new EngineEditorClass();
            _eventAggregator = ContainerManager.GetContainer().Resolve<IEventAggregator>();
            InitEvent();//订阅事件
        }

        #region 方法
        private void BindToMap()
        {
            mainMapControl.ActiveView.FocusMap.Name = "地图";
            toccControl1.DockManager = dockManager;
            toccControl1.Init(mainMapControl.Object as IMapControl4);
            dockPanelBufferResult.Visibility = DockVisibility.Hidden;
            overrideMap1.MainMap = mainMapControl;
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        private void InitEvent()
        {
            var eventAggreator = ContainerManager.GetContainer().Resolve<IEventAggregator>();
            eventAggreator.Subscribe<StartStopEditEvent>(StartStopEdit);//订阅 启动停止编辑事件 
            eventAggreator.Subscribe<TargetLayerChangedEvent>(TargetLayerChanged);//订阅 当前编辑图层发生改变事件
            eventAggreator.Subscribe<ChangeActiveButtonEvent>(ChangeActiveButton);//订阅 编辑工具栏当前功能按钮发生改变事件
            eventAggreator.Subscribe<ChangeButtonCheckedStateEvent>(ChangeButtonCheckedState);// 订阅 编辑工具栏中BarCheckItem的选中状态发生改变事件
            eventAggreator.Subscribe<ChangeBarCheckGroupEnableEvent>(ChangeButtonGroupEnable);//订阅 编辑工具栏中特定分组BarCheckItem的可用性发生改变事件
            //to do:save edits at regular time
            var value = ApplicationContext.GetParameterValue("autoSaveInterval", "3");
            _timer = new Timer(Convert.ToDouble(value) * 60000) {AutoReset = false};
            _timer.Elapsed += SaveEditer;
        }

        private void UpdateBarEditScaleText()
        {
            var scale = new ScaleItem(mainMapControl.MapScale);
            barEditScale.EditValue = scale;
        }

        private object GetServiceByTypeAndName(Type type, string name)
        {
            if (type == typeof(AxMapControl) && (name == null || name == mainMapControl.Name))
                return mainMapControl;
            if (type == typeof(BarManager) && name == null)
                return barManager1;
            return null;
        }

        private IEnumerable<object> GetAllServicesByType(Type type)
        {
            if (type == typeof(AxMapControl))
                yield return mainMapControl;
            if (type == typeof(BarManager))
                yield return barManager1;
        }
       
        /// <summary>
        /// 启动停止编辑触发事件
        /// </summary>
        /// <param name="ev"></param>
        public void StartStopEdit(StartStopEditEvent ev)
        {
            var mapViewer = Workbench.Instance.GetService<IMapViewer>();
            mainMapControl = mapViewer.MapControl;
            EditButtonInitialize();
            if (ev.EditFlag)
            {
                ChangeBarCheckItemsEnabled(true, "1"); //启动编辑时 改变控件可用性
            }
            else
            {
                ChangeBarCheckItemsEnabled(false, "all");//结束编辑时 改变控件可用性
            }
        }
        /// <summary>
        /// 自动保存编辑内容
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void SaveEditer(object source, ElapsedEventArgs e)
        {
            if (InvokeRequired)
            {
                EventHandler<ElapsedEventArgs> handler = SaveEditer;
                Invoke(handler, source, e);
            }
            var value = ApplicationContext.GetParameterValue("autoSaveStyle", "关闭自动保存");
            if (value.Equals("直接保存"))
            {
                SaveEditCommand cmd = new SaveEditCommand { HasPopForm = false };
                cmd.Execute(null);
                SaveTimer.Stop();
                SaveTimer.Start();
            }
            else if (value.Equals("弹框提示"))
            {
                SaveEditCommand cmd = new SaveEditCommand { HasPopForm = true };
                cmd.Execute(null);
                SaveTimer.Stop();
                SaveTimer.Start();
            }
        }  
        /// <summary>
        ///编辑控件刷新可点击性 
        /// </summary>
        private void EditButtonInitialize()
        {
            //重新绑定控件
            mapEditToolbar.ItemLinks.Clear();
            InitMapToolbar();
        }

        /// <summary>
        /// 初始化图形(编辑)工具条
        /// </summary>
        private void InitMapToolbar()
        {
            //添加比例尺列表
            ((RepositoryItemComboBox)barEditScale.Edit).Items.AddRange(ScaleItem.MainMapScaleItems);
            using (_mainUnitOfWork.Start())
            {
                _componentBuilder.BuildUp(mapEditToolbar, ControlNames.MAP_EDIT_TOOLBAR, _componentContext, true, c => !c.Lazy);
            }
            foreach (BarItemLink item in mapEditToolbar.ItemLinks)
            {
                if (item.Item is BarCheckItem)
                {
                    item.Item.ItemClick += BarItemClick;
                }
            }
            ChangeBarCheckItemsEnabled(false, "all");//初始状态下设置按钮不可用
        }

        /// <summary>
        /// 改变地图编辑工具栏工具的可用性
        /// </summary>
        /// <param name="status"></param>
        /// <param name="groupIndex"> BarCheckItem所在分组索引  取值范围为“all" 或"0" "1 " "2" "3 " ...
        /// 值为"all"时表示改变地图编辑工具栏所有BarCheckItem可用性
        /// 其它值表示仅仅改变该组BarCheckItem可用性
        /// </param>
        private void ChangeBarCheckItemsEnabled(bool status, string groupIndex)
        {
            bool blog = groupIndex.Equals("all");
            foreach (BarItemLink item in mapEditToolbar.ItemLinks)
            {
                if ((item.Item is BarStaticItem) || (item.Item is BarButtonItem))
                {
                    item.Item.Enabled = status;
                }
                else if (item.Item is BarSubItem && (!item.Item.Name.Contains("editSubItem")))
                {
                    item.Item.Enabled = status;
                }
                else if ((item.Item is BarCheckItem))
                {
                    if (blog)
                    { item.Item.Enabled = status; }
                    else
                    {
                        int index = Convert.ToInt32(groupIndex);
                        if (((BarCheckItem)item.Item).GroupIndex == index)
                        {
                            item.Item.Enabled = status;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 改变地图编辑工具栏ButtonChecked中的当前选择按钮
        /// </summary>
        /// <param name="ev"></param>
        public void ChangeActiveButton(ChangeActiveButtonEvent ev)
        {
            var mapViewer = Workbench.Instance.GetService<IMapViewer>();
            mainMapControl = mapViewer.MapControl;
            foreach (BarItemLink item in mapEditToolbar.ItemLinks)
            {
                BarCheckItem checkItem = item.Item as BarCheckItem;
                if (item.Item is BarCheckItem)
                {
                    checkItem.Checked = false;
                }
                if (item.Item is BarCheckItem && item.Item.Name.Equals(ev.BarItemName) && ev.EditFlag)
                {
                    checkItem.PerformClick();//代码触发鼠标点击事件
                    _currentBarItem = (BarCheckItem)item.Item;//设置为当前按钮
                }
            }
        }

        /// <summary>
        /// 修改地图编辑工具栏中特定名称的某个BarCheckItem是否选中
        /// </summary>
        /// <param name="ev"></param>
        public void ChangeButtonCheckedState(ChangeButtonCheckedStateEvent ev)
        {
            var mapViewer = Workbench.Instance.GetService<IMapViewer>();
            mainMapControl = mapViewer.MapControl;
            foreach (BarItemLink item in mapEditToolbar.ItemLinks)
            {
                BarCheckItem checkItem = item.Item as BarCheckItem;
                if (item.Item is BarCheckItem && item.Item.Name.Equals(ev.BarItemName))
                {
                    checkItem.Checked = ev.EditFlag;
                }
            }
        }
  
        /// <summary>
        /// 修改地图编辑工具栏中特定分组BarCheckItem的可用性
        /// </summary>
        /// <param name="ev"></param>
        public void ChangeButtonGroupEnable(ChangeBarCheckGroupEnableEvent ev)
        {
            foreach (BarItemLink item in mapEditToolbar.ItemLinks)
            {
                if ((item.Item is BarCheckItem) || ((BarCheckItem)(item.Item)).GroupIndex == ev.BarCheckGroupIndex)
                {
                    item.Item.Enabled = ev.Editable;
                }
            }
        }

        public void TargetLayerChanged(TargetLayerChangedEvent ev)
        {
            if (_engineEditor==null)
                return;

            if (_engineEditor.EditState == esriEngineEditState.esriEngineStateEditing)
            {
                _currentLayer = ev.TargerLayer;
                IEngineEditLayers editorLayer = (IEngineEditLayers)_engineEditor;
                editorLayer.SetTargetLayer(_currentLayer, 0);
            }
        }

        /// <summary>
        /// 显示查询结果总数
        /// </summary>
        private void ShowFeaturesCount()
        {
            int sum = 0;
            foreach (XtraTabPage pTabPage in TabControlFeatureList.TabPages)
            {
                int count;
                int.TryParse(pTabPage.Text.Substring(pTabPage.Text.LastIndexOf('(') + 1, pTabPage.Text.LastIndexOf(')') - pTabPage.Text.LastIndexOf('(') - 1), out count);
                sum += count;
            }
            dockPanelBufferResult.Text = @"查询结果(" + sum + @")";
        }

        private void BarItemClick(object sender, ItemClickEventArgs e)
        {
            ((BarCheckItem)(e.Item)).Checked = true;
            if (_currentBarItem != null)
            {
                ((BarCheckItem)(e.Item)).Checked = false;
                _currentBarItem.Checked = true;
            }
            _currentBarItem = null;
        }
        #endregion

        #region IEditer接口属性和方法
        public List<esriGeometryType> GeometryTypeList
        {
            get { return _geometryTypeList; }
            set { _geometryTypeList = value; }
        }

        public Timer SaveTimer
        {
            get { return _timer; }
        }

        public void StopEdit()
        {
            StopEditCommand cmd = new StopEditCommand();
            cmd.Execute(null);
        }

        public bool IsInEditing
        {
            get
            {
                IEngineEditor _engineEditor = new EngineEditorClass();
                if (_engineEditor.EditState == ESRI.ArcGIS.Controls.esriEngineEditState.esriEngineStateNotEditing)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion
      
        #region IMapViewer接口属性和方法

        DockManager IMapViewer.DockManager
        {
            get { return dockManager; }
        }
        AxMapControl IMapViewer.MapControl {
            get { return mainMapControl; }
        }
        AxTOCControl IMapViewer.TOCControl
        {
            get { return toccControl1.ATOCControl; }
        }
        //修改浏览工具栏可见性
        public bool IsBrowseToolbarVisible
        {
            get { return mapBrowseToolbar.Visible; }
            set { mapBrowseToolbar.Visible = value; }
        }
        //修改地图编辑工具栏可见性
        public bool IsEditorToolbarVisible
        {
            get {  return mapEditToolbar.Visible; }
            set { mapEditToolbar.Visible = value;
                #region 显示编辑工具栏的同时开启编辑
                if (value)
                {
                    StartEditCommand cmd = new StartEditCommand();
                    cmd.Execute(null);
                }
                #endregion
                }
        }
        //修改图层列表可见性
        public bool IsTocVisible
        {
            get { return dockPanelToc.Visibility == DockVisibility.Visible; }
            set
            {
                dockPanelToc.Visibility = value ? DockVisibility.Visible
                    : DockVisibility.Hidden;
            }
        }
        //修改鹰眼可见性
        public bool IsOverviewMapVisible
        {
            get
            {
                return dockPanelEagleEye.Visibility == DockVisibility.Visible;
            }
            set
            {
                dockPanelEagleEye.Visibility = value ? DockVisibility.Visible
                  : DockVisibility.Hidden;
                overrideMap1.IsOverviewMapVisible = value;
            }
        }
        //修改查询结果窗体可见性
        public bool IsQueryResultPanelVisible
        {
            get
            {
                return dockPanelBufferResult.Visibility == DockVisibility.Visible;
            }
            set
            {
              dockPanelBufferResult.Visibility = value ? 
                  DockVisibility.Visible
                        : DockVisibility.Hidden;
            }
        }
        //修改创建要素窗体可见性
        public bool IsCreateFeatuePanelVisible
        {

            get { return dockPanelCreateFeature.Visibility == DockVisibility.Visible; }
            set
            {
                var visiblelayers = LayerUtil.GetVisibleLayers(mainMapControl.Map);
                if (value == true && createFeatureControl1.LayerCount != visiblelayers.Count())
                {
                    createFeatureControl1.RefreshTreeListData();
                }
                dockPanelCreateFeature.Visibility = value ? DockVisibility.Visible
                    : DockVisibility.Hidden;
            }
        }



        /// <summary>
        /// 载入属性查询结果
        /// </summary>
        /// <param name="sourceLayer"></param>
        /// <param name="tableResult"></param>
        public void ShowQueryResult(IFeatureLayer sourceLayer, DataTable tableResult)
        {
            //是否是第一页，若是第一页则清空之前的所有结果
            TabControlFeatureList.TabPages.Clear();
            dockPanelBufferResult.Text = @"查询结果";
            if (sourceLayer == null || tableResult == null) return;

            MapSelectedFeatureListControl gv = new MapSelectedFeatureListControl
            {
                Dock = DockStyle.Fill,
                Name = sourceLayer.Name + "_" + _count
            };
            _count++;
            gv.Layer = sourceLayer;
            gv.GCFeatureList.DataSource = tableResult;
            gv.GVFeatureList.BestFitColumns();
            XtraTabPage pTabPage = new XtraTabPage();
            pTabPage.Name = sourceLayer.Name + "_" + _count; _count++;
            pTabPage.Text = sourceLayer.Name + @"(" + tableResult.Rows.Count + @")";
            pTabPage.Controls.Add(gv);
            TabControlFeatureList.TabPages.Add(pTabPage);
            ShowFeaturesCount();
            if (dockPanelBufferResult.Visibility != DockVisibility.Visible)
                dockPanelBufferResult.Visibility = DockVisibility.Visible;
        }
  
        /// <summary>
        /// 填充缓冲结果。
        /// </summary>
        /// <param name="index">当前页码。</param>
        /// <param name="sourceLayer">图层</param>
        /// <param name="tableResult">IFeature转换成的DataTable。</param>
        public void ShowQueryResult(int index, IFeatureLayer sourceLayer, DataTable tableResult)
        {
            if (index == 0) //是否是第一页，若是第一页则清空之前的所有结果
            {
                TabControlFeatureList.TabPages.Clear();
                dockPanelBufferResult.Text = @"查询结果（0）";
            }

            if (sourceLayer != null && tableResult != null)
            {
                string layerName = sourceLayer.Name;
                MapSelectedFeatureListControl gv = new MapSelectedFeatureListControl
                {
                    Dock = DockStyle.Fill,
                    Name = layerName + "_" + _count
                };
                _count++;
                gv.Layer = sourceLayer;
                gv.GCFeatureList.DataSource = tableResult;
                gv.GVFeatureList.BestFitColumns();
                XtraTabPage pTabPage = new XtraTabPage {Name = layerName + "_" + _count};
                _count++;
                pTabPage.Text = layerName +@"(" + tableResult.Rows.Count + @")";
                pTabPage.Controls.Add(gv);
                TabControlFeatureList.TabPages.Add(pTabPage);
                ShowFeaturesCount();
                if (dockPanelBufferResult.Visibility != DockVisibility.Visible)
                    dockPanelBufferResult.Visibility = DockVisibility.Visible;
            }
        }

        public void ShowQueryResult(int index, IFeatureLayer sourceLayer)
        {
            if (index == 0) //是否是第一页，若是第一页则清空之前的所有结果
            {
                TabControlFeatureList.TabPages.Clear();
                dockPanelBufferResult.Text = @"查询结果";
            }

            if (sourceLayer != null)
            {
                string layerName = sourceLayer.Name;
                MapSelectedFeatureListControl3 gv =
                    new MapSelectedFeatureListControl3(sourceLayer)
                    {
                        Dock = DockStyle.Fill,
                        Name = layerName + "_" + _count
                    };
                _count++;
                gv.Layer = sourceLayer;
                XtraTabPage pTabPage = new XtraTabPage {Name = layerName + "_" + _count};
                _count++;
                pTabPage.Text = layerName + @"(" + (sourceLayer as IFeatureSelection).SelectionSet.Count +@")";
                pTabPage.Controls.Add(gv);
                TabControlFeatureList.TabPages.Add(pTabPage);
                ShowFeaturesCount();
                if (dockPanelBufferResult.Visibility != DockVisibility.Visible)
                {
                    dockPanelBufferResult.Visibility = DockVisibility.Visible;//此操作会引起地图刷新
                }
                else
                {
                    mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, mainMapControl.Extent);
                }
            }
        }

        public void ClearSpatialQueryResult()
        {
           
            if (dockPanelBufferResult.Visibility == DockVisibility.Visible)
            {
                TabControlFeatureList.TabPages.Clear();
                dockPanelBufferResult.Text = @"查询结果（0）";
            }
        }
        #endregion

        #region 地图浏览工具栏按钮事件
        private void barCheckSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            var command = new SelectFeaturesCommand();
            command.SelectFeatures();
        }

        private void barBtnClearSelection_ItemClick(object sender, ItemClickEventArgs e)
        {
            var command = new ClearSelectionAndElementsCommand();
            command.ClearSelectionAndElements();
        }

        private void barCheckZoomIn_ItemClick(object sender, ItemClickEventArgs e)
        {
            MapZoomInCommand command = new MapZoomInCommand();
            command.ZoomIn();
        }

        private void barCheckZoomOut_ItemClick(object sender, ItemClickEventArgs e)
        {
            MapZoomOutCommand command = new MapZoomOutCommand();
            command.ZoomOut();
        }

        private void barCheckPan_ItemClick(object sender, ItemClickEventArgs e)
        {
            MapPanCommand command = new MapPanCommand();
            command.Pan();
        }

        private void barBtnFullExtent_ItemClick(object sender, ItemClickEventArgs e)
        {
            var mapFullExtentCommand = new MapFullExtentCommand();
            mapFullExtentCommand.ZoomToFullExtent();
        }

        private void barBtnPreView_ItemClick(object sender, ItemClickEventArgs e)
        {
            MapZoomToLastExtentBackCommand command = new MapZoomToLastExtentBackCommand();
            command.ZoomToLastExtentBack();
        }

        private void barBtnNextViewer_ItemClick(object sender, ItemClickEventArgs e)
        {
            MapZoomToLastExtentForwardCommand command = new MapZoomToLastExtentForwardCommand();
            command.ZoomToLastExtentForward();
        }

        private void barCheckIdentify_ItemClick(object sender, ItemClickEventArgs e)
        {
            CommandIdentifyTool pIdentify = new CommandIdentifyTool();
            ICommand cmd = pIdentify;
            cmd.OnCreate(mainMapControl.Object);
            cmd.OnClick();
            ITool tool = (ITool)cmd;
            mainMapControl.CurrentTool = tool;
        }

        private void barCheckMeasurement_ItemClick(object sender, ItemClickEventArgs e)
        {
             var cmd = new MapMeasureCommand();
             cmd.ShowMeasureToolForm();
        }

        private void barEditScale_EditValueChanged(object sender, EventArgs e)
        {
            ScaleItem item = barEditScale.EditValue as ScaleItem;
            if (item != null)
            {
                //地图按选择比例尺显示
                mainMapControl.MapScale = item.Scale;
                mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, mainMapControl.Extent);
            }
        }
        #endregion
        
        #region 窗体事件
        private void MapViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                IEngineEditor engineEditor = new EngineEditorClass();
                if (engineEditor.EditState == esriEngineEditState.esriEngineStateEditing)
                {
                    CmdStopEdit cmdStopEdit = new CmdStopEdit();
                    cmdStopEdit.OnCreate(mainMapControl.Object);
                    cmdStopEdit.OnClick();
                    if (cmdStopEdit.CancleFlag)
                    {
                        e.Cancel = true;
                    }
                }
                mainMapControl.Map.ClearLayers();

            }
            catch (Exception ex)
            {
                LogManager.GetLogger(GetType()).Error(ex);
            }
        }     
        private void MapMainControl_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            var args = new MapLocationChangedEventArgs(e.x, e.y, e.mapX, e.mapY, mainMapControl, mainMapControl.Map);
            lblCoord.Text = string.Format("X:{0:F3} Y:{1:F3}", e.mapX, e.mapY);
            OnCurrentLocationChanged(args);
            _eventAggregator.Publish(args);
        }

        private void MapMainControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            #region 控制右键菜单
            bool startEditflag = _engineEditor.EditState == esriEngineEditState.esriEngineStateEditing;
            //当前工具模式下不弹出右键菜单
            bool isTool = !(mainMapControl.CurrentTool is TlwSketchCommonTool) &&
                          !(mainMapControl.CurrentTool is MiddlePointOfLineTool) &&
                          !(mainMapControl.CurrentTool is SketchTool_TwoCircularitiesIntersectPoint) &&
                          !(mainMapControl.CurrentTool is RotationFeatureTool) &&
                          !(mainMapControl.CurrentTool is TraceSketchTool)&&
                          !(mainMapControl.CurrentTool is GapFillTool);
            if (e.button == 2 && mainMapControl.CurrentTool is GapFillTool)
            {
                    EditSketchContextMenu.GetInstance(mainMapControl.Object, ContextMenuType.FillGapToolMenus.ToString()).ToolContextMenu.PopupMenu(e.x, e.y, mainMapControl.ActiveView.ScreenDisplay.hWnd);
            }
            if (e.button == 2 && startEditflag && isTool)
            {
                TlwEditCommonTool tool = TlwEditCommonTool.Instance;// _EditFeature;
                if (tool.IsSketchMode)
                {
                    EditSketchContextMenu.GetInstance(mainMapControl.Object, ContextMenuType.VertexEditContextMenus.ToString()).ToolContextMenu.PopupMenu(e.x, e.y, mainMapControl.ActiveView.ScreenDisplay.hWnd);
                }
                else
                    EditSketchContextMenu.GetInstance(mainMapControl.Object, ContextMenuType.DefaultEditContextMenus.ToString()).ToolContextMenu.PopupMenu(e.x, e.y, mainMapControl.ActiveView.ScreenDisplay.hWnd);
            }

            #endregion


            if (e.button == 4)
            {
                mainMapControl.MousePointer = esriControlsMousePointer.esriPointerPanning;
                mainMapControl.Pan();
                mainMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
            }
        }

        private void MapMainControl_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            if (SetMainMapScale != null)
            {
                SetMainMapScale(mainMapControl.MapScale);
            }
            UpdateBarEditScaleText();
          
        }

        private void MapMainControl_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            if (SetMainMapScale != null)
            {
                SetMainMapScale(mainMapControl.MapScale);
            }
            UpdateBarEditScaleText();
        }
        
       
        private void mainMapControl_OnViewRefreshed(object sender, IMapControlEvents2_OnViewRefreshedEvent e)
        {
            var visiblelayers = LayerUtil.GetVisibleLayers(mainMapControl.Map);
            var nowvisiblecount = visiblelayers.Count();
            if (nowvisiblecount != _visibleLayerCount)//可见图层变化重新加载目标图层
            {
                createFeatureControl1.RefreshTreeListData();
            }
            if (nowvisiblecount == 0)
            {
                TargetLayerChanged(new TargetLayerChangedEvent(null));
            }
            _visibleLayerCount = nowvisiblecount;

        }
        private void mainMapControl_OnAfterDraw(object sender, IMapControlEvents2_OnAfterDrawEvent e)
        {
            //图层数为空或者所有图层均为空图层时停止编辑
            if (this.mainMapControl.LayerCount == 0)
            {
                StopEditCommand cmd = new StopEditCommand();
                cmd.Execute(null);
            }
        }

        private void mainMapControl_OnSelectionChanged(object sender, EventArgs e)
        {
           // if (EditingAttributeForm.Instance.Visible)
           // {
           //     EditingAttributeForm.Instance.ReFreshData();
           // }
            var eventAggreator = ContainerManager.GetContainer().Resolve<IEventAggregator>();
            eventAggreator.Publish(new SelectFeatureEvent(null));//发布选择要素事件
            //AttributeEditCommand cmd = new AttributeEditCommand();
            //cmd.ShowFeatureAttribute();
        }
        /// <summary>
        /// 导出查询结果按钮事件
        /// </summary>
        private void dockPanelBufferResult_CustomButtonClick(object sender, ButtonEventArgs e)
        {
            List<IFeatureLayer> layerList = new List<IFeatureLayer>();
            for (int i = 0; i < TabControlFeatureList.TabPages.Count; i++)
            {
                IFeatureLayer layer = ((TabControlFeatureList.TabPages[i].Controls[0]) as MapSelectedFeatureListControl3).Layer;
                if ((layer as IFeatureSelection).SelectionSet.Count > 1)
                {
                    layerList.Add(layer);
                }
            }
            try
            {
                if (layerList.Count < 1)
                {
                    UIMessageUtil.ShowError("导出失败,请检查地图上是否已经清除了查询结果。");
                    return;
                }
                ExportFeatureData a = new ExportFeatureData(layerList);
                a.StartPosition = FormStartPosition.CenterScreen;
                a.Show();
            }
            catch (Exception ex)
            {
                UIMessageUtil.ShowError("导出失败");
                LoggingManager.GetLogger(GetType()).Error("Layer 已经释放", ex);
            }
        }

        private void OnCurrentLocationChanged(MapLocationChangedEventArgs args)
        {
            var handler = CurrentLocationChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void dockPanelToc_CustomButtonClick(object sender, ButtonEventArgs e)
        {

            if (e.Button.Properties.Tag.ToString() == "btnLayerSelectSetting")
            {
                LayerSelectSettingForm frm = new LayerSelectSettingForm();
                frm.Init(this.mainMapControl.Map);
                frm.ShowDialog();
            }
        }

        #endregion

    }
}