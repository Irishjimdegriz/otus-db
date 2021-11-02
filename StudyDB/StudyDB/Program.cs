using NHibernate;
using StudyDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyDB
{
    public class Program
    {
        static void Main(string[] args)
        {
            DisplayAllCourses();
            Console.WriteLine();
            DisplayAllStudents();
            DisplayAllTeachers();
            Console.WriteLine();
            InsertTeacher();
            Console.WriteLine();
            DisplayAllTeachers();
        }

        static void InsertTeacher()
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            var teacher = new Teacher();
            var isValueCorrect = false;

            while (!isValueCorrect)
            {
                Console.WriteLine("Введите фамилию преподавателя:");
                var value = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(value) && value.All(char.IsLetter))
                {
                    teacher.LastName = value;
                    isValueCorrect = true;
                }
            }

            isValueCorrect = false;
            while (!isValueCorrect)
            {
                Console.WriteLine("Введите имя преподавателя:");
                var value = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(value) && value.All(char.IsLetter))
                {
                    teacher.FirstName = value;
                    isValueCorrect = true;
                }
            }

            isValueCorrect = false;
            while (!isValueCorrect)
            {
                Console.WriteLine("Введите отчество преподавателя:");
                var value = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(value) && value.All(char.IsLetter))
                {
                    teacher.MiddleName = value;
                    isValueCorrect = true;
                }
            }

            isValueCorrect = false;
            Course selectedCourse = null;
            while (!isValueCorrect)
            {
                Console.WriteLine("Введите номер курса, который курирует преподаватель:");
                DisplayAllCourses();

                var value = Console.ReadLine();

                if (int.TryParse(value, out var parseResult))
                {
                    selectedCourse = session.Query<Course>().FirstOrDefault(x => x.Course_Id == parseResult);

                    if (selectedCourse != null)
                        isValueCorrect = true;
                }
            }

            using (ITransaction tx = session.BeginTransaction())
            {
                selectedCourse.AddTeacher(teacher);
                session.SaveOrUpdate(selectedCourse);

                tx.Commit();
            }
        }

        static void DisplayAllCourses()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    var courses = session.Query<Course>().ToList();
                    Console.WriteLine("Список курсов:");

                    foreach (var f in courses)
                    {
                        Console.WriteLine($"{f.Course_Id}. {f.CourseName}");
                    }
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
        }

        static void DisplayAllStudents()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    var students = session.Query<Student>().ToList();
                    Console.WriteLine("Список студентов:");

                    foreach (var s in students)
                    {
                        Console.WriteLine($"{s.LastName} {s.FirstName} {s.MiddleName}");
                        Console.WriteLine("Его курсы:");

                        s.CoursesEnrolledIn.ToList().ForEach(x => Console.WriteLine($"{x.CourseName}"));
                        Console.WriteLine();
                    }
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
        }

        static void DisplayAllTeachers()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    var teachers = session.Query<Teacher>().ToList();
                    Console.WriteLine("Список преподавателей:");

                    foreach (var t in teachers)
                    {
                        Console.WriteLine($"{t.LastName} {t.FirstName} {t.MiddleName} курирует {t.Course.CourseName}");
                    }
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
        }
    }
}
