using System.Collections.Generic;

namespace CV19.Models.Deanery
{
    internal class Group
    {
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }

        public string Description { get; set; }
    }
}
