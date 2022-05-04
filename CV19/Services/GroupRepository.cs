using CV19.Models.Deanery;
using CV19.Services.Base;

namespace CV19.Services
{
    internal class GroupRepository : Repository<Group>
    {
        protected override void Update(Group source, Group destination) => 
            destination = (Group)source.Clone();
    }
}
