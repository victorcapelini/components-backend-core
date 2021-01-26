using Optsol.Components.Application.DataTransferObject;
using Optsol.Components.Application.Service;
using Optsol.Components.Domain.Entities;

namespace Optsol.Components.Service
{
    public interface IServiceApplication : IBaseServiceApplication<AggregateRoot, BaseViewModel, BaseViewModel, BaseViewModel, BaseViewModel>
    {
    }
}
