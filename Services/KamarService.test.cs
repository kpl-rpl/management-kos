using System;
using System.Collections.Generic;
using management_kos.Models;
using management_kos.Repositories;
using Moq;
using Xunit;

namespace management_kos.Services
{
    public class KamarServiceTest
    {
        private readonly Mock<IKamarRepository> _mockKamarRepository;
        private readonly Mock<IKosRepository> _mockKosRepository;
        private readonly KamarService _service;

        public KamarServiceTest()
        {
            _mockKamarRepository = new Mock<IKamarRepository>();
            _mockKosRepository = new Mock<IKosRepository>();
            _service = new KamarService(_mockKamarRepository.Object, _mockKosRepository.Object);
        }

        [Fact]
        public void GetAllKamar_ShouldReturnAllKamar()
        {
            // Arrange
            var kamarList = new List<Kamar>
            {
                new Kamar { Id = 1, KosId = 1, NomorKamar = "A-01", Status = KamarStatus.Kosong },
                new Kamar { Id = 2, KosId = 1, NomorKamar = "A-02", Status = KamarStatus.Terisi }
            };
            _mockKamarRepository.Setup(repo => repo.GetAll()).Returns(kamarList);

            // Act
            var result = _service.GetAllKamar();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetKamarByKosId_ShouldThrowException_WhenKosIdIsInvalid()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.GetKamarByKosId(0));
        }

        [Fact]
        public void TambahKamar_ShouldCallInsert_WhenKamarIsValid()
        {
            // Arrange
            var kamar = new Kamar { KosId = 1, NomorKamar = "A-01", HargaKamar = 1000000, Status = KamarStatus.Kosong };
            _mockKosRepository.Setup(repo => repo.GetById(1)).Returns(new Kos { Id = 1 });

            // Act
            _service.TambahKamar(kamar);

            // Assert
            _mockKamarRepository.Verify(repo => repo.Insert(kamar), Times.Once);
        }

        [Fact]
        public void UbahKamar_ShouldCallUpdate_WhenKamarIsValid()
        {
            // Arrange
            var kamar = new Kamar { Id = 1, KosId = 1, NomorKamar = "A-01", HargaKamar = 1000000, Status = KamarStatus.Kosong };
            _mockKosRepository.Setup(repo => repo.GetById(1)).Returns(new Kos { Id = 1 });

            // Act
            _service.UbahKamar(kamar);

            // Assert
            _mockKamarRepository.Verify(repo => repo.Update(kamar), Times.Once);
        }

        [Fact]
        public void HapusKamar_ShouldCallDelete_WhenIdIsValid()
        {
            // Act
            _service.HapusKamar(1);

            // Assert
            _mockKamarRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
