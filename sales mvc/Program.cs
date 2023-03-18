using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sales_mvc.Data;
using sales_mvc.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<sales_mvcContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("sales_mvcContext") , ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("sales_mvcContext"))));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SeddingService>();

builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<SalesRecordService>();




var app = builder.Build();

var seddingService = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<SeddingService>();

seddingService.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var enUS = new CultureInfo("en-US");
var localizationOption = new RequestLocalizationOptions {
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(enUS),
    SupportedCultures = new List<CultureInfo> { enUS },
    SupportedUICultures = new List<CultureInfo> { enUS }
};

app.UseRequestLocalization(localizationOption);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
