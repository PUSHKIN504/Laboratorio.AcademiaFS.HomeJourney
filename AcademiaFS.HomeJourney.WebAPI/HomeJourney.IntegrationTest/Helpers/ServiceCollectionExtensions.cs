using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaIntegrationTestAndMock.IntegrationTest.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static void RemoveService<T>(this IServiceCollection services)
        {

            var descriptor = services.SingleOrDefault(
                   d => d.ServiceType ==
                       typeof(T));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }

        public static void RemoveDbContext<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            var descriptors = services.Where(
                d => d.ServiceType == typeof(TContext) ||
                     d.ServiceType == typeof(DbContextOptions<TContext>)
            ).ToList();

            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }
        }

    }
}
