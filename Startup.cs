﻿using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Leer la cadena de conexión desde la variable de entorno
        var cosmosDbConnectionString = Environment.GetEnvironmentVariable("COSMOSDB_CONNECTION_STRING");

        if (string.IsNullOrEmpty(cosmosDbConnectionString))
        {
            throw new InvalidOperationException("La cadena de conexión de Cosmos DB no está configurada.");
        }
        else { Debug.WriteLine(cosmosDbConnectionString); }

        // Crear una instancia del cliente de Cosmos y registrarlo como un servicio singleton
        CosmosClient cosmosClient = new CosmosClient(cosmosDbConnectionString);
        services.AddSingleton(cosmosClient);
        services.AddScoped<CosmosDbService>();

        // Registrar otros servicios necesarios
        services.AddControllers();
        services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configuración del pipeline de la aplicación
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}