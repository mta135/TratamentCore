using Microsoft.Extensions.Options;
using System.Text.Json;
using Tratament.Web.Recaptcha.Interface;
using Tratament.Web.Recaptcha.RecaptchaHelpers;

namespace Tratament.Web.Recaptcha
{
    public class RecaptchaService : IRecaptchaService
    {

        private readonly RecaptchatOption _recaptchaOption;
        private readonly HttpClient _httpClient;

        public RecaptchaService(IOptions<RecaptchatOption> options, HttpClient httpClient)
        {
            _recaptchaOption = options.Value;
            _httpClient = httpClient;
        }


        public async Task<bool> VerifyRecaptchaAsync(string token)
        {
            var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_recaptchaOption.SecretKey}&response={token}";
            var response = await _httpClient.GetStringAsync(url);

            var recaptchaResponse = JsonSerializer.Deserialize<RecaptchaResponse>(response);
            return recaptchaResponse != null && recaptchaResponse.Success;
        }
    }
}
