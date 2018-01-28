using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clong.BaseEngine.TApplication.Command.MapControlCommand;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace Clong.BaseEngine.TApplication.MainForm
{
    public partial class MainMapForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainMapForm()
        {
            InitializeComponent();
            MainRibbonForm = this;
            this.WindowState = FormWindowState.Maximized;
        }



        private MapViewForm MapForm;
        public void InitMapForm()
        {
            MapForm = new MapViewForm();
            nbiOpenLink(MapForm, "地图");
        }
        public static RibbonForm MainRibbonForm { get; set; }  
        public static void nbiOpenLink(Form frm, string caption)
        {
            try
            {
                MainRibbonForm.Cursor = Cursors.WaitCursor;
                frm.Text = caption;
                frm.MdiParent = MainRibbonForm;
                var f = frm as RibbonForm; if (f != null)
                {
                    f.Ribbon.AllowMinimizeRibbon = true;
                    f.Ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True;
                    f.Ribbon.Minimized = true;
                    //f.Ribbon.ApplicationIcon = Program.icoWinform.ToBitmap();  
                }
                frm.Show();
                MainRibbonForm.Cursor = Cursors.Default;
            }
            catch (Exception e)
            {

            }
        }

        private void menuOpenMxd_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenMxdCommand command = new OpenMxdCommand();
            command.OnCreate(this.MapForm.MapControl.Object);
            command.Execute(null);
        }

        private void MainMapForm_Load(object sender, EventArgs e)
        {
            InitMapForm();
        }  
    }
}