using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Controls;

namespace Clong.BaseEngine.TApplication.Commom
{
   public  class TMapBaseCommand
    {
        public IMapControl4 MapControl { get; private set; }

        public void OnCreate(object hook)
        {
            MapControl = (IMapControl4)hook;
        }

        public virtual void Execute(object parameter)
        {
        }
    }
}
