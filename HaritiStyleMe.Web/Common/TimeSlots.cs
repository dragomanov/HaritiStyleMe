namespace HaritiStyleMe.Web.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TimeSlots
    {
        public const int TimeSlotInMinutes = 15;

        public static int Count { get { return (int)(TimeSpan.Parse(CloseHour) - TimeSpan.Parse(OpenHour)).TotalMinutes / TimeSlotInMinutes; } }

        public static List<int> All { get { return Enumerable.Range(0, Count).ToList(); } }

        public static string OpenHour { get { return Resources.Strings.OpenHour; } }

        public static string CloseHour { get { return Resources.Strings.CloseHour; } }

        public static int ToTimeSlot(string time)
        {
            return (int)(TimeSpan.Parse(time) - TimeSpan.Parse(OpenHour)).TotalMinutes / TimeSlotInMinutes;
        }

        public static int ToNumberOfSlots(TimeSpan duration)
        {
            return (int)duration.TotalMinutes / TimeSlotInMinutes;
        }

        public static int ToTimeSlot(DateTime time)
        {
            return (int)(time.TimeOfDay.TotalMinutes - TimeSpan.Parse(OpenHour).TotalMinutes) / TimeSlotInMinutes;
        }

        public static IList<int> ToTimeSlots(DateTime time, TimeSpan duration)
        {
            return Enumerable.Range(ToTimeSlot(time), ToNumberOfSlots(duration)).ToList();
        }

        public static IList<int> AvailableSlots(List<int> slots, TimeSpan duration)
        {
            return AvailableSlots(slots, ToNumberOfSlots(duration));
        }

        public static IList<int> AvailableSlots(List<int> slots, int numberOfSlots)
        {
            var availableSlots = new List<int>();
            if (slots.Count < numberOfSlots)
            {
                return availableSlots;
            }

            for (int i = 0, len = slots.Count - numberOfSlots; i <= len; i++)
            {
                var areConsecutiveSlots = slots[i].Equals(slots[i + numberOfSlots - 1] - numberOfSlots + 1);
                if (areConsecutiveSlots)
                {
                    availableSlots.Add(slots[i]);
                }
            }

            return availableSlots;
        }

        public static IList<int> UnavailableSlots(DateTime time, TimeSpan duration)
        {
            return RemoveSlots(new List<int>(All), time, duration);
        }

        public static IList<int> RemoveSlots(List<int> slots, DateTime time, TimeSpan duration)
        {
            var timeSlots = ToTimeSlots(time, duration);
            slots.RemoveAll(s => timeSlots.Contains(s));
            return slots;
        }

        public static IList<TimeSpan> ToTimesOfDay(IList<int> slots)
        {
            var times = new List<TimeSpan>();
            foreach (var slot in slots)
            {
                times.Add(ToTimeOfDay(slot));
            }

            return times;
        }

        public static IList<DateTime> ToTimesOfDay(DateTime date, IList<int> slots)
        {
            var times = new List<DateTime>();
            foreach (var slot in slots)
            {
                times.Add(ToDateTime(date, slot));
            }

            return times;
        }

        public static TimeSpan ToDuration(int numOfSlots)
        {
            return TimeSpan.FromMinutes(numOfSlots * TimeSlotInMinutes);
        }

        public static TimeSpan ToTimeOfDay(int slot)
        {
            return TimeSpan.FromMinutes(TimeSpan.Parse(OpenHour).TotalMinutes + (slot * TimeSlotInMinutes));
        }

        public static string ToString(int slot)
        {
            return TimeSlots.ToDateTime(slot).ToString("hh:mm");
        }

        public static DateTime ToDateTime(int slot)
        {
            return DateTime.Now.Date + ToTimeOfDay(slot);
        }

        public static DateTime ToDateTime(DateTime date, int slot)
        {
            return date.Date + ToTimeOfDay(slot);
        }
    }
}