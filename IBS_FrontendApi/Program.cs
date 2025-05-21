using IBS_FrontendApi.Helper;
using IBS_FrontendApi.Services.ApiServices;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register Custom Services
builder.Services.AddSingleton<ValidationHelper>();
builder.Services.AddSingleton<HttpStatusCodeToCsustomCode>();
builder.Services.AddScoped<IAdminApiServices, AdminApiServices>();
builder.Services.AddScoped<ICustomerApiServices, CustomerApiServices>();
builder.Services.AddScoped<IAccountApiServices, AccountApiServices>();
builder.Services.AddScoped<ITransactionApiServices, TransactionApiServices>();

var app = builder.Build();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
    var response = new { error = exception.Message };
    await context.Response.WriteAsJsonAsync(response);
}));

app.UseExceptionHandler("/error");

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
