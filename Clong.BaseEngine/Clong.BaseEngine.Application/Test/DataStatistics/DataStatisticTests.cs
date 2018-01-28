using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using Teleware.OneMap.ArcGIS;

namespace Clong.BaseEngine.TApplication.Test.DataStatistics
{
  public   class DataStatisticTests
    {
        public static void DataStatistic(IFeatureClass featureCls)
        {
            IDataset pDataset = (IDataset)featureCls;
            IFeatureWorkspace featuretWrok = (IFeatureWorkspace)pDataset.Workspace;
            IQueryDef2 queryDef = featuretWrok.CreateQueryDef() as IQueryDef2;
            queryDef.Tables = pDataset.Name;
            queryDef.SubFields = "zldwdm,sum(TBMJ) as mj";
            queryDef.PostfixClause = "group by zldwdm ";
            ICursor cursor = queryDef.Evaluate();
            //todo:
            var zlFieldIndex = cursor.FindField("zldwdm");
            IRow row = cursor.NextRow();
            while (row!=null)
            {
                var dm = row.get_Value(zlFieldIndex);
                    row = cursor.NextRow();
            }

        }

        public static IEnumerable<T> GetUniqueColumnValues<T>(ICursor cursor,string columnName)
        {
            IDataStatistics dataStatistics = null;
            try
            {

                //获取null值
                T nullValue = default(T);
                dataStatistics = new DataStatisticsClass();
                dataStatistics.Cursor = cursor;
                dataStatistics.Field = columnName;
                IEnumerator enumerator = dataStatistics.UniqueValues;
                enumerator.Reset();
                while (enumerator.MoveNext())
                {
                    object currValue = enumerator.Current;
                    if (currValue == DBNull.Value || currValue == null)
                    {
                        yield return nullValue;
                    }
                    else
                    {
                        yield return (T)Convert.ChangeType(currValue, typeof(T));
                    }
                }
            }
            finally
            {
                AOUtil.ReleaseComObjectAllRefs(dataStatistics);
            }
        }

        


        public static void QueryDataByOrder(IFeatureClass featureCls)
        {
            IQueryFilter qf = new QueryFilterClass();
            qf.SubFields = "zldwdm";
            //,sum(TBMJ) as mj,count(*) as num
            //qf .WhereClause="1=1";
            ((IQueryFilterDefinition)qf).PostfixClause = "order by zldwdm ";

            IFeatureCursor feacur = featureCls.Search(qf, false);

            //todo:
            var zlFieldIndex = feacur.FindField("zldwdm");
            IRow row = feacur.NextFeature();
            while (row != null)
            {
                var dm = row.get_Value(zlFieldIndex);
                row = feacur.NextFeature();
            }

        }

      
    }
}
