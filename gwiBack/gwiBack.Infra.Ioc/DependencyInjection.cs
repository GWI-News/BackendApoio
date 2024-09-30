using gwiBack.Application.Services;
using gwiBack.Domain.Interfaces;
using gwiBack.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

public class DependencyInjectionConfig
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Repositórios
        services.AddScoped<IFormationRepository, FormationRepository>();
        services.AddScoped<IProfessionalInformationRepository, ProfessionalInformationRepository>();
        services.AddScoped<IProfessionalSkillRepository, ProfessionalSkillRepository>();

        // Serviços
        services.AddScoped<IFormationService, FormationService>();
        services.AddScoped<IProfessionalInformationService, ProfessionalInformationService>();
        services.AddScoped<IProfessionalSkillService, ProfessionalSkillService>();
    }
}
