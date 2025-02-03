using Tratament.Model.Models.ExternalServices;
using Tratament.Web.Services.MConnect.Models.Person;

namespace Tratament.Web.Services.MConnect
{
    public interface IMConnectService
    {
        Task<PersonModel> GetPerson(PersonFilter search);
    }
}
