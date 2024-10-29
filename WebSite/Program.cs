using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiClient;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//Registers the Dependency Injection code
//for providing an implementation of IBlogSharpApiClient whenever needed
//to instantiate a Controller
builder.Services.AddScoped<IBlogSharpApiClient>(
    (cs) => new BlogSharpApiClient(builder.Configuration["WebApiURI"]));

AddCookieAuthentication(builder.Services);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();  //ask the browser to use HTTPS henceforth
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static void AddCookieAuthentication(IServiceCollection services)
{
    //Adds the cookie authentication scheme
    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    {
        //sets the paths to send users, because the default paths are
        //account/login
        //account/accessdenied
        //account/logout
        //but we like the plural 's' on Accounts ;-)
        options.LoginPath = "/Accounts/Login";
        options.AccessDeniedPath = "/Accounts/AccessDenied";
        options.LogoutPath = "/Accounts/LogOut";
    });
}