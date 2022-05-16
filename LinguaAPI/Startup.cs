using AutoMapper;
using LinguaAPI.Mapper;
using LinguaAPI.Repositories.Dapper;
using LinguaAPI.Repositories.Dapper.Implementations;
using LinguaAPI.Repositories.Dapper.Interfaces;
using LinguaAPI.Services.Implementations;
using LinguaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace LinguaAPI
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
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WebMappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            services.AddCors(options =>
            {
                options.AddPolicy("MyDefaultCorsPolicy", builder =>
                {
                    //builder.WithOrigins(appSettings.CORSPolicyUrls);
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinguaAPI", Version = "v1" });
            });

            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));


            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IPredmetiRepository, PredmetiRepository>();
            services.AddTransient<IUcioniceRepository, UcioniceRepository>();
            services.AddTransient<ITipoviNastaveRepository, TipoviNastaveRepository>();
            services.AddTransient<IDokumentiRepository, DokumentiRepository>();
            services.AddTransient<IPredavanjaRepository, PredavanjaRepository>();
            services.AddTransient<IPredavanjaSudioniciRepository, PredavanjaSudioniciRepository>();
            services.AddTransient<ICalendarEventsRepository, CalendarEventsRepository>();


            //services.AddTransient<IKontaktFormaService, KontaktFormaService>();
            //services.AddTransient<IUcioniceService, UcioniceService>();
            //services.AddTransient<IPredmetiService, PredmetiService>();
            //services.AddTransient<ITokensService, TokensService>();
            //services.AddTransient<IDokumentiService, DokumentiService>();
            //services.AddTransient<ITipoviNastaveService, TipoviNastaveService>();
            //services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IDokumentiService, DokumentiService>();
            services.AddTransient<IPredavanjaService, PredavanjaService>();
            services.AddTransient<ICalendarEventsService, CalendarEventsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinguaAPI v1"));
            }

            app.UseCors("MyDefaultCorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
