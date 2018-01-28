using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Utils.MVVM;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using ESRI.ArcGIS.Controls;
using Teleware.DataCenter.MapViewer.Controls;

namespace Teleware.DataCenter.MapViewer.Forms
{
    partial class MapViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapViewerForm));
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanelEagleEye = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanelToc = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer3 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanelBufferResult = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.TabControlFeatureList = new DevExpress.XtraTab.XtraTabControl();
            this.dockPanelCreateFeature = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.mapEditToolbar = new DevExpress.XtraBars.Bar();
            this.mapBrowseToolbar = new DevExpress.XtraBars.Bar();
            this.barCheckZoomIn = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckZoomOut = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckPan = new DevExpress.XtraBars.BarCheckItem();
            this.barBtnFullExtent = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPreViewer = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnNextViewer = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckSelect = new DevExpress.XtraBars.BarCheckItem();
            this.barBtnClearSelection = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckIdentify = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckMeasurement = new DevExpress.XtraBars.BarCheckItem();
            this.barStaticItem3 = new DevExpress.XtraBars.BarStaticItem();
            this.barEditScale = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblCoord = new DevExpress.XtraEditors.LabelControl();
            this.mainMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnZoomout = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPan = new DevExpress.XtraBars.BarButtonItem();
            this.barListItem1 = new DevExpress.XtraBars.BarListItem();
            this.barToggleSwitchItem1 = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.dockPanelEagleEye.SuspendLayout();
            this.dockPanelToc.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanelBufferResult.SuspendLayout();
            this.controlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabControlFeatureList)).BeginInit();
            this.dockPanelCreateFeature.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainMapControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager
            // 
            this.dockManager.DockingOptions.DockPanelInCaptionRegion = DevExpress.Utils.DefaultBoolean.False;
            this.dockManager.Form = this;
            this.dockManager.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelEagleEye,
            this.dockPanel1,
            this.dockPanelBufferResult,
            this.dockPanelCreateFeature});
            this.dockManager.MenuManager = this.barManager1;
            this.dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelToc});
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane"});
            // 
            // dockPanelEagleEye
            // 
            this.dockPanelEagleEye.Controls.Add(this.dockPanel2_Container);
            this.dockPanelEagleEye.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelEagleEye.ID = new System.Guid("39f41788-3f8b-4029-90b1-08fc50d32c67");
            this.dockPanelEagleEye.Location = new System.Drawing.Point(0, 285);
            this.dockPanelEagleEye.Name = "dockPanelEagleEye";
            this.dockPanelEagleEye.OriginalSize = new System.Drawing.Size(200, 208);
            this.dockPanelEagleEye.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelEagleEye.SavedIndex = 1;
            this.dockPanelEagleEye.SavedParent = this.dockPanelToc;
            this.dockPanelEagleEye.Size = new System.Drawing.Size(229, 285);
            this.dockPanelEagleEye.Text = "鹰眼";
            this.dockPanelEagleEye.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(200, 100);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // dockPanelToc
            // 
            this.dockPanelToc.Controls.Add(this.dockPanel1_Container);
            this.dockPanelToc.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("Button", null, -1, DevExpress.XtraBars.Docking2010.HorizontalImageLocation.Default, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", false, -1, true, null, true, false, true, serializableAppearanceObject1, null, "btnLayerSelectSetting", -1)});
            this.dockPanelToc.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelToc.FloatVertical = true;
            this.dockPanelToc.ID = new System.Guid("81c85936-216e-4dd1-9303-bb5b309043e3");
            this.dockPanelToc.Location = new System.Drawing.Point(0, 0);
            this.dockPanelToc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dockPanelToc.Name = "dockPanelToc";
            this.dockPanelToc.OriginalSize = new System.Drawing.Size(257, 429);
            this.dockPanelToc.Size = new System.Drawing.Size(257, 733);
            this.dockPanelToc.Text = "图层列表";
            this.dockPanelToc.CustomButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.dockPanelToc_CustomButtonClick);
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(5, 27);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(247, 701);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.controlContainer3);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            this.dockPanel1.FloatLocation = new System.Drawing.Point(277, 246);
            this.dockPanel1.FloatSize = new System.Drawing.Size(596, 200);
            this.dockPanel1.ID = new System.Guid("301f732b-3a37-428b-afcc-606de65de151");
            this.dockPanel1.Location = new System.Drawing.Point(-32768, -32768);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel1.SavedIndex = 1;
            this.dockPanel1.Size = new System.Drawing.Size(596, 200);
            this.dockPanel1.Text = "dockPanel1";
            this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // controlContainer3
            // 
            this.controlContainer3.Location = new System.Drawing.Point(3, 22);
            this.controlContainer3.Name = "controlContainer3";
            this.controlContainer3.Size = new System.Drawing.Size(590, 175);
            this.controlContainer3.TabIndex = 0;
            // 
            // dockPanelBufferResult
            // 
            this.dockPanelBufferResult.Controls.Add(this.controlContainer2);
            serializableAppearanceObject2.ForeColor = System.Drawing.Color.White;
            serializableAppearanceObject2.Options.UseForeColor = true;
            this.dockPanelBufferResult.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("导出", null, -1, DevExpress.XtraBars.Docking2010.HorizontalImageLocation.Default, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", true, -1, true, null, true, false, true, serializableAppearanceObject2, null, null, -1)});
            this.dockPanelBufferResult.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanelBufferResult.FloatVertical = true;
            this.dockPanelBufferResult.ID = new System.Guid("81884868-d570-4ac5-94c9-2a6552a8e4ce");
            this.dockPanelBufferResult.Location = new System.Drawing.Point(229, 327);
            this.dockPanelBufferResult.Name = "dockPanelBufferResult";
            this.dockPanelBufferResult.OriginalSize = new System.Drawing.Size(200, 243);
            this.dockPanelBufferResult.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanelBufferResult.SavedIndex = 1;
            this.dockPanelBufferResult.Size = new System.Drawing.Size(1083, 243);
            this.dockPanelBufferResult.Text = "查询结果";
            this.dockPanelBufferResult.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            this.dockPanelBufferResult.CustomButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.dockPanelBufferResult_CustomButtonClick);
            // 
            // controlContainer2
            // 
            this.controlContainer2.Controls.Add(this.TabControlFeatureList);
            this.controlContainer2.Location = new System.Drawing.Point(4, 26);
            this.controlContainer2.Name = "controlContainer2";
            this.controlContainer2.Size = new System.Drawing.Size(1075, 213);
            this.controlContainer2.TabIndex = 0;
            // 
            // TabControlFeatureList
            // 
            this.TabControlFeatureList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControlFeatureList.Location = new System.Drawing.Point(0, 0);
            this.TabControlFeatureList.Name = "TabControlFeatureList";
            this.TabControlFeatureList.Size = new System.Drawing.Size(1075, 213);
            this.TabControlFeatureList.TabIndex = 0;
            // 
            // dockPanelCreateFeature
            // 
            this.dockPanelCreateFeature.Controls.Add(this.controlContainer1);
            this.dockPanelCreateFeature.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanelCreateFeature.ID = new System.Guid("faf46edc-001a-4d8d-a180-92b1ed065417");
            this.dockPanelCreateFeature.Location = new System.Drawing.Point(1057, 0);
            this.dockPanelCreateFeature.Name = "dockPanelCreateFeature";
            this.dockPanelCreateFeature.OriginalSize = new System.Drawing.Size(255, 200);
            this.dockPanelCreateFeature.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanelCreateFeature.SavedIndex = 0;
            this.dockPanelCreateFeature.Size = new System.Drawing.Size(255, 570);
            this.dockPanelCreateFeature.Text = "创建要素";
            this.dockPanelCreateFeature.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // controlContainer1
            // 
            this.controlContainer1.Location = new System.Drawing.Point(4, 23);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(247, 543);
            this.controlContainer1.TabIndex = 0;
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.mapEditToolbar,
            this.mapBrowseToolbar});
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager;
            this.barManager1.Form = this.panelControl1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barEditItem1,
            this.barCheckItem1,
            this.barButtonItem3,
            this.barBtnZoomout,
            this.barBtnPan,
            this.barBtnFullExtent,
            this.barBtnPreViewer,
            this.barBtnNextViewer,
            this.barStaticItem3,
            this.barListItem1,
            this.barEditScale,
            this.barCheckZoomIn,
            this.barCheckZoomOut,
            this.barCheckPan,
            this.barToggleSwitchItem1,
            this.barCheckSelect,
            this.barBtnClearSelection,
            this.barCheckIdentify,
            this.barCheckMeasurement,
            this.barButtonItem4,
            this.barButtonItem5});
            this.barManager1.MaxItemId = 72;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemComboBox3});
            // 
            // mapEditToolbar
            // 
            this.mapEditToolbar.BarName = "mapEditToolbar";
            this.mapEditToolbar.DockCol = 0;
            this.mapEditToolbar.DockRow = 0;
            this.mapEditToolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.mapEditToolbar.FloatLocation = new System.Drawing.Point(563, 216);
            this.mapEditToolbar.OptionsBar.AllowQuickCustomization = false;
            this.mapEditToolbar.OptionsBar.DisableCustomization = true;
            this.mapEditToolbar.OptionsBar.DrawDragBorder = false;
            this.mapEditToolbar.OptionsBar.DrawSizeGrip = true;
            this.mapEditToolbar.OptionsBar.RotateWhenVertical = false;
            this.mapEditToolbar.OptionsBar.UseWholeRow = true;
            this.mapEditToolbar.Text = "编辑工具栏";
            this.mapEditToolbar.Visible = false;
            // 
            // mapBrowseToolbar
            // 
            this.mapBrowseToolbar.BarName = "mapBrowseToolbar";
            this.mapBrowseToolbar.DockCol = 0;
            this.mapBrowseToolbar.DockRow = 1;
            this.mapBrowseToolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.mapBrowseToolbar.FloatLocation = new System.Drawing.Point(334, 171);
            this.mapBrowseToolbar.FloatSize = new System.Drawing.Size(344, 31);
            this.mapBrowseToolbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckZoomIn),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckZoomOut),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckPan),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnFullExtent),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPreViewer),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnNextViewer),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckSelect),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnClearSelection),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckIdentify),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckMeasurement),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barStaticItem3, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditScale)});
            this.mapBrowseToolbar.OptionsBar.UseWholeRow = true;
            this.mapBrowseToolbar.Text = "地图工具";
            // 
            // barCheckZoomIn
            // 
            this.barCheckZoomIn.Id = 65;
            this.barCheckZoomIn.Name = "barCheckZoomIn";
            // 
            // barCheckZoomOut
            // 
            this.barCheckZoomOut.Id = 66;
            this.barCheckZoomOut.Name = "barCheckZoomOut";
            // 
            // barCheckPan
            // 
            this.barCheckPan.Id = 67;
            this.barCheckPan.Name = "barCheckPan";
            // 
            // barBtnFullExtent
            // 
            this.barBtnFullExtent.Id = 62;
            this.barBtnFullExtent.Name = "barBtnFullExtent";
            // 
            // barBtnPreViewer
            // 
            this.barBtnPreViewer.Id = 63;
            this.barBtnPreViewer.Name = "barBtnPreViewer";
            // 
            // barBtnNextViewer
            // 
            this.barBtnNextViewer.Id = 64;
            this.barBtnNextViewer.Name = "barBtnNextViewer";
            // 
            // barCheckSelect
            // 
            this.barCheckSelect.Id = 68;
            this.barCheckSelect.Name = "barCheckSelect";
            // 
            // barBtnClearSelection
            // 
            this.barBtnClearSelection.Id = 69;
            this.barBtnClearSelection.Name = "barBtnClearSelection";
            // 
            // barCheckIdentify
            // 
            this.barCheckIdentify.Id = 70;
            this.barCheckIdentify.Name = "barCheckIdentify";
            // 
            // barCheckMeasurement
            // 
            this.barCheckMeasurement.Id = 71;
            this.barCheckMeasurement.Name = "barCheckMeasurement";
            // 
            // barStaticItem3
            // 
            this.barStaticItem3.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItem3.Caption = "地图比例:";
            this.barStaticItem3.Id = 39;
            this.barStaticItem3.Name = "barStaticItem3";
            this.barStaticItem3.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barEditScale
            // 
            this.barEditScale.Caption = "比例";
            this.barEditScale.Edit = this.repositoryItemComboBox3;
            this.barEditScale.Id = 41;
            this.barEditScale.Name = "barEditScale";
            this.barEditScale.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            this.barEditScale.Width = 100;
            this.barEditScale.EditValueChanged += new System.EventHandler(this.barEditScale_EditValueChanged);
            // 
            // repositoryItemComboBox3
            // 
            this.repositoryItemComboBox3.AutoHeight = false;
            this.repositoryItemComboBox3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox3.Name = "repositoryItemComboBox3";
            this.repositoryItemComboBox3.NullText = "1：";
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            this.barAndDockingController1.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.barAndDockingController1.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.barDockControlTop.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.barDockControlTop.Appearance.Options.UseBackColor = true;
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(1238, 67);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 731);
            this.barDockControlBottom.Size = new System.Drawing.Size(1238, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 69);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 662);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1240, 69);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 662);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblCoord);
            this.panelControl1.Controls.Add(this.mainMapControl);
            this.panelControl1.Controls.Add(this.barDockControlLeft);
            this.panelControl1.Controls.Add(this.barDockControlRight);
            this.panelControl1.Controls.Add(this.barDockControlBottom);
            this.panelControl1.Controls.Add(this.barDockControlTop);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(257, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1242, 733);
            this.panelControl1.TabIndex = 4;
            // 
            // lblCoord
            // 
            this.lblCoord.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblCoord.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblCoord.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblCoord.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCoord.Location = new System.Drawing.Point(2, 713);
            this.lblCoord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblCoord.Name = "lblCoord";
            this.lblCoord.Size = new System.Drawing.Size(29, 18);
            this.lblCoord.TabIndex = 11;
            this.lblCoord.Text = "X:Y:";
            // 
            // mainMapControl
            // 
            this.mainMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMapControl.Location = new System.Drawing.Point(2, 69);
            this.mainMapControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainMapControl.Name = "mainMapControl";
            this.mainMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mainMapControl.OcxState")));
            this.mainMapControl.Size = new System.Drawing.Size(1238, 662);
            this.mainMapControl.TabIndex = 16;
            this.mainMapControl.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.MapMainControl_OnMouseDown);
            this.mainMapControl.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.MapMainControl_OnMouseMove);
            this.mainMapControl.OnSelectionChanged += new System.EventHandler(this.mainMapControl_OnSelectionChanged);
            this.mainMapControl.OnViewRefreshed += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnViewRefreshedEventHandler(this.mainMapControl_OnViewRefreshed);
            this.mainMapControl.OnAfterDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterDrawEventHandler(this.mainMapControl_OnAfterDraw);
            this.mainMapControl.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.MapMainControl_OnExtentUpdated);
            this.mainMapControl.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.MapMainControl_OnMapReplaced);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 5;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "barEditItem1";
            this.barEditItem1.Edit = this.repositoryItemTextEdit1;
            this.barEditItem1.Id = 13;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Caption = "barCheckItem1";
            this.barCheckItem1.Id = 15;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 31;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barBtnZoomout
            // 
            this.barBtnZoomout.Id = 60;
            this.barBtnZoomout.Name = "barBtnZoomout";
            // 
            // barBtnPan
            // 
            this.barBtnPan.Id = 61;
            this.barBtnPan.Name = "barBtnPan";
            // 
            // barListItem1
            // 
            this.barListItem1.Caption = "barListItem1";
            this.barListItem1.Id = 40;
            this.barListItem1.Name = "barListItem1";
            // 
            // barToggleSwitchItem1
            // 
            this.barToggleSwitchItem1.Caption = "barToggleSwitchItem1";
            this.barToggleSwitchItem1.Id = 45;
            this.barToggleSwitchItem1.Name = "barToggleSwitchItem1";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Id = 58;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Id = 59;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // MapViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1499, 733);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.dockPanelToc);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MapViewerForm";
            this.Text = "地图";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapViewerForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.dockPanelEagleEye.ResumeLayout(false);
            this.dockPanelToc.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanelBufferResult.ResumeLayout(false);
            this.controlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TabControlFeatureList)).EndInit();
            this.dockPanelCreateFeature.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainMapControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DockManager dockManager;
        private DockPanel dockPanelToc;
        private ControlContainer dockPanel1_Container;
        private DockPanel dockPanelEagleEye;
        private ControlContainer dockPanel2_Container;
        private PanelControl panelControl1;
       
        private DockPanel dockPanelBufferResult;
        private ControlContainer controlContainer2;
        private XtraTabControl TabControlFeatureList;
        private BarAndDockingController barAndDockingController1;
        private BarManager barManager1;
        private BarDockControl barDockControlTop;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarButtonItem barButtonItem1;
        private BarButtonItem barButtonItem2;
        private BarEditItem barEditItem1;
        private RepositoryItemTextEdit repositoryItemTextEdit1;
        private BarCheckItem barCheckItem1;
        private BarButtonItem barButtonItem3;
        private DockPanel dockPanel1;
        private ControlContainer controlContainer3;
        private Bar mapBrowseToolbar;
        private BarButtonItem barBtnZoomout;
        private BarButtonItem barBtnPan;
        private BarButtonItem barBtnFullExtent;
        private BarButtonItem barBtnPreViewer;
        private BarButtonItem barBtnNextViewer;
        private BarCheckItem barCheckZoomIn;
        private BarCheckItem barCheckZoomOut;
        private BarCheckItem barCheckPan;
        private BarStaticItem barStaticItem3;
        private BarEditItem barEditScale;
        private RepositoryItemComboBox repositoryItemComboBox3;
        private BarListItem barListItem1;
        private BarToggleSwitchItem barToggleSwitchItem1;
        private BarCheckItem barCheckSelect;
        private BarButtonItem barBtnClearSelection;
        private BarCheckItem barCheckIdentify;
        private BarCheckItem barCheckMeasurement;
        private Bar mapEditToolbar;
        private LabelControl lblCoord;
        private AxMapControl mainMapControl;
        private DockPanel dockPanelCreateFeature;
        private ControlContainer controlContainer1;
        private BarButtonItem barButtonItem4;
        private BarButtonItem barButtonItem5;
    }
}