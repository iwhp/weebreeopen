﻿namespace WeebreeOpen.SystemLib.Scheduler.Model
{
    using System;
    using System.Linq;
    using WeebreeOpen.SystemLib;

    public class Trigger
    {
        #region Constructors

        private Trigger()
        {
        }

        #endregion

        #region Creator

        public static Trigger CreateRecurDaily(DateTime dateTimeStart, int recurEveryXDay)
        {
            return new Trigger()
            {
                TriggerFrequency = TriggerFrequency.Daily,
                StartDateTime = dateTimeStart,
                IsActive = true,
                RecurDaily = true,
                RecurEveryXDay = recurEveryXDay
            };
        }

        public static Trigger CreateRecurDaily(DateTime dateTimeStart, int recurEveryXDay, string repeatUnit, int repeatQuantity)
        {
            return new Trigger()
            {
                TriggerFrequency = TriggerFrequency.Daily,
                StartDateTime = dateTimeStart,
                IsActive = true,
                RecurDaily = true,
                RecurEveryXDay = recurEveryXDay,
                RepeatUnit = repeatUnit,
                RepeatQuantity = repeatQuantity
            };
        }

        public static Trigger CreateRecurMonthlyLastWorkingDay(DateTime dateTimeStart, int recurEveryXDay, int daysBeforeLastWorkingDay = 0)
        {
            return new Trigger()
            {
                TriggerFrequency = TriggerFrequency.Monthly,
                StartDateTime = dateTimeStart,
                IsActive = true,
                RecurMonthly = true,
                RecurEveryMonthLastWorkingDay = true,
                RecurEveryMonthLastWorkingDaysBefore = daysBeforeLastWorkingDay
            };
        }

        #endregion

        #region Properties

        #region Basic Schedule Properties

        /// <summary>
        /// Frequency of the trigger: once, daily, weekly, monthly
        /// </summary>
        public TriggerFrequency TriggerFrequency { get; set; }

        /// <summary>
        /// First DateTime, when the trigger is executed.
        /// </summary>
        public DateTimeOffset StartDateTime { get; set; }

        /// <summary>
        /// First DateTime, when the trigger is executed.
        /// </summary>
        public DateTimeOffset LastDateTime { get; set; }

        /// <summary>
        /// First DateTime, when the trigger is executed.
        /// </summary>
        public DateTimeOffset NextDateTime { get; set; }

        ///// <summary>
        ///// Last DateTime, when the trigger is executed.
        ///// </summary>
        //public DateTimeOffset ExpireDateTime { get; set; }

        /// <summary>
        /// Indicates if the trigger is active or not.
        /// </summary>
        public bool IsActive { get; set; }

        #endregion

        #region Recurring Properties

        #region  Daily

        public bool RecurDaily { get; set; }

        public int RecurEveryXDay { get; set; }

        #endregion

        #region Weekly

        //public bool RecurWeekly { get; set; }

        //public int RecurEveryXWeek { get; set; }

        //public bool[] RecurEveryXWeekWeekDay { get; set; }

        #endregion

        #region Monthly

        public bool RecurMonthly { get; set; }

        public bool RecurEveryMonthLastWorkingDay { get; set; }
        public int RecurEveryMonthLastWorkingDaysBefore { get; set; }

        //public bool[] RecurEveryMonthOn { get; set; }
        //public bool[] RecurEveryMonthDay { get; set; }

        //public bool RecurEveryMonthFirst { get; set; }
        //public bool RecurEveryMonthSecond { get; set; }
        //public bool RecurEveryMonthThird { get; set; }
        //public bool RecurEveryMonthForuth { get; set; }
        //public bool RecurEveryMonthLast { get; set; }
        //public bool[] RecurEveryMonthWeekDay { get; set; }

        #endregion

        #endregion

        #region Repetition Prpoerties

        /// <summary>
        /// Unit used for repeating: Seconds, Minutes, Hours
        /// </summary>
        public string RepeatUnit { get; set; }

        /// <summary>
        /// Quantity for the units, e.g. 10 (minutes)
        /// </summary>
        public int RepeatQuantity { get; set; }


        ///// <summary>
        ///// Unit used for defining the duration for the repetition.
        ///// </summary>
        //public int RepeatDurationUnit { get; set; }

        ///// <summary>
        ///// Quantity used for defining the duration for the repetition.
        ///// </summary>
        //public int RepeatDurationQuantity { get; set; }

        #endregion

        #endregion

        #region Methods

        public DateTimeOffset CalculateNextDateTimeTrigger()
        {
            #region Calculate for Recur Daily

            if (this.RecurDaily)
            {
                if (this.StartDateTime > DateTime.Now)
                {
                    this.NextDateTime = this.StartDateTime.Date;
                }
                else
                {
                    this.NextDateTime = DateTime.Now.Date;
                }

                // Set the next run to today plus the time of the StartDateTime
                this.NextDateTime = this.NextDateTime
                    + TimeSpan.FromHours(this.StartDateTime.Hour)
                    + TimeSpan.FromMinutes(this.StartDateTime.Minute)
                    + TimeSpan.FromSeconds(this.StartDateTime.Second);

                // Verify if next run is smaller then now: if so, add one day (runs next day so)
                if (this.NextDateTime < DateTimeOffset.Now)
                {
                    this.NextDateTime = this.NextDateTime + TimeSpan.FromDays(1);
                }
            }

            #endregion

            #region Calculate for Recur Daily

            if (this.RecurMonthly)
            {
                if (RecurEveryMonthLastWorkingDay)
                {
                    // Set the next run to this month last day
                    this.NextDateTime = DateTimeExtensions.LastDateInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    this.NextDateTime = this.NextDateTime.Date
                        + TimeSpan.FromHours(this.StartDateTime.Hour)
                        + TimeSpan.FromMinutes(this.StartDateTime.Minute)
                        + TimeSpan.FromSeconds(this.StartDateTime.Second);

                    // Verify if next run is smaller then now: if so, add to next month day
                    if (this.NextDateTime < DateTimeOffset.Now)
                    {
                        this.NextDateTime = DateTimeExtensions.LastDateInMonth(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month);
                        this.NextDateTime = this.NextDateTime.Date
                            + TimeSpan.FromHours(this.StartDateTime.Hour)
                            + TimeSpan.FromMinutes(this.StartDateTime.Minute)
                            + TimeSpan.FromSeconds(this.StartDateTime.Second);
                    }
                }
            }

            #endregion

            return this.NextDateTime;
        }

        #endregion
    }
}
