﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shipping.Model
{
    public class Party
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PartWeight { get; set; }
        public int PartCount { get; set; }
        public int Amount { get; set; }
        public virtual List<PartyPerBox> PartyPerBox { get; set; }
    }
}
