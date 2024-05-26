using ModelApi.Entities;

namespace ModelApi.Test.EntitiesTest
{
    [TestClass]
    public class TypesExpensesTest
    {
        [TestMethod]
        public void WhenTheRequiredParamsAreNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new TypeExpense(null!, null));
            //Assert.ThrowsException<ArgumentNullException>(() => new TypeExpense(null, null, new ValueObjects.DateOperation(), null));
            //Assert.ThrowsException<ArgumentNullException>(() => new TypeExpense(new ValueObjects.Name("name"), null, null, null));
        }
    }
}
