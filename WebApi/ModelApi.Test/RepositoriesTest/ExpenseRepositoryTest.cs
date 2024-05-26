using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using ModelApi.Services.UnitOfWork;
using ModelApi.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApi.Test.RepositoriesTest
{
    [TestClass]
    public class ExpenseRepositoryTest
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

        //[TestMethod]
        //public void GetNullAllExpenses()
        //{
        //    IEnumerable<Expense>? listIncomes = _unitOfWork?.Expenses.GetAll().Result;

        //    Assert.IsNotNull(listIncomes);
        //    Assert.IsTrue(listIncomes.Count() == 0);
        //}

        [TestMethod]
        public void GetNullExpenseById()
        {
            int id = 200;

            Expense? expense = _unitOfWork?.Expenses.GetById(id).Result;

            Assert.IsNull(expense);
        }

        [TestMethod]
        public void GetAllExpenses()
        {
            IEnumerable<Expense>? listExpenses = _unitOfWork?.Expenses.GetAll().Result;

            Assert.IsNotNull(listExpenses);
            Assert.IsTrue(listExpenses.Count() >= 0);
        }

        [TestMethod]
        public void GetExpenseById()
        {
            int id = 1;
            double amount = 2500;
            int typeId = 1;
            DateTime createdDateTime = DateTime.Parse("2024-03-15 00:00:00.000");

            Expense? expense = _unitOfWork?.Expenses.GetById(id).Result;

            Assert.IsNotNull(expense);
            Assert.AreEqual(id, expense.Id);
            Assert.AreEqual(amount, expense.Amount.Value);
            Assert.AreEqual(typeId, expense.TypeId);
            Assert.IsTrue(createdDateTime.ToString() == expense.CreateDate.Value.ToString());
        }

        [TestMethod]
        public void CreateExpense()
        {
            int? lastId = _unitOfWork?.Expenses.GetAll().Result.Last().Id;
            Expense? createExpense = new Expense(new Amount(1458.54), 4, null);

            _unitOfWork?.Expenses.CreateAsync(createExpense);
            _unitOfWork?.Save();
            Expense? insertedExpense = _unitOfWork?.Expenses.GetById(lastId + 1).Result;

            Assert.IsNotNull(insertedExpense);
            Assert.IsTrue(insertedExpense == createExpense);
        }

        [TestMethod]
        public void UpdateExpense()
        {
            Expense? editingExpense = _unitOfWork?.Expenses.GetById(13).Result;
            FreeText? commentBeforeEdit = editingExpense?.Comments;

            editingExpense?.Change(null!, null, new FreeText("comment2_update1"));
            _unitOfWork?.Expenses.Update(editingExpense!);
            _unitOfWork?.Save().Wait();
            Expense? editedExpense = _unitOfWork?.Expenses.GetById(13).Result;
            FreeText? commentAfterEdit = editedExpense?.Comments;

            Assert.IsNotNull(editedExpense);
            Assert.IsTrue(commentAfterEdit! != commentBeforeEdit!);
        }

        [TestMethod]
        public void DeleteExpense()
        {
            Expense? deletingExpense = _unitOfWork?.Expenses.GetById(13).Result;

            _unitOfWork?.Expenses.Delete(deletingExpense!);
            _unitOfWork?.Save().Wait();
            Expense? deletedExpense = _unitOfWork?.Expenses.GetById(13).Result;

            Assert.IsNull(deletedExpense);
        }

        [TestMethod]
        public void GetSymYesterday_Test()
        {
            double? expected = 1000;

            double? actual = _unitOfWork?.Expenses.GetSumYesterday().Result;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetSymByPeriod_Test()
        {
            double? expected = 5200;

            double? actual = _unitOfWork?.Expenses.GetSumByPeriod(DateTime.Parse("2024-03-01"), DateTime.Parse("2024-03-31")).Result;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetByYesterdayWithDetail_Test()
        {
            int? expectedId = 11;
            int? expectedTypeId = 4;
            string? expectedNameType = "other";

            IEnumerable<Expense>? expenses = _unitOfWork?.Expenses.GetByYesterdayWithDetail().Result;
            int? count = expenses?.Count();
            Expense? firstItem = expenses?.FirstOrDefault();

            int? actualId = firstItem?.Id;
            int? actualTypeId = firstItem?.TypeId;
            string? actualNameType = firstItem?.TypeExpense?.Name.Value;

            Assert.IsNotNull(expenses);
            Assert.IsTrue(count > 0);
            Assert.IsNotNull(firstItem);
            Assert.IsNotNull(firstItem.TypeExpense);

            Assert.AreEqual(expectedId, actualId);
            Assert.AreEqual(expectedTypeId, actualTypeId);
            Assert.AreEqual(expectedNameType, actualNameType);
        }

        [TestMethod]
        public void GetByPeriodWithDetail_Test()
        {
            int? expectedFirstId = 1;
            int? expectedFirstTypeId = 1;
            string? expectedFirstNameType = "communal payments";

            int? expectedLastId = 5;
            int? expectedLastTypeId = 4;
            string? expectedLastNameType = "other";

            IEnumerable<Expense>? expenses = _unitOfWork?.Expenses.GetByPeriodWithDetail(DateTime.Parse("2024-03-01"), DateTime.Parse("2024-03-31")).Result;
            int? count = expenses?.Count();

            Expense? firstItem = expenses?.FirstOrDefault();
            Expense? lastItem = expenses?.LastOrDefault();

            int? actualFirstId = firstItem?.Id;
            int? actualFirstTypeId = firstItem?.TypeId;
            string? actualFirstNameType = firstItem?.TypeExpense?.Name.Value;

            int? actualLastId = lastItem?.Id;
            int? actualLastTypeId = lastItem?.TypeId;
            string? actualLastNameType = lastItem?.TypeExpense?.Name.Value;

            Assert.IsNotNull(expenses);
            Assert.IsTrue(count > 0);

            Assert.IsNotNull(firstItem);
            Assert.IsNotNull(firstItem.TypeExpense);

            Assert.IsNotNull(lastItem);
            Assert.IsNotNull(lastItem.TypeExpense);

            Assert.AreEqual(expectedFirstId, actualFirstId);
            Assert.AreEqual(expectedFirstTypeId, actualFirstTypeId);
            Assert.AreEqual(expectedFirstNameType, actualFirstNameType);

            Assert.AreEqual(expectedLastId, actualLastId);
            Assert.AreEqual(expectedLastTypeId, actualLastTypeId);
            Assert.AreEqual(expectedLastNameType, actualLastNameType);
        }
    }
}
