namespace InfrastructureApi.DTO
{
    public abstract class TypeBaseDTO : BaseEntityDTO
    {
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
    }
}
