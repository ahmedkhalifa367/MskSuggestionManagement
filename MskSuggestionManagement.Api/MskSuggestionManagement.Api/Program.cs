using Microsoft.OpenApi.Models;
using MskSuggestionManagement.Application;
using MskSuggestionManagement.Infrastructure;
using MskSuggestionManagement.Infrastructure.DataManagement;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MskFrondEnd", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MSK Management Board",
        Description = "A regional director of health, safety and wellbeing at a client has been tasked with reducing rates of MSK absence in the company. They've rolled out VIDA to help them achieve it. Now they need to ensure employees are reducing their risk based on suggestions made by VIDA.",
        Contact = new OpenApiContact
        {
            Name = "Ahmed Khalifa",
            Email = "ahmedkhalifa367@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/ahmed-abd-el-ghany-khalifa-20a3b6152/")
        },
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("MskFrondEnd");
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MskManagementDbContext>();
    await SeedData.SeedAsync(context);
}
app.Run();