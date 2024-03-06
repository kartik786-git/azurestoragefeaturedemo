using azurestoragefeaturedemo.api;
using azurestoragefeaturedemo.api.Services;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddQueueServiceClient(builder.Configuration["ConnectionStrings:queuecs"]);
});
builder.Services.AddScoped<IAzureServiceBusService, AzureServiceBusService>();
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
