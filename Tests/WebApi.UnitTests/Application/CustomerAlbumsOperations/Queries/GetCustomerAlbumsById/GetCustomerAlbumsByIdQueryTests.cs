using AutoMapper;
using FluentAssertions;
using WebApi.Application.CustomerAlbumsOperations.Queries.GetCustomerAlbumsById;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerAlbumsOperations.Queries.GetCustomerAlbumsById;

public class GetCustomerAlbumsByIdQueryTests : IClassFixture<CommonTestFixture> 
{
    private readonly AlbumStoreDbContext context;
    private readonly IMapper mapper;

    public GetCustomerAlbumsByIdQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

     [Fact]
    public void WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var command = new GetCustomerAlbumsByIdQuery(context, mapper);
        command.CustomerId = -1;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerId: " + command.CustomerId + " does not exist.");
    }

    [Fact]
    public void WhenGivenCustomerDoesNotHaveAlbums_InvalidOperationException_ShouldBeReturned()
    {
        // arrange
        var customer = new Customer{ 
            Name = "WhenGivenCustomerAlbumsIdDoesExistInDb_CustomerAlbums_ShouldBeReturned", 
            Surname = "WhenGivenCustomerAlbumsIdDoesExistInDb_CustomerAlbums_ShouldBeReturned",
            Email = "WhenGivenCustomerDoesNotHaveAlbums_InvalidOperationException_ShouldBeReturned",
            Password = "WhenGivenCustomerDoesNotHaveAlbums_InvalidOperationException_ShouldBeReturned"};
        context.Customers.Add(customer);
        context.SaveChanges();

        var command = new GetCustomerAlbumsByIdQuery(context, mapper);
        command.CustomerId = customer.Id;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerId: " + command.CustomerId + " does not have any album.");
    }

    [Fact]
    public void WhenGivenCustomerHasAlbum_CustomerAlbums_ShouldBeReturned()
    {
        // arrange
        var albumInDb = new Album{ 
                        Title = "WhenGivenCustomerHasAlbum_CustomerAlbums_ShouldBeReturned", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        ProducerId = 1};
        
        var customerInDb = new Customer{ 
            Name = "WhenGivenCustomerHasAlbum_CustomerAlbums_ShouldBeReturned", 
            Surname = "WhenGivenCustomerHasAlbum_CustomerAlbums_ShouldBeReturned",
            Email = "WhenGivenCustomerHasAlbum_CustomerAlbums_ShouldBeReturned",
            Password = "WhenGivenCustomerHasAlbum_CustomerAlbums_ShouldBeReturned"};

        context.Albums.Add(albumInDb);
        context.Customers.Add(customerInDb);
        context.SaveChanges();

        var customerAlbumInDb = new CustomerAlbum{
                                    CustomerId = customerInDb.Id,
                                    AlbumId = albumInDb.Id};
        
        context.CustomerAlbums.Add(customerAlbumInDb);
        context.SaveChanges();

        var command = new GetCustomerAlbumsByIdQuery(context, mapper);
        command.CustomerId = customerInDb.Id;

        // act
        var customerAlbums = FluentActions.Invoking(() => command.Handle()).Invoke();

        // assert
        customerAlbums.Should().NotBeNull().And.HaveCount(1);
        customerAlbums[0].Id.Should().Be(customerAlbumInDb.Id);
        customerAlbums[0].Price.Should().Be(customerAlbumInDb.Price);
        customerAlbums[0].OrderDate.Should().Be(customerAlbumInDb.OrderDate.ToString("yyyy-MM-dd hh:mm:ss"));
        customerAlbums[0].AlbumId.Should().Be(customerAlbumInDb.AlbumId);
    }
}