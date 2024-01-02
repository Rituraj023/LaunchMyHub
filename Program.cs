using LaunchMyHub.Data;
using LaunchMyHub.DTOs;
using LaunchMyHub.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("fer_Connection")));


builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

// Configure Email Settings (as a singleton if it's a configuration object)
builder.Services.AddSingleton(builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>());
builder.Services.AddScoped<IEmailSender, EmailSender>();

// Add CORS services and set the policy to allow any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("CallCreateClientPolicy", policy =>
    {
        policy.WithOrigins(builder.Configuration.GetValue<string>("Client:CallerUrl")) // Replace with your client's URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // If cookies or authentication is involved
    });
});


// Add services to the container.
builder.Services.AddRazorPages();


// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();


// Use CORS with the specified policy
// Error handling middleware
 if (app.Environment.IsDevelopment())
    {
    app.UseDeveloperExceptionPage();
}
    else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You might want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// CORS middleware
app.UseCors("CallCreateClientPolicy");

// Custom Middleware for Headers
app.Use(async (context, next) =>
{
    context.Response.Headers.Remove("X-Frame-Options");
    // To set CSP header:
    context.Response.Headers.Add("Content-Security-Policy", "frame-ancestors 'self' http://localhost/");

    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Place any middleware that should be executed after routing but before authorization here
// For example, custom middleware for logging, validation, etc.

app.UseAuthorization();

// Endpoint mapping
app.MapRazorPages();

app.Run();