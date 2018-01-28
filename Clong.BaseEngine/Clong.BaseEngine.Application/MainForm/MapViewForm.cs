using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace Clong.BaseEngine.TApplication.MainForm
{
    public partial class MapViewForm : DevExpress.XtraEditors.XtraForm
    {
        public MapViewForm()
        {
            InitializeComponent();
            BindToMap();
        }

        private void BindToMap()
        {
            mainMapControl.ActiveView.FocusMap.Name = "地图";
            toccControl1.DockManager = dockManager1;
            toccControl1.Init(mainMapControl.Object as IMapControl4);
        }
        public IMap MainMap
        {
            get { return this.mainMapControl.Map; }
        }

        public AxMapControl MapControl
        {
            get { return this.mainMapControl; }
        }

        public DockManager DockManager
        {
            get { return this.dockManager1; }
        }

        #region 浏览常用工具
        private void barCheckZoomIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlsMapZoomInToolClass zoomIn = new ControlsMapZoomInToolClass();
            zoomIn.OnCreate(this.MapControl.Object);
            MapControl.CurrentTool = zoomIn;
        }

        private void barCheckZoomout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlsMapZoomOutToolClass zoomOut = new ControlsMapZoomOutToolClass();
            zoomOut.OnCreate(this.MapControl.Object);
            MapControl.CurrentTool = zoomOut;
        }

        private void barCheckItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlsMapPanToolClass tool = new ControlsMapPanToolClass();
            tool.OnCreate(this.MapControl.Object);
            MapControl.CurrentTool = tool;
        }

        private void barButtonFullExtent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlsMapFullExtentCommandClass tool = new ControlsMapFullExtentCommandClass();
            tool.OnCreate(this.MapControl.Object);
            tool.OnClick();
        }

        private void barButtonPreView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlsMapZoomToLastExtentBackCommandClass tool = new ControlsMapZoomToLastExtentBackCommandClass();
            tool.OnCreate(this.MapControl.Object);
            tool.OnClick();
        }

        private void barButtonNextView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlsMapZoomToLastExtentForwardCommandClass tool = new ControlsMapZoomToLastExtentForwardCommandClass();
            tool.OnCreate(this.MapControl.Object);
            tool.OnClick();
        }

        private void barCheckSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlsSelectFeaturesToolClass tool = new ControlsSelectFeaturesToolClass();
            tool.OnCreate(this.MapControl.Object);
            MapControl.CurrentTool = tool;
        }

        private void barButtonClearSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlsClearSelectionCommandClass tool = new ControlsClearSelectionCommandClass();
            tool.OnCreate(this.MapControl.Object);
            tool.OnClick();
        }

        private void barCheckIdentify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlsMapIdentifyToolClass tool = new ControlsMapIdentifyToolClass();
            tool.OnCreate(this.MapControl.Object);
            MapControl.CurrentTool = tool;
        }

        #endregion
    }
}