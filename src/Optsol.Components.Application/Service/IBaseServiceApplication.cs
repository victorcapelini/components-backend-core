using System;
using System.Threading.Tasks;
using Optsol.Components.Application.DataTransferObject;
using Optsol.Components.Application.Result;
using Optsol.Components.Domain.Entities;

namespace Optsol.Components.Application.Service
{
    public interface IBaseServiceApplication<TEntity, TGetByIdDto, TGetAllDto, TInsertData, TUpdateData> : IDisposable
        where TEntity : AggregateRoot
        where TGetByIdDto : BaseViewModel
        where TGetAllDto : BaseViewModel
        where TInsertData : BaseViewModel
        where TUpdateData : BaseViewModel
    {
        Task<ServiceResult<TGetByIdDto>> GetByIdAsync(Guid id);

        Task<ServiceResultList<TGetAllDto>> GetAllAsync();

        Task<ServiceResult> InsertAsync(TInsertData data);

        Task<ServiceResult> UpdateAsync(TUpdateData data);

        Task<ServiceResult> DeleteAsync(Guid id);
    }
}