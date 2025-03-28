using System.Reflection;
using Microsoft.OpenApi.Models;
using NPU.Data.Config;

namespace NPU;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseDefaultServiceProvider((context, options) =>
        {
            // force scope validation in development
            options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
        });
        
        builder.Services.AddHealthChecks();

        var cosmosSettings = builder.Configuration.GetSection("COSMOS").Get<CosmosSettings>();
        var storageSettings = builder.Configuration.GetSection("STORAGE").Get<StorageSettings>();

        if (cosmosSettings == null || storageSettings == null)
        {
            throw new ArgumentException("Cosmos and Storage settings are required");
        }
        
        builder.Services.AddSingleton(storageSettings);
        builder.Services.AddSingleton(cosmosSettings);

        // using azure ad
        // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //     .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCustomServices();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo() { Title = "NPU Backend API", Version = "v1.0.0" }
            );
            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Description =
                        """
                        JWT Authorization header using the Bearer scheme. 
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: \"Bearer <token>\"
                        """,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                }
            );
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement()
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
                }
            );
            
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        var app = builder.Build();
        
        app.MapHealthChecks("/healthz");
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(policyBuilder =>
        {
            // TODO: fix hardcoded ip
            policyBuilder.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}