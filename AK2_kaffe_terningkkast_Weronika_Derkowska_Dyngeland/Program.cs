using System.Text.Json;
using BlazorApp1.Components;
using BlazorApp1.State;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient<GameApiClient>(client => client.BaseAddress = new Uri(builder.Configuration["GameApi:BaseUrl"] ?? throw new InvalidOperationException("GameApi BaseUrl is missing")));
builder.Services.AddScoped<GameNightState, GameNightState>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
// Minimal API
var json = File.ReadAllText("Data/games.json");

var games = JsonSerializer.Deserialize<List<BoardGame>>
(json,
    new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    }) ?? new List<BoardGame>();
app.MapGet("/api/games", () => games);
app.Run();