namespace InfrastructureApi.DTO
{
    public abstract class TypeBaseDTO : BaseEntityDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
