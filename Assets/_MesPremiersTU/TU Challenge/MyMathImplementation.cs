using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge
{
    public class MyMathImplementation
    {
        internal static int Add(int a, int b)
        {
            return a + b;
        }

        internal static List<int> GenericSort(List<int> toSort, Func<int, int, bool> isInOrder)
        {
            List<int> result = new(toSort);

            for (int y = 0; y < result.Count; y++)
            {
                for (int i = 0; i < result.Count - 1; i++)
                {
                    if (isInOrder.Invoke(result[i + 1], result[i]))
                    {
                        int bubble = result[i + 1];
                        result[i + 1] = result[i];
                        result[i] = bubble;
                    }
                }
            }

            return result;
        }

        internal static List<int> GetAllPrimary(int a)
        {
            List<int> allPrimery = new();
            for (int i = 2; i < a + 1; i++)
            {
                if (IsPrimary(i))
                    allPrimery.Add(i);
            }

            return allPrimery;
        }

        internal static bool IsDivisible(int a, int b)
        {
            return a % b == 0;
        }

        internal static bool IsEven(int a)
        {
            return a % 2 == 0;
        }

        internal static bool IsInOrder(int a, int b)
        {
            return a <= b;
        }

        internal static bool IsInOrderDesc(int arg1, int arg2)
        {
            return arg1 >= arg2;
        }

        internal static bool IsListInOrder(List<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i] > list[i + 1])
                    return false;
            }

            return true;
        }

        internal static bool IsMajeur(int age)
        {
            if (age < 0 || age >= 150) throw new ArgumentException("Age must be positiv and less that the human life time.");

            return age >= 18;
        }

        internal static bool IsPrimary(int a)
        {
            if (a > 1)
            {
                for (int i = 2; i < a; i++)
                {
                    if (IsDivisible(a, i))
                        return false;
                }

                return true;
            }

            return false;
        }

        internal static int Power(int a, int b)
        {
            int result = a;
            for (int i = 1; i < b; i++)
            {
                result *= a;
            }

            return result;
        }

        internal static int Power2(int a)
        {
            return a * a;
        }

        internal static List<int> Sort(List<int> toSort)
        {
            List<int> result = new List<int>(toSort);

            for (int y = 0; y < result.Count; y++)
            {
                for (int i = 0; i < result.Count - 1; i++)
                {
                    if (result[i] > result[i + 1])
                    {
                        int bubble = result[i + 1];
                        result[i + 1] = result[i];
                        result[i] = bubble;
                    }
                }
            }

            return result;
        }
    }
}
