using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Core.Entities;
using SimpleBookingSystem.Core.Interfaces.IRepositories;
using SimpleBookingSystem.Core.Interfaces.IServices;
using SimpleBookingSystem.Core.Requests;
using SimpleBookingSystem.Core.Services;
using SimpleBookingSystem.Core.Validators;
using SimpleBookingSystem.Infrastructure.Data;
using SimpleBookingSystem.Infrastructure.Repositories;
using System.Reflection;

namespace SimpleBookingSystem.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static void InitializeDb(this IServiceCollection services)
        {
            using var scope = services.BuildServiceProvider();
            var appDb = scope.GetService<AppDbContext>();
            appDb?.Database.EnsureCreated();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepositoryAsync<,>), typeof(BaseRepositoryAsync<,>));
            services.AddTransient<IResourceRepositoryAsync, ResourceRepositoryAsync>();
            services.AddTransient<IBookingRepositoryAsync, BookingRepositoryAsync>();
            return services;
        }

        public static IServiceCollection AddApplicationLayers(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddValidatorsFromAssemblyContaining<BookingRequestValidator>(ServiceLifetime.Transient);
            services.AddFluentValidationAutoValidation();
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
