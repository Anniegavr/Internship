using System;

namespace ConsoleApp1
{
    public class Grades
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public string CourseId { get; set; }
        public int Midterm1 { get; set; }
        public int Midterm2 { get; set; }
        public int Exam { get; set; }
        public int Total { get; set; }
    }
}
