using gwiBack.Application.Services;
using gwiBack.Domain.Entities;
using gwiBack.Domain.Interfaces;
using Moq;

namespace gwiBack.Tests
{
    public class ProfessionalInformationServiceTests
    {
        private readonly Mock<IProfessionalInformationRepository> _professionalInformationRepositoryMock;
        private readonly ProfessionalInformationService _professionalInformationService;

        public ProfessionalInformationServiceTests()
        {
            _professionalInformationRepositoryMock = new Mock<IProfessionalInformationRepository>();
            _professionalInformationService = new ProfessionalInformationService(_professionalInformationRepositoryMock.Object);
        }

        [Fact]
        public async Task GetProfessionalInformationByIdAsync_ShouldReturnCorrectInformation()
        {
            // Arrange
            var informationId = Guid.NewGuid();
            var mockInformation = new ProfessionalInformation(informationId, "John Doe", "+123456789", "john@example.com", "https://linkedin.com/johndoe", "123 Main St", "Career Goal", "https://image.url");

            _professionalInformationRepositoryMock.Setup(repo => repo.GetByIdAsync(informationId)).ReturnsAsync(mockInformation);

            // Act
            var result = await _professionalInformationService.GetProfessionalInformationByIdAsync(informationId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.CompleteName);
        }

        [Fact]
        public async Task CreateProfessionalInformationAsync_ShouldReturnNewlyCreatedInformation()
        {
            // Arrange
            var newInformation = new ProfessionalInformation("John Doe", "+123456789", "john@example.com", "https://linkedin.com/johndoe", "123 Main St", "Career Goal", "https://image.url");
            _professionalInformationRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<ProfessionalInformation>())).ReturnsAsync(newInformation);

            // Act
            var result = await _professionalInformationService.CreateProfessionalInformationAsync(newInformation);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.CompleteName);
        }

        [Fact]
        public async Task UpdateProfessionalInformationAsync_ShouldUpdateAndReturnUpdatedInformation()
        {
            // Arrange
            var existingInformation = new ProfessionalInformation(Guid.NewGuid(), "John Doe", "+123456789", "john@example.com", "https://linkedin.com/johndoe", "123 Main St", "Career Goal", "https://image.url");

            _professionalInformationRepositoryMock.Setup(repo => repo.GetByIdAsync(existingInformation.Id)).ReturnsAsync(existingInformation);
            _professionalInformationRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<ProfessionalInformation>())).ReturnsAsync(existingInformation);

            // Act
            existingInformation.Update(existingInformation.Id, "Jane Doe", "+987654321", "jane@example.com", "https://linkedin.com/janedoe", "456 Another St", "New Career Goal", "https://newimage.url");
            var result = await _professionalInformationService.UpdateProfessionalInformationAsync(existingInformation);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Jane Doe", result.CompleteName);
        }

        [Fact]
        public async Task DeleteProfessionalInformationAsync_ShouldDeleteInformation()
        {
            // Arrange
            var informationId = Guid.NewGuid();
            var mockInformation = new ProfessionalInformation(informationId, "John Doe", "+123456789", "john@example.com", "https://linkedin.com/johndoe", "123 Main St", "Career Goal", "https://image.url");

            _professionalInformationRepositoryMock.Setup(repo => repo.GetByIdAsync(informationId)).ReturnsAsync(mockInformation);
            _professionalInformationRepositoryMock.Setup(repo => repo.DeleteAsync(informationId)).Returns(Task.CompletedTask);

            // Act
            await _professionalInformationService.DeleteProfessionalInformationAsync(informationId);

            // Assert
            _professionalInformationRepositoryMock.Verify(repo => repo.DeleteAsync(informationId), Times.Once);
        }

        [Fact]
        public async Task GetProfessionalInformationByIdAsync_ShouldThrowKeyNotFoundException_WhenInformationNotFound()
        {
            // Arrange
            var informationId = Guid.NewGuid();
            _professionalInformationRepositoryMock.Setup(repo => repo.GetByIdAsync(informationId)).ReturnsAsync((ProfessionalInformation)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _professionalInformationService.GetProfessionalInformationByIdAsync(informationId));
        }
    }
}
