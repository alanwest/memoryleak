using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource =>
    {
        resource.AddService("DotnetRuntimeMetrics");
    })
    .WithMetrics(meterProviderBuilder =>
    {
        meterProviderBuilder
            .AddRuntimeInstrumentation()
            .AddProcessInstrumentation()
            .AddOtlpExporter();
    });

builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
