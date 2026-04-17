using RoomReservationApi.Controllers;
using RoomReservationApi.Repositories;
using RoomReservationApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRoomService,RoomService>();
builder.Services.AddSingleton<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IReservationService,ReservationService>();
builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "RoomReservationApi v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();