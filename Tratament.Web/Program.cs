internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services
        builder.Services.AddControllersWithViews();

        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


        var app = builder.Build();

        app.UseDeveloperExceptionPage();
        app.UseStatusCodePages();

        app.UseStaticFiles();

        app.UseRouting();
        app.MapDefaultControllerRoute();


        app.Run();
    }
}