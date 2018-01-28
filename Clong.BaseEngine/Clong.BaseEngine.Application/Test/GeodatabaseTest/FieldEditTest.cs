using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;

namespace Clong.BaseEngine.TApplication.Test.GeodatabaseTest
{
    class FieldEditTest
    {

        public static void AddFeildToMeery(IFeatureClass featureCls)
        {
            ISchemaLock schemaLock = featureCls as ISchemaLock;
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);

            //
            var field = CreateStringField("Name", "", 8);
            IClass addFeild = (IClass) featureCls;
            addFeild.AddField(field);

            //加载到内存，上下文中
            IFields fields = featureCls.Fields;
            IFieldEdit fieldsEdit = fields as IFieldEdit;
            featureCls.AddField(field);
            //fieldsEdit.AddField(field);
        }

        /// <summary>
        /// 创建简单的字符串字段。
        /// </summary>
        /// <param name="fieldName">字段名称。</param>
        /// <param name="aliasName">字段别名。</param>
        /// <param name="length">字段长度。</param>
        /// <returns>字符串字段。</returns>
        public static IField CreateStringField(string fieldName, string aliasName, int length)
        {
            // create Name field  
            IField field = new Field();
            IFieldEdit fieldEdit = (IFieldEdit)field;
            fieldEdit.Name_2 = fieldName;
            fieldEdit.AliasName_2 = aliasName;
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldEdit.Length_2 = length;
            return field;
        }
    }
}
