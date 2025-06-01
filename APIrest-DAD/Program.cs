
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIrest_DAD.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Hangfire;
using Hangfire.SqlServer;
using APIrest_DAD.Services;
using APIrest_DAD.Services.Interfaces;
using APIrest_DAD.Jobs;

namespace APIrest_DAD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddDbContext<AppContext>(opt =>
            //opt.UseInMemoryDatabase("NotificacaoDB"));

            // Add services to the container.
            builder.Services                
                .AddDbContext<AppDbContext>(opt => opt
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHangfire(config =>
                config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddHangfireServer();

            builder.Services.AddScoped<INotificacaoService, NotificacaoService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => { 
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DESENVOLVIMENTO-DE-APLICACOES-DISTRIBUIDAS-SISTEMA-DE-INFORMACAO-CAMPUS-CONTAGEM")),
                };
            });

            //builder.Services
            //    .AddGraphQLServer()
            //    .AddAuthorization()
            //    .AddMutationType<Mutation>()
            //    .AddQueryType<Query>()
            //    .AddProjections().AddFiltering().AddSorting();

            var app = builder.Build();

            app.UseHangfireServer();

            //app.UseAuthentication();

            //app.MapGraphQL("/");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            RecurringJob.AddOrUpdate<NotificacaoJob>(
                "processar-notificacoes",
                job => job.ProcessarNotificacoes(),
                Cron.Minutely);

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
