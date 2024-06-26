using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NexusPilot_Tasks_Service_src.DAO;
using NexusPilot_Tasks_Service_src.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Setting up CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", 
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});
IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();

var JWTIssuer = _configuration["JWTConfig:Issuer"];
var JWTAudience = _configuration["JWTConfig:Audience"];
var JWTSecretKey = _configuration["JWTConfig:SecretKey"];

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
//Registers DAO as a singleton and prepares for DI
builder.Services.AddSingleton<SupabaseClient>();
//Registers the service as a singleton and prepares for DI
builder.Services.AddScoped<TaskService>();


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
x.TokenValidationParameters = new TokenValidationParameters{

    ValidIssuer = JWTIssuer,
    ValidAudience = JWTAudience,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSecretKey)),
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
};
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Adding CORS policy to the app
app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
