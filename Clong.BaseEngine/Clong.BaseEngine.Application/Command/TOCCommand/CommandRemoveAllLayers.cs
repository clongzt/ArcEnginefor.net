using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Controls;
using Teleware.OneMap.ArcGIS;

namespace Teleware.DataCenter.MapViewer
{
    class CommandRemoveAllLayers : BaseCommand
    {
       private IMapControl4 _mapControl;

        public CommandRemoveAllLayers()
        {
            m_caption = "移除所有图层";
        }
        public override void OnCreate(object hook)
        {
            _mapControl = (IMapControl4)hook;
        }

        public override void OnClick()
        {
            _mapControl.Map.ClearLayers();
            AOUtil.ReleaseComObjectAllRefs(_mapControl.Map.Layers);
             _mapControl.ActiveView.Refresh();
        }
    }
}
