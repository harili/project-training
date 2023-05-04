using BankAccount.Core.Data.DbContext;
using BankAccount.Core.Services;
using BankAccount.Core.Services.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Not using appsetting.development.json properties with IConfiguration, it's easier to use a string to make it simple for the challenge
string connectionString = "Filename=:memory:";
SqliteConnection conn = new SqliteConnection(connectionString);
conn.Open();

builder.Services.AddDbContext<BankAccountDbContext>(opt => opt.UseSqlite(conn));

EnsureDatabase.Created(conn);

builder.Services.AddScoped<IAccountRepository, AccountService>();


builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.EnableEndpointRouting = false;
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "API Documentation",
        Description = "A RESTFul API for BankAccount kata",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "CHARFA Ilies", Email = "charfa.ilies@gmail.com" }
    });

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseMvcWithDefaultRoute();
app.UseSwagger();
app.UseSwaggerUI();


app.Run();

