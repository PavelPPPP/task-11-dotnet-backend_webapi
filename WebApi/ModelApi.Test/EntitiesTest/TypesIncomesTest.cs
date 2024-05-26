using ModelApi.Entities;

namespace ModelApi.Test.EntitiesTest
{
    [TestClass]
    public class TypesIncomesTest
    {
        [TestMethod]
        public void WhenTheRequiredParamsAreNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new TypeIncome(null!, null/*, null, null*/));
            //Assert.ThrowsException<ArgumentNullException>(() => new TypeIncome(null, null, new ValueObjects.DateOperation(), null));
            //Assert.ThrowsException<ArgumentNullException>(() => new TypeIncome(new ValueObjects.Name("name"), null, null, null));
        }
    }
}