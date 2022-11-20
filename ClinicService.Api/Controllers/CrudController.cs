using ClinicService.Domain.Entities;
using ClinicService.Domain.Models;
using ClinicService.Domain.Services;

namespace ClinicService.Api.Controllers;

//public abstract class CrudController<TEntity, TId>
//    : CrudControllerBase<IGetResponse<TEntity, TId>,
//                         IGetAllResponse<TEntity, TId>,
//                         IEntityResponse<TEntity, TId>,
//                         ICreateRequest<TEntity, TId>,
//                         ICreateResponse<TEntity, TId>,
//                         IUpdateRequest<TEntity, TId>,
//                         IUpdateResponse<TEntity, TId>,
//                         IDeleteResponse<TEntity, TId>,
//                         TEntity,
//                         TId>
//     where TEntity : IEntity<TId>, new()
//{
//    protected CrudController(CrudServiceBase<TEntity, TId> service) : base(service)
//    {
//    }
//}

public class ClientControllerBase
    : CrudControllerBase<GetClientResponse,
                         GetClientsResponse,
                         ClientResponse,
                         CreateClientRequest,
                         CreateClientResponse,
                         UpdateClientRequest,
                         UpdateClientResponse,
                         DeleteClientResponse,
                         Client,
                         int>
{
    public ClientControllerBase(CrudServiceBase<Client, int> service) : base(service)
    {
    }
}