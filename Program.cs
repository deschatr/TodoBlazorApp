using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Net.Http.Headers;
using BlazorApp.Data;
using TodoItemManagement;

// specifies the URI to the base of the api, so before the path before api/todoitems
const string baseAddress = "http://localhost:5097/";
//const string baseAddress =  "https://appservice.azurewebsites.net/";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// creates data service for todo items using the IHttpClientFactory
// it uses the interface and class defined for the TodoItemService located in /Data
builder.Services.AddHttpClient<ITodoItemService,TodoItemService>("todoAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri(baseAddress);
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept,"application/json");
    httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent,"BlazorApp/0.2");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
