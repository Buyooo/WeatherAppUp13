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
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<IGeocodingService, GeocodingService>();

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
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                
            });
        }
    }
}
