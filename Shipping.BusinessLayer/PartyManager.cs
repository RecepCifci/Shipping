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
   public class PartyManager : ManagerBase<Party>
    {
        public List<Party> GetPartyList()
        {
            return List();
        }
    }
}
