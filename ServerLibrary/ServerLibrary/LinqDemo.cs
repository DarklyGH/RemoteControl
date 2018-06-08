using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary
{
    class LinqDemo
    {
        class LinqElements { public string Name { set; get; } public int Id { set; get; } public string OtherValue { set; get; } }

        private List<LinqElements> ListDemo = new List<LinqElements>
        {
            new LinqElements{ Name = "Frank", Id = 1, OtherValue = "This is a second value" },
            new LinqElements{ Name = "Bill", Id = 2, OtherValue = "This is a third value" },
            new LinqElements{ Name = "Fred", Id = 3, OtherValue = "This is a fourth value" },
            new LinqElements{ Name = "Jeff", Id = 4, OtherValue = "This is a fifth value" },
            new LinqElements{ Name = "Dan", Id = 5, OtherValue = "This is a sixth value" },
            new LinqElements{ Name = "Billy", Id = 6, OtherValue = "This is a seven value" },
            new LinqElements{ Name = "Fredrick", Id = 7, OtherValue = "This is a eighth value" },
        };

        [Test]
        public void FunctionDemo()
        {
            var where = ListDemo.Where(linqElement => linqElement.Id <= 4 || linqElement.Id == 7);
            var select = ListDemo.Select(linqElement => linqElement.OtherValue);

        }
    }
}
