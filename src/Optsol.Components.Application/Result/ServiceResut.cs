using Flunt.Notifications;
using Optsol.Components.Application.DataTransferObject;
using System;
using System.Collections.Generic;

namespace Optsol.Components.Application.Result
{
    public class ServiceResult : Notifiable { }

    public class ServiceResult<TDto> : ServiceResult
        where TDto : BaseViewModel
    {
        public TDto Data { get; private set; }

        public ServiceResult(TDto data)
        {
            Data = data;

            Data.Validate();
            AddNotifications(data);
        }
    }

    public class ServiceResultList<TDto> : ServiceResult
        where TDto : BaseViewModel
    {
        public IEnumerable<TDto> Data { get; private set; }

        public ServiceResultList(IEnumerable<TDto> dataList)
        {
            Data = dataList;
            
            executeValidation(dataList, AddNotifications);
            foreach (var data in Data)
            {
                data.Validate();
                AddNotifications(data);
            }
        }

        Action<IEnumerable<TDto>, Action<TDto>> executeValidation = (dataList, addNotifications) =>
        {
            foreach (var data in dataList)
            {
                data.Validate();
                addNotifications(data);
            }
        };
    }
}
