using eDataPracticalTest.BoundedContext.Repositories.Security;
using eDataPracticalTest.BoundedContext.Services.Main;
using eDataPracticalTest.Infrastructure.Databases.SqlServer;
using eDataPracticalTest.Infrastructure.Models;
using Meteors;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();


builder.Services.AddMrDbContext<AppDbContext>(
(options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<Account, IdentityRole<Guid>>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders().AddRoles<IdentityRole<Guid>>();
//.net 8 ,  the Test only feature in .net 6.0
//builder.Services.AddIdentityApiEndpoints<Account>();

// for more: https://github.com/MhozaifaA/Meteors.DependencyInjection.AutoService
builder.Services.AddAutoService(BoundedContextServicesMainAssembly.Assembly,
    BoundedContextRepositoriesSecurityAssembly.Assembly);

builder.Services.AddMrRepository((e) =>
{
    e.OrderBy((nameof(IBaseEntity<Guid>.DateCreated)));
    //e.AutoInject();
});


//.net 8 ,  the Test only features in .net 6.0
//builder.Services
//    .AddAuthentication()
//    .AddBearerToken();
//builder.Services.AddAuthorization();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
   {

       //options.SaveToken = true;
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Jwt:IssuerAudience"],
           ValidAudience = builder.Configuration["Jwt:IssuerAudience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
       };
   });



builder.Services.AddMrSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMrSwagger();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseCors(b =>
{

    var org = builder.Configuration.GetSection("WithOrigins").Get<string[]>();
    if (org.Contains("*"))
        b.AllowAnyHeader().AllowAnyMethod()
    .SetIsOriginAllowed((host) => true).AllowCredentials();
    else
        b.AllowAnyOrigin().WithOrigins(org).AllowAnyMethod()
               .AllowAnyHeader().AllowCredentials();
    //  b.Build().SupportsCredentials = true;
}
);

app.UseAuthorization();

app.MapControllers();

//.net 8 ,  the Test only feature in .net 6.0
//app.MapIdentityApi<Account>();


app.UseSqlServerSeed<AppDbContext>(async (context, provider) =>
{
    //context.Database.GetPendingMigrationsAsync
    await context.Database.MigrateAsync();
    await context.Database.EnsureCreatedAsync();

    await context.AccountsSeedAsync(provider);
    

    app.DisposeSqlServerSeed();
});

app.Run();
