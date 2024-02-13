using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persons.Data;
using Persons.Interfaces;
using Persons.Models;
using Persons.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Persons",
        Description = "Rest API приложение для работы с пользователями"
    });
    var basePath = AppContext.BaseDirectory;

    var xmlPath = Path.Combine(basePath, "Persons.xml");
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    Seed.SeedData(app);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Person/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Persons v1"));
    /*app.UseSwagger()
       .UseSwaggerUI(c =>
       {
           c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
       });*/
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Person}/{action=Index}/{id?}");

app.Run();
