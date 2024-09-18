using System.Reflection;
using System.Text;
using KoishopBusinessObjects;
using KoishopRepositories;
using KoishopServices;
using KoishopWebAPI.Data;
using KoishopWebAPI.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowLocalhost3000",
      builder => builder
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials()
          .WithExposedHeaders("Pagination")
          .WithOrigins("http://localhost:3000"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  var jwtSecurityScheme = new OpenApiSecurityScheme
  {
    BearerFormat = "JWT",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = JwtBearerDefaults.AuthenticationScheme,
    Description = "Put Bearer + your token in the box below",
    Reference = new OpenApiReference
    {
      Id = JwtBearerDefaults.AuthenticationScheme,
      Type = ReferenceType.SecurityScheme
    }
  };

  c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

  c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<string>()
        }
    });
  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
 
builder.Services.AddDbContext<KoishopDBContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString"), b => b.MigrationsAssembly("KoishopWebAPI")));

builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<KoishopDBContext>();
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>
  {
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = false,
      ValidateAudience = false,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:TokenKey"]))
    };
  });

builder.Services.AddAuthorization(options =>
{
  options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
  options.AddPolicy("RequireStaffRole", policy => policy.RequireRole("Staff"));
  options.AddPolicy("RequireCustomerRole", policy => policy.RequireRole("Customer"));
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
  c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
});
app.UseCors("AllowLocalhost3000");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<KoishopDBContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
try
{
  await DBInitializer.Initialize(context, userManager);
}
catch (Exception ex)
{
  logger.LogError(ex, "A problem occurred during migration");
}

app.Run();
