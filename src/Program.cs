using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Factories;
using ProvaPub.Services.Interfaces;
using ProvaPub.Services.PaymentProcessors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));

builder.Services.AddScoped<IPaymentProcessor, PixPaymentProcessor>();
builder.Services.AddScoped<IPaymentProcessor, CreditCardPaymentProcessor>();
builder.Services.AddScoped<IPaymentProcessor, PaypalPaymentProcessor>();

builder.Services.AddScoped<IPaymentProcessorFactory, PaymentProcessorFactory>();


builder.Services.AddScoped<RandomService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();


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
