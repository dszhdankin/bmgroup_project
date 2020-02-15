using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Version_1._0.Model;

namespace UnitTestFront
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ModelEvent a = new ModelEvent();
            a.get("http://localhost:8080/");
            a.get("");

            var mas = a.jsonEventParse(str);

            Assert.AreEqual(mas.Count, 2);
            Assert.AreEqual(mas[0].Description, "nothing");
            Assert.AreEqual(mas[1].Description, "Go home");
            Assert.AreEqual(mas[0].Title, "Nothing");
            Assert.AreEqual(mas[1].Title, "Go home");
            Assert.AreEqual(mas[0].EventId, 1);
            Assert.AreEqual(mas[1].EventId, 3);

        }
        string str = "[{\"EventId\":1,\"Description\":\"nothing\",\"Title\":\"Nothing\"," +
            "\"StartTime\":\"\\/Date(-2208962400000)\\/\",\"Photo\":null}," +
            "{\"EventId\":3,\"Description\":\"Go home\",\"Title\":\"Go home\"," +
            "\"StartTime\":\"\\/Date(1607720400000)\\/\",\"Photo\":null}]";
    }
}
