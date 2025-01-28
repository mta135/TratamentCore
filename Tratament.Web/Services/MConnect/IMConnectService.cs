using MAIeDosar.API.ApiViewModels.ExternalServices;
using MAIeDosar.API.ServicesModels.Civil;
using MAIeDosar.API.ServicesModels.Org;

namespace MAIeDosar.API.Services.MConnect
{
    public interface IMConnectService
    {
        Task<OrganizationServiceResult> GetOrganization(OrganizationFilter search);
        Task<PersonAPIModel> GetPerson(PersonFilter search);
    }
}
