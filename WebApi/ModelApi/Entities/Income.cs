using ModelApi.ValueObjects;

namespace ModelApi.Entities
{
    public class Income : Ballanse
    {
        public TypeIncome? TypeIncome { get; private set; }
        
        protected Income() : base() { }
        public Income(Amount amount, int? typeId, FreeText? comments) : this()
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
