using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogPessoal.src.data;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implements;

namespace BlogPessoal
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
            // Configuração Banco
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddDbContext<BlogPessoalContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // Repositorios
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<ITheme, ThemeRepository>();
            services.AddScoped<IPost, PostRepository>();

            // Controladores
            services.AddCors();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BlogPessoalContext context)
        {
            // Ambiente de desenvolvimento
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            } 

            // Ambiente de produção
            //Rotas
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
