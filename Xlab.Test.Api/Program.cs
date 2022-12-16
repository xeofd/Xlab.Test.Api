using Xlab.Test.Data;

const string localCorsOptions = "_localCorsOptions";
const string productionCorsOptions = "_productionCorsOptions";

var builder = WebApplication.CreateBuilder(args);

// Configure Mongodb

MongoDbConfigurator.Configure();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: localCorsOptions,
        policyBuilder => { policyBuilder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod(); });
    
    options.AddPolicy(name: productionCorsOptions, policyBuilder => policyBuilder.WithOrigins().WithMethods().AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddData(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(localCorsOptions);
}

if (app.Environment.IsProduction())
{
    app.UseCors(productionCorsOptions);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}