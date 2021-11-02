using FluentNHibernate.Mapping;
using StudyDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyDB.Mappings
{
	public class TeacherMap : ClassMap<Teacher>
	{
		public TeacherMap()
		{
			Id(x => x.Teacher_Id).GeneratedBy.SequenceIdentity("TEACHER_ID_SEQ");
			Map(x => x.FirstName);
			Map(x => x.LastName);
			Map(x => x.MiddleName);
			References(x => x.Course);
			Table("Teacher");
		}
	}
}
