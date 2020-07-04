using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Model
{
    public  class PartyPerBox
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PartWeight { get; set; }
        public int PartCost { get; set; }


        public int PartyId { get; set; }
        public virtual Party Party { get; set; }
    }
}
