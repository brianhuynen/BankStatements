using Microsoft.EntityFrameworkCore;
using BankStatements.Models;
using BankStatements.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IBankStatementService), typeof(BankStatementService));
builder.Services.AddScoped(typeof(IIbanService), typeof(IbanService));
/*
 //FOR EX 3:
 //add claims handler to only accept requests with a specific claim.
 builder.Services.AddTransient<IClaimsTransformation, BankStatements.ClaimsTransformer>();
 */
builder.Services.AddDbContext<BankStatementContext>(opt =>
    opt.UseInMemoryDatabase("BankStatement"));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "BankStatementsApi", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankStatementsApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// To ensure we can use this file for testing purposes
public partial class Program { }