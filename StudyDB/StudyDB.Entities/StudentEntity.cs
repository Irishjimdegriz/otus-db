
namespace StudyDB.Entities
{
    public class StudentEntity
    {
        public virtual int StudentId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MiddleName { get; set; }
    }
}
