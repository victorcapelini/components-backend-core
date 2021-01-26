using Flunt.Notifications;
using Optsol.Components.Application.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Optsol.Components.Application.Result
{
    public class ServiceResult
    {
        public bool Valid { get; private set; }

        public bool Invalid { get; private set; }

        public string Message { get; set; }

        public ServiceResult()
        {
            Valid = true;
            Invalid = false;
            Message = "Success!";
        }

        public ServiceResult SetValid()
        {
            Valid = !Invalid;

            return this;
        }

        public ServiceResult SetInvalid()
        {
            Invalid = !Valid;

            return this;
        }

        public ServiceResult SetMessage(string message)
        {
            Message = message;

            return this;
        }
    }

    public class ServiceResult<TDto> : ServiceResult
        where TDto : BaseViewModel
    {
        public TDto Data { get; private set; }

        public ServiceResult(TDto data)
        {
            Data = data;

            if (Data.Valid)
            {
                SetValid();
            }

            if(Data.Invalid)
            {
                SetInvalid();
            }
        }
    }

    public class ServiceResultList<TDto> : ServiceResult
        where TDto : BaseViewModel
    {
        public IEnumerable<TDto> Data { get; private set; }

        public ServiceResultList(IEnumerable<TDto> dataList)
        {
            Data = dataList;

            if (Data.All(data => data.Invalid))
            {
                SetValid();
            }

            if (Data.Any(a => a.Invalid))
            {
                SetInvalid();
            }
        }
    }
}
