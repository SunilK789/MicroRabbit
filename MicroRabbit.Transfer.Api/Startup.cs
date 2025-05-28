using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.IoC;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.EventHandlers;
using MicroRabbit.Transfer.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace MicroRabbit.Transfer.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transfer MicroService", Version = "v1" });
            });

            services.AddMediatR(typeof(Startup));

            services.AddDbContext<TransferDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TransferDbConnection"));
            });

            DependencyContainer.RegisterServices(services);
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transfer Microservice V1");
                });
            }

            ConfigureEventBus(app);
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }

        private void ConfigureEventBus(WebApplication app)
        {
            var eventBus = app.Services.GetRequiredService<IEventBus>();

            eventBus.Subscribe<TransferCreatedEvent, TransferEventHandler>();
        }
    }
}