using System;
using ESRI.ArcGIS;

namespace Teleware.DataCenter.Common
{
    public partial class LicenseInitializer
    {
        public LicenseInitializer()
        {
            ResolveBindingEvent += BindingArcGISRuntime;
        }

        void BindingArcGISRuntime(object sender, EventArgs e)
        {
            //
            // TODO: Modify ArcGIS runtime binding code as needed
            //
            //if (!RuntimeManager.Bind(ProductCode.Desktop))
            //{
            //    // Failed to bind, announce and force exit
            //    //Console.WriteLine("Invalid ArcGIS runtime binding. Application will shut down.");
            //    //System.Environment.Exit(0);
            //}
            BindArcGISRuntime(new ProductCode[]{ ProductCode.Desktop, ProductCode.Engine});
        }

        /// <summary>
        /// �󶨵�ArcGIS Runtime��
        /// </summary>
        public static bool BindArcGISRuntime(ProductCode[] products)
        {
            foreach(var product in products)
            {
                if (RuntimeManager.Bind(product))
                {
                    return true;
                }
            }
            return false;
        }
    }
}