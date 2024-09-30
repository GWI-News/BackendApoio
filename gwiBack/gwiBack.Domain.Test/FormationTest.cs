using gwiBack.Application.Services;
using gwiBack.Domain.Entities;
using gwiBack.Domain.Interfaces;
using Moq;

namespace gwiBack.Tests
{
    public class FormationServiceTests
    {
        private readonly Mock<IFormationRepository> _formationRepositoryMock;
        private readonly FormationService _formationService;

        public FormationServiceTests()
        {
            _formationRepositoryMock = new Mock<IFormationRepository>();
            _formationService = new FormationService(_formationRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllFormationsAsync_ShouldReturnAllFormations()
        {
            // Arrange
            var mockFormations = new List<Formation>
            {
                new Formation(Guid.NewGuid(), "Formation 1", "Institution 1", DateTime.Now, DateTime.Now.AddYears(1), "Activity 1", "Activity 2", "Activity 3"),
                new Formation(Guid.NewGuid(), "Formation 2", "Institution 2", DateTime.Now, DateTime.Now.AddYears(2), "Activity 4", "Activity 5", "Activity 6")
            };

            _formationRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockFormations);

            // Act
            var result = (await _formationService.GetAllFormationsAsync()).ToList();
            Assert.Equal(2, result.Count);

        }

        [Fact]
        public async Task GetFormationByIdAsync_ShouldReturnCorrectFormation()
        {
            // Arrange
            var formationId = Guid.NewGuid();
            var mockFormation = new Formation(formationId, "Formation 1", "Institution 1", DateTime.Now, DateTime.Now.AddYears(1), "Activity 1", "Activity 2", "Activity 3");

            _formationRepositoryMock.Setup(repo => repo.GetByIdAsync(formationId)).ReturnsAsync(mockFormation);

            // Act
            var result = await _formationService.GetFormationByIdAsync(formationId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Formation 1", result.Name);
        }

        [Fact]
        public async Task CreateFormationAsync_ShouldReturnNewlyCreatedFormation()
        {
            // Arrange
            var newFormation = new Formation("New Formation", "New Institution", DateTime.Now, DateTime.Now.AddYears(1), "Activity 1", "Activity 2", "Activity 3");
            _formationRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Formation>())).ReturnsAsync(newFormation);

            // Act
            var result = await _formationService.CreateFormationAsync(newFormation);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Formation", result.Name);
        }

        [Fact]
        public async Task UpdateFormationAsync_ShouldUpdateAndReturnUpdatedFormation()
        {
            // Arrange
            var existingFormation = new Formation(Guid.NewGuid(), "Old Formation", "Old Institution", DateTime.Now, DateTime.Now.AddYears(1), "Activity 1", "Activity 2", "Activity 3");

            _formationRepositoryMock.Setup(repo => repo.GetByIdAsync(existingFormation.Id)).ReturnsAsync(existingFormation);
            _formationRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Formation>())).ReturnsAsync(existingFormation);

            // Act
            existingFormation.Name = "Updated Formation";
            var result = await _formationService.UpdateFormationAsync(existingFormation);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Formation", result.Name);
        }

        [Fact]
        public async Task DeleteFormationAsync_ShouldDeleteFormation()
        {
            // Arrange
            var formationId = Guid.NewGuid();
            var mockFormation = new Formation(formationId, "Formation to delete", "Institution", DateTime.Now, DateTime.Now.AddYears(1), "Activity 1", "Activity 2", "Activity 3");

            _formationRepositoryMock.Setup(repo => repo.GetByIdAsync(formationId)).ReturnsAsync(mockFormation);
            _formationRepositoryMock.Setup(repo => repo.DeleteAsync(formationId)).Returns(Task.CompletedTask);

            // Act
            await _formationService.DeleteFormationAsync(formationId);

            // Assert
            _formationRepositoryMock.Verify(repo => repo.DeleteAsync(formationId), Times.Once);
        }
    }
}
