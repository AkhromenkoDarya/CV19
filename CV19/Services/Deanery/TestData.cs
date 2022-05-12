using CV19.Models.Deanery;
using System;
using System.Linq;

namespace CV19.Services.Deanery
{
    internal static class TestData
    {
        private const int StudentCount = 10;

        public static Group[] Groups { get; } = Enumerable
            .Range(1, 10)
            .Select(i => new Group
            {
                Name = $"Group {i}"
            })
            .ToArray();

        public static Student[] Students { get; } = CreateStudents(Groups);

        private static Student[] CreateStudents(Group[] groups)
        {
            var random = new Random();
            var index = 1;

            foreach (Group group in groups)
            {
                for (var i = 0; i < StudentCount; i++)
                {
                    group.Students.Add(new Student
                    {
                        Name = $"Name {index}",
                        Surname = $"Surname {index}",
                        Patronymic = $"Patronymic {index++}",
                        Birthday = DateTime.Now.Subtract(TimeSpan.FromDays(300 * random
                            .Next(19, 30))),
                        Rating = random.NextDouble() * 100
                    });
                }
            }

            return groups.SelectMany(g => g.Students).ToArray();
        }
    }
}
