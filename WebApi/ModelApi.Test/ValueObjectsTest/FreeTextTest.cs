using ModelApi.ValueObjects;

namespace ModelApi.Test.ValueObjectsTest
{
    [TestClass]
    public class FreeTextTest
    {
        [TestMethod]
        public void WhenTheRequiredConstructorParamsAreNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FreeText(null));
        }
    }
}
