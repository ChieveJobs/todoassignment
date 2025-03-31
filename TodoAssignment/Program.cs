using Microsoft.EntityFrameworkCore;
using TodoAssignment.Data;
using TodoAssignment.Repositories;

var builder = WebApplication.CreateBuilder(args);

var corsPolicy = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Frontend URL (React app)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API V1");
        options.RoutePrefix = string.Empty; // Sets Swagger UI at root
    });
}

// Enable CORS
app.UseCors(corsPolicy);

// Enable HTTPS Redirection and authorization middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers for the API
app.MapControllers();

// Optionally, serve static files (React frontend build)
app.UseStaticFiles(); // This will serve files from wwwroot folder
app.MapFallbackToFile("index.html"); // This serves the React app when a route is not found

app.Run();
