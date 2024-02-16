using Campground.Services.Emails.Services;
using Campground.Shared.Communication.AzureServiceBus;
using Campground.Shared.Communication.AzureServiceBus.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<EmailService>();
builder.Services.AddSingleton<IMessageHandler, EmailService>();
builder.Services.AddAzureServiceBusHandler(builder.Configuration);

builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Iniciar el procesador de Service Bus
var serviceBusHandler = app.Services.GetRequiredService<IMessageSender>() as AzureServiceBusHandler;
serviceBusHandler!.RegisterMessageHandler(builder.Configuration.GetSection("Queue:Name").Value!, app.Services.GetRequiredService<IMessageHandler>());
serviceBusHandler!.StartProcessingAsync(builder.Configuration.GetSection("Queue:Name").Value!).GetAwaiter().GetResult();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
