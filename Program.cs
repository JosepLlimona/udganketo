using SignalRChat.Hubs;
using udganketo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<CosmosDbService>();
builder.Services.AddSignalR().AddAzureSignalR("Endpoint=https://udganketo.service.signalr.net;AccessKey=uKv5ecO35CwEUASkJhXcwuBI+6d7YjD9nbiHtNrCj4I=;Version=1.0;");

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

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<ChatHub>("/chathub");

app.Run();
