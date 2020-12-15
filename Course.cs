using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;

namespace ConsoleApp1
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Length_Hrs { get; set; }

        public void renderCourse(string option)
        {
            Console.Clear();

            using (var Ccontext = new AppContext())
            {
                var courses = Ccontext.Course.ToList();
                Console.WriteLine("Use pagination?\n1. Yes\n2. No");
                string op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        Console.WriteLine("You are creating pages.");
                        Console.Write("# of records on a page: ");
                        int pageSize = int.Parse(Console.ReadLine());
                        Console.Write("Display page number: ");
                        int pageIndex = int.Parse(Console.ReadLine());
                        IPagedList<Course> coursesi = OrderBy(courses, option).ToPagedList(pageIndex, pageSize);
                        foreach (Course s in coursesi)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Name:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + s.Name);
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Duration:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + s.Length_Hrs + "\n");
                        }                         
                        break;
                    case "2":
                        courses = (List<Course>)OrderBy(courses, option);
                        foreach (Course c in courses)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Name:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + c.Name);
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Duration:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + c.Length_Hrs);
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Id:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + c.Id + "\n");
                        }
                        break;
                }

            }
        }

        public IList<Course> OrderBy(IList<Course> courses, string choice)
        {
            using (var Ccontext = new AppContext())
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("1. Ascending;\n2. Descending;");
                        string op = Console.ReadLine();
                        if (op == "1")
                        {
                            courses = Ccontext.Course.OrderBy(Course => Course.Name).ToList();
                        }
                        else
                        {
                            courses = Ccontext.Course.OrderByDescending(Course => Course.Name).ToList();
                        }                        
                        break;
                    case "2":
                        Console.WriteLine("1. Ascending;\n2. Descending;");
                        string opt = Console.ReadLine();
                        if (opt == "1")
                        {
                            courses = Ccontext.Course.OrderBy(Course => Course.Id).ToList();
                        }
                        else
                        {
                            courses = Ccontext.Course.OrderByDescending(Course => Course.Id).ToList();
                        }
                        break;
                    case "3":
                        Console.WriteLine("1. Ascending;\n2. Descending;");
                        string opts = Console.ReadLine();
                        if (opts == "1")
                        {
                            courses = Ccontext.Course.OrderBy(Course => Course.Length_Hrs).ToList();
                        }
                        else
                        {
                            courses = Ccontext.Course.OrderByDescending(Course => Course.Length_Hrs).ToList();
                        }
                        break;
                    default:
                        courses = Ccontext.Course.OrderBy(Course => Course.Name).ToList();
                        break;
                }
            }

            return courses;
        }


        public bool ifCourseExists(string Name)
        {
            using (var context = new AppContext())
            {
                return context.Course.Any(Course => Course.Name == Name);
            }
        }

        public void findC(string str)
        {
            using (var context = new AppContext())
            {
                Course cs = context.Course.FirstOrDefault(Course =>  Course.Name.Contains(str) | Course.Length_Hrs == int.Parse(str));
                if (cs != null)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Name:");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" " + cs.Name);
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Duration:");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" " + cs.Length_Hrs);
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Id:");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" " + cs.Id);
                }
                else
                {
                    Console.WriteLine("Sorry, we couldn't find any results.");
                }
            }
        }

        public void Create(string name, int Len)
        {
            using (var context = new AppContext())
            {
                if (!ifCourseExists(name))
                {
                    var course = new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Length_Hrs = Len
                    };

                    context.Course.Add(course);
                    context.SaveChanges();
                }
            }
        }

        public void readCourse(string nume)
        {
            using (var context = new AppContext())
            {

                var entity = context.Course.FirstOrDefault(Course => Course.Name == nume);
                
                if (entity == null)
                {
                    Console.WriteLine("This course has not been registered, yet.");
                    return;
                }
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Name:");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" " + entity.Name);
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Duration:");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" " + entity.Length_Hrs);
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Id:");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" " + entity.Id);
            }
        }

        public void UpdateCourse(string prevName)
        {   
            using (var context = new AppContext())
            {
                //var name = new Course();
                if (!ifCourseExists(prevName))
                {
                    Console.WriteLine("This course is not registered");
                }
                else
                {
                    Course entity = context.Course.FirstOrDefault(Course => Course.Name == prevName);
                    Console.WriteLine("What would you like to update?\n1.Course name:\n2.Course' length:");
                    string caseSwitch = Console.ReadLine();
                    switch (caseSwitch)
                    {
                        case "1":
                            Console.WriteLine("Enter the new name of the course: ");
                            string newName = Console.ReadLine();
                            entity.Name = newName;
                            context.SaveChanges();
                            Console.WriteLine("Now, the course information is: ");
                            entity.readCourse(newName);
                            break;
                        case "2":
                            Console.WriteLine("Enter the new length of the course: ");
                            string newLen = Console.ReadLine();
                            entity.Length_Hrs = int.Parse(newLen);
                            context.SaveChanges();
                            Console.WriteLine("Now, the course information is: ");
                            entity.readCourse(prevName);
                            break;
                        default:
                            Console.WriteLine("Please try again and enter a valid field.");
                            break;
                    }
                }  
            } 
        }       

        public void deleteCourse(string nume)
        {
            using (var context = new AppContext())
            {
                var entity = context.Course.FirstOrDefault(Course => Course.Name == nume);
                //context.Course.Attach(entity);
                context.Course.Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
