using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge
{
    public class MyStringImplementation
    {
        internal static string BazardString(string input)
        {
            string begin = "";
            string end = "";

            for (int i = 0; i < input.Length; i++)
            {
                if(i % 2 == 0)
                    begin += input[i];
                else
                    end += input[i];
            }

            return begin + end;
        }

        internal static bool IsNullEmptyOrWhiteSpace(string input)
        {
            if (input == null)
                return true;
            else {
                foreach(char letter in input)
                {
                    if (letter != ' ')
                        return false;
                }

                return true;
            }
        }

        internal static string MixString(string a, string b)
        {
            if (IsNullEmptyOrWhiteSpace(a) || IsNullEmptyOrWhiteSpace(b))
                throw new ArgumentException("Both values need to not be null or empty.");

            string result = "";
            bool takeA = true;
            int indexA = 0;
            int indexB = 0;

            for (int i = 0; i < a.Length + b.Length; i++)
            {
                if (takeA)
                {
                    result += a[indexA++];

                    if (indexB < b.Length)
                    {
                        takeA = false;
                    }
                }
                else
                {
                    result += b[indexB++];

                    if (indexA < a.Length)
                    {
                        takeA = true;
                    }
                }
            }

            return result;
        }

        internal static string ToCesarCode(string input, int offset)
        {
            string result = "";
            foreach(char letter in input)
            {
                int newletter = letter;
                if(letter != ' ')
                {
                    newletter = letter + offset;
                    if (newletter > 122)
                        newletter = newletter - 26;
                }

                result += (char)newletter;
            }

            return result;
        }

        internal static string ToLowerCase(string a)
        {
            // En utf-16
            string result = "";
            foreach(char letter in a)
            {
                if ((letter >= 65 && letter <= 90) || (letter >= 192 && letter <= 221))
                {
                    char lowerChar = (char)(letter + 32);
                    result += lowerChar;
                }
                else
                    result += letter;
            }

            return result;
        }

        internal static string UnBazardString(string input)
        {
            string result = "";
            int half = input.Length / 2;
            bool isEvent = MyMathImplementation.IsEven(input.Length);
            
            for (int i = 0; i < half; i++)
            {
                result += input[i];
                if(isEvent)
                    result += input[i + half];
                else
                    result += input[i + half + 1];
            }

            if (!isEvent)
                result += input[half];

            return result;
        }

        internal static string Voyelles(string a)
        {
            int[] voyelles = { 65, 69, 73, 79, 85, 89, 97, 101, 105, 111, 117, 121 };
            List<int> used = new List<int>(12);
            string result = "";

            foreach(char letter in a)
            {
                if (voyelles.Contains(letter) && !used.Contains(letter))
                {
                    result += letter;
                    used.Add(letter);
                }
            }

            return ToLowerCase(result);
        }

        internal static string ReverseString(string a)
        {
            string result = "";
            for (int i = a.Length - 1; i >= 0; i--)
            {
                result += a[i];
            }

            return result;
        }
    }
}
