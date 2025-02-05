using DEVSteamAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//adicionando CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {

        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Adiciomar a string de conexão ao container
builder.Services.AddDbContext<DEVsteamAPIContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Somee")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
// Adionar o Swagger com JWT Bearer
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
    {
        new OpenApiSecurityScheme
        {
        Reference = new OpenApiReference
            {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

        },
        new List<string>()
        }
    });
});

// Serviço de EndPoints do Identity Framework
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    // por padrão usar options
    //dessa forma é possivel alterar as configurações padrão do Identity Framework e custumizar a aplicação
})
    .AddEntityFrameworkStores<DEVsteamAPIContext>()
    .AddDefaultTokenProviders(); // Adiocionando o provedor de tokens padrão

// add de autorização e autenticação
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();



var app = builder.Build();

// Configure the HTTP request pipeline.
//swaggwe wm ambiente de produção 
app.UseSwagger();
app.UseSwaggerUI();

//mapear os endpoints padão do identity framework
app.MapGroup("/Users").MapIdentityApi<IdentityUser>();
//app.MapGroup("/Roles").MapIdentityApi<IdentityRole>();

app.UseHttpsRedirection();
//permitir a autenticação e autorização de qualquer origem
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("Allowall");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
