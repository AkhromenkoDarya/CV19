using CV19.Models.Deanery;
using CV19.Services.Base;

namespace CV19.Services
{
    internal class StudentRepository : Repository<Student>
    {
        protected override void Update(Student source, Student destination) => 
            destination = (Student)source.Clone();
    }
}
