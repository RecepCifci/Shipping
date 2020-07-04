using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shipping.Model
{
    public class GeneralParameter
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int shippingPerAmount { get; set; }
        public int pricePerPart { get; set; }
        public int maxDifferenceBetweenParts { get; set; }
    }
}
