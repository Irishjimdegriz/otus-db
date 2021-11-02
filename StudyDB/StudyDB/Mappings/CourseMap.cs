using FluentNHibernate.Mapping;
using StudyDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyDB.Mappings
{
    public class CourseMap: ClassMap<Course>
    {
        public CourseMap()
        {
            Id(x => x.Course_Id).GeneratedBy.SequenceIdentity("COURSE_ID_SEQ");
            Map(x => x.CourseName);
            Table("course");

            HasMany(x => x.Staff)
              .Inverse()
              .Cascade.All();
            HasManyToMany(x => x.Students)
              .Cascade.All()
              .Table("StudentCourse");
        }
    }
}
