using AspNetCore.Swagger.Themes;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(
        ModernStyle.Futuristic,
        options => options.SwaggerEndpoint("/openapi/v1.json", "ServiceA v1"));
}

app.UseAuthorization();
app.MapControllers();
app.Run();