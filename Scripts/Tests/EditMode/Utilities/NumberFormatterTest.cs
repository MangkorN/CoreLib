using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CoreLib.Utilities;

namespace CoreLib.UnitTest.Utilities
{
    public class NumberFormatterTest
    {
        [Test]
        public void FormatFull_FormatsCorrectly()
        {
            Assert.AreEqual("1", NumberFormatter.FormatFull(1));
            Assert.AreEqual("10", NumberFormatter.FormatFull(10));
            Assert.AreEqual("100", NumberFormatter.FormatFull(100));
            Assert.AreEqual("1,000", NumberFormatter.FormatFull(1000));
            Assert.AreEqual("10,000", NumberFormatter.FormatFull(10000));
            Assert.AreEqual("100,000", NumberFormatter.FormatFull(100000));
            Assert.AreEqual("1,000,000", NumberFormatter.FormatFull(1000000));
            Assert.AreEqual("10,000,000", NumberFormatter.FormatFull(10000000));
            Assert.AreEqual("100,000,000", NumberFormatter.FormatFull(100000000));
            Assert.AreEqual("1,000,000,000", NumberFormatter.FormatFull(1000000000));
            Assert.AreEqual("1", NumberFormatter.FormatFull(1));
            Assert.AreEqual("12", NumberFormatter.FormatFull(12));
            Assert.AreEqual("123", NumberFormatter.FormatFull(123));
            Assert.AreEqual("1,234", NumberFormatter.FormatFull(1234));
            Assert.AreEqual("12,345", NumberFormatter.FormatFull(12345));
            Assert.AreEqual("123,456", NumberFormatter.FormatFull(123456));
            Assert.AreEqual("1,234,567", NumberFormatter.FormatFull(1234567));
            Assert.AreEqual("12,345,678", NumberFormatter.FormatFull(12345678));
            Assert.AreEqual("123,456,789", NumberFormatter.FormatFull(123456789));
            Assert.AreEqual("1,234,567,891", NumberFormatter.FormatFull(1234567891));
        }

        [Test]
        public void FormatToK_FormatsCorrectly()
        {
            Assert.AreEqual("1", NumberFormatter.FormatToK(1));
            Assert.AreEqual("10", NumberFormatter.FormatToK(10));
            Assert.AreEqual("100", NumberFormatter.FormatToK(100));
            Assert.AreEqual("1K", NumberFormatter.FormatToK(1000));
            Assert.AreEqual("10K", NumberFormatter.FormatToK(10000));
            Assert.AreEqual("100K", NumberFormatter.FormatToK(100000));
            Assert.AreEqual("1,000K", NumberFormatter.FormatToK(1000000));
            Assert.AreEqual("10KK", NumberFormatter.FormatToK(10000000));
            Assert.AreEqual("100KK", NumberFormatter.FormatToK(100000000));
            Assert.AreEqual("1,000KK", NumberFormatter.FormatToK(1000000000));
            Assert.AreEqual("1", NumberFormatter.FormatToK(1));
            Assert.AreEqual("12", NumberFormatter.FormatToK(12));
            Assert.AreEqual("123", NumberFormatter.FormatToK(123));
            Assert.AreEqual("1.2K", NumberFormatter.FormatToK(1234));
            Assert.AreEqual("12.3K", NumberFormatter.FormatToK(12345));
            Assert.AreEqual("123K", NumberFormatter.FormatToK(123456));
            Assert.AreEqual("1,234K", NumberFormatter.FormatToK(1234567));
            Assert.AreEqual("12.3KK", NumberFormatter.FormatToK(12345678));
            Assert.AreEqual("123KK", NumberFormatter.FormatToK(123456789));
            Assert.AreEqual("1,234KK", NumberFormatter.FormatToK(1234567891));
        }

        [Test]
        public void FormatToKSkipFirstK_FormatsCorrectly()
        {
            Assert.AreEqual("1", NumberFormatter.FormatToKSkipFirstK(1));
            Assert.AreEqual("10", NumberFormatter.FormatToKSkipFirstK(10));
            Assert.AreEqual("100", NumberFormatter.FormatToKSkipFirstK(100));
            Assert.AreEqual("1,000", NumberFormatter.FormatToKSkipFirstK(1000));
            Assert.AreEqual("10K", NumberFormatter.FormatToKSkipFirstK(10000));
            Assert.AreEqual("100K", NumberFormatter.FormatToKSkipFirstK(100000));
            Assert.AreEqual("1,000K", NumberFormatter.FormatToKSkipFirstK(1000000));
            Assert.AreEqual("10KK", NumberFormatter.FormatToKSkipFirstK(10000000));
            Assert.AreEqual("100KK", NumberFormatter.FormatToKSkipFirstK(100000000));
            Assert.AreEqual("1,000KK", NumberFormatter.FormatToKSkipFirstK(1000000000));
            Assert.AreEqual("1", NumberFormatter.FormatToKSkipFirstK(1));
            Assert.AreEqual("12", NumberFormatter.FormatToKSkipFirstK(12));
            Assert.AreEqual("123", NumberFormatter.FormatToKSkipFirstK(123));
            Assert.AreEqual("1,234", NumberFormatter.FormatToKSkipFirstK(1234));
            Assert.AreEqual("12.3K", NumberFormatter.FormatToKSkipFirstK(12345));
            Assert.AreEqual("123K", NumberFormatter.FormatToKSkipFirstK(123456));
            Assert.AreEqual("1,234K", NumberFormatter.FormatToKSkipFirstK(1234567));
            Assert.AreEqual("12.3KK", NumberFormatter.FormatToKSkipFirstK(12345678));
            Assert.AreEqual("123KK", NumberFormatter.FormatToKSkipFirstK(123456789));
            Assert.AreEqual("1,234KK", NumberFormatter.FormatToKSkipFirstK(1234567891));
        }

        [Test]
        public void FormatToKMB_FormatsCorrectly()
        {
            Assert.AreEqual("1", NumberFormatter.FormatToKMB(1));
            Assert.AreEqual("10", NumberFormatter.FormatToKMB(10));
            Assert.AreEqual("100", NumberFormatter.FormatToKMB(100));
            Assert.AreEqual("1K", NumberFormatter.FormatToKMB(1000));
            Assert.AreEqual("10K", NumberFormatter.FormatToKMB(10000));
            Assert.AreEqual("100K", NumberFormatter.FormatToKMB(100000));
            Assert.AreEqual("1M", NumberFormatter.FormatToKMB(1000000));
            Assert.AreEqual("10M", NumberFormatter.FormatToKMB(10000000));
            Assert.AreEqual("100M", NumberFormatter.FormatToKMB(100000000));
            Assert.AreEqual("1B", NumberFormatter.FormatToKMB(1000000000));
            Assert.AreEqual("1", NumberFormatter.FormatToKMB(1));
            Assert.AreEqual("12", NumberFormatter.FormatToKMB(12));
            Assert.AreEqual("123", NumberFormatter.FormatToKMB(123));
            Assert.AreEqual("1.2K", NumberFormatter.FormatToKMB(1234));
            Assert.AreEqual("12.3K", NumberFormatter.FormatToKMB(12345));
            Assert.AreEqual("123.5K", NumberFormatter.FormatToKMB(123456));
            Assert.AreEqual("1.23M", NumberFormatter.FormatToKMB(1234567));
            Assert.AreEqual("12.35M", NumberFormatter.FormatToKMB(12345678));
            Assert.AreEqual("123.46M", NumberFormatter.FormatToKMB(123456789));
            Assert.AreEqual("1.23B", NumberFormatter.FormatToKMB(1234567891));
        }
    }
}
