using Journalist.Crm.Api;
using Journalist.Crm.Api.Infrastructure;
using Journalist.Crm.Domain;
using Journalist.Crm.GraphQL;
using Journalist.Crm.MongoDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var MyAllowSpecificOrigins = "LocalOnly";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var authenticationOptions = configuration
    .GetSection(KeycloakAuthenticationOptions.Section)
    .Get<KeycloakAuthenticationOptions>();

//var neo4jConfigurationSection = configuration.GetSection("Neo4j");
var mongoDBConfigurationSection = configuration.GetSection("MongoDB");
// Add services to the container.
builder.Services
     .AddMongoDB(mongoDBConfigurationSection)
    .AddJournalistGraphQL()
    .AddHealthChecks();

builder.Services.AddHttpContextAccessor()
                .AddTransient<IContext, Context>();

builder.Services.AddKeycloackAuthentication(authenticationOptions);

builder.Services.AddAuthorization((options)=>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser().Build();
});


builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                          });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapGraphQL();
app.MapWhen(context => !(context.Request.Path.Value ?? string.Empty).Contains("/graphql") && !(context.Request.Path.Value ?? string.Empty).Contains("/healthz"), app =>
{
    app.Use((context, next) =>
    {
        context.Request.Path = "/index.html";
        return next();
    }).UseStaticFiles();
});
app.MapHealthChecks("/healthz");


app.Run();
