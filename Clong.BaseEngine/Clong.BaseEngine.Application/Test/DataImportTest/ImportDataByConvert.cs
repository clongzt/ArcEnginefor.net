using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;

namespace Clong.BaseEngine.TApplication.Test.DataImportTest
{
    class ImportDataByConvert
    {

        public void DataImport(IWorkspace sourceWorkspace, IWorkspace targetWorkspace,IFeatureClass sourceFeatureClass ,IDataset targetFeatureDataset ,string storeName)
        {
            int errorRowCount = 0;
            int totalRowCount = 0;
            Dictionary<int, string> dicError = new Dictionary<int, string>();
            IDataset sourceWorkspaceDataset = (IDataset)sourceWorkspace;
            IDataset targetWorkspaceDataset = (IDataset)targetWorkspace;
            string sourceLayerName = ((IDataset) sourceFeatureClass).Name;
            IName sourceWorkspaceDatasetName = sourceWorkspaceDataset.FullName;
            IName targetWorkspaceDatasetName = targetWorkspaceDataset.FullName;
            IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDatasetName;
            IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDatasetName;

            // Create a name object for the shapefile and cast it to the IDatasetName interface.
            IFeatureClassName sourceFeatureClassName = new FeatureClassNameClass();
            IDatasetName sourceDatasetName = (IDatasetName)sourceFeatureClassName;
            sourceDatasetName.Name = sourceLayerName;
            sourceDatasetName.WorkspaceName = sourceWorkspaceName;

            // Create a name object for the FGDB feature class and cast it to the IDatasetName interface.
            IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
            IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
            targetDatasetName.Name = storeName;// importMappingScheme.StoreDatasetName;"GEODATA.DLTB7"
            targetDatasetName.WorkspaceName = targetWorkspaceName;

            // Create the objects and references necessary for field validation.
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IFields sourceFields = sourceFeatureClass.Fields;
            IFields targetFields = null;
            IEnumFieldError enumFieldError = null;

            // Set the required properties for the IFieldChecker interface.
            fieldChecker.InputWorkspace = sourceWorkspace;
            fieldChecker.ValidateWorkspace = targetWorkspace;

            // Validate the fields and check for errors.
            fieldChecker.Validate(sourceFields, out enumFieldError, out targetFields);

            // Find the shape field.
            String shapeFieldName = sourceFeatureClass.ShapeFieldName;
            int shapeFieldIndex = sourceFeatureClass.FindField(shapeFieldName);
            IField shapeField = sourceFields.get_Field(shapeFieldIndex);

            // Get the geometry definition from the shape field and clone it.
            IGeometryDef geometryDef = shapeField.GeometryDef;
            IClone geometryDefClone = (IClone)geometryDef;
            IClone targetGeometryDefClone = geometryDefClone.Clone();
            IGeometryDef targetGeometryDef = (IGeometryDef)targetGeometryDefClone;

            // Cast the IGeometryDef to the IGeometryDefEdit interface.
            IGeometryDefEdit targetGeometryDefEdit = (IGeometryDefEdit)targetGeometryDef;

            // Set the IGeometryDefEdit properties.
            targetGeometryDefEdit.GridCount_2 = 1;
            targetGeometryDefEdit.set_GridSize(0, 0.75);

            //修改目标字段
            string fieldNameString = ChangeTargetFields(targetFields);
            if (fieldNameString != string.Empty)
            {
                fieldNameString += string.Format(",{0}", shapeFieldName);
            }
            else
            {
                fieldNameString = shapeFieldName;
            }
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.SubFields = fieldNameString;

            //设置导出的要素集
            IFeatureDatasetName featureDatasetName = null;
            if (targetFeatureDataset != null)
            {
                featureDatasetName = (IFeatureDatasetName)targetFeatureDataset.FullName;
            }

            // Create the converter and run the conversion.
            IFeatureDataConverter featureDataConverter = new FeatureDataConverterClass();
            IEnumInvalidObject enumInvalidObject = featureDataConverter.ConvertFeatureClass
                (sourceFeatureClassName, queryFilter, featureDatasetName, targetFeatureClassName,
                targetGeometryDef, targetFields, "", 1000, 0);

            // Check for errors.
            IInvalidObjectInfo invalidObjectInfo = null;
            enumInvalidObject.Reset();
            while ((invalidObjectInfo = enumInvalidObject.Next()) != null)
            {
                if (dicError.ContainsKey(invalidObjectInfo.InvalidObjectID) == false)
                {
                    // Handle the errors in a way appropriate to the application.
                    dicError.Add(invalidObjectInfo.InvalidObjectID, invalidObjectInfo.ErrorDescription);
                    errorRowCount++;
                }
                Console.WriteLine(invalidObjectInfo.InvalidObjectID + "" + invalidObjectInfo.ErrorDescription);
            }
        }

        /// <summary>
        /// 可以设置导出的字段与
        /// </summary>
        /// <param name="targetFields"></param>
        /// <returns></returns>
        private string ChangeTargetFields(IFields targetFields)
        {
            var fieldNameString = "";
            for (int i = 0; i < targetFields.FieldCount; ++i)
            {
                IField field = targetFields.get_Field(i);
                string fieldName = field.Name;
                fieldNameString += string.Format("{0},", fieldName);
            }

            if (fieldNameString != string.Empty)
            {
                fieldNameString = fieldNameString.Substring(0, fieldNameString.Length - 1);
            }

            return fieldNameString;
        }
    }

}
