using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;
using Teleware.DataCenter.Common;

namespace Teleware.DataCenter.DataImport.Model.SpatialData
{

    /// <summary> 
    /// 采用Feature Insert的方式进行导入
    /// </summary>
    public class SpatialDataImporter
    {
      

        public SpatialDataImporter(IWorkspace sourceWorkspace, IWorkspace targetWorkspace)
        {
            _sourceWorkspace = sourceWorkspace;
            _targetWorkspace = targetWorkspace;
        }

        private int totalRowCount = 0;
        /// <summary>
        /// 总导入记录
        /// </summary>
        public int TotalRowCount
        {
            get { return totalRowCount; }
            set { totalRowCount = value; }
        }
        private int errorRowCount = 0;
        /// <summary>
        /// 导入错误记录数
        /// </summary>
        public int ErrorRowCount
        {
            get { return errorRowCount; }
            set { errorRowCount = value; }
        }

     
        private IWorkspace _sourceWorkspace;

        public IWorkspace SourceWorkspace
        {
            get
            {
                
                return _sourceWorkspace; 
            }
            
        }
        private IWorkspace _targetWorkspace;

        public IWorkspace TargetWorkspace
        {
            get
            {
               
                return _targetWorkspace;
            }
        }


        /// <summary>
        /// 导入FeatureClass
        /// </summary>
        /// <param name="sourceFeatureClassName">源featureClass名称</param>
        /// <param name="targetFeatureClassName">目标FeatureClass名称</param>
        public void ImportFeatureClass(String sourceFeatureClassName, String targetFeatureClassName)
        {
            IFeatureClass sourceFeatureClass = ((IFeatureWorkspace) SourceWorkspace).OpenFeatureClass(sourceFeatureClassName);
            totalRowCount = sourceFeatureClass.FeatureCount(null);
            IFeatureClass targetFeatureClass = ((IFeatureWorkspace) TargetWorkspace).OpenFeatureClass(targetFeatureClassName);
            CopyFeatureData(sourceFeatureClass, targetFeatureClass);
        }

        public void ImportFeatureClass(String sourceFeatureClassName, String targetFeatureClassName, IDictionary<string, string> fieldNameCompaire)
        {
            IFeatureClass sourceFeatureClass = ((IFeatureWorkspace) SourceWorkspace).OpenFeatureClass(sourceFeatureClassName);
            totalRowCount = sourceFeatureClass.FeatureCount(null);
            IFeatureClass targetFeatureClass = ((IFeatureWorkspace) TargetWorkspace).OpenFeatureClass(targetFeatureClassName);
            IDictionary<int, int> filedIndexDictionary = GetTheFieldIndexDic(sourceFeatureClass, targetFeatureClass, fieldNameCompaire);
            CopyFeatureClassData(sourceFeatureClass, targetFeatureClass, filedIndexDictionary);
        }

        public void ImportTable(String sourceTableName, String targetTableName, IDictionary<string, string> fieldNameCompaire)
        {
            ITable sourceTable = ((IFeatureWorkspace)SourceWorkspace).OpenTable(sourceTableName);
            totalRowCount = sourceTable.RowCount(null);
            ITable targetTable = ((IFeatureWorkspace)TargetWorkspace).OpenTable(targetTableName);
            IDictionary<int, int> filedIndexDictionary = GetTheFieldIndexDic(sourceTable, targetTable, fieldNameCompaire);
            CopyTableData(sourceTable, targetTable, filedIndexDictionary);
        }

        /// <summary>
        /// featureClass数据拷贝
        /// </summary>
        /// <param name="sourceFeatureClass">源Featureclass</param>
        /// <param name="targetFeatureClass">目标FeatureClass</param>
        public void CopyFeatureData(IFeatureClass sourceFeatureClass, IFeatureClass targetFeatureClass)
        {
            IDictionary<int, int> fieldIndexDic = GetTheFieldIndexDic(sourceFeatureClass, targetFeatureClass);
            CopyFeatureClassData(sourceFeatureClass, targetFeatureClass, fieldIndexDic);

        }

        private void CopyFeatureClassData(IFeatureClass sourceFeatureClass, IFeatureClass targetFeatureClass,IDictionary<int,int> filedIndexDictionary)
        {
            errorRowCount = 0;
            using (var comReleaser = new ComReleaser())
            {
                IFeatureCursor sourceFeatureCursor = sourceFeatureClass.Search(null, false);
                IFeatureCursor targetFeatureCursor = targetFeatureClass.Insert(true);
                comReleaser.ManageLifetime(sourceFeatureCursor);
                comReleaser.ManageLifetime(targetFeatureCursor);

                List<int> targeFieldIndexs = filedIndexDictionary.Keys.ToList();
                int fieldsLen = targeFieldIndexs.Count;

                IFeature sourceFeature = sourceFeatureCursor.NextFeature();
                var count = 0;
                while (sourceFeature != null)
                {
                    try
                    {
                        //错误：Geometry cannot have Z values 处理
                        IGeometry geometry = sourceFeature.ShapeCopy;
                        IZAware zAware=geometry as IZAware;
                        if (zAware != null && zAware.ZAware == true)
                        {
                            zAware.ZAware = false;
                        }

                        IFeatureBuffer targetFatureBuffer = targetFeatureClass.CreateFeatureBuffer();
                        targetFatureBuffer.Shape = geometry;//sourceFeature.ShapeCopy;
                        for (int i = 0; i < fieldsLen; i++)
                        {
                            var targetIndex = targeFieldIndexs[i];
                            int sourceIndex = filedIndexDictionary[targeFieldIndexs[i]];
                            var value = sourceFeature.get_Value(sourceIndex);
                            targetFatureBuffer.set_Value(targetIndex, value);
                        }
                        targetFeatureCursor.InsertFeature(targetFatureBuffer);
                        count++;
                        if (count == 1000)
                        {
                            targetFeatureCursor.Flush();
                            count = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorRowCount++;
                        var message="字段赋值错误";
                        //LoggingManager.GetLogger(GetType()).Error(message, ex);
                    }
                    sourceFeature = sourceFeatureCursor.NextFeature();
                }
                targetFeatureCursor.Flush();
            }
        }

        private void CopyTableData(ITable  sourceTable, ITable targetTable, IDictionary<int, int> filedIndexDictionary)
        {
            errorRowCount = 0;
            using (var comReleaser = new ComReleaser())
            {
                ICursor sourceFeatureCursor = sourceTable.Search(null, false);
                ICursor targetFeatureCursor = targetTable.Insert(true);
                comReleaser.ManageLifetime(sourceFeatureCursor);
                comReleaser.ManageLifetime(targetFeatureCursor);

                List<int> targeFieldIndexs = filedIndexDictionary.Keys.ToList();
                int fieldsLen = targeFieldIndexs.Count;

                IRow sourceFeature = sourceFeatureCursor.NextRow();
                var count = 0;
                while (sourceFeature != null)
                {
                    try
                    {
                        IRowBuffer targetFatureBuffer = targetTable.CreateRowBuffer();
                        for (int i = 0; i < fieldsLen; i++)
                        {
                            var targetIndex = targeFieldIndexs[i];
                            int sourceIndex = filedIndexDictionary[targeFieldIndexs[i]];
                            var value = sourceFeature.get_Value(sourceIndex);
                            targetFatureBuffer.set_Value(targetIndex, value);
                        }
                        targetFeatureCursor.InsertRow(targetFatureBuffer);
                        count++;
                        if (count == 1000)
                        {
                            targetFeatureCursor.Flush();
                            count = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorRowCount++;
                        var message = "字段赋值错误";
                       // LoggingManager.GetLogger(GetType()).Error(message, ex);
                    }
                    sourceFeature = sourceFeatureCursor.NextRow();
                }
                targetFeatureCursor.Flush();
            }
        }

        ///<summary>  
        ///获取对应FeatureCls的之间字段index的对应值
        ///</summary>  
        /// <param name="sourceFeatureClass">源要素类。</param>
        /// <param name="targetFeatureClass">目标要素类。</param>
      /// <returns>字段索引值的字典。</returns>
       ///<remarks>该方法只是对字段的名称做比较，未对类型做比较</remarks>  
        private IDictionary<int, int> GetTheFieldIndexDic(IFeatureClass sourceFeatureClass, IFeatureClass targetFeatureClass)
        {
            Dictionary<int, int> filedIndexDictionary = new Dictionary<int, int>();
            IFields fields = targetFeatureClass.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field = fields.get_Field(i);
                if (field.Type != esriFieldType.esriFieldTypeGeometry && field.Type != esriFieldType.esriFieldTypeOID
                    && field.Editable)
                {
                    int fieldIndex = sourceFeatureClass.Fields.FindField(field.Name);
                    if (fieldIndex > -1)
                    {
                        filedIndexDictionary.Add(i, fieldIndex);
                    }
                }
            }

            return filedIndexDictionary;
        }


        ///<summary>  
        ///获取对应FeatureCls的之间字段index的对应值
        ///</summary>  
        /// <param name="sourceFeatureClass">源要素类。</param>
        /// <param name="targetFeatureClass">目标要素类。</param>
        /// <param name="fieldNameCompaire">目标要素类。</param>
        /// <returns>字段索引值的字典。</returns>
        ///<remarks>该方法只是对字段的名称做比较，未对类型做比较</remarks>  
        private IDictionary<int, int> GetTheFieldIndexDic(IFeatureClass sourceFeatureClass, IFeatureClass targetFeatureClass, IDictionary<string, string> fieldNameCompaire)
        {
            Dictionary<int, int> filedIndexDictionary = new Dictionary<int, int>();
            int len = fieldNameCompaire.Count;
            List<string> listKeys=fieldNameCompaire.Keys.ToList();
            for (int i = 0; i < len; i++)
            {
                string targeFieldName = listKeys[i];
                int trageFieldIndex = targetFeatureClass.FindField(targeFieldName);
                if (trageFieldIndex > -1)
                {
                    string sourceFieldName = listKeys[i];
                    int sourceFieldIndex = sourceFeatureClass.FindField(sourceFieldName);
                    if (sourceFieldIndex > -1)
                    {
                        filedIndexDictionary.Add(trageFieldIndex, sourceFieldIndex);
                    }
                }
            }
            return filedIndexDictionary;
        }

        ///<summary>  
        ///获取对应FeatureCls的之间字段index的对应值
        ///</summary>  
        /// <param name="sourceTable">源要素类。</param>
        /// <param name="targetTable">目标要素类。</param>
        /// <param name="fieldNameCompaire">目标要素类。</param>
        /// <returns>字段索引值的字典。</returns>
        ///<remarks>该方法只是对字段的名称做比较，未对类型做比较</remarks>  
        private IDictionary<int, int> GetTheFieldIndexDic(ITable sourceTable, ITable targetTable, IDictionary<string, string> fieldNameCompaire)
        {
            Dictionary<int, int> filedIndexDictionary = new Dictionary<int, int>();
            int len = fieldNameCompaire.Count;
            List<string> listKeys = fieldNameCompaire.Keys.ToList();
            for (int i = 0; i < len; i++)
            {
                string targeFieldName = listKeys[i];
                int trageFieldIndex = targetTable.FindField(targeFieldName);
                if (trageFieldIndex > -1)
                {
                    string sourceFieldName = listKeys[i];
                    int sourceFieldIndex = sourceTable.FindField(sourceFieldName);
                    if (sourceFieldIndex > -1)
                    {
                        filedIndexDictionary.Add(trageFieldIndex, sourceFieldIndex);
                    }
                }
            }
            return filedIndexDictionary;
        }



    }
}
