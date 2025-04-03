
using Serilog;
using Serilog.Events;
using WebAppDatabase.Configuration;
using WebAppDatabase.DAO;
using WebAppDatabase.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the IoC container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IStudentDAO, StudentDAOImpl>(); //inject DAO implementation
builder.Services.AddScoped<IStudentService, StudentServiceImpl>(); // //inject service implementation
builder.Services.AddScoped<ITeacherDAO, TeacherDAOImpl>();
builder.Services.AddScoped<ITeacherService, TeacherServiceImpl>();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Host.UseSerilog((context, config) =>
{
     config.ReadFrom.Configuration(context.Configuration);
    //     .MinimumLevel.Debug()
    //     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    //     .Enrich.FromLogContext()
    //     // .Enrich.WithAspNetCore()
    //     .WriteTo.Console()
    //     .WriteTo.File(
    //         "Logs/logs.txt",
    //         rollingInterval: RollingInterval.Day,
    //         outputTemplate:
    //         "{Timestamp:dd-MM-yyyy HH:mm:ss:fff zzz} {SourceContext} [{Debug}]} {Message}{NewLine}{Exception}",
    //         retainedFileCountLimit: null, fileSizeLimitBytes: null);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();