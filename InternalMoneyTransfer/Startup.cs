using System.Threading.Tasks;
using AutoMapper;
using InternalMoneyTransfer.Core;
using InternalMoneyTransfer.Core.DataModel;
using InternalMoneyTransfer.Core.Helpers;
using InternalMoneyTransfer.DAL;
using InternalMoneyTransfer.DAL.Repository.AccountRepository;
using InternalMoneyTransfer.DAL.Repository.TransactionRepository;
using InternalMoneyTransfer.DAL.Repository.UserRepository;
using InternalMoneyTransfer.Services.Transaction;
using InternalMoneyTransfer.Services.User;
using InternalMoneyTransfer.Services.UserAccount;
using InternalMoneyTransfer.SwaggerExt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace InternalMoneyTransfer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        private ServiceProvider ServiceProviderContainer { get; set; }
        public IHostingEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
                services.AddMvc(opts => { opts.Filters.Add(new AllowAnonymousFilter()); });
            else
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddSwaggerDocumentation();

            var key = JwtHelper.GetSecurityKey();
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                            var userId = int.Parse(context.Principal.Identity.Name);
                            var user = userService.GetUserById(userId);
                            if (user == null) context.Fail("Unauthorized");
                            return Task.CompletedTask;
                        }
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            var mappingConfig = new MapperConfiguration(
                config => { config.AddProfile(new MappingProfile()); });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IDbContext, ApplicationContext>();
            services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
            services.AddScoped<IAccountRepository<UserAccount>, AccountRepository>();

            services.AddScoped<ITransactionRepository<Transaction>, TransactionRepository>();
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IUserAccountService), typeof(UserAccountService));
            services.AddScoped(typeof(ITransactionService), typeof(TransactionService));

            ServiceProviderContainer = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) //, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseSwaggerDocumentation();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}