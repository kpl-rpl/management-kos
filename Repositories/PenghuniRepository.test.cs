using System;
using System.Collections.Generic;
using Xunit;
using Moq;

namespace management_kos.Repositories
{
    public class PenghuniRepositoryTest
    {
        private readonly Mock<IPenghuniRepository> _mockRepository;

        public PenghuniRepositoryTest()
        {
            _mockRepository = new Mock<IPenghuniRepository>();
        }

        [Fact]
        public void Add_ShouldAddPenghuni()
        {
            var penghuni = new management_kos.Models.Penghuni { Id = 1, Nama = "Andi" };
            _mockRepository.Setup(repo => repo.Insert(penghuni)).Verifiable();

            _mockRepository.Object.Insert(penghuni);

            _mockRepository.Verify(repo => repo.Insert(penghuni), Times.Once);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectPenghuni()
        {
            var penghuni = new management_kos.Models.Penghuni { Id = 1, Nama = "Andi" };
            _mockRepository.Setup(repo => repo.GetById(1)).Returns(penghuni);

            var result = _mockRepository.Object.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetAll_ShouldReturnAllPenghuni()
        {
            var penghuniList = new List<management_kos.Models.Penghuni>
            {
                new management_kos.Models.Penghuni { Id = 1, Nama = "Andi" },
                new management_kos.Models.Penghuni { Id = 2, Nama = "Budi" }
            };
            _mockRepository.Setup(repo => repo.GetAll()).Returns(penghuniList);

            var result = _mockRepository.Object.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void Delete_ShouldRemovePenghuni()
        {
            _mockRepository.Setup(repo => repo.Delete(1)).Verifiable();

            _mockRepository.Object.Delete(1);

            _mockRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
