using Core;
using Core.Data;
using Core.Data.Contract.EventBus;
using EventBus;
using MicroserviceCarReservation;
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

namespace MicroserviceHotelReservation
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
            services.AddSingleton<IReservationProcess<ReservationItem>, HotelReservationProcess>();
            services.AddSingleton<IEventBus, EventBusRabbitMQ>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            var reservationProcess = app.ApplicationServices.GetRequiredService<IReservationProcess<ReservationItem>>();

            new HotelReservationQueueHandler(reservationProcess, eventBus);
        }
    }
}
