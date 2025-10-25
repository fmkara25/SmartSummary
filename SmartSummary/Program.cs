using SmartSummary.Services;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// 🔹 OpenAI HTTP Client yapılandırması
builder.Services.AddHttpClient("OpenAI", (sp, client) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    // appsettings.json → "OpenAI:ApiBase": "https://api.openai.com/v1"
    client.BaseAddress = new Uri(cfg["OpenAI:ApiBase"]!);
    // User Secrets → "OpenAI:ApiKey": "sk-...."
    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", cfg["OpenAI:ApiKey"]);
});

// 🔹 OpenAI servisimiz (bir sonraki adımda dosyasını oluşturacağız)
builder.Services.AddScoped<IOpenAiService, OpenAiService>();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Summary}/{action=Index}/{id?}"); // İstersen Home/Index kalabilir

app.Run();
