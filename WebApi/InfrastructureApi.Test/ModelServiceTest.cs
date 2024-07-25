using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfrastructureApi.Services;

namespace InfrastructureApi.Test
{
    [TestClass]
    public class ModelServiceTest
    {
        [TestMethod]
        public void ConstructorModelService_ThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new TypeIncomeService(null!));
        }
    }
}
