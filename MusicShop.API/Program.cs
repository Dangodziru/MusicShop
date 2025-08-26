using MusicShop.API;
using MusicShop.Bussines.Features.Albums.Service;
using MusicShop.Bussines.Features.Artists.Service;
using MusicShop.Bussines.Features.Customers.Services;
using MusicShop.Bussines.Features.Genre.Services;
using MusicShop.Bussines.Features.MediaType.Services;
using MusicShop.Bussines.Features.Playlist.Services;
using MusicShop.Bussines.Features.Tracks.Services;
using MusicShop.Data;
using MusicShop.Data.Dapper;
using MusicShop.DataEntity;
using MusicShop.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediator();

//EF
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();

builder.Services.AddTransient<IAlbumRepository, AlbumDapperRepository>();
builder.Services.AddTransient<IGenreRepository, GenreDapperRepository>();
builder.Services.AddTransient<ICustomerRepositoty, CustomerDapperRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeDapperRepository>();
builder.Services.AddTransient<IMediaTypeRepository, MediaTypeDapperRepository>();
builder.Services.AddTransient<IPlaylistRepository, PlaylistDapperRepository>();
builder.Services.AddTransient<ITrackDapperRepository, TrackDapperRepository>();

builder.Services.AddTransient<IArtistService, ArtistService>();
builder.Services.AddTransient<IPlaylistService, PlaylistService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IMediaTypeService, MediaTypeService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IAlbumService, AlbumService>();
builder.Services.AddTransient<ITrackService, TrackService>();

builder.Services.AddDbContext<ChinookContext>();

var app = builder.Build();

foreach (var service in builder.Services)
{
    Console.WriteLine($"Service: {service.ServiceType.Name}, Lifetime: {service.Lifetime}");
}

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
