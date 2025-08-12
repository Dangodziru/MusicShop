using Microsoft.AspNetCore.Mvc;
using MusicShop.Bussines.Features.Artists.Delete;
using MusicShop.Bussines.Features.Artists.Get;
using MusicShop.Bussines.Features.Artists.Insert;
using MusicShop.Bussines.Features.Artists.Search;
using MusicShop.Bussines.Features.Artists.Update;
using MusicShop.Data;



namespace MusicShop.Tests
{
    public class ArtistControllerTests
    {
        ArtistController artistController;
        public ArtistControllerTests()
        {
            IArtistRepository artistRepository = new FakeArtistRepository();
            this.artistController = new ArtistController(artistRepository);
        }

        [Fact]
        public async Task GetAll_Returns_Artists()
        {
            var result = await artistController.GetAll();
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Get_Return_Artist()
        {
            var result = await artistController.Get(new ArtistGetRequest()
            {
                ArtistId = 1
            });
            Assert.True((result as OkObjectResult).StatusCode == 200);
        }

        [Fact]
        public async void Get_Return_Null()
        {
            var artist = await artistController.Get(new ArtistGetRequest()
            {
                ArtistId = 0
            });

            var result = artist as NotFoundObjectResult;
            var text = result!.Value!.ToString();

            Assert.True(result.StatusCode == 404);
            Assert.True(text == "Исполнитель 0 не найден");
        }

        [Fact]
        public async Task Insert_Is_Inserted()
        {
            var artist = await artistController.InsertArtist(new ArtistInsertRequests()
            {
                Name = "ok"
            });

            var result = artist as OkObjectResult;
            Assert.True(result!.StatusCode == 200);
        }

        [Fact]
        public async Task Insert_Is_Not_Inserted_Because_Exist()
        {
            var artist = await artistController.InsertArtist(new ArtistInsertRequests()
            {
                Name = "Artist1"
            });

            var result = artist as BadRequestObjectResult;
            Assert.True(result!.StatusCode == 400);
            var text = result!.Value!.ToString();
            Assert.Equal("Такой исполнитель уже существует", text);
        }

        [Fact]
        public async Task Insert_Artist_When_Id_Is_Null()
        {
            var artist = await artistController.InsertArtist(new ArtistInsertRequests()
            {
                Name = "bad"
            });
            var result = artist as BadRequestObjectResult;
            Assert.True(result!.StatusCode == 400);
            var text = result!.Value!.ToString();
            Assert.Equal("Не удалось создать исполнителя", text);
        }

        [Fact]
        public async Task Search_Returns_Artists()
        {
            var result = await artistController.Search(new ArtistSearchRequest()
            {
                NameSearch = "Artist"
            });
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Search_Returns_Empty()
        {
            var result = await artistController.Search(new ArtistSearchRequest()
            {
                NameSearch = "NonExisting"
            });
            Assert.Empty(result);
        }

        [Fact]
        public async Task Delete_Existing_Artist()
        {
            var result = await artistController.DeleteArtist(new ArtistDeleteRequest()
            {
                ArtistId = 1
            });
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Исполнитель 1 удален", okResult.Value);
        }

        [Fact]
        public async Task Delete_NonExisting_Artist()
        {
            var result = artistController.DeleteArtist(new ArtistDeleteRequest()
            {
                ArtistId = 999
            });
            var notFoundResult = await result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Исполнитель 999 не найден", notFoundResult.Value);
        }

        [Fact]
        public async Task Update_Existing_Artist()
        {
            var result = await artistController.UpdateArtist(new ArtistUpdateRequest()
            {
                ArtistId = 2,
                Name = "UpdatedName"
            });
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Исполнитель 2 обновлен", okResult.Value);
        }

        [Fact]
        public async Task Update_NonExisting_Artist()
        {
            var result = await artistController.UpdateArtist(new ArtistUpdateRequest()
            {
                ArtistId = 999,
                Name = "NewName"
            });
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Исполнитель 999 не найден", notFoundResult.Value);
        }
    }
}