using Bowllytics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterProgram(builder.Configuration);

var app = builder.Build();
app.UseProgram();

await app.RunAsync();