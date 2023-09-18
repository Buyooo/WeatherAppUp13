using Microsoft.OpenApi.Models;
using System.Reflection;
using WeatherAppUp13.Services;

namespace WeatherAppUp13
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin() // Allow requests from any origin
                           .AllowAnyMethod() // Allow any HTTP method
                           .AllowAnyHeader(); // Allow any HTTP headers
                });
            });

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherAppUp13 API", Version = "v1" });

                // Specify the path to the XML documentation file
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<IGeocodingService, GeocodingService>();
            services.AddTransient<IAddressCoordinatesParser, AddressCoordinatesParser>();

            services.AddHttpClient("GeocodingClient", client =>
            {
                var baseUrl = Configuration["GeocodingApi:BaseUrl"];
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddHttpClient("WeatherForecastClient", client =>
            {
                var baseUrl = Configuration["WeatherForecast:BaseUrl"];
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("User-Agent", "(upstart13app, htrevinomtz@gmail.com)");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                
            });
        }
    }
}
