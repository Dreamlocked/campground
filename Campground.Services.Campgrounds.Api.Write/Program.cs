using Campground.Services.Campgrounds.Api.Write.Utils;
using Campground.Services.Campgrounds.Infrastructure;
using Campground.Shared.Communication.AzureServiceBus.Interfaces;
using Campground.Shared.Communication.AzureServiceBus;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Campground.Services.Campgrounds.Infrastructure.WebSockets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Custom", o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey") ?? builder.Configuration["Jwt:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };

    o.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies[builder.Configuration["Jwt:CookieName"]!];
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes("Custom")
        .Build();
});


builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

var serviceBusHandler = app.Services.GetRequiredService<IMessageSender>() as AzureServiceBusHandler;
serviceBusHandler!.RegisterMessageHandler(builder.Configuration.GetSection("Queue:Billing").Value!, app.Services.GetRequiredService<IMessageHandler>());
serviceBusHandler!.StartProcessingAsync(builder.Configuration.GetSection("Queue:Billing").Value!).GetAwaiter().GetResult();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<MessageHub>("api/socket");

app.Run();
