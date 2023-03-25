using Journalist.Crm.Api;
using Journalist.Crm.Api.Infrastructure;
using Journalist.Crm.CommandHandlers;
using Journalist.Crm.Domain;
using Journalist.Crm.GraphQL;
using Journalist.Crm.Marten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(new RenderedCompactJsonFormatter())
            .CreateLogger();

        try
        {
            Log.Information("Starting web application");

            var MyAllowSpecificOrigins = "LocalOnly";

            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            var authenticationOptions = configuration
                .GetSection(KeycloakAuthenticationOptions.Section)
                .Get<KeycloakAuthenticationOptions>();

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services
                .AddCommandHandlers()
                 .AddJournalistMarten(configuration)
                .AddJournalistGraphQL()
                .AddHealthChecks();

            builder.Services.AddHttpContextAccessor()
                            .AddTransient<IContext, Context>();

            builder.Services.AddKeycloackAuthentication(authenticationOptions!);

            builder.Services.AddAuthorization((options) =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser().Build();
                options.AddPolicy("Administrators", policy => policy.RequireClaim("roles", "[admin]"));
                options.AddPolicy("Users", policy => policy.RequireClaim("roles", "[user]"));
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
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}