using ModelApi.ValueObjects;

namespace ModelApi.Entities
{
    public class TypeIncome : TypeBase
    {
        public List<Income>? Incomes { get; private set; }
        protected TypeIncome() : base() { }
        public TypeIncome(Name name, FreeText? description/*, DateOperation createDate, DateOperation? updateDate*/) : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            //CreateDate = createDate ?? throw new ArgumentNullException(nameof(createDate));
            //CreateDate = createDate;
            //UpdateDate = updateDate;
            Description = description;
        }
    }
}
