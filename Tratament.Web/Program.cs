using DNTCaptcha.Core;
using QuestPDF.Infrastructure;
using Tratament.Web.Recaptcha.Interface;
using Tratament.Web.Recaptcha;
using Tratament.Web.Recaptcha.RecaptchaHelpers;
using MAIeDosar.API.Services.MConnect;
using Tratament.Web.Services.MConectService;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Set the QuestPDF license type to Community
        QuestPDF.Settings.License = LicenseType.Community;

        // Add services
        builder.Services.AddControllersWithViews();

        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
        var configuration = builder.Configuration;

        #region reCaptcha V3

        builder.Services.Configure<RecaptchatOption>(builder.Configuration.GetSection(nameof(RecaptchatOption)));
        builder.Services.AddHttpClient<IRecaptchaService, RecaptchaService>();

        #endregion

        #region DNT Captcha

        builder.Services.AddDNTCaptcha(options =>
               options.UseCookieStorageProvider(SameSiteMode.None).ShowThousandsSeparators(false)
                 .WithEncryptionKey("12345"));

        #endregion

        #region Dependency Injection

        builder.Services.AddScoped<IMConnectService, MConnectService>();

        #endregion

        #region InitializeSettings

        MccCertificateConfig.InitializeSettings(configuration);
        MConnectHelper.InitializeSettings(configuration);

        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseAuthorization();


        app.MapDefaultControllerRoute();
        app.MapRazorPages();

        app.UseAntiforgery();

        app.Run();
    }
}