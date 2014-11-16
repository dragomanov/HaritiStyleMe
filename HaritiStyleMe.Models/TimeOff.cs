using System;
using System.ComponentModel.DataAnnotations;

namespace HaritiStyleMe.Models
{
    public class TimeOff
    {
        #region Contructors
        public TimeOff()
        {
        }

        public TimeOff(DateTime time, TimeSpan duration, string employeeId)
        {
            this.Time = time;
            this.Duration = duration;
            this.EmployeeId = employeeId;
        }
        #endregion

        public int Id { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        public virtual User Employee { get; set; }
    }
}
