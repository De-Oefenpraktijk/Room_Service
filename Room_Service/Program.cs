using Microsoft.EntityFrameworkCore;
using Room_Service.Contracts;
using Room_Service.Data;
using Room_Service.Entities;
using Room_Service.Services.Services;
using Seq.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDBContext, DBContext>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IWorkspaceService, WorkspaceService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq();
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
