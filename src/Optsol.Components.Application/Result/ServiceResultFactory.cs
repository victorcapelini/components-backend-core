using Optsol.Components.Application.DataTransferObject;
using System.Collections.Generic;

namespace Optsol.Components.Application.Result
{
    public class ServiceResultFactory : IServiceResultFactory
    {
        public ServiceResult<TDto> Create<TDto>(TDto viewModel)
            where TDto : BaseViewModel
        {
            return new ServiceResult<TDto>(viewModel);
        }

        public ServiceResultList<TDto> Create<TDto>(IEnumerable<TDto> viewModels)
            where TDto : BaseViewModel
        {
            return new ServiceResultList<TDto>(viewModels);
        }

        public ServiceResult Create()
        {
            return new ServiceResult();
        }
    }
}
