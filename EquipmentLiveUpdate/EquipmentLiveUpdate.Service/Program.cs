using EquipmentLiveUpdate.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer("Server=mssql;Database=EquipmentLiveUpdate;User Id=appuser;Password=Passsword!123;TrustServerCertificate=True;"));

var app = builder.Build();

Thread.Sleep(90000);
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dbContext.Database.Migrate();
}

app.Run();