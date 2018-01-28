using System;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Teleware.DataCenter.MapViewer.Controls
{
    public partial class TOCCControl : XtraUserControl
    {
        private IMapControl4 _mapControl;
        private IToolbarMenu toolbarMenu;
        private ITOCControl2 _tocControl;
        public ILayer pMoveLayer;//需要被调整的图层；
        public int toIndex;//将要调整到的目标图层的索引；
        private ILayer _layer;
        private object _other;
        private object _index;
        public TOCCControl()
        {
            InitializeComponent();
        }

        public void Init(IMapControl4 mapControl)
        {
            _mapControl = mapControl;
            ATOCControl.SetBuddyControl(mapControl);
        }

        public AxTOCControl ATOCControl
        {
            get { return axTOCControl; }
        }

        public DockManager DockManager { get; set; }

        private void CreatePopupMenu()
        {
                toolbarMenu = new ToolbarMenuClass();
                toolbarMenu.AddItem(new CommandZoomToLayer(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
                toolbarMenu.AddItem(new CommandZoomToVisible(), -1, 1, false, esriCommandStyles.esriCommandStyleTextOnly);

                IToolbarMenu subToolbarMenu = new ToolbarMenuClass { Caption = "移动图层" }; ;
                subToolbarMenu.AddItem(new CommandMoveLayerUp(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
                subToolbarMenu.AddItem(new CommandMoveLayerDown(), -1, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
                subToolbarMenu.AddItem(new CommandMoveLayerTop(), -1, 2, false, esriCommandStyles.esriCommandStyleTextOnly);
                subToolbarMenu.AddItem(new CommandMoveLayerBottom(), -1, 3, false, esriCommandStyles.esriCommandStyleTextOnly);
                toolbarMenu.AddSubMenu(subToolbarMenu, 2, false);

                toolbarMenu.AddItem(new CommandRemoveLayer(), -1, 3, false, esriCommandStyles.esriCommandStyleTextOnly);
               // toolbarMenu.AddItem(new CommandLayerAttribute(), -1, 4, true, esriCommandStyles.esriCommandStyleTextOnly);
                //toolbarMenu.AddItem(new CommandLayerProperties(axTOCControl.Object as ITOCControl2), -1, 5, true, esriCommandStyles.esriCommandStyleTextOnly);

                toolbarMenu.SetHook(_mapControl); 
        }
        private void CreateGroupLayerPopupMenu()
        {
                toolbarMenu = new ToolbarMenuClass();
                toolbarMenu.SetHook(_mapControl);
                toolbarMenu.AddItem(new CommandRemoveLayer(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
               // toolbarMenu.AddItem(new CmdClearEmptyLayerFromGroupLayer(), -1, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
                IToolbarMenu subToolbarMenu = new ToolbarMenuClass { Caption = "移动图层" }; ;
                subToolbarMenu.AddItem(new CommandMoveLayerUp(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
                subToolbarMenu.AddItem(new CommandMoveLayerDown(), -1, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
                subToolbarMenu.AddItem(new CommandMoveLayerTop(), -1, 2, false, esriCommandStyles.esriCommandStyleTextOnly);
                subToolbarMenu.AddItem(new CommandMoveLayerBottom(), -1, 3, false, esriCommandStyles.esriCommandStyleTextOnly);
                toolbarMenu.AddSubMenu(subToolbarMenu, 2, false);
        }

        private void CreateMapPopupMenu()
        {
                toolbarMenu = new ToolbarMenuClass();
                toolbarMenu.SetHook(_mapControl);
                //toolbarMenu.AddItem(new CmdClearEmptyLayerFromMap(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
                toolbarMenu.AddItem(new CommandRemoveAllLayers(), -1, 1, false, esriCommandStyles.esriCommandStyleTextOnly);           
        }

        private void axTOCControl_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            if(e.button==2)
            {
                IBasicMap map = null;
                ILayer layer = null;
                object other = null;
                object index = null;
                _tocControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);
                if(layer!=null)
                {
                    _tocControl.SelectItem(layer, null);
                    _mapControl.CustomProperty = layer;
                }
                if (item==esriTOCControlItem.esriTOCControlItemLayer)//单击图层
                {
                    if (layer is GroupLayer)//组图层
                    {
                        CreateGroupLayerPopupMenu();
                        toolbarMenu.PopupMenu(e.x, e.y, _tocControl.hWnd);
                    }
                    else//普通图层
                    {
                        CreatePopupMenu();
                        toolbarMenu.PopupMenu(e.x, e.y, _tocControl.hWnd);
                    }
                }
                else if(item==esriTOCControlItem.esriTOCControlItemMap)//单击地图
                {
                    CreateMapPopupMenu();
                    toolbarMenu.PopupMenu(e.x, e.y, _tocControl.hWnd);
                }
            }
        }

        private void TOCCControl_Load(object sender, EventArgs e)
        {
            _tocControl = (ITOCControl2)ATOCControl.Object;
        }

        private void axTOCControl1_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            IBasicMap map = null;
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            ATOCControl.HitTest(e.x, e.y, ref item, ref map, ref _layer, ref _other, ref _index);

            if (item == esriTOCControlItem.esriTOCControlItemLegendClass)
            {
                SetSymbol(sender, e);
            }
        }


        /// <summary>
        /// 符号化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolSymbolConfig_Click(object sender, EventArgs e)
        {
            /*
            if (_layer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = _layer as IFeatureLayer;

                string sStyleFileName = Application.StartupPath + "\\Symbol\\" + "ESRI.ServerStyle";
                if (!File.Exists(sStyleFileName))
                {
                    XtraMessageBox.Show("未能找到符号库文件", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (sStyleFileName != "")
                {
                    ILegendGroup pLegendGroup = (ILegendGroup)_other;
                    ILegendClass pLegendClass = pLegendGroup.get_Class(Convert.ToInt16(_index));
                    ISymbol pSymbol = pLegendClass.Symbol;
                    SymbolSelectForm pSymbolSelectForm = null;
                    if (pSymbol is IMarkerSymbol)
                    {
                        pSymbolSelectForm = new SymbolSelectForm(sStyleFileName, esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
                    }
                    else if (pSymbol is ILineSymbol)
                    {
                        pSymbolSelectForm = new SymbolSelectForm(sStyleFileName, esriSymbologyStyleClass.esriStyleClassLineSymbols);
                    }
                    else if (pSymbol is IFillSymbol)
                    {
                        pSymbolSelectForm = new SymbolSelectForm(sStyleFileName, esriSymbologyStyleClass.esriStyleClassFillSymbols);
                    }

                    if (pSymbolSelectForm != null)
                    {
                        if (pSymbolSelectForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            IStyleGalleryItem sytleitem = pSymbolSelectForm._StyleGalleryItem;
                            if (sytleitem != null)
                            {
                                string sColor = "";// pSymbolSelectForm._Color;
                                ISymbol pNewSymbol = sytleitem.Item as ISymbol;
                                if (sColor != "")
                                {
                                    IRgbColor pRgbColor = SymbolManager.CreateRgbColor(sColor);
                                    SymbolManager.instance.SetSymbolColor(ref pNewSymbol, pRgbColor, null);
                                }
                                pLegendClass.Symbol = sytleitem.Item as ISymbol;
                            }
                            pSymbolSelectForm.Close();
                           
                        }
                    }
                }

                ATOCControl.ActiveView.Refresh();
                ATOCControl.Update();
            }*/
        }

        private void SetSymbol(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            esriTOCControlItem toccItem = esriTOCControlItem.esriTOCControlItemNone;
            ILayer iLayer = null; 
            IBasicMap iBasicMap = null;        
            object unk = null;             
            object data = null;                   
            if (e.button == 1)//鼠标左键按下     
            {
                axTOCControl.HitTest(e.x, e.y, ref toccItem, ref iBasicMap, ref iLayer, ref unk, ref data);            
                System.Drawing.Point pos = new System.Drawing.Point(e.x, e.y);    
                if (toccItem == esriTOCControlItem.esriTOCControlItemLegendClass)  
                {                      
                    ESRI.ArcGIS.Carto.ILegendClass pLC = new LegendClassClass();  
                    ESRI.ArcGIS.Carto.ILegendGroup pLG = new LegendGroupClass(); 
                    if (unk is ILegendGroup)             
                    {                          
                        pLG = (ILegendGroup)unk;      
                    }                      
                    pLC = pLG.get_Class((int)data);    
                    ISymbol pSym;                     
                    pSym = pLC.Symbol;          
                    ESRI.ArcGIS.DisplayUI.ISymbolSelector pSS = new  ESRI.ArcGIS.DisplayUI.SymbolSelectorClass();  
                    bool bOK = false;                                        
                    pSS.AddSymbol(pSym);               
                    bOK = pSS.SelectSymbol(0);                 
                    if (bOK)                  
                    { 

                        pLC.Symbol = pSS.GetSymbolAt(0);              
                    }
                    ATOCControl.ActiveView.Refresh();
                    ATOCControl.Update();    
                    this.axTOCControl.Refresh();              
                }            
            }      
        }



    }
}
