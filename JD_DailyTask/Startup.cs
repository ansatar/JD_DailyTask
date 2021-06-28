using AutoMapper;
using JD.Application;
using JD.Application.DailyTasks;
using JD.Application.JDTaskSummaries;
using JD.Application.JDUserTaskRecords;
using JD.Application.Users;
using JD.Application.UserVerif;
using JD.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JD_DailyTask
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //Serilog
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

            _logger = Log.Logger;
        }

        public class ApplicationSettings
        {
            public SerilogModel Serilog { get; set; }
            public class SerilogModel
            {
                public MinimumLevelModel MinimumLevel { get; set; }
                public class MinimumLevelModel
                {
                    public string Default { get; set; }
                }
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(options =>
            {
                options.Filters.Add(new CustomerExceptionFilter(_logger));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JD_DailyTask", Version = "v1" });
            });

            //ÅäÖÃÊý¾Ý¿âÁ¬½Ó×Ö·û´®
            services.AddDbContextPool<JDDailyTaskDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Default"), new MySqlServerVersion(new Version(8, 0, 22))));

            //×¢²áIHttpClientFactory
            services.AddHttpClient();

            //×¢²áAutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            //DI
            services.AddScoped<IUserVerifAppService, UserVerifAppService>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IDailyTaskAppService, DailyTaskAppService>();
            services.AddScoped<IJDUserTaskRecordAppService, JDUserTaskRecordAppService>();
            services.AddScoped<IJDTaskSummaryAppService, JDTaskSummaryAppService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JD_DailyTask v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Ìí¼ÓSerilog
            loggerFactory.AddSerilog();
        }
    }
}
