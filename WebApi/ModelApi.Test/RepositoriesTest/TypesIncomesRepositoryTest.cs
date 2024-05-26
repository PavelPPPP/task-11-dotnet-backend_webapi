using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using ModelApi.Services.UnitOfWork;
using ModelApi.ValueObjects;

namespace ModelApi.Test.RepositoriesTest
{
    [TestClass]
    public class TypesIncomesRepositoryTest
    {
        private static SelfFinanceDbContext? _dbContext;
        private static IUnitOfWork? _unitOfWork;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SelfFinanceDbContext>();
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=SelfFinance;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
            _dbContext = new SelfFinanceDbContext(optionsBuilder.Options);
            _unitOfWork = new EFUnitOfWork(_dbContext);
        }

        [TestMethod]
        public void GetTypeIncomeById()
        {
            int id = 1;
            string name = "salary";
            string desc = "salary";
            DateTime createdDateTime = DateTime.Parse("2024-05-18 22:23:09.880");

            TypeIncome typeIncome = _unitOfWork!.TypesIncomes.GetById(id).Result;

            Assert.IsNotNull(typeIncome);
            Assert.AreEqual(id, typeIncome.Id);
            Assert.AreEqual(name, typeIncome.Name.Value);
            Assert.AreEqual(desc, typeIncome.Description!.Value);
            Assert.IsTrue(createdDateTime.ToString() == typeIncome.CreateDate.Value.ToString());
        }

        [TestMethod]
        public void GetNullTypeIncomeById()
        {
            int id = 200;

            TypeIncome typeIncome = _unitOfWork!.TypesIncomes.GetById(id).Result;

            Assert.IsNull(typeIncome);
        }

        [TestMethod]
        public void GetAllTypesIncomes()
        {
            IEnumerable<TypeIncome> listTypesIncomes = _unitOfWork!.TypesIncomes.GetAll().Result;

            Assert.IsNotNull(listTypesIncomes);
            Assert.IsTrue(listTypesIncomes.Count() >= 3);
        }

        [TestMethod]
        public void GetNullTypesIncomesWithFilter()
        {
            IEnumerable<TypeIncome> listTypesIncomes = _unitOfWork!.TypesIncomes.GetWithFilterById(100).Result;

            Assert.IsNotNull(listTypesIncomes);
            Assert.IsTrue(listTypesIncomes.Count() == 0);
        }

        [TestMethod]
        public void CreateTypeIncome()
        {
            TypeIncome insertedTypeIncome = null!;
            int? lastId = _unitOfWork!.TypesIncomes.GetAll().Result.Last().Id;
            TypeIncome createTypeIncome = new TypeIncome(new Name("test"), null);

            _unitOfWork.TypesIncomes.CreateAsync(createTypeIncome);
            _unitOfWork.Save();
            insertedTypeIncome = _unitOfWork.TypesIncomes.GetById(lastId + 1).Result;
            
            Assert.IsNotNull(insertedTypeIncome);
            Assert.IsTrue(insertedTypeIncome == createTypeIncome);
        }

        [TestMethod]
        public void UpdateTypeIncome()
        {
            TypeIncome editingTypeIncome = _unitOfWork!.TypesIncomes.GetById(3).Result;
            FreeText descBeforeEdit = editingTypeIncome.Description!;

            editingTypeIncome.Change(new Name("name3_edit1"), new FreeText("desc3_edit1"));
            _unitOfWork.TypesIncomes.Update(editingTypeIncome);
            _unitOfWork.Save().Wait();
            TypeIncome editedTypeIncome = _unitOfWork!.TypesIncomes.GetById(3).Result;
            FreeText descAfterEdit = editingTypeIncome.Description!;

            Assert.IsNotNull(editedTypeIncome);
            Assert.IsTrue(descAfterEdit != descBeforeEdit);
        }

        [TestMethod]
        public void DeleteTypeIncome()
        {
            TypeIncome deletingTypeIncome = _unitOfWork!.TypesIncomes.GetById(4).Result;

            _unitOfWork.TypesIncomes.Delete(deletingTypeIncome);
            _unitOfWork.Save().Wait();
            TypeIncome deletedTypeIncome = _unitOfWork.TypesIncomes.GetById(4).Result;

            Assert.IsNull(deletedTypeIncome);
        }
    }
}
