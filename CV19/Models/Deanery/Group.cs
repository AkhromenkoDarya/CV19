using CV19.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace CV19.Models.Deanery
{
    internal class Group : IEntity, ICloneable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Student> Students { get; set; } = new List<Student>();

        public string Description { get; set; }

        public Group()
        {

        }

        public Group(string name, string description = null)
        {
            Name = name;
            Description = description;
        }

        public object Clone() => new Group(Name, Description);
    }
}
