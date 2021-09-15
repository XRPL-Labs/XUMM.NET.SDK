using System;
using System.Runtime.InteropServices;
using System.Security;

namespace XUMM.Net.Extensions
{
    public static class SecureStringExtensions
    {
        /// <summary>
        /// Create a secure string from a string
        /// </summary>
        /// <param name="source">Source string to secure</param>
        /// <returns>Return the read-only secure string</returns>
        internal static SecureString ToSecureString(this string source)
        {
            var secureString = new SecureString();
            foreach (var c in source)
            {
                secureString.AppendChar(c);
            }

            secureString.MakeReadOnly();
            return secureString;
        }

        /// <summary>
        /// Get the string the secure string is representing
        /// </summary>
        /// <param name="source">The secure string</param>
        /// <returns>Returns the string representation of the secure string.</returns>
        internal static string GetString(this SecureString source)
        {
            lock (source)
            {
                string result;
                var length = source.Length;
                var pointer = IntPtr.Zero;
                var chars = new char[length];

                try
                {
                    pointer = Marshal.SecureStringToBSTR(source);
                    Marshal.Copy(pointer, chars, 0, length);

                    result = string.Join("", chars);
                }
                finally
                {
                    if (pointer != IntPtr.Zero)
                    {
                        Marshal.ZeroFreeBSTR(pointer);
                    }
                }

                return result;
            }
        }
    }
}
