using System;
using System.Collections.Generic;
using System.Windows.Automation.Peers;
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
        public bool Add(Student student, string groupName)
        {
            if (student is null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentException("Invalid group name", nameof(groupName));
            }

            Group group = _groups.Get(groupName);

            if (group is null)
            {
                group = new Group
                {
                    Name = groupName
                };

                _groups.Add(group);
            }

            group.Students.Add(student);
            _students.Add(student);
            return true;
        }

        public void Update(Student student) => _students.Update(student.Id, student);
    }
}
