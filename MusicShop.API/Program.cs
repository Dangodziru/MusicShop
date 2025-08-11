using MusicShop.Data;
using MusicShop.Data.Dapper;
using MusicShop.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediator();
builder.Services.AddTransient<artistRepository, ArtistDapperRepository>();
builder.Services.AddTransient<IAlbumRepository, AlbumDapperRepository>();
builder.Services.AddTransient<IGenreRepository, GenreDapperRepository>();
builder.Services.AddTransient<ICustomerRepositoty, CustomerDapperRepositoty>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeDapperRepository>();
builder.Services.AddTransient<IMediaTypeRepository, MediaTypeDapperRepositiry>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
