internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        // Add services
        builder.Services.AddControllersWithViews();

        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


        app.UseDeveloperExceptionPage();
        app.UseStatusCodePages();

        app.UseStaticFiles();

        app.UseRouting();
        app.MapDefaultControllerRoute();
    }
}