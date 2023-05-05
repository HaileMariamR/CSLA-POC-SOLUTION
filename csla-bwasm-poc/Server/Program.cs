using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Csla.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
      builder =>
      {
          builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
         .WithOrigins("https://localhost:5188");

});
});


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer ="http://localhost:5187",
        ValidAudience = "https://localhost:5188",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sampleScretKey@1234"))

    };

});


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();


builder.Services.AddTransient(typeof(Data_Layer.Interface.IEmployeService), typeof(Data_Layer.Services.EmployeService));
builder.Services.AddTransient(typeof(Data_Layer.Interface.IAuthService) , typeof(Data_Layer.Services.AuthService));

builder.Services.AddDbContext<Data_Layer.DatabaseContext>(options => options.UseSqlite("Data Source=csla-bwasm.db"));



builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});



builder.Services.AddCsla(o => o
  .AddAspNetCore()
  .DataPortal(dpo => dpo
    .AddServerSideDataPortal()
    .UseLocalProxy()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();


app.UseRouting();

app.UseCors("AllowAllOrigins");


app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
