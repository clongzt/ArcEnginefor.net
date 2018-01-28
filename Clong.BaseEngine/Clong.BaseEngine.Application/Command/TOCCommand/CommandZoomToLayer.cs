using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace Teleware.DataCenter.MapViewer
{
    public sealed class CommandZoomToLayer : BaseCommand
    {
        private IMapControl4 _mapControl;

        public CommandZoomToLayer()
        {
            m_caption = "缩放至图层";
        }

        public override void OnCreate(object hook)
        {
            _mapControl = (IMapControl4)hook;
        }

        public override void OnClick()
        {
            ILayer pLayer = (ILayer)_mapControl.CustomProperty;
            _mapControl.Extent = pLayer.AreaOfInterest;
            _mapControl.ActiveView.Refresh();
        }
    }
}
