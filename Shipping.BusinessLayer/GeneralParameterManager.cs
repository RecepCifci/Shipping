using Shipping.BusinessLayer.Abstract;
using Shipping.DataAccessLayer;
using Shipping.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BusinessLayer
{
  public  class GeneralParameterManager : ManagerBase<GeneralParameter>
    {
        public void initdb()
        {
            DatabaseContext db = new DatabaseContext();
            db.GeneralParameters.ToList();
        }
    }
}
