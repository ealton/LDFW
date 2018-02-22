using UnityEngine;


using System.Collections;

namespace LDFW
{

    public static partial class CommonExtensions
    {
        public static string Concatenate(this string str, params string[] strArray)
        {
            if (strArray != null)
            {
                foreach (var part in strArray)
                    str += part;
            }

            return str;
        }
    }

}