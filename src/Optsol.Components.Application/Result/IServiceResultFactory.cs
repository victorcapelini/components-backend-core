using Optsol.Components.Application.DataTransferObject;
using System.Collections.Generic;

namespace Optsol.Components.Application.Result
{
    public interface IServiceResultFactory
    {
        ServiceResult Create();

        ServiceResult<TDto> Create<TDto>(TDto viewModel) where TDto : BaseViewModel;

        ServiceResultList<TDto> Create<TDto>(IEnumerable<TDto> viewModels) where TDto : BaseViewModel;
    }
}
