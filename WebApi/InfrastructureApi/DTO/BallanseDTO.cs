using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureApi.DTO
{
    public abstract class BallanseDTO : BaseEntityDTO
    {
        public double? Amount { get; set; }
        public int? TypeId { get; set; }
        public string? Comments { get; set; }
    }
}
