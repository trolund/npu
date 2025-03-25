using Moq;
using NPU.Bl;
using NPU.Data.Model;
using NPU.Data.Repositories;
using NPU.UnitTests.Stubs;

namespace NPU.UnitTests.Tests;

public class NpuServiceTests
{
    private readonly Mock<IFileUploadService> _fileUploadServiceMock;
    private readonly INpuService _npuService;

    public NpuServiceTests()
    {
        var npuRepositoryStub = new NpuRepository(new CosmosRepositoryStub<Npu>());
        var scoreRepositoryMock = new Mock<IScoreRepository>();
        _fileUploadServiceMock = new Mock<IFileUploadService>();

        _npuService = new NpuService(
            npuRepositoryStub,
            scoreRepositoryMock.Object,
            _fileUploadServiceMock.Object);
    }

    [Fact]
    public async Task WHEN_CreateNpuWithImagesAsync_THEN_ShouldCreateNpu()
    {
        // Arrange
        const string name = "TestNpu";
        const string description = "TestDescription";
        var images = new List<(string, Stream)>
        {
            ("image1.jpg", new MemoryStream())
        };

        var uploadedLinks = new List<string> { "http://test.com/image1.jpg" };

        _fileUploadServiceMock.Setup(s =>
                s.UploadFileAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Stream>()))
            .ReturnsAsync(uploadedLinks[0]);

        // Act
        var result = await _npuService.CreateNpuWithImagesAsync(name, description, images);

        // Assert
        Assert.Equal(name, result.Name);
        Assert.Equal(description, result.Description);
        Assert.Equal(uploadedLinks, result.Images);
    }

    [Fact]
    public async Task WHEN_GetNpuPaginatedAsync_THEN_ShouldReturnPaginatedResults()
    {
        // Arrange
        const string name = "TestNpu";
        const string description = "TestDescription";
        var images = new List<(string, Stream)>
        {
            ("image1.jpg", new MemoryStream())
        };

        var uploadedLinks = new List<string> { "http://test.com/image1.jpg" };

        _fileUploadServiceMock.Setup(s =>
                s.UploadFileAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Stream>()))
            .ReturnsAsync(uploadedLinks[0]);

        await _npuService.CreateNpuWithImagesAsync(name, description, images);

        // Act
        var result = await _npuService.GetNpuPaginatedAsync(null, 1, 10, true, null);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Items);
        Assert.Single(result.Items);
        Assert.Equal(result.Items.Count(), result.TotalCount);
    }
}