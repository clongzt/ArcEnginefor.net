using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clong.BaseEngine.TApplication.MainForm;
using ESRI.ArcGIS;
using Teleware.DataCenter.Common;
using ESRI.ArcGIS.esriSystem;

namespace Clong.BaseEngine
{
    class Program
    {

        private static LicenseInitializer _licenseInitializer;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Insert this line before invoking any ArcObjects to bind Engine runtime. 
           // ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine);

            VerifyLicense();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMapForm());
        }

        public static  bool VerifyLicense()
        {
            if (!LicenseInitializer.BindArcGISRuntime(new ProductCode[] { ProductCode.Desktop, ProductCode.Engine }))
            {
                MessageBox.Show("绑定ArcGIS许可失败，请检查当前电脑上的ArcGIS许可", "ArcGIS许可");
                return false;
            }
            _licenseInitializer = new LicenseInitializer();
            if (!_licenseInitializer.InitializeApplication(GetBindLicense(), GetArcGISLicenseExtensionCodes()))
            {
                MessageBox.Show("初始化ArcGIS许可失败，请检查当前电脑上的ArcGIS许可", "ArcGIS许可");
                return false;
            }
            return true;
        }

        private static  esriLicenseProductCode[] GetBindLicense()
        {
            return new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeAdvanced };
        }

        private static esriLicenseExtensionCode[] GetArcGISLicenseExtensionCodes()
        {
            return new esriLicenseExtensionCode[] { esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst };
        }

        protected  void ShutdownLicense()
        {
            if (_licenseInitializer != null)
            {
                _licenseInitializer.ShutdownApplication();
            }
        }
    }
}
