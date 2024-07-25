using InfrastructureApi.Common;
using InfrastructureApi.DTO;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.ValueObjects;

namespace InfrastructureApi.Services
{
    public class TypeIncomeService : ModelService, IEntityService<TypeIncomeDTO>
    {
        private readonly ITypesBaseRepository<TypeIncome>? _typeIncomeRepository;

        public TypeIncomeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _typeIncomeRepository = unitOfWork.TypesIncomes;
        }

        public async Task<IEnumerable<TypeIncomeDTO>> GetAllAsync()
        {
            var typesIncomes = await _typeIncomeRepository!.GetAllWithProjectionAsync(TypeIncomeDTO.TypeIncomeSelector);

            return typesIncomes;
        }

        public async Task<TypeIncomeDTO> GetByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var typeIncome = await _typeIncomeRepository!.GetByIdWithProjectionAsync(id, TypeIncomeDTO.TypeIncomeSelector);

            return typeIncome!;
        }

        public async Task CreateAsync(TypeIncomeDTO typeIncomeDTO)
        {
            if (typeIncomeDTO is null) throw new ArgumentNullException(nameof(typeIncomeDTO));

            var typeIncome = new TypeIncome(new Name(typeIncomeDTO.Name), new FreeText(typeIncomeDTO.Description));

            await _typeIncomeRepository!.CreateAsync(typeIncome);
            await _unitOfWork!.SaveAsync();
        }

        public async Task UpdateAsync(TypeIncomeDTO typeIncomeDTO)
        {
            if (typeIncomeDTO is null) throw new ArgumentNullException(nameof(typeIncomeDTO));

            var typeIncome = await _typeIncomeRepository!.GetByIdAsync(typeIncomeDTO.Id);
            typeIncome.Change(new Name(typeIncomeDTO.Name), new FreeText(typeIncomeDTO.Description));

            _typeIncomeRepository.Update(typeIncome);
            await _unitOfWork!.SaveAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var typeIncome = await _typeIncomeRepository!.GetByIdWithDetailAsync(id);

            _typeIncomeRepository.Delete(typeIncome);
            await _unitOfWork!.SaveAsync();
        }
    }
}
