//using Microsoft.AspNetCore.Mvc;
//using MusicShop.API.Features.Playlists;
//using MusicShop.Data;



//namespace MusicShop.Tests
//{
//    public class ArtistControllerTests
//    {
//        ArtistController artistController;
//        public ArtistControllerTests()
//        {
//            IArtistRepository artistRepository = new FakeArtistRepository();
//            this.artistController = new ArtistController(artistRepository);
//        }

//        [Fact]
//        public void GetAll_Returns_Artists()
//        {
//            var result = artistController.GetAll();
//            Assert.NotEmpty(result);
//        }

//        [Fact]
//        public void Get_Return_Artist()
//        {
//            var result = artistController.Get(new API.Features.Artist.Request.ArtistGetRequest()
//            {
//                ArtistId = 1
//            });
//            Assert.True((result as OkObjectResult).StatusCode == 200);
//        }

//        [Fact]
//        public void Get_Return_Null()
//        {
//            var artist = artistController.Get(new API.Features.Artist.Request.ArtistGetRequest()
//            {
//                ArtistId = 0
//            });

//            var result = artist as NotFoundObjectResult;
//            var text = result!.Value!.ToString();

//            Assert.True(result.StatusCode == 404);
//            Assert.True(text == "Исполнитель 0 не найден");
//        }

//        [Fact]
//        public void Insert_Is_Inserted()
//        {
//            var artist = artistController.InsertArtist(new API.Features.Artist.Request.ArtistInsertReqestcs()
//            {
//                Name = "ok"
//            });

//            var result = artist as OkObjectResult;
//            Assert.True(result!.StatusCode == 200);
//        }

//        [Fact]
//        public void Insert_Is_Not_Inserted_Because_Exist()
//        {
//            var artist = artistController.InsertArtist(new API.Features.Artist.Request.ArtistInsertReqestcs()
//            {
//                Name = "exist"
//            });

//            var result = artist as BadRequestObjectResult;
//            Assert.True(result!.StatusCode == 400);
//            var text = result!.Value!.ToString();
//            Assert.Equal("Такой исполнитель уже существует", text);
//        }

//        [Fact]
//        public void Insert_Artist_When_Id_Is_Null()
//        {
//            var artist = artistController.InsertArtist(new API.Features.Artist.Request.ArtistInsertReqestcs()
//            {
//                Name = "bad"
//            });
//            var result = artist as BadRequestObjectResult;
//            Assert.True(result!.StatusCode == 400);
//            var text = result!.Value!.ToString();
//            Assert.Equal("Не удалось создать исполнителя", text);
//        }

//        [Fact]
//        public void Search_Returns_Artists()
//        {
//            var result = artistController.Search(new API.Features.Artist.Request.ArtistSearchReqest()
//            {
//                NameSearch = "Artist"
//            });
//            Assert.NotEmpty(result);
//            Assert.Equal(2, result.Count);
//        }

//        [Fact]
//        public void Search_Returns_Empty()
//        {
//            var result = artistController.Search(new API.Features.Artist.Request.ArtistSearchReqest()
//            {
//                NameSearch = "NonExisting"
//            });
//            Assert.Empty(result);
//        }

//        [Fact]
//        public void Delete_Existing_Artist()
//        {
//            var result = artistController.DeleteArtist(new API.Features.Artist.Request.ArtistDeleteRequest()
//            {
//                ArtistId = 1
//            });
//            var okResult = result as OkObjectResult;
//            Assert.NotNull(okResult);
//            Assert.Equal(200, okResult.StatusCode);
//            Assert.Equal("Исполнитель 1 удален", okResult.Value);
//        }

//        [Fact]
//        public void Delete_NonExisting_Artist()
//        {
//            var result = artistController.DeleteArtist(new API.Features.Artist.Request.ArtistDeleteRequest()
//            {
//                ArtistId = 999
//            });
//            var notFoundResult = result as NotFoundObjectResult;
//            Assert.NotNull(notFoundResult);
//            Assert.Equal(404, notFoundResult.StatusCode);
//            Assert.Equal("Исполнитель 999 не найден", notFoundResult.Value);
//        }

//        [Fact]
//        public void Update_Existing_Artist()
//        {
//            var result = artistController.UpdateArtist(new API.Features.Artist.Request.ArtistUpdateRequest()
//            {
//                ArtistId = 2,
//                Name = "UpdatedName"
//            });
//            var okResult = result as OkObjectResult;
//            Assert.NotNull(okResult);
//            Assert.Equal(200, okResult.StatusCode);
//            Assert.Equal("Исполнитель 2 обновлен", okResult.Value);
//        }

//        [Fact]
//        public void Update_NonExisting_Artist()
//        {
//            var result = artistController.UpdateArtist(new API.Features.Artist.Request.ArtistUpdateRequest()
//            {
//                ArtistId = 999,
//                Name = "NewName"
//            });
//            var notFoundResult = result as NotFoundObjectResult;
//            Assert.NotNull(notFoundResult);
//            Assert.Equal(404, notFoundResult.StatusCode);
//            Assert.Equal("Исполнитель 999 не найден", notFoundResult.Value);
//        }
//    }
//}