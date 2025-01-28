namespace Tratament.Web.Recaptcha.Interface
{
    public interface IRecaptchaService
    {
        Task<bool> VerifyRecaptchaAsync(string token);
    }
}
