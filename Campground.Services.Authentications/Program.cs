using Campground.Shared.Authentication;
using Campground.Shared.Authentication.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddCustomJwtAuthentication(builder.Configuration);
builder.Services.AddScoped<JwtTokenHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://campgrounds-frontend.vercel.app")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins");

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();