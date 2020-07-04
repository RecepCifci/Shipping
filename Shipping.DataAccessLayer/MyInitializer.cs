using Shipping.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Shipping.DataAccessLayer
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Party party1 = new Party
            {
                PartWeight = 3
            };
            Party party2 = new Party
            {
                PartWeight = 3
            };
            Party party3 = new Party
            {
                PartWeight = 8
            };
            Party party4 = new Party
            {
                PartWeight = 11
            };
            Party party5 = new Party
            {
                PartWeight = 13
            };
            context.Parties.Add(party1);
            context.Parties.Add(party2);
            context.Parties.Add(party3);
            context.Parties.Add(party4);
            context.Parties.Add(party5);

            GeneralParameter generalParameter = new GeneralParameter
            {
                shippingPerAmount = 50,
                pricePerPart = 7,
                maxDifferenceBetweenParts = 1
            };
            context.GeneralParameters.Add(generalParameter);

            context.SaveChanges();
        }
    }
}
