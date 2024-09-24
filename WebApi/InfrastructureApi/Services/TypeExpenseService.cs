using InfrastructureApi.Common;
using InfrastructureApi.DTO;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.ValueObjects;

namespace InfrastructureApi.Services
{
    public class TypeExpenseService : ModelService, IEntityService<TypeExpenseDTO>
    {
        private readonly ITypesBaseRepository<TypeExpense>? _typeExpenseRepository;

        public TypeExpenseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _typeExpenseRepository = unitOfWork.TypesExpenses;
        }

        public async Task<IEnumerable<TypeExpenseDTO>> GetAllAsync()
        {
            var typesExpenses = await _typeExpenseRepository!.GetAllWithProjectionAsync(TypeExpenseDTO.TypeExpenseSelector);

            return typesExpenses;
        }

        public async Task<TypeExpenseDTO?> GetByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var typeExpense = await _typeExpenseRepository!.GetByIdWithProjectionAsync(id, TypeExpenseDTO.TypeExpenseSelector);

            return typeExpense;
        }

        public async Task CreateAsync(TypeExpenseDTO typeExpenseDTO)
        {
            if (typeExpenseDTO is null) throw new ArgumentNullException(nameof(typeExpenseDTO));
            await CheckDuplicateByName(typeExpenseDTO.Name);

            var typeExpense = new TypeExpense(new Name(typeExpenseDTO.Name), new FreeText(typeExpenseDTO.Description));

            await _typeExpenseRepository!.CreateAsync(typeExpense);
            await _unitOfWork!.SaveAsync();
        }

        public async Task UpdateAsync(int? id, TypeExpenseDTO typeExpenseDTO)
        {
            if (typeExpenseDTO is null) throw new ArgumentNullException(nameof(typeExpenseDTO));

            var typeExpense = await _typeExpenseRepository!.GetByIdAsync(id) ?? throw new InvalidOperationException($"Data by id({id}) not found!");
            await CheckDuplicateByNameAndNotId(id, typeExpenseDTO.Name);
            typeExpense.Change(new Name(typeExpenseDTO.Name), new FreeText(typeExpenseDTO.Description));

            _typeExpenseRepository.Update(typeExpense);
            await _unitOfWork!.SaveAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var typeExpense = await _typeExpenseRepository!.GetByIdWithDetailAsync(id) ?? throw new InvalidOperationException($"Data by id({id}) not found!");

            _typeExpenseRepository.Delete(typeExpense);
            await _unitOfWork!.SaveAsync();
        }

        private async Task CheckDuplicateByName(string name)
        {
            var checkItem = await _typeExpenseRepository!.IsFoundByFilterAsync(typeExpense => typeExpense.Name.Value == name);

            if (!checkItem)
                throw new InvalidOperationException($"A record with this name - {name} already exists");
        }

        private async Task CheckDuplicateByNameAndNotId(int? id, string? name)
        {
            var checkItem = await _typeExpenseRepository!.IsFoundByFilterAsync(typeExpense => typeExpense.Id != id && typeExpense.Name.Value == name);

            if (!checkItem)
                throw new InvalidOperationException($"A record with the specified name - {name} already exists");
        }
    }
}
