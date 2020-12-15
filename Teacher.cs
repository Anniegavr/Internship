using System;
using System.Linq;
using System.Collections.Generic;
using PagedList;

namespace ConsoleApp1
{
    public  class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }

        public void renderTeach(string option)
        {
            Console.Clear();

            using (var Tcontext = new AppContext())
            {
                var teachers = Tcontext.Teachers.ToList();
                Console.WriteLine("Use pagination?\n1. Yes\n2. No");
                string op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        Console.WriteLine("You are creating pages.");
                        Console.Write("# of records on this page: ");
                        int pageSize = int.Parse(Console.ReadLine());
                        Console.Write("Display page #: ");
                        int pageIndex = int.Parse(Console.ReadLine());
                        IPagedList<Teacher> teachersi = OrderBy(teachers, option).ToPagedList(pageIndex, pageSize);
                        foreach (Teacher t in teachers)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Name:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + t.Name);
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Email:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + t.Email);
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Id:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + t.Id + "\n");
                        }
                        break;
                    case "2":
                        teachers = OrderBy(teachers, option);
                        foreach (Teacher t in teachers)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Name:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + t.Name);
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Email:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + t.Email);
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Id:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + t.Id + "\n");
                        }
                        break;
                }
                
            }
        }

        public List<Teacher> OrderBy(List<Teacher> teachers, string choice)
        {
            using (var Tcontext = new AppContext())
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("1. Ascending;\n2. Descending;");
                        string op = Console.ReadLine();
                        if (op == "1")
                        {
                            teachers = Tcontext.Teachers.OrderBy(Teacher => Teacher.Name).ToList();
                        }
                        else
                        {
                            teachers = Tcontext.Teachers.OrderByDescending(Teacher => Teacher.Name).ToList();
                        }                        
                        break;
                    case "2":
                        Console.WriteLine("1. Ascending;\n2. Descending;");
                        string opts = Console.ReadLine();
                        if (opts == "1")
                        {
                            teachers = Tcontext.Teachers.OrderBy(Teacher => Teacher.Id).ToList();
                        }
                        else
                        {
                            teachers = Tcontext.Teachers.OrderByDescending(Teacher => Teacher.Id).ToList();
                        }
                        break;
                    case "3":
                        Console.WriteLine("1. Ascending;\n2. Descending;");
                        string opt = Console.ReadLine();
                        if (opt == "1")
                        {
                            teachers = Tcontext.Teachers.OrderBy(Teacher => Teacher.Email).ToList();
                        }
                        else
                        {
                            teachers = Tcontext.Teachers.OrderByDescending(Teacher => Teacher.Email).ToList();
                        }
                        break;
                    default:
                        teachers = Tcontext.Teachers.OrderBy(Teacher => Teacher.Name).ToList();
                        break;
                }                
            }

            return teachers;
        }

        public bool ifteacherExists(string Name, string Email)
        {
            using (var Tcontext = new AppContext())
            {
                return Tcontext.Teachers.Any(Teachers => Teachers.Name == Name && Teachers.Email == Email);
            }
        }

        public void findT(string str)
        {
            using (var Tcontext = new AppContext())
            {
                Teacher tt = Tcontext.Teachers.FirstOrDefault(Teacher => Teacher.Name.Contains(str) | Teacher.Email.Contains(str));
                if (tt != null)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Name:");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" " + tt.Name);
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Email:");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" " + tt.Email);
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Id:");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" " + tt.Id);
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Password:");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" " + tt.Password);
                }
                else
                {
                    Console.WriteLine("Sorry, we couldn't find any results.");
                }
            }
        }

        public void Create(string name, string email, string pass, string photo)
        {
            using (var Tcontext = new AppContext())
            {
                if (!ifteacherExists(name, email))
                {
                    var teacher = new Teacher
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Email = email,
                        Password = pass,
                        Photo = photo
                    };

                    Tcontext.Teachers.Add(teacher);
                    Tcontext.SaveChanges();
                }
            }
        }

        public void readTeacher(string nume, string email)
        {
            using (var Tcontext = new AppContext())
            {
                var TeacherEntity = Tcontext.Teachers.FirstOrDefault(Teacher => Teacher.Name == nume && Teacher.Email == email);
                if (TeacherEntity == null)
                {
                    Console.WriteLine("This teacher does not exist in our database.");
                    return;
                }
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Name:");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" " + TeacherEntity.Name);
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Email:");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" " + TeacherEntity.Email);
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Id:");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" " + TeacherEntity.Id);
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Password:");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" "+ TeacherEntity.Password);
            }
        }

        public void UpdateTeacher(String prevName, string email)
        {
            using (var Tcontext = new AppContext())
            {
                //var teach = new Teacher();                
                var TeacherEntity = Tcontext.Teachers.FirstOrDefault(Teacher => Teacher.Name == prevName && Teacher.Email == email);
                Console.WriteLine("What would you like to modify?");
                Console.WriteLine("1. Teacher's name;\n2.Teacher's email;\n3. Teacher's password;\n");
                string caseSwitch = Console.ReadLine();
                switch (caseSwitch)
                {
                    case "1":
                        Console.WriteLine("Enter the new name: ");
                        string newName = Console.ReadLine();
                        TeacherEntity.Name = newName;
                        Tcontext.SaveChanges();
                        break;
                    case "2":
                        Console.WriteLine("Enter the new email: ");
                        string newEmail = Console.ReadLine();
                        TeacherEntity.Email = newEmail;
                        Tcontext.SaveChanges();
                        break;
                    case "3":
                        Console.WriteLine("Enter the new password: ");
                        string newPass = Console.ReadLine();
                        Tcontext.SaveChanges();
                        Console.WriteLine("The new password is: " + newPass);
                        Console.WriteLine("Confirm changes?\n1. Yes\n2.No");
                        string yesno = Console.ReadLine();
                        switch (yesno)
                        {
                            case "1":
                                TeacherEntity.Password = newPass;
                                Tcontext.SaveChanges();
                                Console.WriteLine("Password successfully updated.");
                                return;
                            case "2":
                                break;
                            default:
                                break;
                        }
                        break;           
                    default:
                        Console.WriteLine("Please try again and enter a valid field: ");
                        break;
                }                        
                Console.WriteLine("Now, the teacher's information is: ");
                TeacherEntity.readTeacher(Name, Email);
            }
        }

        public void deleteTeacher(string nume, string email)
        {
            using (var Tcontext = new AppContext())
            {
                var TeacherEntity = Tcontext.Teachers.FirstOrDefault(Teachers => Teachers.Name == nume && Teachers.Email == email);
                //context.teacher.Attach(entity);
                Tcontext.Teachers.Remove(TeacherEntity);
                Tcontext.SaveChanges();
            }
        }
    }
}
