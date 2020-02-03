using System;
using System.Text;

namespace GameFramework
{
    public static partial class Utility
    {
        public static class Converter
        {
            public static byte[] GetBytes(string value)
            {
                return Encoding.UTF8.GetBytes(value);
            }

            public static string GetString(byte[] value)
            {
                return Encoding.UTF8.GetString(value);
            }
        }
    }
}
