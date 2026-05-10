using System.Collections.Generic;
using Xunit;
using Moq;
using management_kos.Models;

namespace management_kos.Repositories
{
    public class KontrakSewaRepositoryTest
    {
        private readonly Mock<IKontrakSewaRepository> _mockRepository;

        public KontrakSewaRepositoryTest()
        {
            _mockRepository = new Mock<IKontrakSewaRepository>();
        }

        [Fact]
        public void GetAll_ShouldReturnAllKontrak()
        {
            var list = new List<KontrakSewa>
            {
                new KontrakSewa { Id = 1, PenghuniId = 1, KamarId = 1, Status = "Aktif" },
                new KontrakSewa { Id = 2, PenghuniId = 2, KamarId = 2, Status = "Selesai" }
            };
            _mockRepository.Setup(repo => repo.GetAll()).Returns(list);

            var result = _mockRepository.Object.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectKontrak()
        {
            var kontrak = new KontrakSewa { Id = 1, PenghuniId = 1, KamarId = 1, Status = "Aktif" };
            _mockRepository.Setup(repo => repo.GetById(1)).Returns(kontrak);

            var result = _mockRepository.Object.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Aktif", result.Status);
        }

        [Fact]
        public void GetByPenghuniId_ShouldReturnKontrakByPenghuni()
        {
            var list = new List<KontrakSewa>
            {
                new KontrakSewa { Id = 1, PenghuniId = 5, KamarId = 1, Status = "Aktif" },
                new KontrakSewa { Id = 2, PenghuniId = 5, KamarId = 3, Status = "Selesai" }
            };
            _mockRepository.Setup(repo => repo.GetByPenghuniId(5)).Returns(list);

            var result = _mockRepository.Object.GetByPenghuniId(5);

            Assert.Equal(2, result.Count);
            Assert.All(result, k => Assert.Equal(5, k.PenghuniId));
        }

        [Fact]
        public void GetByStatus_ShouldReturnKontrakByStatus()
        {
            var list = new List<KontrakSewa>
            {
                new KontrakSewa { Id = 1, Status = "Aktif" },
                new KontrakSewa { Id = 2, Status = "Aktif" }
            };
            _mockRepository.Setup(repo => repo.GetByStatus("Aktif")).Returns(list);

            var result = _mockRepository.Object.GetByStatus("Aktif");

            Assert.Equal(2, result.Count);
            Assert.All(result, k => Assert.Equal("Aktif", k.Status));
        }

        [Fact]
        public void Insert_ShouldAddKontrak()
        {
            var kontrak = new KontrakSewa { Id = 1, PenghuniId = 1, KamarId = 1, Status = "Aktif" };
            _mockRepository.Setup(repo => repo.Insert(kontrak)).Verifiable();

            _mockRepository.Object.Insert(kontrak);

            _mockRepository.Verify(repo => repo.Insert(kontrak), Times.Once);
        }

        [Fact]
        public void Update_ShouldUpdateKontrak()
        {
            var kontrak = new KontrakSewa { Id = 1, PenghuniId = 1, KamarId = 1, Status = "Selesai" };
            _mockRepository.Setup(repo => repo.Update(kontrak)).Verifiable();

            _mockRepository.Object.Update(kontrak);

            _mockRepository.Verify(repo => repo.Update(kontrak), Times.Once);
        }

        [Fact]
        public void Delete_ShouldRemoveKontrak()
        {
            _mockRepository.Setup(repo => repo.Delete(1)).Verifiable();

            _mockRepository.Object.Delete(1);

            _mockRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
