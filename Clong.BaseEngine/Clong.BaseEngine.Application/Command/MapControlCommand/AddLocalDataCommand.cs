using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clong.BaseEngine.TApplication.Commom;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;

namespace Clong.BaseEngine.TApplication.Command.MapControlCommand
{
    class AddLocalDataCommand : TMapBaseCommand
    {
        public override void Execute(object parameter)
        {
            IWorkspaceFactory pWorkspaceFactory;
            IFeatureWorkspace pFeatureWorkspace;
            IFeatureLayer pFeatureLayer;

            // 获取当前路径和文件名 
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = " Shape(*.shp)|*.shp|All Files(*.*)|*.* ";
            dlg.Title = " Open Shapefile data ";
            dlg.ShowDialog();

            string strFullPath = dlg.FileName;
            if (strFullPath == "") return;
            int Index = strFullPath.LastIndexOf(" // ");
            string filePath = strFullPath.Substring(0, Index);
            string fileName = strFullPath.Substring(Index + 1);

            // 打开工作空间并添加shp文件 
            pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            pFeatureWorkspace = (IFeatureWorkspace) pWorkspaceFactory.OpenFromFile(filePath, 0);
            pFeatureLayer = new FeatureLayerClass();

            pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(fileName);
            pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;

            MapControl.Map.AddLayer(pFeatureLayer);
            MapControl.ActiveView.Refresh();
        }}
}
