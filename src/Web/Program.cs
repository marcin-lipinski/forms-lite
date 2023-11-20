using FastEndpoints;
using FastEndpoints.Swagger;
using FluentValidation;
using Infrastructure;
using Web.Handlers.QuizHandlers.Create;
using Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddFastEndpoints().SwaggerDocument();

builder.Services.AddAuthorization();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddExceptionHandler();
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssembly(typeof(CreateQuizValidator).Assembly);


builder.Services.AddCors(opt => 
    opt.AddPolicy("CorsPolicy", policy => policy.WithOrigins("http://localhost:3000", "https://formslite-frontend.azurewebsites.net").AllowAnyMethod().AllowAnyHeader().AllowCredentials()));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseFastEndpoints().UseSwaggerGen();

app.UseAuthentication();
app.UseAuthorization();
app.UseInfrastructure(app.Configuration);

app.MapControllers();

app.UseExceptionHandler();

app.Run();