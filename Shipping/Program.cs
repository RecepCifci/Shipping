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
        private static GeneralParameter generalParameter = new GeneralParameter();
        private static GeneralParameterManager generalParameterManager = new GeneralParameterManager();
        private static PartyManager partyManager = new PartyManager();
        private static PartyPerBoxManager partyPerBoxManager = new PartyPerBoxManager();
        public static List<Party> partyList = new List<Party>();
        public static List<PartyPerBox> partyPerBoxList = new List<PartyPerBox>();
        static void Main(string[] args)
        {
            //generalParameterManager.initdb(); // Create DB and Objects
            generalParameter = generalParameterManager.List().FirstOrDefault();

            partyList = partyManager.List();
            foreach (var item in partyList)
            {
                item.PartCount = FindPartCount(item);
                item.Amount = (generalParameter.shippingPerAmount * item.PartCount) + (generalParameter.pricePerPart * item.PartWeight);
            }

            foreach (var item in partyList)
            {
                partyManager.Update(item);
            }
            foreach (var item in partyPerBoxList)
            {
                partyPerBoxManager.Insert(item);
            }
            WritePackageCounts();
            WritePackagePerPart();

            Console.ReadLine();
        }
        public static void WritePackageCounts()
        {
            foreach (var item in partyList)
            {
                Console.WriteLine("BOX_ID: " + item.Id + " WEIGHT: " + item.PartWeight + " PART_COUNT: " + item.PartCount);
            }
        }
        public static void WritePackagePerPart()
        {
            foreach (var item in partyPerBoxList)
            {
                Console.WriteLine("BOX_ID: " + item.PartyId + " PART_NUMBER: " + item.Id + " PART_WEIGHT: " + item.PartWeight + " PART_COST: " + item.PartCost);
            }
        }
        public static int FindPartCount(Party shipping)
        {
            for (int index = 2; index <= shipping.PartWeight; index++)
            {
                if (partyList.Any(x => x.PartCount == index)) continue;
                List<int> splittedList = Split(shipping.PartWeight, index);
                if (CompareItemsOfArray(splittedList.ToArray()))
                {
                    foreach (var item in splittedList)
                    {
                        partyPerBoxList.Add(new PartyPerBox
                        {
                            PartWeight = item,
                            PartCost = generalParameter.shippingPerAmount + (generalParameter.pricePerPart * item),
                            PartyId = shipping.Id
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
                    if (Math.Abs(array[index] - array[i]) > generalParameter.maxDifferenceBetweenParts) return false;
                }
                CompareItemsOfArray(array.Skip(1).ToArray());
            }
            return true;
        }
    }
}
