using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_Enviornment
{
    internal static class DistinctHelper
    {
        public static string[] Distinct(string[] rawData)
        {
            int[] exludedItemIndex = new int[rawData.Length];
            string current = string.Empty;
            string[] distinctData = new string[rawData.Length];

            for (int i = 0; i < rawData.Length; i++)
            {
                current = rawData[i];
                for (int j = i+1; j < rawData.Length; j++) 
                { 
                    if(current == rawData[j])
                        exludedItemIndex[j] = j;
                }
                if(exludedItemIndex[i] == 0)
                    distinctData[i] = current;
            }

            return distinctData;
        }
    }
}
