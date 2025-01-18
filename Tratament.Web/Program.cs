internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services
        builder.Services.AddControllersWithViews();

        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


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