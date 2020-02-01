using System;
using System.Text;

namespace GameFramework
{
    public static partial class Utility
    {
        public static class Text
        {
            private static StringBuilder cachedStringBuilder = null;

            public static string Format(string format, object arg0)
            {
                CheckCachedStringBuilder();
                cachedStringBuilder.Length = 0;
                cachedStringBuilder.AppendFormat(format, arg0);

                return cachedStringBuilder.ToString();
            }

            public static string Format(string format, object arg0, object arg1)
            {
                CheckCachedStringBuilder();
                cachedStringBuilder.Length = 0;
                cachedStringBuilder.AppendFormat(format, arg0, arg1);

                return cachedStringBuilder.ToString();
            }

            public static string Format(string format, object arg0, object arg1, object arg2)
            {
                CheckCachedStringBuilder();
                cachedStringBuilder.Length = 0;
                cachedStringBuilder.AppendFormat(format, arg0, arg1, arg2);

                return cachedStringBuilder.ToString();
            }

            public static string Format(string format, params object[] args)
            {
                CheckCachedStringBuilder();
                cachedStringBuilder.Length = 0;
                cachedStringBuilder.AppendFormat(format, args);

                return cachedStringBuilder.ToString();
            }

            private static void CheckCachedStringBuilder()
            {
                if(cachedStringBuilder == null)
                {
                    cachedStringBuilder = new StringBuilder(1024);
                }
            }
        }
    }
}