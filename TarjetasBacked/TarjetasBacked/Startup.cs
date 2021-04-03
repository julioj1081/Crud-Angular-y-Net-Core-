using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

//importar el entity framework core
using Microsoft.EntityFrameworkCore;

namespace TarjetasBacked
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //agregamos la cadena que esta en app settings y despues ejecutamos en la consola Add-Migration v1.0.0
            //Update-database
            //y agregar la variable de conexion en el json
            services.AddDbContext<AplicationDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            //RESULVE EL PROBLEMA net::ERR_CERT_AUTHORITY_INVALID
            //zone-evergreen.js:2845 GET https://localhost:44318/api/Tarjeta/ net::ERR_CERT_AUTHORITY_INVALID AplicationDBContext
            services.AddCors(options => options.AddPolicy("AllowWepApp", 
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
            );
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //AGREGAR PARA QUE FUNCIONE EN ANGULAR
            app.UseCors("AllowWepApp");
            app.UseCors(options =>
            options.WithOrigins("https://localhost:44318")
            .AllowAnyHeader()
            .AllowAnyMethod()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
