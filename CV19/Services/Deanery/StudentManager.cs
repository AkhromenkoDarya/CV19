using System.Collections.Generic;
using CV19.Models.Deanery;

namespace CV19.Services.Deanery
{
    internal class StudentManager
    {
        private readonly GroupRepository _groups;

        private readonly StudentRepository _students;

        public IEnumerable<Group> Groups => _groups.GetAll();

        public IEnumerable<Student> Students => _students.GetAll();

        public StudentManager(GroupRepository groups, StudentRepository students)
        {
            _groups = groups;
            _students = students;
        }
    }
}
