using JWTWithRest.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "halkbank.server",
                        ValidateAudience = true,
                        ValidAudience = "halkbank.client",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("onay-icin-gereken-cumle")),
                        ValidateIssuerSigningKey = true
                    };
                });

builder.Services.AddCors(opt => opt.AddPolicy("allow", builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();

    /*
     *   http://www.turkayurkmez.com
     *   https://www.turkayurkmez.com
     *   https://post.turkayurkmez.com
     *   https://post.turkayurkmez.com:8088
     *   
     *   
     */
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("allow");
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (!context.Response.Headers.ContainsKey("X-Frame-Options"))
    {
        context.Response.Headers.Add("X-Frame-Options", "DENY");
    }
    await next();
});

app.MapControllers();

app.Run();
