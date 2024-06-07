using SignalRChat.Hubs;
using udganketo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<CosmosDbService>();

// Add SignalR with Azure SignalR service
builder.Services.AddSignalR().AddAzureSignalR(Environment.GetEnvironmentVariable("SIGNALR_CONNECTION_STRING"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<UdgAnketoHub>("/udganketohub");

app.Run();