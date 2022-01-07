using System;
using Xunit;

namespace TodoIt.Test
{
    public class TestPerson
    {
        [Theory]
        [InlineData(0, "Ada", "Lovelace")]
        [InlineData(1, "Albert", "Einstein")]
        public void TestGetSetProperties(
            int id, string fname, string lname)
        {
            Person person = new Person(id, fname, lname);

            Assert.Equal(id, person.PersonId);
            Assert.Equal(fname, person.FirstName);
            Assert.Equal(lname, person.LastName);
        }

        [Theory]
        [InlineData(0, "", "Lovelace")]
        [InlineData(1, "Albert", "")]
        [InlineData(0, null, "Lovelace")]
        [InlineData(1, "Albert", null)]
        public void TestExceptions(
            int id, string fname, string lname)
        {
            Assert.Throws<ArgumentException>(() => new Person(id, fname, lname));
        }
    }
}
