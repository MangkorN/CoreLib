using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreLib.Utilities
{
    public static class NumberFormatter
    {
        /// <summary>
        ///1 => 1 <br></br>
        ///12 => 12 <br></br>
        ///123 => 123 <br></br>
        ///1234 => 1,234 <br></br>
        ///12345 => 12,345 <br></br>
        ///123456 => 123,456 <br></br>
        ///1234567 => 1,234,567 <br></br>
        ///12345678 => 12,345,678 <br></br>
        ///123456789 => 123,456,789 <br></br>
        ///1234567891 => 1,234,567,891 <br></br>
        /// </summary>
        /// <param name="n">Value to format.</param>
        /// <returns>Formatted string.</returns>
        public static string FormatFull(int n)
        {
            return string.Format("{0:#,###0}", n);
        }

        /// <summary>
        ///1 => 1 <br></br>
        ///12 => 12 <br></br>
        ///123 => 123 <br></br>
        ///1234 => 1,234 <br></br>
        ///12345 => 12.3K <br></br>
        ///123456 => 123K <br></br>
        ///1234567 => 1,234K <br></br>
        ///12345678 => 12.3KK <br></br>
        ///123456789 => 123KK <br></br>
        ///1234567891 => 1,234KK <br></br>
        /// </summary>
        /// <param name="n">Value to format.</param>
        /// <returns>Formatted string.</returns>
        public static string FormatToKSkipFirstK(int n)
        {
            if (n >= 100000)
                return FormatToKSkipFirstK(n / 1000) + "K";

            if (n >= 10000)
                return (n / 1000D).ToString("0.#") + "K";

            return n.ToString("#,0");
        }

        /// <summary>
        ///1 => 1 <br></br>
        ///12 => 12 <br></br>
        ///123 => 123 <br></br>
        ///1234 => 1.2K <br></br>
        ///12345 => 12.3K <br></br>
        ///123456 => 123K <br></br>
        ///1234567 => 1,234K <br></br>
        ///12345678 => 12.3KK <br></br>
        ///123456789 => 123KK <br></br>
        ///1234567891 => 1,234KK <br></br>
        /// </summary>
        /// <param name="n">Value to format.</param>
        /// <returns>Formatted string.</returns>
        public static string FormatToK(int n)
        {
            if (n < 10000 && n >= 1000)
            {
                return (n / 1000D).ToString("0.#") + "K";
            }
            else
            {
                return FormatToKSkipFirstK(n);
            }
        }

        /// <summary>
        ///1 => 1 <br></br>
        ///12 => 12 <br></br>
        ///123 => 123 <br></br>
        ///1234 => 1.2K <br></br>
        ///12345 => 12.3K <br></br>
        ///123456 => 123.5K <br></br>
        ///1234567 => 1.23M <br></br>
        ///12345678 => 12.35M <br></br>
        ///123456789 => 123.46M <br></br>
        ///1234567891 => 1.23B <br></br>
        /// </summary>
        /// <param name="n">Value to format.</param>
        /// <returns>Formatted string.</returns>
        public static string FormatToKMB(int n)
        {
            if (n >= 1000000000)
                return (n / 1000000000D).ToString("0.##") + "B";

            if (n >= 1000000)
                return (n / 1000000D).ToString("0.##") + "M";

            if (n >= 1000)
                return (n / 1000D).ToString("0.#") + "K";

            return n.ToString("#,0");
        }
    }
}
