using System;
using System.Collections.Generic;
using Xunit;
using Moq;

namespace management_kos.Repositories
{
    public class PembayaranRepositoryTest
    {
        private readonly Mock<IPembayaranRepository> _mockRepository;

        public PembayaranRepositoryTest()
        {
            _mockRepository = new Mock<IPembayaranRepository>();
        }

        [Fact]
        public void Add_ShouldAddPembayaran()
        {
            // Arrange
            var pembayaran = new management_kos.Models.Pembayaran { Id = 1, JumlahTagihan = 100000, TanggalBayar = DateTime.Now };
            _mockRepository.Setup(repo => repo.Insert(pembayaran)).Verifiable();

            // Act
            _mockRepository.Object.Insert(pembayaran);

            // Assert
            _mockRepository.Verify(repo => repo.Insert(pembayaran), Times.Once);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectPembayaran()
        {
            // Arrange
            var pembayaran = new management_kos.Models.Pembayaran { Id = 1, JumlahTagihan = 100000, TanggalBayar = DateTime.Now };
            _mockRepository.Setup(repo => repo.GetById(1)).Returns(pembayaran);

            // Act
            var result = _mockRepository.Object.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetAll_ShouldReturnAllPembayaran()
        {
            // Arrange
            var pembayaranList = new List<management_kos.Models.Pembayaran>
            {
                new management_kos.Models.Pembayaran { Id = 1, JumlahTagihan = 100000, TanggalBayar = DateTime.Now },
                new management_kos.Models.Pembayaran { Id = 2, JumlahTagihan = 200000, TanggalBayar = DateTime.Now }
            };
            _mockRepository.Setup(repo => repo.GetAll()).Returns(pembayaranList);

            // Act
            var result = _mockRepository.Object.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void Delete_ShouldRemovePembayaran()
        {
            // Arrange
            var pembayaran = new management_kos.Models.Pembayaran { Id = 1, JumlahTagihan = 100000, TanggalBayar = DateTime.Now };
            _mockRepository.Setup(repo => repo.Delete(1)).Verifiable();

            // Act
            _mockRepository.Object.Delete(1);

            // Assert
            _mockRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
