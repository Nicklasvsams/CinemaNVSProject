using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Repositories.Movies;
using CinemaNVS.DAL.Repositories.Transactions;
using CinemaNVS.DAL.Repositories.Users;
using CinemasNVS.BLL.Services.MovieServices;
using CinemasNVS.BLL.Services.TransactionServices;
using CinemasNVS.BLL.Services.UserServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CinemaNVS
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
            services.AddCors(options =>
            {
                options.AddPolicy(name: "_CORSRules",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddDbContext<CinemaDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CinemaDBContext")));

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();

            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IActorService, ActorService>();

            services.AddScoped<IMovieActorRepository, MovieActorRepository>();
            services.AddScoped<IMovieActorService, MovieActorService>();

            services.AddScoped<IDirectorRepository, DirectorRepository>();
            services.AddScoped<IDirectorService, DirectorService>();

            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<ILoginService, LoginService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBookingService, BookingService>();

            services.AddScoped<ISeatingRepository, SeatingRepository>();
            services.AddScoped<ISeatingService, SeatingService>();

            services.AddScoped<IShowingRepository, ShowingRepository>();
            services.AddScoped<IShowingService, ShowingService>();

            services.AddScoped<IBookingSeatingRepository, BookingSeatingRepository>();
            services.AddScoped<IBookingSeatingService, BookingSeatingService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CinemaNVS", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CinemaNVS v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors("_CORSRules");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
