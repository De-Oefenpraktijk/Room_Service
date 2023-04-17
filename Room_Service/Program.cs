using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Room_Service.Authentication;
using Room_Service.Contracts;
using Room_Service.Data;
using Room_Service.Entities;
using Room_Service.Services.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


string domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = builder.Configuration["Auth0:Audience"];
        // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("create:public-rooms", policy => policy.Requirements.Add(new HasScopeRequirement("create:public-rooms", domain)));
});
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

// Add services to the container.
builder.Services.AddScoped<IDBContext, DBContext>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<ISocialServiceData, SocialServiceData>();
builder.Services.AddScoped<IPublicRoomService, PublicRoomService>();
builder.Services.AddScoped<IWorkspaceService, WorkspaceService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq();
});

//Add cors policy
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
