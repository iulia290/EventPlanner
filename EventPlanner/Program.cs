using EventPlanner.Data;
using EventPlanner.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<EventPlannerContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services
    .AddIdentityApiEndpoints<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddRazorPages();
builder.Services.AddControllers();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
    {
        var roleResult = roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
        if (!roleResult.Succeeded)
            throw new Exception("Cannot create Admin role: " +
                string.Join(" | ", roleResult.Errors.Select(e => e.Description)));
    }

    var adminEmail = "admin@eventplanner.local";
    var adminPassword = "Admin123!";

    var admin = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
    if (admin == null)
    {
        admin = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var createAdmin = userManager.CreateAsync(admin, adminPassword).GetAwaiter().GetResult();
        if (!createAdmin.Succeeded)
            throw new Exception("Cannot create Admin user: " +
                string.Join(" | ", createAdmin.Errors.Select(e => e.Description)));
    }

    if (!userManager.IsInRoleAsync(admin, "Admin").GetAwaiter().GetResult())
    {
        var addRole = userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
        if (!addRole.Succeeded)
            throw new Exception("Cannot add Admin role to Admin user: " +
                string.Join(" | ", addRole.Errors.Select(e => e.Description)));
    }

    var userEmail = "user@eventplanner.local";
    var userPassword = "User123!";

    var user = userManager.FindByEmailAsync(userEmail).GetAwaiter().GetResult();
    if (user == null)
    {
        user = new IdentityUser
        {
            UserName = userEmail,
            Email = userEmail,
            EmailConfirmed = true
        };

        var createUser = userManager.CreateAsync(user, userPassword).GetAwaiter().GetResult();
        if (!createUser.Succeeded)
            throw new Exception("Cannot create User: " +
                string.Join(" | ", createUser.Errors.Select(e => e.Description)));
    }
}



if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.MapIdentityApi<IdentityUser>();

app.Run();
