using CV19.Models.Deanery;
using CV19.Services.Base;
using System.Linq;

namespace CV19.Services.Deanery
{
    internal class GroupRepository : Repository<Group>
    {
        public GroupRepository() : base(TestData.Groups)
        {
            
        }

        public Group Get(string groupName) => GetAll().FirstOrDefault(g => g.Name == groupName);

        protected override void Update(Group source, Group destination) => 
            destination = (Group)source.Clone();
    }
}
