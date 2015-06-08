namespace WeebreeOpen.SystemLib.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DateTimeExtensionsTest
    {
        #region DateTimeExtensions_LastDateInMonth

        [TestMethod]
        public void DateTimeExtensions_LastDateInMonth()
        {
            // Act-Assert 1
            DateTime result = DateTimeExtensions.LastDateInMonth(2010, 1);
            Assert.AreEqual<DateTime>(new DateTime(2010, 1, 31), result);

            // Act-Assert 1 (leap year)
            result = DateTimeExtensions.LastDateInMonth(2008, 2);
            Assert.AreEqual<DateTime>(new DateTime(2008, 2, 29), result);
        }

        #endregion

        #region DateTimeExtensions_AddWorkDays

        [TestMethod]
        public void DateTimeExtensions_AddWorkDays()
        {
            // Act-Assert 1
            DateTime result = DateTimeExtensions.AddWorkDays(new DateTime(2015, 11, 2, 7, 8, 9), 5); // Wednesday
            Assert.AreEqual<DateTime>(new DateTime(2015, 11, 9, 7, 8, 9), result);

        }

        [TestMethod]
        public void DateTimeExtensions_AddWorkDays_OffsetDate()
        {
            // Act-Assert 1
            DateTimeOffset result = DateTimeExtensions.AddWorkDays(new DateTimeOffset(2015, 11, 2, 7, 8, 9, new TimeSpan(-5, 0, 0)), 5); // Wednesday
            Assert.AreEqual<DateTimeOffset>(new DateTimeOffset(2015, 11, 9, 7, 8, 9, new TimeSpan(-5, 0, 0)), result);

        }

        #endregion
    }
}
