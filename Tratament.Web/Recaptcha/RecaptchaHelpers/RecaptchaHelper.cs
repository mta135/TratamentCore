namespace Tratament.Web.Recaptcha.RecaptchaHelpers
{
    public class RecaptchaHelper
    {

        public static string GenerateRecaptchaHtml(string siteKey)
        {
            return $"<div class='g-recaptcha' data-sitekey='{siteKey}'></div>";
        }


        //private RecaptchatOption _options { get; set; }

        //public RecaptchaHelper(IOptions<RecaptchatOption> options)
        //{
        //    _options = options.Value;
        //}


        //public RecaptchaResponse ValidateCaptha(string response)
        //{
        //    using (var client = new WebClient())
        //    {
        //        string secret = _options.SecretKey;
        //        string url = $"{_options.Url}secret={secret}&response={response}";

        //        var result = client.DownloadString(url);

        //        try
        //        {
        //            var data = JsonConvert.DeserializeObject<RecaptchaResponse>(result.ToString());

        //            return data;

        //        }
        //        catch (Exception ex)
        //        {
        //            return default;
        //        }
        //    }


    }


    }
