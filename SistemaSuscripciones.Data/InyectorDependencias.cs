using SistemaSuscripciones.Data.Infrastructure;
using SistemaSuscripciones.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using SistemaSuscripciones.Data.Infrastructure;
using SistemaSuscripciones.Data.Repositories;

namespace SistemaSuscripciones.Data
{
    public static class InyectorDependencias
    {
        public static void RegistrarRepositorios(IServiceCollection services)
        {
            services.AddScoped<ISuscripcionRepository, SuscripcionRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IPagoRepository, PagoRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IBitacoraRepository, BitacoraRepository>();
        }
    }
}
