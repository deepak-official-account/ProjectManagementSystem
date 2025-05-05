
using System.Runtime.CompilerServices;
using PatientManagementSystem.API.Services;

namespace PatientManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Add SQL Server connection string
            builder.Services.AddScoped<IPatientService, PatientServiceImpl>();
            //avoid Cors
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllOrigins",
            //        builder =>
            //        {
            //            builder.AllowAnyOrigin()
            //                   .AllowAnyMethod()
            //                   .AllowAnyHeader();
            //        });
            //});
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:7247") // Specify your allowed origin here
                                                                      //    .AllowAnyHeader()
                               .AllowAnyMethod();

                        // If you need to allow multiple specific origins:
                        // builder.WithOrigins("https://www.domain1.com", "https://www.domain2.com")

                        // If you need to allow credentials:
                         builder.AllowCredentials();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowSpecificOrigin"); // <-- ADD this line BEFORE UseAuthorization()

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
