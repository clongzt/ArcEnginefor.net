using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Teleware.OneMap.ArcGIS;
using Teleware.OneMap.ArcGIS.Carto;

namespace Teleware.DataCenter.MapViewer
{
    public sealed class CommandRemoveLayer : BaseCommand
    {
        private IMapControl4 _mapControl;

        public CommandRemoveLayer()
        {
            m_caption = "移除图层";
        }
        public override void OnCreate(object hook)
        {
            _mapControl = (IMapControl4)hook;
        }

        public override void OnClick()
        {
            ILayer pLayer = (ILayer)_mapControl.CustomProperty;
            if (pLayer is IGroupLayer)
            {
                //获取GroupLayer中的图层
                var subLayers = LayerUtil.GetFeatureLayersFromGroupLayer(pLayer as IGroupLayer);
                foreach (var subLayer in subLayers)
                {
                    _mapControl.Map.DeleteLayer(subLayer);
                     AOUtil.ReleaseComObjectAllRefs(subLayer);
                }
            }
            _mapControl.Map.DeleteLayer(pLayer);
            _mapControl.ActiveView.Refresh();
            AOUtil.ReleaseComObjectAllRefs(pLayer);

        }
    }
}
