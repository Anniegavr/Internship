using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PagedList;
using PagedList.Mvc;



namespace ConsoleApp1
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int GroupId { get; set; }
        public string Photo { get; set; }

        public void renderStuds(string option)
        {
            Console.Clear();
            
            using (var Scontext = new AppContext())
            {
                var students = Scontext.Students.ToList();                
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
                        IPagedList<Student> studentsi = OrderBy(students, option).ToPagedList(pageIndex, pageSize);
                        foreach (Student s in studentsi)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Name:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + s.Name);
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Email:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + s.Email + "\n");
                        }                        
                        break;
                    case "2":                        
                        students = (List<Student>)OrderBy(students, option);
                        foreach (Student s in students)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Name:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + s.Name);
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Email:");
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" " + s.Email + "\n");
                        }
                        break;                        
                }                
            }
        }

        public IList<Student> OrderBy(IList<Student> students, string choice)
        {
            using (var Scontext = new AppContext())
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("1. Ascending;\n2. Descending;");
                        string op = Console.ReadLine();
                        if (op == "1")
                        {
                            students = Scontext.Students.OrderBy(Student => Student.Name).ToList();
                        }
                        else
                        {
                            students = Scontext.Students.OrderByDescending(Student => Student.Name).ToList();
                        }
                        
                        break;
                    case "2":
                        Console.WriteLine("1. Ascending;\n2. Descending;");
                        string opt = Console.ReadLine();
                        if (opt == "1")
                        {
                            students = Scontext.Students.OrderBy(Student => Student.Id).ToList();
                        }
                        else
                        {
                            students = Scontext.Students.OrderByDescending(Student => Student.Id).ToList();
                        }                        
                        break;
                    case "3":
                        Console.WriteLine("1. Ascending;\n2. Descending;");
                        string opts = Console.ReadLine();
                        if (opts == "1")
                        {
                            students = Scontext.Students.OrderBy(Student => Student.Email).ToList();
                        }
                        else
                        {
                            students = Scontext.Students.OrderByDescending(Student => Student.Email).ToList();
                        }
                        break;
                    default:
                        students = Scontext.Students.OrderBy(Student => Student.Name).ToList();
                        break;
                }                
            }

            return students;
        }

        public bool ifStudentExists(string Name, string email)
        {
            using (var Tcontext = new AppContext())
            {
                return Tcontext.Students.Any(Students => Students.Name == Name && Students.Email == email);
            }
        }

        public void findS(string str)
        {
            using (var Scontext = new AppContext())
            {
                Student st =  Scontext.Students.FirstOrDefault(Student => Student.Name.Contains(str) | Student.Email.Contains(str));
                if (st != null)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Name:");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" " + st.Name);
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Email:");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" " + st.Email);
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Id:");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" " + st.Id);
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Group:");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" " + st.GroupId);
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Password:");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" " + st.Password);
                }
                else
                {
                    Console.WriteLine("Sorry, we couldn't find any results.");
                }                
            }
        }

        public void Create(string name, string email, string pass, int Group, string photo)
        {
            using (var Stcontext = new AppContext())
            {
                
                
                if (!ifStudentExists(name, email))
                {
                    var student = new Student
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Email = email,
                        Password = pass,
                        GroupId = Group,
                        Photo = photo
                    };

                    Stcontext.Students.Add(student);
                    
                    Stcontext.SaveChanges();
                }
            }
        }

        public void readStudent(string nume, string email)
        {
            using (var Stcontext = new AppContext())
            {
                var StEntity = Stcontext.Students.FirstOrDefault(Students => Students.Name == nume && Students.Email == email);
                if (StEntity == null)
                {
                    Console.WriteLine("This student does not exist in our database.");
                    return;
                }
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Name:");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" " + StEntity.Name);
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Email:");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" " +StEntity.Email);
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Id:");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" " + StEntity.Id);
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Group:");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" " + StEntity.GroupId);
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Password:");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" " + StEntity.Password);
            }
        }

        public void UpdateStudent(String prevName, string email)
        {
            using (var Stcontext = new AppContext())
            {
                var name = new Student();

                var StEntity = Stcontext.Students.FirstOrDefault(Students => Students.Name == prevName && Students.Email == email);
                if (!ifStudentExists(prevName, email))
                {
                    Console.WriteLine("This student is not registered");
                }
                else
                {
                    Console.WriteLine("What would you like to modify?");
                    Console.WriteLine("1. Student's name;\n2.Student's email;\n3. Student's password;");
                    string caseSwitch = Console.ReadLine();
                    switch (caseSwitch)
                    {
                        case "1":
                            Console.WriteLine("Enter the new name: ");
                            string newName = Console.ReadLine();
                            StEntity.Name = newName;
                            Stcontext.SaveChanges();
                            Console.WriteLine("Now, the student's information is: ");
                            StEntity.readStudent(newName, Email);
                            break;
                        case "2":
                            Console.WriteLine("Enter the new email: ");
                            string newEmail = Console.ReadLine();
                            StEntity.Email = newEmail;
                            Stcontext.SaveChanges();
                            Console.WriteLine("Now, the student's information is: ");
                            StEntity.readStudent(prevName, Email);
                            break;
                        case "3":
                            Console.WriteLine("Enter the new password: ");
                            string newPass = Console.ReadLine();
                            Console.WriteLine("The new password is: " + newPass);
                            Console.WriteLine("Confirm changes?\n1. Yes\n2.No");
                            string yesno = Console.ReadLine();
                            switch (yesno)
                            {
                                case "1":
                                    StEntity.Password = newPass;
                                    Stcontext.SaveChanges();
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
                }                               
            }
        }

        public void deleteStudent(string nume, string email)
        {
            using (var Scontext = new AppContext())
            {
                var StEntity = Scontext.Students.FirstOrDefault(Students => Students.Name == nume && Students.Email == email);                
                Scontext.Students.Remove(StEntity);
                Scontext.SaveChanges();
            }
        }

    }
}
