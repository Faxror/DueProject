using DaireYonetimAPI.  Business.Abstrack;
using DaireYonetimAPI.Business.Concrete;
using DaireYonetimAPI.Controllers;
using DaireYonetimAPI.DataAccess;
using DaireYonetimAPI.DataAccess.Abstrack;
using DaireYonetimAPI.DataAccess.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDaireService, DaireManager>();
builder.Services.AddScoped<EmailSenderService>();
builder.Services.AddScoped<EmailController>();
builder.Services.AddScoped<IDaireRepository, DaireRepository>();
builder.Services.AddScoped<DaireRepository>();
builder.Services.AddHostedService<EmailSenderService>();

builder.Services.AddHostedService<DailyControlService>();

builder.Services.AddHostedService<TaxSenderService>();

builder.Services.AddScoped<IBakiyeService, BakiyeManager>();
builder.Services.AddScoped<IBakiyeRepository, BakiyeRepository>();
builder.Services.AddScoped<BakiyeRepository>();
builder.Services.AddScoped<TaxSenderService>();

builder.Services.AddDbContext<DaireDbContext>();

builder.Services.AddSwaggerGen(c => {
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
