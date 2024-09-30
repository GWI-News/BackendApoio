using gwiBack.Application.Services;
using gwiBack.Domain.Entities;
using gwiBack.Domain.Interfaces;
using Moq;

namespace gwiBack.Tests
{
    public class ProfessionalSkillServiceTests
    {
        private readonly Mock<IProfessionalSkillRepository> _professionalSkillRepositoryMock;
        private readonly ProfessionalSkillService _professionalSkillService;

        public ProfessionalSkillServiceTests()
        {
            _professionalSkillRepositoryMock = new Mock<IProfessionalSkillRepository>();
            _professionalSkillService = new ProfessionalSkillService(_professionalSkillRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllProfessionalSkillsAsync_ShouldReturnAllSkills()
        {
            // Arrange
            var mockSkills = new List<ProfessionalSkill>
            {
                new ProfessionalSkill(Guid.NewGuid(), "Skill 1", "Skill 2", "Skill 3", "Skill 4"),
                new ProfessionalSkill(Guid.NewGuid(), "Skill 5", "Skill 6", "Skill 7", "Skill 8")
            };

            _professionalSkillRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockSkills);

            // Act
            var result = await _professionalSkillService.GetAllProfessionalSkillsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetProfessionalSkillByIdAsync_ShouldReturnCorrectSkill()
        {
            // Arrange
            var skillId = Guid.NewGuid();
            var mockSkill = new ProfessionalSkill(skillId, "Skill 1", "Skill 2", "Skill 3", "Skill 4");

            _professionalSkillRepositoryMock.Setup(repo => repo.GetByIdAsync(skillId)).ReturnsAsync(mockSkill);

            // Act
            var result = await _professionalSkillService.GetProfessionalSkillByIdAsync(skillId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Skill 1", result.Skill1);
        }

        [Fact]
        public async Task CreateProfessionalSkillAsync_ShouldReturnNewlyCreatedSkill()
        {
            // Arrange
            var newSkill = new ProfessionalSkill("Skill 1", "Skill 2", "Skill 3", "Skill 4");
            _professionalSkillRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<ProfessionalSkill>())).ReturnsAsync(newSkill);

            // Act
            var result = await _professionalSkillService.CreateProfessionalSkillAsync(newSkill);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Skill 1", result.Skill1);
        }

        [Fact]
        public async Task UpdateProfessionalSkillAsync_ShouldUpdateAndReturnUpdatedSkill()
        {
            // Arrange
            var existingSkill = new ProfessionalSkill(Guid.NewGuid(), "Old Skill 1", "Old Skill 2", "Old Skill 3", "Old Skill 4");

            _professionalSkillRepositoryMock.Setup(repo => repo.GetByIdAsync(existingSkill.Id)).ReturnsAsync(existingSkill);
            _professionalSkillRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<ProfessionalSkill>())).ReturnsAsync(existingSkill);

            // Act
            existingSkill.Update(existingSkill.Id, "Updated Skill 1", "Updated Skill 2", "Updated Skill 3", "Updated Skill 4");
            var result = await _professionalSkillService.UpdateProfessionalSkillAsync(existingSkill);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Skill 1", result.Skill1);
        }

        [Fact]
        public async Task DeleteProfessionalSkillAsync_ShouldDeleteSkill()
        {
            // Arrange
            var skillId = Guid.NewGuid();
            var mockSkill = new ProfessionalSkill(skillId, "Skill 1", "Skill 2", "Skill 3", "Skill 4");

            _professionalSkillRepositoryMock.Setup(repo => repo.GetByIdAsync(skillId)).ReturnsAsync(mockSkill);
            _professionalSkillRepositoryMock.Setup(repo => repo.DeleteAsync(skillId)).Returns(Task.CompletedTask);

            // Act
            await _professionalSkillService.DeleteProfessionalSkillAsync(skillId);

            // Assert
            _professionalSkillRepositoryMock.Verify(repo => repo.DeleteAsync(skillId), Times.Once);
        }
    }
}
