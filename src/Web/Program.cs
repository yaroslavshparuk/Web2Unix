using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.Configurations;
using Web2Unix.Application.Abstractions;
using Web2Unix.Application.Data;
using Web2Unix.Infrastructure;
using Web2Unix.Infrastructure.Access;
using Web2Unix.Infrastructure.Authentication;
using Web2Unix.Infrastructure.ConnectionToUnix;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IJwtProvider, JwtProvider>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddTransient<IUnixTerminal, UnixTerminal>();
builder.Services.AddTransient<IFirewall, Firewall>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddApplicationPart(Web2Unix.Presentation.AssemblyReference.Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Web2Unix.Application.AssemblyReference.Assembly));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.ConfigureOptions<JwtConfiguration>();
builder.Services.ConfigureOptions<JwtBearerConfiguration>();
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(o => o.AddPolicy("Dev", builder =>
{
    builder.WithOrigins("https://localhost:7123", "https://localhost:44456")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
}));
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
app.UseCors("Dev"); 
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();