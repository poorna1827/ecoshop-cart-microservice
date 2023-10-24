using CartMicroservice.Controllers;
using CartMicroservice.DbContexts;
using CartMicroservice.Dto;
using CartMicroservice.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net;
using System.Text.Json;
using Tests.MockData;
using Xunit;

namespace tests.Systems.Controllers
{
    public  class TestCartController : IDisposable
    {
        private readonly CartDbContext _context;
        public TestCartController()
        {
            var options = new DbContextOptionsBuilder<CartDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _context = new CartDbContext(options);

            _context.Database.EnsureCreated();

        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();

        }


        [Fact]
        public async Task ClearCustomerCartAsync_ShouldReturn200Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            ApiServiceMock.Setup(x => x.getUserId(It.IsAny<string>())).Returns(new Guid("20188938-5308-4B19-8E97-57E7F36A6184"));

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };



            //Act
            var result = await sut.ClearCustomerCart();



            //Assert
            result.GetType().Should().Be(typeof(OkResult));
        }

        [Fact]
        public async Task ClearCustomerCartAsync_ShouldReturn404Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            ApiServiceMock.Setup(x => x.getUserId(It.IsAny<string>())).Returns(new Guid("10188938-5308-4B19-8E97-57E7F36A6184"));

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };



            //Act
            var result = await sut.ClearCustomerCart();



            //Assert
            result.GetType().Should().Be(typeof(NotFoundResult));
        }

        [Fact]
        public async Task ClearCustomerCartAsync_ShouldReturn401Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            ApiServiceMock.Setup(x => x.getUserId(It.IsAny<string>())).Returns(new Guid("20188938-5308-4B19-8E97-57E7F36A6184"));

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };



            //Act
            var result = await sut.ClearCustomerCart();



            //Assert
            result.GetType().Should().Be(typeof(UnauthorizedResult));
        }


        [Fact]
        public async Task ReduceQuantityAsync_ShouldReturn200Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };



            //Act
            var cartId = new Guid("10288938-5308-4B19-8E97-57E7F36A6184");
            var result = await sut.ReduceQuantity(cartId);



            //Assert
            result.GetType().Should().Be(typeof(OkResult));
        }

        [Fact]
        public async Task ReduceQuantityAsync_ShouldReturn404Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };



            //Act
            var cartId = new Guid("20288938-5308-4B19-8E97-57E7F36A6184");
            var result = await sut.ReduceQuantity(cartId);



            //Assert
            result.GetType().Should().Be(typeof(NotFoundResult));
        }

        [Fact]
        public async Task ReduceQuantityAsync_ShouldReturn401Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };



            //Act
            var cartId = new Guid("10288938-5308-4B19-8E97-57E7F36A6184");
            var result = await sut.ReduceQuantity(cartId);



            //Assert
            result.GetType().Should().Be(typeof(UnauthorizedResult));
        }

        [Fact]
        public async Task DeleteCartItemAsync_ShouldReturn200Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };



            //Act
            var cartId = new Guid("10288938-5308-4B19-8E97-57E7F36A6184");
            var result = await sut.DeleteCartItem(cartId);



            //Assert
            result.GetType().Should().Be(typeof(OkResult));
        }
        [Fact]
        public async Task DeleteCartItemAsync_ShouldReturn404Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };



            //Act
            var cartId = new Guid("20288938-5308-4B19-8E97-57E7F36A6184");
            var result = await sut.DeleteCartItem(cartId);



            //Assert
            result.GetType().Should().Be(typeof(NotFoundResult));
        }

        [Fact]
        public async Task DeleteCartItemAsync_ShouldReturn401Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };



            //Act
            var cartId = new Guid("10288938-5308-4B19-8E97-57E7F36A6184");
            var result = await sut.DeleteCartItem(cartId);



            //Assert
            result.GetType().Should().Be(typeof(UnauthorizedResult));
        }

        [Fact]
        public async Task AddtoCartAsync_ShouldReturn200Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            ApiServiceMock.Setup(x => x.getUserId(It.IsAny<string>())).Returns(new Guid("20188938-5308-4B19-8E97-57E7F36A6184"));


            //Product microservice call mock 
            ApiServiceMock.Setup(x => x.isValidProduct(It.IsAny<Guid>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };



            //Act
            AddToCartDto data = new AddToCartDto()
            {
                PId = new Guid("30188938-5308-4B19-8E97-57E7F36A6184")

            };
            var result = await sut.AddtoCart(data);



            //Assert
            result.GetType().Should().Be(typeof(OkResult));
        }


        [Fact]
        public async Task AddtoCartAsync_ShouldReturn404Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            ApiServiceMock.Setup(x => x.getUserId(It.IsAny<string>())).Returns(new Guid("20188938-5308-4B19-8E97-57E7F36A6184"));


            //Product microservice call mock 
            ApiServiceMock.Setup(x => x.isValidProduct(It.IsAny<Guid>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound));

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };



            //Act
            AddToCartDto data = new AddToCartDto()
            {
                PId = new Guid("30188938-5308-4B19-8E97-57E7F36A6184")

            };
            var result = await sut.AddtoCart(data);



            //Assert
            result.GetType().Should().Be(typeof(NotFoundResult));
        }

        [Fact]
        public async Task AddtoCartAsync_ShouldReturn401Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            ApiServiceMock.Setup(x => x.getUserId(It.IsAny<string>())).Returns(new Guid("20188938-5308-4B19-8E97-57E7F36A6184"));


            //Product microservice call mock 
            ApiServiceMock.Setup(x => x.isValidProduct(It.IsAny<Guid>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };



            //Act
            AddToCartDto data = new AddToCartDto()
            {
                PId = new Guid("30188938-5308-4B19-8E97-57E7F36A6184")

            };
            var result = await sut.AddtoCart(data);



            //Assert
            result.GetType().Should().Be(typeof(UnauthorizedResult));
        }

        [Fact]
        public async Task GetItemsAsync_ShouldReturn200Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            ApiServiceMock.Setup(x => x.getUserId(It.IsAny<string>())).Returns(new Guid("20188938-5308-4B19-8E97-57E7F36A6184"));


            //product microservice call mock 
            var products = new[] {
                                    new {
                                            pId = "30188938-5308-4B19-8E97-57E7F36A6184",
                                            name = "Trefoil Linear Tee",
                                            price = 1500,
                                            image = "https://senjota.blob.core.windows.net/clothes/ryan-hoffman-6Nub980bI3I-unsplash.jpg"
                                        }
                                 };
            var json = JsonSerializer.Serialize(products);
            ApiServiceMock.Setup(x => x.getProductDetails(It.IsAny<List<Guid>>())).ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            });


            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };




            var result = await sut.GetItems();



            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }


        [Fact]
        public async Task GetItemsAsync_ShouldReturn503Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));
            ApiServiceMock.Setup(x => x.getUserId(It.IsAny<string>())).Returns(new Guid("20188938-5308-4B19-8E97-57E7F36A6184"));


            //product microservice call mock 

            ApiServiceMock.Setup(x => x.getProductDetails(It.IsAny<List<Guid>>())).ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError
            });


            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };




            var result = await sut.GetItems();



            //Assert
            result.Should().BeOfType<ObjectResult>()
                            .Which.StatusCode.Should().Be(503);  
        }

        [Fact]
        public async Task GetItemsAsync_ShouldReturn401Status()
        {
            //Arrange


            //Identity microservice call mock 
            var ApiServiceMock = new Mock<IApiService>();
            ApiServiceMock.Setup(x => x.isAuthorized(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            ApiServiceMock.Setup(x => x.getUserId(It.IsAny<string>())).Returns(new Guid("20188938-5308-4B19-8E97-57E7F36A6184"));



            //product microservice call mock 
            var products = new[] {
                                    new {
                                            pId = "30188938-5308-4B19-8E97-57E7F36A6184",
                                            name = "Trefoil Linear Tee",
                                            price = 1500,
                                            image = "https://senjota.blob.core.windows.net/clothes/ryan-hoffman-6Nub980bI3I-unsplash.jpg"
                                        }
                                 };
            var json = JsonSerializer.Serialize(products);
            ApiServiceMock.Setup(x => x.getProductDetails(It.IsAny<List<Guid>>())).ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            });

            //database mock
            _context.Cart.AddRange(CartMockData.GetSampleCartItems());
            _context.SaveChanges();


            //request header token mock
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Headers["Authorization"]).Returns("Bearer test_token");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);


            var sut = new CartController(_context, ApiServiceMock.Object);

            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };




            var result = await sut.GetItems();



            //Assert
            result.GetType().Should().Be(typeof(UnauthorizedResult));
        }




    }
}
