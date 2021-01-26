using Flunt.Notifications;
using System.Runtime.Serialization;

namespace Optsol.Components.Application.DataTransferObject
{
    [DataContract]
    public abstract class BaseViewModel : Notifiable
    {
        public abstract void Validate();

    }
}
