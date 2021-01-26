using System.Collections.Generic;
using Optsol.Components.Application.DataTransferObject;

namespace Optsol.Components.Service.Response
{
    public class Response
    {
        public bool Success { get; set; }

        public bool Failure { get; set; }

        public IEnumerable<string> Messages { get; set; }

        public Response()
        {

        }

        public Response(bool success)
        {
            Success = success;
            Failure = !Success;
        }

        public Response(bool success, IEnumerable<string> messages)
            : this(success)
        {
            Messages = messages;
        }
    }

    public class Response<TData> : Response
        where TData : BaseViewModel
    {
        public TData Data { get; set; }

        public Response()
        {

        }

        public Response(TData viewModel, bool success)
            : base(success)
        {
            Data = viewModel;
            Success = success;
            Failure = !Success;
        }

        public Response(TData viewModel, bool success, IEnumerable<string> messages)
            : this(viewModel, success)
        {
            Messages = messages;
        }
    }

    public class ResponseList<TData> : Response
        where TData : BaseViewModel
    {
        public IEnumerable<TData> Data { get; set; }

        public ResponseList()
        {

        }

        public ResponseList(IEnumerable<TData> viewModel, bool success)
            : base(success)
        {
            Data = viewModel;
        }

        public ResponseList(IEnumerable<TData> viewModels, bool success, IEnumerable<string> messages)
            : this(viewModels, success)
        {
            Messages = messages;
        }
    }
}
