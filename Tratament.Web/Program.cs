using DNTCaptcha.Core;
using QuestPDF.Infrastructure;
using Tratament.Web.Recaptcha.Interface;
using Tratament.Web.Recaptcha;
using Tratament.Web.Recaptcha.RecaptchaHelpers;
using Tratament.Web.Services.MConnect;
using Tratament.Web.LoggerSetup;
using Tratament.Web.Services.MConnect.MConnectCore;
using Tratament.Web.Services.Tickets;

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
    

        #region Session

        builder.Services.AddDistributedMemoryCache();
        // Configure session
        builder.Services.AddSession(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true; // Ensures the cookie is always sent

            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.None;
            options.IdleTimeout = TimeSpan.FromMinutes(20);
        });

        #endregion

        #region reCaptcha V3

        builder.Services.Configure<RecaptchatOption>(builder.Configuration.GetSection(nameof(RecaptchatOption)));
        builder.Services.AddHttpClient<IRecaptchaService, RecaptchaService>();

        #endregion

        #region DNT Captcha

        builder.Services.AddDNTCaptcha(options =>
               options.UseDistributedCacheStorageProvider().ShowThousandsSeparators(false)
                 .WithEncryptionKey("12345"));

        #endregion

        #region Dependency Injection

        builder.Services.AddScoped<IMConnectService, MConnectService>();
        builder.Services.AddScoped<ITreatmentTicket, TreatmentTicket>();

        #endregion

        #region InitializeSettings

        MccCertificateConfig.InitializeSettings(configuration);
        TreatmentTicketClient.InitializeSettings(configuration);

        WriteLog.InitLoggers();

        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsStaging())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.MapRazorPages();

        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=SendRequest}/{action=Send}/{id?}");


        app.UseSession();

        app.Run();
    }
}