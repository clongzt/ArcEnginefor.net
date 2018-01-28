using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Clong.BaseEngine.TApplication.Test.QueryCursorTest
{
   public  class CursorTest
   {
       #region 查询数据

       public void TestSearch(IFeatureClass fearureCls)
       {
           Search(fearureCls, true);
           
           //
           Search(fearureCls, false);
       }

       public void Search(IFeatureClass featureCls, bool recyling)
       {
           using (var comReleaser =new ComReleaser())
           {
               IFeatureCursor featureCursor = featureCls.Search(null, recyling);
               IFeature feature = featureCursor.NextFeature();
               while (feature != null)
               {
                   Console.WriteLine("oid:" + feature.OID);
                   feature = featureCursor.NextFeature();
               }

           }
       }

       public void Search2(IFeatureClass featureCls, bool recyling)
       {
           using (var comReleaser = new ComReleaser())
           {
               IFeatureCursor featureCursor = featureCls.Search(null, true);
               comReleaser.ManageLifetime(featureCursor);

               IFeature feature = featureCursor.NextFeature();
               IFeature feature2 = featureCursor.NextFeature();

               if (feature.OID == feature2.OID)
               {
                   Console.WriteLine("this is search true");
               }
           }
       }
       #endregion 
       #region 通过ID查询数据

     
       /// <summary>
       ///测试getFeature与getFeatures
       /// </summary>
       /// <param name="featureCls"></param>
       /// <param name="oids"></param>
       /// <param name="fieldIndex"></param>
       public void SearchById(IFeatureClass featureCls, int[] oids,int fieldIndex)
       {
           foreach (var oid in oids)
           {
               var feature= featureCls.GetFeature(oid);
               Console.WriteLine("oid:" + feature.get_Value(fieldIndex));
           }

          
           
           using (var comReleaser = new ComReleaser())
           {
               IFeatureCursor featureCursor = featureCls.GetFeatures(oids, true); ;
               IFeature feature = featureCursor.NextFeature();
               while (feature != null)
               {
                   Console.WriteLine("oid:" + feature.get_Value(fieldIndex));
                   feature = featureCursor.NextFeature();
               }

           }

           using (var comReleaser = new ComReleaser())
           {
               IGeoDatabaseBridge geodatabaseBridge = new GeoDatabaseHelperClass();
               IFeatureCursor featureCursor = geodatabaseBridge.GetFeatures(featureCls,oids, true); ;
               IFeature feature = featureCursor.NextFeature();
               while (feature != null)
               {
                   Console.WriteLine("oid:" + feature.get_Value(fieldIndex));
                   feature = featureCursor.NextFeature();
               }

           }
       }

      
       #endregion 

       #region 插入数据

       public void InsertHigh(IFeatureClass featureCls,IGeometry geometry)
       {
           using (var comReleaser = new ComReleaser())
           {
               IFeatureBuffer featureBuffer = featureCls.CreateFeatureBuffer();
               IFeatureCursor featureCursor = featureCls.Insert(true);
               comReleaser.ManageLifetime(featureCursor);
               featureBuffer.set_Value(featureBuffer.Fields.FindField("InstBy"), "B Pierce");
               for (int ic = 0; ic < 99; ic++)
               {
                   featureBuffer.Shape = geometry;
                   var featureOID = featureCursor.InsertFeature(featureBuffer);
                   if (ic % 10 == 0)
                   {
                       featureCursor.Flush();
                   }
               }
               featureCursor.Flush();
           }
       }

       public static void InsertLoadOnly(IFeatureClass featureCls, IList<IGeometry> geoList)
       {
           IFeatureClassLoad featureClsLoad = (IFeatureClassLoad) featureCls;
           ISchemaLock schemaLock = (ISchemaLock) featureCls;
           try
           {
               schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);
               featureClsLoad.LoadOnlyMode = true;
               using (var comReleaser = new ComReleaser())
               {
                   IFeatureBuffer featureBuffer = featureCls.CreateFeatureBuffer();
                   IFeatureCursor featureCursor = featureCls.Insert(true);
                   comReleaser.ManageLifetime(featureCursor);
                   featureBuffer.set_Value(featureBuffer.Fields.FindField("InstBy"), "B Pierce");
                   for (int ic = 0; ic < geoList.Count; ic++)
                   {
                       featureBuffer.Shape = geoList[0];
                       var featureOID = featureCursor.InsertFeature(featureBuffer);
                       if (ic % 10 == 0)
                       {
                           featureCursor.Flush();
                       }
                   }
                   featureCursor.Flush();
               }
           }
           finally
           {
               featureClsLoad.LoadOnlyMode = false;
               schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
           }
       }

       public void InsertLow(IFeatureClass featureCls, IGeometry geometry)
       {
           using (var comReleaser = new ComReleaser())
           {
              
               for (int ic = 0; ic < 99; ic++)
               {
                   IFeature feature = featureCls.CreateFeature();
                   feature.set_Value(feature.Fields.FindField("InstBy"), "B Pierce");
                   feature.Shape = geometry;
                   feature.Store();
               }
           }
       }


       #endregion

       #region 高效 更新数据
       public void UpdateHigh(IFeatureClass featureCls, IGeometry geometry)
       {
           using (var comReleaser = new ComReleaser())
           {
               
               IFeatureCursor featureCursor = featureCls.Update(null,true);
               IFeature feature = featureCursor.NextFeature();
               var value = "223";
               while (feature!=null)
               {
                   feature.set_Value(1, value);
                   featureCursor.UpdateFeature(feature);
                   feature = featureCursor.NextFeature();
               }
           }
       }

      
       #endregion 

       #region 高效 删除数据
       /// <summary>
       /// 高效删除，常用
       /// </summary>
       /// <param name="featureCls"></param>
       /// <param name="geometry"></param>
       public void DeleteHigh(IFeatureClass featureCls, IGeometry geometry)
       {
           using (var comReleaser = new ComReleaser())
           {
               IFeatureCursor updateCursor = featureCls.Update(null, true);
               IFeature feature = updateCursor.NextFeature();
               var value = "223";
               while (feature != null)
               {
                   updateCursor.DeleteFeature();
                   feature = updateCursor.NextFeature();
               }
           }
       }

       /// <summary>
       /// 单个要素的时候使用
       /// </summary>
       /// <param name="featureCls"></param>
       public void DeleteOne(IFeatureClass featureCls)
       {
           using (var comReleaser = new ComReleaser())
           {
               IFeatureCursor updateCursor = featureCls.Search(null, false);
               IFeature feature = updateCursor.NextFeature();
               var value = "223";
               while (feature != null)
               {
                   feature.Delete();
                   feature = updateCursor.NextFeature();
               }
           }
       }

       /// <summary>
       /// 注意：非版本的数据
       ///  只有当前用户连接到数据库
       /// </summary>
       /// <param name="featureCls"></param>
       public void DeleteAllFeature(IFeatureClass featureCls)
       {
           ITableWrite2 tableWrite = (ITableWrite2) featureCls;
           try
           {
               tableWrite.Truncate();
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               throw;
           }
       }


       #endregion 
   }
}
