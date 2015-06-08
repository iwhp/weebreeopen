namespace WeebreeOpen.SystemLib.Test.Scheduler.Model
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.SystemLib.Scheduler.Model;

    [TestClass]
    public class TriggerTest
    {
        #region Scheduler_Trigger_CalculateNextDateTimeTrigger

        [Ignore]
        [TestMethod]
        public void Scheduler_Trigger_CalculateNextDateTimeTrigger_Daily_Past()
        {
            // Assign
            Trigger sut = Trigger.CreateRecurDaily(new DateTime(2010, 10, 10, 10, 10, 10), 1);

            // Act
            DateTimeOffset result = sut.CalculateNextDateTimeTrigger();

            // Assert
            Assert.AreEqual<DateTime>(DateTime.Now.Date.AddHours(10).AddMinutes(10).AddSeconds(10), result.DateTime);
        }

        [TestMethod]
        public void Scheduler_Trigger_CalculateNextDateTimeTrigger_Daily_Future()
        {
            // Assign
            Trigger sut = Trigger.CreateRecurDaily(new DateTime(2020, 10, 10, 10, 10, 10), 2);

            // Act
            DateTimeOffset result = sut.CalculateNextDateTimeTrigger();

            // Assert
            Assert.AreEqual<DateTime>(new DateTime(2020, 10, 10, 10, 10, 10), result.DateTime);
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
