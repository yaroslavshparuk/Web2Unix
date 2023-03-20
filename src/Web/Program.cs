using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.Configurations;
using Web2Unix.Application.Abstractions;
using Web2Unix.Infrastructure.Authentication;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IJwtProvider, JwtProvider>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddApplicationPart(Web2Unix.Presentation.AssemblyReference.Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Web2Unix.Application.AssemblyReference.Assembly));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.ConfigureOptions<JwtConfiguration>();
builder.Services.ConfigureOptions<JwtBearerConfiguration>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
