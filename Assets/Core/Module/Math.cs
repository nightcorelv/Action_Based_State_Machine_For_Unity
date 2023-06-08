using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class Math
    {

        public static int NearestPowerOfTwo(int value)
        {
            return (int)Mathf.Pow(2, Mathf.Round(Mathf.Log(value) / Mathf.Log(2)));
        }
        public static int NearestPowerOfTwo(ref int value)
        {
            return value = (int)Mathf.Pow(2, Mathf.Round(Mathf.Log(value) / Mathf.Log(2)));
            //return (int)Mathf.Pow(2, (int)Mathf.Log(value - 1, 2) + 1);
        }

        public static int UpperPowerOfTwo(int value)
        {
            return (int)Mathf.Pow(2, (int)Mathf.Log(value - 1, 2) + 1);
            //return (int)Mathf.Pow(2, Mathf.Ceil(Mathf.Log(value) / Mathf.Log(2)));
        }
        public static int UpperPowerOfTwo(ref int value)
        {
            return value = (int)Mathf.Pow(2, (int)Mathf.Log(value - 1, 2) + 1);
            //return (int)Mathf.Pow(2, Mathf.Ceil(Mathf.Log(value) / Mathf.Log(2)));
        }

        public static int LowerPowerOfTwo(int value)
        {
            return (int)Mathf.Pow(2, (int)Mathf.Log(value, 2));
        }
        public static int LowerPowerOfTwo(ref int value)
        {
            return value = (int)Mathf.Pow(2, (int)Mathf.Log(value, 2));
        }
    }
}
