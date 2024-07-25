using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureApi.DTO
{
    public abstract class TypeBaseDTO : BaseEntityDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
