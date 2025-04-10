using Microsoft.EntityFrameworkCore;
using TodoAssignment.Data;
using TodoAssignment.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Define CORS Policy
var corsPolicy = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, policy => // Make sure corsPolicy matches
    {
        policy.WithOrigins("http://localhost:51274") // Replace with your frontend URL
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
app.UseCors(corsPolicy); // Make sure to use the correct policy name

// Enable HTTPS Redirection and authorization middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers for the API
app.MapControllers();

// Optionally, serve static files (React frontend build)
app.UseStaticFiles(); // This will serve files from wwwroot folder
app.MapFallbackToFile("index.html"); // This serves the React app when a route is not found

app.Run();
