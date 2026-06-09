using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services
{
    public class PembayaranServiceTest
    {
        private readonly Mock<IPembayaranRepository> _mockRepository;
        private readonly PembayaranService _service;

        public PembayaranServiceTest()
        {
            _mockRepository = new Mock<IPembayaranRepository>();
            _service = new PembayaranService(_mockRepository.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnAllPembayaran()
        {
            // Arrange
            var pembayaranList = new List<Pembayaran>
            {
                new Pembayaran { Id = 1, JumlahDibayar = 50000 },
                new Pembayaran { Id = 2, JumlahDibayar = 200000 }
            };
            _mockRepository.Setup(repo => repo.GetAll()).Returns(pembayaranList);

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetByKontrak_ShouldThrowException_WhenKontrakIdIsInvalid()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.GetByKontrak(0));
        }

        [Fact]
        public void CatatPembayaran_ShouldInsert_WhenDataIsValid()
        {
            // Arrange
            var pembayaran = new Pembayaran { Id = 1, KontrakSewaId = 1, JumlahDibayar = 100000, MetodePembayaran = "Tunai" };

            // Act
            _service.CatatPembayaran(pembayaran);

            // Assert
            _mockRepository.Verify(repo => repo.Insert(pembayaran), Times.Once);
        }

        [Fact]
        public void UbahPembayaran_ShouldUpdatePembayaran_WhenDataIsValid()
        {
            // Arrange
            var pembayaran = new Pembayaran { Id = 1, KontrakSewaId = 1, JumlahDibayar = 100000, MetodePembayaran = "Transfer" };

            // Act
            _service.UbahPembayaran(pembayaran);

            // Assert
            Assert.Equal(100000, pembayaran.JumlahDibayar);
            Assert.Equal("Transfer", pembayaran.MetodePembayaran);
            _mockRepository.Verify(repo => repo.Update(pembayaran), Times.Once);
        }

        [Fact]
        public void HapusPembayaran_ShouldCallDelete_WhenIdIsValid()
        {
            // Act
            _service.HapusPembayaran(1);

            // Assert
            _mockRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
