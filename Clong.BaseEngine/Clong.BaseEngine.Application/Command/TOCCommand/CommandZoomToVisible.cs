using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;

namespace Teleware.DataCenter.MapViewer
{
    /// <summary>
    /// 缩放图层至可见范围
    /// </summary>
    public class CommandZoomToVisible : BaseCommand
    {
        private IMapControl4 _mapControl;
        public CommandZoomToVisible()
        {
            m_caption = "缩放至可见范围";
        }

        public override void OnCreate(object hook)
        {
            _mapControl = (IMapControl4)hook;
        }

        public override void OnClick()
        {
            ILayer currentLayer = (ILayer)_mapControl.CustomProperty;
            if (currentLayer != null)
            {
                LayerScaleState(currentLayer);
            }
        }

        private void LayerScaleState(ILayer layer)
        {
            if (layer.MaximumScale != 0.0)
            {
                if (layer.MinimumScale != 0.0)
                {
                    //////////////////有最大最小比例尺/////////////////////
                    CurrentScaleCompareExtreme(layer);
                }
                else
                {
                    //////////////有最大比例尺，无最小////////////////
                    if (_mapControl.MapScale <= layer.MaximumScale)  //当前MapControl的比例尺大于图层最大比例尺
                    {
                        _mapControl.MapScale = layer.MaximumScale;
                    }
                }
            }
            else
            {
                if (layer.MinimumScale != 0.0)
                {
                    /////////有最小比例尺，无最大////////////
                    if (_mapControl.MapScale >= layer.MinimumScale) //当前MapControl的比例尺小于图层最小比例尺
                    {
                        _mapControl.MapScale = layer.MinimumScale;
                    }
                }
                else
                {
                    //////////////无最大最小比例尺///////////////
                    _mapControl.Extent = layer.AreaOfInterest;
                }
            }
            IPoint point = new PointClass();
            point.PutCoords((layer.AreaOfInterest.Envelope.XMax + layer.AreaOfInterest.Envelope.XMin) / 2, (layer.AreaOfInterest.Envelope.YMax + layer.AreaOfInterest.Envelope.YMin) / 2);
            _mapControl.CenterAt(point);
            _mapControl.ActiveView.Refresh();
        }

        private void CurrentScaleCompareExtreme(ILayer layer)
        {
            if (_mapControl.MapScale <= layer.MaximumScale) //当前MapControl的比例尺大于图层最大比例尺
            {
                _mapControl.MapScale = layer.MaximumScale;
                return;
            }

            if (_mapControl.MapScale >= layer.MinimumScale) //当前MapControl的比例尺小于图层最小比例尺
            {
                _mapControl.MapScale = layer.MinimumScale;
                return;
            }

            _mapControl.Extent = layer.AreaOfInterest; //判断 ILayer.Extent 的比例尺与图层最大、最小比例尺的关系
            //double extentScale = this._mapControl.MapScale;
            if (_mapControl.MapScale <= layer.MaximumScale) //当前MapControl的比例尺大于图层最大比例尺
            {
                _mapControl.MapScale = layer.MaximumScale;
            }
            else if (_mapControl.MapScale >= layer.MinimumScale) //当前MapControl的比例尺小于图层最小比例尺
            {
                _mapControl.MapScale = layer.MinimumScale;
            }
        }
    }
}

