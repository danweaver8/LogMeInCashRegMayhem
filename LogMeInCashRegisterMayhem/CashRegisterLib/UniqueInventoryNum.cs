using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterLib
{
    /// <summary>
    /// This class generates a unique number like an item inventory number for new items and existing for the simplicity of the coding challenge.
    /// </summary>
    public static class UniqueInventoryNum
    {
        public static HashSet<string> UniqueNums = new HashSet<string>();

        public static string getRandomID()
        {

            Random r = new Random();
            bool exists = UniqueNums.Add(r.Next(100000, 999999).ToString());
            if (exists)
                getRandomID();
            return r.Next(100000, 999999).ToString();
        }
    }
}
