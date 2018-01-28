using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clong.BaseEngine.TApplication.Commom;
using ESRI.ArcGIS.Carto;

namespace Clong.BaseEngine.TApplication.Command.MapControlCommand
{
    class LoadMapServiceLayerCommand : TMapBaseCommand
    {
        public override void Execute(object parameter)
        {
            IMapServerRESTLayer pRestLayer = new MapServerRESTLayerClass();
            pRestLayer.Connect("http://myServer:6080/arcgis/rest/services/WaterMap2015/MapServer");
            this.MapControl.AddLayer(pRestLayer as ILayer);
        }
    }
}
