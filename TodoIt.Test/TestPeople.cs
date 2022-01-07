using System;
using Xunit;

namespace TodoIt.Test
{
    public class TestPeople
    {
        private readonly People people;

        public TestPeople()
        {
            PersonSequencer.Reset();
            people = new People();
            people.Clear();

        }

        private void AddPersonsToPeople(People peoplePopulation, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                peoplePopulation.CreatePerson($"FirstName{i}", $"LastName{i}");
            }
        }

        [Fact]
        public void TestPeopleInitializedToZeroLength()
        {
            people.Clear();

            Assert.Empty(people.FindAll());
        }

        [Theory]
        [InlineData(10)]
        public void TestPeopleSize(int iterations)
        {
            People someOtherPeople = new People();

            AddPersonsToPeople(people, iterations);
            AddPersonsToPeople(someOtherPeople, iterations);

            Assert.Equal(iterations * 2, people.Size());
        }

        [Theory]
        [InlineData(2)]
        public void TestPeopleFindById(int iterations)
        {
            AddPersonsToPeople(people, iterations);

            for (int i = 0; i < iterations; i++)
            {
                Assert.Equal($"FirstName{i}", people.FindById(i).FirstName);
                Assert.Equal($"LastName{i}", people.FindById(i).LastName);
            }
        }

        [Fact]
        public void TestFindByIdThrowsException()
        {
            Assert.Throws<Exception>(() => people.FindById(100));
        }

        [Theory]
        [InlineData(2)]
        public void TestPeopleCreatePerson(int iterations)
        {
            AddPersonsToPeople(people, iterations);

            for (int i = 0; i < iterations; i++)
            {
                Assert.Equal($"FirstName{i}", people.FindAll()[i].FirstName);
                Assert.Equal($"LastName{i}", people.FindAll()[i].LastName);
            }
        }

        [Theory]
        [InlineData(5, 3)]
        [InlineData(10, 7)]
        public void TestPeopleRemovePerson(int iterations, int personToRemoveId)
        {
            AddPersonsToPeople(people, iterations);
            people.RemovePerson(people.FindById(personToRemoveId));

            Assert.Throws<Exception>(() => people.FindById(personToRemoveId));
        }
    }
}
