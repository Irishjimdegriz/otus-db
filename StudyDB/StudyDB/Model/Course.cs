using System.Collections.Generic;

namespace StudyDB.Model
{
    public class Course
    {
        public virtual int Course_Id { get; set; }
        public virtual string CourseName { get; set; }
        public virtual IList<Student> Students { get; set; }
        public virtual IList<Teacher> Staff { get; set; }

        public Course()
        {
            Students = new List<Student>();
            Staff = new List<Teacher>();
        }

        public virtual void AddProduct(Student student)
        {
            student.CoursesEnrolledIn.Add(this);
            Students.Add(student);
        }

        public virtual void AddTeacher(Teacher teacher)
        {
            teacher.Course = this;
            Staff.Add(teacher);
        }
    }
}
