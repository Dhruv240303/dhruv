using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// Employee CRUD endpoints
app.MapGet("/api/employees", async (AppDbContext db) =>
	await db.Employees.AsNoTracking().ToListAsync())
	.WithName("GetEmployees")
	.WithOpenApi();

app.MapGet("/api/employees/{id:int}", async (int id, AppDbContext db) =>
	await db.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id)
		is Employee employee
			? Results.Ok(employee)
			: Results.NotFound())
	.WithName("GetEmployeeById")
	.WithOpenApi();

app.MapPost("/api/employees", async (Employee employee, AppDbContext db) =>
{
	await db.Employees.AddAsync(employee);
	await db.SaveChangesAsync();
	return Results.Created($"/api/employees/{employee.Id}", employee);
})
	.WithName("CreateEmployee")
	.WithOpenApi();

app.MapPut("/api/employees/{id:int}", async (int id, Employee updatedEmployee, AppDbContext db) =>
{
	var existing = await db.Employees.FirstOrDefaultAsync(e => e.Id == id);
	if (existing is null)
	{
		return Results.NotFound();
	}

	existing.FirstName = updatedEmployee.FirstName;
	existing.LastName = updatedEmployee.LastName;
	existing.Email = updatedEmployee.Email;
	existing.Phone = updatedEmployee.Phone;
	existing.HireDate = updatedEmployee.HireDate;
	existing.Salary = updatedEmployee.Salary;
	existing.Department = updatedEmployee.Department;

	await db.SaveChangesAsync();
	return Results.NoContent();
})
	.WithName("UpdateEmployee")
	.WithOpenApi();

app.MapDelete("/api/employees/{id:int}", async (int id, AppDbContext db) =>
{
	var existing = await db.Employees.FirstOrDefaultAsync(e => e.Id == id);
	if (existing is null)
	{
		return Results.NotFound();
	}

	db.Employees.Remove(existing);
	await db.SaveChangesAsync();
	return Results.NoContent();
})
	.WithName("DeleteEmployee")
	.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
