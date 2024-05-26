using ModelApi.ValueObjects;

namespace ModelApi.Entities
{
    public class Expense : Ballanse
    {
        public TypeExpense? TypeExpense { get; private set; }

        protected Expense() : base() { }
        public Expense(Amount amount, int? typeId, FreeText? comments) : this()
        {
            ValidateArguments(amount, typeId);
            //if (createDate is null) throw new ArgumentNullException(nameof(createDate));

            Amount = amount;
            //CreateDate = createDate;
            //UpdateDate = updateDate;
            TypeId = typeId;
            Comments = comments;
        }
    }
}
