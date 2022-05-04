using CV19.Models.Interfaces;
using System;

namespace CV19.Models.Deanery
{
    internal class Student : IEntity, ICloneable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime Birthday { get; set; }
        
        public double Rating { get; set; }

        public string Description { get; set; }

        public Student()
        {

        }

        public Student(string name, string surname, string patronymic, DateTime birthday, 
            double rating)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Birthday = birthday;
            Rating = rating;
        }

        public object Clone() => new Student(Name, Surname, Patronymic, Birthday, Rating);
    }
}
