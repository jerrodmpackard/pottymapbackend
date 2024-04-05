using Microsoft.EntityFrameworkCore;
using pottymapbackend.Services;
using pottymapbackend.Services.Context;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// builder.Services.AddScoped<Insert Service Name here>(); Don't forget the parenthesis
// If your service name is giving an error with red squiggly line underneath, use quickfix to add the using statement at the top
    // --> using projectName.Services
builder.Services.AddScoped<BathroomService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordService>();


// Now we want to add our connection string. We will create a variable to hold the connection string
var connectionString = builder.Configuration.GetConnectionString("MyConnectionString");


// Adding DbContext
// When adding DataContext, add the using Day2_BlockBackEnd.Services.Context at the top
// Configures entity framework core to use SQL server as the database provider for a datacontext DbContext in our project
builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connectionString));

builder.Services.AddCors(options => options.AddPolicy("PottyMapPolicy", 
builder => {
    builder.WithOrigins("http://localhost:5156")
    .AllowAnyHeader()
    .AllowAnyMethod();
}));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PottyMapPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
