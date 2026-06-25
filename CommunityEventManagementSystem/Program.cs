using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Data.SeedData;
using CommunityEventManagementSystem.Repositories.Interfaces;
using CommunityEventManagementSystem.Repositories.Implementations;
using CommunityEventManagementSystem.Services.Interfaces;
using CommunityEventManagementSystem.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Blazored.Toast;

var builder = WebApplication.CreateBuilder(args);

// UI libraries & static assets
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configure EF Core: use SQLite if DefaultConnection provided, otherwise InMemory for dev/tests
var defaultConn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (!string.IsNullOrWhiteSpace(defaultConn) && defaultConn != "UseInMemory")
    {
        options.UseSqlite(defaultConn);
    }
    else
    {
        options.UseInMemoryDatabase("CEMS_InMemory");
    }
});

// Register repositories
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IVenueRepository, VenueRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();

// Register services
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<IActivityService, ActivityService>();

// Blazored.Toast for nice toasts
builder.Services.AddBlazoredToast();

var app = builder.Build();

// Ensure DB is created and seeded (safe for InMemory or SQLite)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        if (context.Database.IsRelational())
        {
            context.Database.EnsureCreated();
        }

        await DbSeeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during DB setup: {ex.Message}");
        throw;
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
