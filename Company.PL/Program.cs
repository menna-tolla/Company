using Company.BLL.Interfaces;
using Company.BLL.Repositores;
using Company.DAL.Data.Contexts;
using Company.PL.Mapping;
using Company.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();// Register Built-in MVC Services
            builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>(); //Allow ID For DepartmentRepository
            builder.Services.AddScoped<IEmplyeeRepository, EmplyeeRepository>(); //Allow ID For EmplyeeRepository

            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile()));

            //builder.Services.AddIdentity<AppUser, IdentityRole>()
            //                 .AddEntityFrameworkStores<CompanyDbContext>()
            //                 .AddDefaultTokenProviders();

            // Life Time
            //builder.Services.AddScoped();    // Create Object Life Per Request - UnReachable Object 
            //builder.Services.AddTransient(); // Create Object Life Per Operation 
            //builder.Services.AddSingleton(); // Create Object Life Per Application

            builder.Services.AddScoped<IScopedService, ScopedService>(); // Per Request
            builder.Services.AddTransient<ITransentService, TransentService>(); // Per Operation
            builder.Services.AddSingleton<ISingletonService, SingletonService>(); // Per Application



            //builder.Services.Configure<Mailsettings>(builder.Configuration.GetSection(nameof(Mailsettings)));
            //builder.Services.AddScoped<IMailService, MailService>();
            //builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection(nameof(TwilioSettings)));
            //builder.Services.AddTransient<ITwilioService, TwilioService>();




            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); //Allow ID For CompanyDbContext

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
