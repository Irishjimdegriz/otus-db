
using System.Collections.Generic;

namespace StudyDB.Model
{
    public class Student
    {
        public virtual int Student_Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual IList<Course> CoursesEnrolledIn { get; protected set; }
        public Student()
        {
            CoursesEnrolledIn = new List<Course>();
        }
    }
}
