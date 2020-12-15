using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Timetable
    {
        public Guid Id { get; set; }
        public string WeekType { get; set; }
        public string Day { get; set; }
        public DateTime Time { get; set; }
        public string CourseName { get; set; }
        public int TeacherId { get; set; }
        public string Room { get; set; }

    }
}
