namespace Clong.BaseEngine.TApplication.MainForm
{
    partial class MapViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapViewForm));
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barCheckZoomIn = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckZoomout = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckItem2 = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonFullExtent = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonPreView = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonNextView = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckSelect = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonClearSelect = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckIdentify = new DevExpress.XtraBars.BarCheckItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.mainMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.toccControl1 = new Teleware.DataCenter.MapViewer.Controls.TOCCControl();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainMapControl)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Controller = this.barAndDockingController1;
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
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
            // barAndDockingController1
            // 
            this.barAndDockingController1.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.barAndDockingController1.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this.panelControl1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barCheckZoomIn,
            this.barCheckZoomout,
            this.barCheckItem2,
            this.barCheckSelect,
            this.barButtonFullExtent,
            this.barButtonPreView,
            this.barButtonNextView,
            this.barButtonClearSelect,
            this.barCheckIdentify});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 9;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(556, 204);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckZoomIn),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckZoomout),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonFullExtent),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonPreView),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonNextView),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckSelect),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonClearSelect),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckIdentify)});
            this.bar1.Text = "Tools";
            // 
            // barCheckZoomIn
            // 
            this.barCheckZoomIn.Caption = "barCheckZoomIn";
            this.barCheckZoomIn.Glyph = global::Clong.BaseEngine.TApplication.Properties.Resources._800;
            this.barCheckZoomIn.GroupIndex = 1;
            this.barCheckZoomIn.Id = 0;
            this.barCheckZoomIn.Name = "barCheckZoomIn";
            this.barCheckZoomIn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckZoomIn_ItemClick);
            // 
            // barCheckZoomout
            // 
            this.barCheckZoomout.Caption = "barCheckZoomout";
            this.barCheckZoomout.Glyph = global::Clong.BaseEngine.TApplication.Properties.Resources._802;
            this.barCheckZoomout.GroupIndex = 1;
            this.barCheckZoomout.Id = 1;
            this.barCheckZoomout.Name = "barCheckZoomout";
            this.barCheckZoomout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckZoomout_ItemClick);
            // 
            // barCheckItem2
            // 
            this.barCheckItem2.Caption = "barCheckPan";
            this.barCheckItem2.Glyph = global::Clong.BaseEngine.TApplication.Properties.Resources._804;
            this.barCheckItem2.GroupIndex = 1;
            this.barCheckItem2.Id = 2;
            this.barCheckItem2.Name = "barCheckItem2";
            this.barCheckItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem2_ItemClick);
            // 
            // barButtonFullExtent
            // 
            this.barButtonFullExtent.Caption = "barButtonFullExtent";
            this.barButtonFullExtent.Glyph = global::Clong.BaseEngine.TApplication.Properties.Resources._806;
            this.barButtonFullExtent.Id = 4;
            this.barButtonFullExtent.Name = "barButtonFullExtent";
            this.barButtonFullExtent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonFullExtent_ItemClick);
            // 
            // barButtonPreView
            // 
            this.barButtonPreView.Caption = "barButtonPreView";
            this.barButtonPreView.Glyph = global::Clong.BaseEngine.TApplication.Properties.Resources._808;
            this.barButtonPreView.Id = 5;
            this.barButtonPreView.Name = "barButtonPreView";
            this.barButtonPreView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonPreView_ItemClick);
            // 
            // barButtonNextView
            // 
            this.barButtonNextView.Caption = "barButtonNextView";
            this.barButtonNextView.Glyph = global::Clong.BaseEngine.TApplication.Properties.Resources._810;
            this.barButtonNextView.Id = 6;
            this.barButtonNextView.Name = "barButtonNextView";
            this.barButtonNextView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonNextView_ItemClick);
            // 
            // barCheckSelect
            // 
            this.barCheckSelect.Caption = "barCheckSelect";
            this.barCheckSelect.Glyph = global::Clong.BaseEngine.TApplication.Properties.Resources.ElementSelectTool161;
            this.barCheckSelect.GroupIndex = 1;
            this.barCheckSelect.Id = 3;
            this.barCheckSelect.Name = "barCheckSelect";
            this.barCheckSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckSelect_ItemClick);
            // 
            // barButtonClearSelect
            // 
            this.barButtonClearSelect.Caption = "barButtonClearSelect";
            this.barButtonClearSelect.Glyph = global::Clong.BaseEngine.TApplication.Properties.Resources._228;
            this.barButtonClearSelect.Id = 7;
            this.barButtonClearSelect.Name = "barButtonClearSelect";
            this.barButtonClearSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonClearSelect_ItemClick);
            // 
            // barCheckIdentify
            // 
            this.barCheckIdentify.Caption = "barCheckIdentify";
            this.barCheckIdentify.Glyph = global::Clong.BaseEngine.TApplication.Properties.Resources.IdentifyTool16;
            this.barCheckIdentify.GroupIndex = 1;
            this.barCheckIdentify.Id = 8;
            this.barCheckIdentify.Name = "barCheckIdentify";
            this.barCheckIdentify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckIdentify_ItemClick);
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(631, 212);
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(877, 57);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 494);
            this.barDockControlBottom.Size = new System.Drawing.Size(877, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 59);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 435);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(879, 59);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 435);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.mainMapControl);
            this.panelControl1.Controls.Add(this.barDockControlLeft);
            this.panelControl1.Controls.Add(this.barDockControlRight);
            this.panelControl1.Controls.Add(this.barDockControlBottom);
            this.panelControl1.Controls.Add(this.barDockControlTop);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(252, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(881, 519);
            this.panelControl1.TabIndex = 23;
            // 
            // mainMapControl
            // 
            this.mainMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMapControl.Location = new System.Drawing.Point(2, 59);
            this.mainMapControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainMapControl.Name = "mainMapControl";
            this.mainMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mainMapControl.OcxState")));
            this.mainMapControl.Size = new System.Drawing.Size(877, 435);
            this.mainMapControl.TabIndex = 18;
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("08190266-daf0-4167-9cff-4d4d80776618");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(252, 200);
            this.dockPanel1.Size = new System.Drawing.Size(252, 519);
            this.dockPanel1.Text = "dockPanel1";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.toccControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(5, 27);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(242, 487);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // toccControl1
            // 
            this.toccControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toccControl1.DockManager = null;
            this.toccControl1.Location = new System.Drawing.Point(0, 0);
            this.toccControl1.Name = "toccControl1";
            this.toccControl1.Size = new System.Drawing.Size(242, 487);
            this.toccControl1.TabIndex = 0;
            // 
            // MapViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 519);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.dockPanel1);
            this.Name = "MapViewForm";
            this.Text = "MapViewForm";
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainMapControl)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private ESRI.ArcGIS.Controls.AxMapControl mainMapControl;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Teleware.DataCenter.MapViewer.Controls.TOCCControl toccControl1;
        private DevExpress.XtraBars.BarCheckItem barCheckZoomIn;
        private DevExpress.XtraBars.BarCheckItem barCheckZoomout;
        private DevExpress.XtraBars.BarCheckItem barCheckItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonFullExtent;
        private DevExpress.XtraBars.BarCheckItem barCheckSelect;
        private DevExpress.XtraBars.BarButtonItem barButtonPreView;
        private DevExpress.XtraBars.BarButtonItem barButtonNextView;
        private DevExpress.XtraBars.BarButtonItem barButtonClearSelect;
        private DevExpress.XtraBars.BarCheckItem barCheckIdentify;
    }
}