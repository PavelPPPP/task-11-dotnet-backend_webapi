using InfrastructureApi.DTO;
using SelfFinanceApp.CollectionEndpoints;

namespace SelfFinanceApp.ServicesEndpoints
{
    public class EntityEndpointsService<TypeDTO> where TypeDTO : BaseEntityDTO
    {
        private WebApplication? _app;
        private EntityEndpoints<TypeDTO> _entityEndpoints;
        public EntityEndpointsService(WebApplication? app)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
            _entityEndpoints = new EntityEndpoints<TypeDTO>();
        }

        public void Map()
        {
            _app?.MapGet(_entityEndpoints.GetAllRoute, _entityEndpoints.GetAllFunc);
            _app?.MapGet(_entityEndpoints.GetOrDeleteByIdRoute, _entityEndpoints.GetByIdFunc);
            _app?.MapPost(_entityEndpoints.PostAddRoute, _entityEndpoints.PostAddFunc);
            _app?.MapPut(_entityEndpoints.PutEditRoute, _entityEndpoints.PutEditFunc);
            _app?.MapDelete(_entityEndpoints.GetOrDeleteByIdRoute, _entityEndpoints.DeleteFunc);
        }
    }
}
