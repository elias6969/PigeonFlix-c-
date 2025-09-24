using backend.Api.controllers;
using backend.Api.models;
using backend.Api.services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MoviesControllerTests
{
    [Fact]
    public async Task GetAll_ReturnsListOfMovies()
    {
        // Arrange
        var mockService = new Mock<IMovieService>();
        mockService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(new List<Movie>
            {
                new Movie { Id = 1, Title = "Inception" },
                new Movie { Id = 2, Title = "The Matrix" }
            });

        var controller = new MoviesController(mockService.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var movies = Assert.IsAssignableFrom<IEnumerable<Movie>>(okResult.Value);
        Assert.Collection(movies,
            m => Assert.Equal("Inception", m.Title),
            m => Assert.Equal("The Matrix", m.Title));
    }
}
