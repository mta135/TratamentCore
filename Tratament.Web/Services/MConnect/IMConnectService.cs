using Tratament.Web.ApiViewModels.ExternalServices;
using Tratament.Web.Services.MConnect.MConnectCore;
using Tratament.Web.Services.MConnect.Models.Person;
using Tratament.Web.ServicesModels.Org;

namespace Tratament.Web.Services.MConnect
{
    public interface IMConnectService
    {
        Task<OrganizationServiceResult> GetOrganization(OrganizationFilter search);
        Task<PersonAPIModel> GetPerson(PersonFilter search);
    }
}
