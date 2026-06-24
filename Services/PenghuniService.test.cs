using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services
{
    public class PenghuniServiceTest
    {
        private readonly Mock<IPenghuniRepository> _mockPenghuniRepository;
        private readonly Mock<IKamarRepository> _mockKamarRepository;
        private readonly PenghuniService _service;

        public PenghuniServiceTest()
        {
            _mockPenghuniRepository = new Mock<IPenghuniRepository>();
            _mockKamarRepository = new Mock<IKamarRepository>();
            _service = new PenghuniService(_mockPenghuniRepository.Object, _mockKamarRepository.Object);
        }

        [Fact]
        public void GetAllPenghuni_ShouldReturnAllPenghuni()
        {
            var penghuniList = new List<Penghuni>
            {
                new Penghuni { Id = 1, Nama = "Andi" },
                new Penghuni { Id = 2, Nama = "Budi" }
            };
            _mockPenghuniRepository.Setup(repo => repo.GetAll()).Returns(penghuniList);

            var result = _service.GetAllPenghuni();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetPenghuniById_ShouldThrowException_WhenIdInvalid()
        {
            Assert.Throws<ArgumentException>(() => _service.GetPenghuniById(0));
        }

        [Fact]
        public void TambahPenghuni_ShouldCallInsert_WhenValid()
        {
            var penghuni = new Penghuni { Id = 1, Nama = "Andi", NomorTelepon = "081234567890" };
            _mockPenghuniRepository.Setup(r => r.Insert(penghuni));

            _service.TambahPenghuni(penghuni);

            _mockPenghuniRepository.Verify(r => r.Insert(penghuni), Times.Once);
            _mockKamarRepository.Verify(r => r.Update(It.IsAny<Kamar>()), Times.Never);
        }
    }
}
