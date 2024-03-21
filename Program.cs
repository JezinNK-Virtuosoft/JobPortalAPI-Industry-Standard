using JobPortalAPI_1.Repository;
using JobPortalAPI_1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRegistration, Registration>();
builder.Services.AddScoped<IAdminDataHandling, AdminDataHandling>();
builder.Services.AddScoped<IRetrivingData, RetrievingData>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IValidation, Validation>();

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
