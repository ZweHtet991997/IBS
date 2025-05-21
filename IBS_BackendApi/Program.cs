using IBS_BackendApi.Services.Account;
using IBS_BackendApi.Services.Admin;
using IBS_BackendApi.Services.Common;
using IBS_BackendApi.Services.Customer;
using IBS_BackendApi.Services.TransactionAndTopUp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Register DB Context Class
builder.Services.AddDbContext<EFDBContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("IBSBackendDBConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register my services
builder.Services.AddScoped<IDBOperationService, DBOperationService>();
builder.Services.AddScoped<IAdminServices, AdminServices>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<ITransactionAndTopupServices, TransactionAndTopupServices>();

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
