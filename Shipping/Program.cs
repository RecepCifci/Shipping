using Shipping.BusinessLayer;
using Shipping.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping
{
    class Program
    {
        public static List<ShippingInfo> shippingList = new List<ShippingInfo>();
        static int shippingPerAmount = 50;
        static int pricePerPart = 7;
        static int maxDifferenceBetweenParts = 1;

        private static GeneralParameter generalParameter = new GeneralParameter();
        private static GeneralParameterManager generalParameterManager = new GeneralParameterManager();
        private static PartyManager partyManager = new PartyManager();
        static void Main(string[] args)
        {
            if (Debugger.IsAttached)
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            generalParameterManager.initdb();
            generalParameter = generalParameterManager.GetGeneralParameter();



            shippingList = new List<ShippingInfo>
            {
                new ShippingInfo{  PartWeight = 3},
                new ShippingInfo{  PartWeight = 3},
                new ShippingInfo{  PartWeight = 8},
                new ShippingInfo{  PartWeight = 11},
                new ShippingInfo{  PartWeight = 13 }
            };

            foreach (var item in shippingList)
            {
                item.PartCount = FindPartCount(item);
                item.Amount = (shippingPerAmount * item.PartCount) + (pricePerPart * item.PartWeight);
            }

            foreach (var item in shippingList)
            {
                Console.WriteLine("PartWeight: " + item.PartWeight + " PartCount: " + item.PartCount + " Amount: " + item.Amount);
            }
            Console.ReadLine();
        }
        public class ShippingInfo
        {
            public int PartWeight { get; set; }
            public int PartCount { get; set; }
            public int Amount { get; set; }
            public List<ShippingInfo> PartList { get; set; }
        }
        public static int FindPartCount(ShippingInfo shipping)
        {
            for (int index = 2; index <= shipping.PartWeight; index++)
            {
                if (shippingList.Any(x => x.PartCount == index)) continue;
                List<int> splittedList = Split(shipping.PartWeight, index);
                if (CompareItemsOfArray(splittedList.ToArray()))
                {
                    shipping.PartList = new List<ShippingInfo>();
                    foreach (var item in splittedList)
                    {
                        shipping.PartList.Add(new ShippingInfo
                        {
                            PartWeight = item,
                            Amount = shippingPerAmount + (pricePerPart * item)
                        });
                    }

                    return index;
                }
            }
            return 0;
        }

        private static List<int> Split(int number, int splitBy)
        {
            List<int> returnList = new List<int>();
            int averageWeight = Convert.ToInt32(number / splitBy);

            returnList.AddRange(Enumerable.Repeat(averageWeight, splitBy - 1).ToArray());

            returnList.Add(averageWeight + (number % splitBy));


            return returnList;
        }

        private static bool CompareItemsOfArray(int[] array)
        {
            for (int index = 0; index < array.Length; index++)
            {
                if (array.Length == 1) return true;
                int current = array[index];
                for (int i = index + 1; i < index; i++)
                {
                    if (Math.Abs(array[index] - array[i]) > maxDifferenceBetweenParts) return false;
                }
                CompareItemsOfArray(array.Skip(1).ToArray());
            }
            return true;
        }
    }
}
