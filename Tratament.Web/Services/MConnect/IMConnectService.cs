using Tratament.Model.Models.ExternalServices;
using Tratament.Web.Services.MConnect.Models.Person;

namespace Tratament.Web.Services.MConnect
{
    public interface IMConnectService
    {
        Task<PersonAPIModel> GetPerson(PersonFilter search);
    }
}
