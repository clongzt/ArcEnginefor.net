using System;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Teleware.OneMap.ArcGIS.Carto;

namespace Teleware.DataCenter.MapViewer
{
    /// <summary>
    /// 图层上移
    /// </summary>
    public sealed class CommandMoveLayerUp : BaseCommand
    {
        private IMapControl4 _mapControl;

        public CommandMoveLayerUp()
        {
            m_caption = "上移图层";
        }
        public override void OnCreate(object hook)
        {
            _mapControl = (IMapControl4)hook;
        }

        public override void OnClick()
        {
            ILayer pLayer = (ILayer)_mapControl.CustomProperty;
            LayerMoveClass.MoveLayer(_mapControl.Map, pLayer, enumMoveTo.enumMoveUp);        
        }
    }

    /// <summary>
    /// 图层下移
    /// </summary>
    public sealed class CommandMoveLayerDown : BaseCommand
    {
        private IMapControl4 _mapControl;
        //private ITOCControl2 _tocControl;

        public CommandMoveLayerDown()//ITOCControl2 tocControl)
        {
            m_caption = "下移图层";
            //_tocControl = tocControl;
        }
        public override void OnCreate(object hook)
        {
            _mapControl = (IMapControl4)hook;
        }

        public override void OnClick()
        {
            ILayer pLayer = (ILayer)_mapControl.CustomProperty;
            LayerMoveClass.MoveLayer(_mapControl.Map, pLayer, enumMoveTo.enumMoveDown);
            //_tocControl.SelectItem(pLayer, null);
            //_tocControl.Update();
        }
    }

    /// <summary>
    /// 图层置顶
    /// </summary>
    public sealed class CommandMoveLayerTop : BaseCommand
    {
        private IMapControl4 _mapControl;

        public CommandMoveLayerTop()
        {
            m_caption = "移至顶层";
        }
        public override void OnCreate(object hook)
        {
            _mapControl = (IMapControl4)hook;
        }

        public override void OnClick()
        {
            ILayer pLayer = (ILayer)_mapControl.CustomProperty;
            LayerMoveClass.MoveLayer(_mapControl.Map, pLayer, enumMoveTo.enumMoveTop );       
        }
    }

    /// <summary>
    /// 图层置底
    /// </summary>
    public sealed class CommandMoveLayerBottom : BaseCommand
    {
        private IMapControl4 _mapControl;

        public CommandMoveLayerBottom()
        {
            m_caption = "移至底层";
        }
        public override void OnCreate(object hook)
        {
            _mapControl = (IMapControl4)hook;
        }

        public override void OnClick()
        {
            ILayer pLayer = (ILayer)_mapControl.CustomProperty;
            LayerMoveClass.MoveLayer(_mapControl.Map, pLayer, enumMoveTo.enumMoveBottom );   
        }
    }

    public class LayerMoveClass
    {
        public static void MoveLayer(IMap pMap,ILayer pLayer, enumMoveTo moveTo)
        {
            if (pMap == null || pLayer == null) return;
            int LayerIndex = -1;
            int index = 0;
            if (LayerUtil.IsFirstLevelLayer(pMap, pLayer))
            {
                LayerIndex = LayerUtil.GetLayerIndex(pMap, pLayer.Name);
                if (LayerIndex > -1)
                {
                    if (moveTo == enumMoveTo.enumMoveTop)
                    {
                        pMap.MoveLayer(pLayer, 0);
                    }
                    else if (moveTo == enumMoveTo.enumMoveBottom)
                    {

                        index = (LayerIndex - 1) > 0 ? (LayerIndex - 1) : 0;
                        pMap.MoveLayer(pLayer, pMap.LayerCount - 1);
                    }
                    else if (moveTo == enumMoveTo.enumMoveUp)
                    {
                        var toIndex = LayerIndex - 1;
                        if (toIndex < 0)
                        {
                            toIndex = 0;}
                        pMap.MoveLayer(pLayer, toIndex);
                    }
                    else if (moveTo == enumMoveTo.enumMoveDown)
                    {
                        if (LayerIndex < pMap.LayerCount - 1)
                            pMap.MoveLayer(pLayer, LayerIndex + 1);
                    }
                }
            }
            else
            {
                IGroupLayer pBelongGroupLayer =GetBelongGroupLayer(pMap, pLayer);
                if (pBelongGroupLayer != null)
                {

                    LayerIndex = LayerUtil.GetLayerIndexInGroupLayer(pBelongGroupLayer, pLayer);
                    //当pLayer的直接组图层不是pBelongGroupLayer时无效！！！
                    if (LayerIndex > -1)
                    {
                        if (moveTo == enumMoveTo.enumMoveTop)
                            MapUtil.MoveLayerInGroupLayer(pMap, pBelongGroupLayer, pBelongGroupLayer, pLayer, 0);
                        else if(moveTo ==enumMoveTo.enumMoveBottom)
                        {
                            ICompositeLayer pCompositeLayer = pBelongGroupLayer as ICompositeLayer;
                            MapUtil.MoveLayerInGroupLayer(pMap, pBelongGroupLayer, pBelongGroupLayer, pLayer, pCompositeLayer.Count - 1);
                        }
                        else if(moveTo ==enumMoveTo.enumMoveUp)
                        {
                            index = (LayerIndex - 1) > 0 ? (LayerIndex - 1) : 0;
                            MapUtil.MoveLayerInGroupLayer(pMap, pBelongGroupLayer, pBelongGroupLayer, pLayer, index);
                        }
                            
                        else if (moveTo == enumMoveTo.enumMoveDown)
                        { 
                            if (LayerIndex < LayerUtil.GetLayerCountInGroupLayer(pBelongGroupLayer, false) - 1)
                            {
                                int count=(pBelongGroupLayer as ICompositeLayer).Count;
                                index = (LayerIndex + 1) > count ? (LayerIndex + 1) :(count-1);
                                MapUtil.MoveLayerInGroupLayer(pMap, pBelongGroupLayer, pBelongGroupLayer, pLayer, index);
                            }
                                
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取图层所在的最低一级组图层。
        /// </summary>
        /// <param name="map">地图对象。</param>
        /// <param name="layer">图层对象。</param>
        /// <returns>图层所在的组图层或者null</returns>
        public static IGroupLayer GetBelongGroupLayer(IMap map, ILayer layer)
        {
            IEnumLayer mapGroupLayers = LayerUtil.GetMapGroupLayers(map, true);
            IGroupLayer groupLayer = mapGroupLayers.Next() as IGroupLayer;
            if (groupLayer != null)
            {
                Predicate<ILayer> layerPredicate = currentLayer => currentLayer == layer;
                while (groupLayer != null)
                {
                    if (LayerUtil.GetLayerOfGroupLayer(groupLayer, layerPredicate,false) != null)
                    {
                        if (groupLayer != null) return groupLayer;
                    }
                    groupLayer = (mapGroupLayers.Next() as IGroupLayer);
                }
            }
            return null;
        }
    }

     


    public enum enumMoveTo
    { 
        enumMoveUp=1,
        enumMoveDown=2,
        enumMoveTop=3,
        enumMoveBottom=4
    }

}
