using Bazaro.Data;
using Bazaro.Data.Models;
using Bazaro.Web.Areas.Identity;
using Bazaro.Web.Data;
using Bazaro.Web;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// read pw for db from .env file and add it to the db connectionstring
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

string? pw = Environment.GetEnvironmentVariable("DB_PW");
if(pw == null)
{
    throw new FileLoadException("Please set the .env File!");
}

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
connectionString = connectionString.Replace("{db_pw}", pw);
builder.Services.AddDbContext<BazaroContext>(options =>
    options.UseNpgsql(connectionString));
  //  options.UseInMemoryDatabase("Jonas-der-Spacken"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BazaroContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services
      .AddBlazorise(options =>
      {
          options.ChangeTextOnKeyPress = true; // optional
      })
      .AddBootstrap5Providers()
      .AddFontAwesomeIcons();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
