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
                new Pembayaran { Id = 1, JumlahTagihan = 100000, JumlahDibayar = 50000 },
                new Pembayaran { Id = 2, JumlahTagihan = 200000, JumlahDibayar = 200000 }
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
        public void TambahTagihan_ShouldSetStatusToBelumBayar_WhenJumlahDibayarIsZero()
        {
            // Arrange
            var pembayaran = new Pembayaran { Id = 1, KontrakSewaId = 1, Periode = "Januari 2024", JumlahTagihan = 100000, JumlahDibayar = 0 };

            // Act
            _service.TambahTagihan(pembayaran);

            // Assert
            Assert.Equal("BelumBayar", pembayaran.Status);
            _mockRepository.Verify(repo => repo.Insert(pembayaran), Times.Once);
        }

        [Fact]
        public void BayarTagihan_ShouldUpdatePembayaran_WhenValidIdIsProvided()
        {
            // Arrange
            var pembayaran = new Pembayaran { Id = 1, JumlahTagihan = 100000, JumlahDibayar = 50000 };
            _mockRepository.Setup(repo => repo.GetById(1)).Returns(pembayaran);

            // Act
            _service.BayarTagihan(1, 50000, "Transfer");

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
