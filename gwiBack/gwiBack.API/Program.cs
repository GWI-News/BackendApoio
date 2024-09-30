using gwiBack.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace gwiBack.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar Swagger e habilitar anotações
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "gwiBack API", Version = "v1" });
                c.EnableAnnotations(); // Habilitar suporte a anotações
            });

            // Adicione o serviço do DbContext aqui
            builder.Services.AddDbContext<gwiDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Adicione os serviços de controle
            builder.Services.AddControllers(); // Use apenas AddControllers se você não precisar de views

            var app = builder.Build();

            // Ativar o middleware do Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "gwiBack API v1");
                    c.RoutePrefix = string.Empty; // Faz com que o Swagger UI esteja na raiz (http://localhost:5000/)
                });
            }

            // Configuração do pipeline HTTP
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers(); // Mapeia os controladores

            app.Run();
        }
    }
}
