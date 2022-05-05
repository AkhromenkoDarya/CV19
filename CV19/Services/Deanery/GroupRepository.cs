using CV19.Models.Deanery;
using CV19.Services.Base;

namespace CV19.Services.Deanery
{
    internal class GroupRepository : Repository<Group>
    {
        public GroupRepository() : base(TestData.Groups)
        {
            
        }

        protected override void Update(Group source, Group destination) => 
            destination = (Group)source.Clone();
    }
}
