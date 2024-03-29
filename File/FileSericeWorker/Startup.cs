﻿using FileSericeWorker.RabitMQ;
using FileSericeWorker.Service;
using FileSericeWorker.Service.StorageService;
using FileWorkerService.Worker;
using Quartz;
using Quartz.AspNetCore;

namespace FileSericeWorker
{
    public class Startup
    {
        private WebApplicationBuilder _builder;
        public Startup(WebApplicationBuilder builder, IWebHostEnvironment env)
        {
            this._builder = builder;
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseCors(x => {
                x.AllowAnyHeader();
                x.AllowAnyOrigin(); 
                x.AllowAnyMethod();
            });
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddScoped<IRabitMqProducer, RabitMqProducer>();
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHostedService<ConsumeRabiitMQService>();
            services.AddFileStorageService("ReadMe.txt");
            //services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        }
    }
}
