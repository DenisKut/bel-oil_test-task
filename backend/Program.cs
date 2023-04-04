using backend.Contexts;
using backend.Configurations;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// использование SCRUTOR для регистрации зависимостей
builder.Services.Scan(scan =>
    scan.FromCallingAssembly()
        .AddClasses()
        .AsMatchingInterface());

// использование сканирования, чтобы зарегистрировать
//службы как службы с ограниченной областью для своих интерфейсов

// var type = typeof(EntityRepo<>);
// builder.Services.Scan(scan => scan.FromAssembliesOf(type)
//     .AddClasses(classes => classes.InExactNamespaceOf(type))
//     .AsImplementedInterfaces()
//     .WithScopedLifetime());
//builder.Services.AddScoped<IEntityRepo<Payment>, EntityRepo<Payment>>();
builder.Services.AddEntityFrameworkNpgsql()
  .AddDbContext<KinderContext>(options =>
    options.UseNpgsql(builder.Configuration
      .GetConnectionString("ApiDatabase")));

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options => {
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

  .AddJwtBearer(jwt => {
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);

    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters() {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(key),
      ValidateIssuer = false, // for dev because on localhost ssl keys can be invalid for https
      ValidateAudience = false, // for dev too
      RequireExpirationTime = false, //for dev -- needs to be updated when refresh token is added
      ValidateLifetime = true
    };
  });

//adding IdentityManager
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
  options.SignIn.RequireConfirmedAccount = false;
  options.Password.RequiredLength = 5;
  options.Password.RequireLowercase = false;
  options.Password.RequireUppercase = false;
  options.Password.RequireNonAlphanumeric = false;
  options.Password.RequireDigit = false;
})
  .AddEntityFrameworkStores<KinderContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
