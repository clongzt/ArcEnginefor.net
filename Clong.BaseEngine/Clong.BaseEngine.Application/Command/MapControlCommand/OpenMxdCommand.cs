using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clong.BaseEngine.TApplication.Commom;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace Clong.BaseEngine.TApplication.Command.MapControlCommand
{
    public class OpenMxdCommand : TMapBaseCommand
   {


        public override void Execute(object parameter)
        {
            IMapDocument xjMxdMapDocument = new MapDocumentClass();
            OpenFileDialog xjMxdOpenFileDialog = new OpenFileDialog();
            xjMxdOpenFileDialog.Filter = "地图文档(*.mxd)|*.mxd";
            if (xjMxdOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string xjmxdFilePath = xjMxdOpenFileDialog.FileName;
                if (MapControl.CheckMxFile(xjmxdFilePath))
                {
                    MapControl.Map.ClearLayers();
                    MapControl.LoadMxFile(xjmxdFilePath);
                }
                MapControl.ActiveView.Refresh();
            }
        }
    }
}
