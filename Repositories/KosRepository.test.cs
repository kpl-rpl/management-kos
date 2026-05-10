using System.Collections.Generic;
using Xunit;
using Moq;
using management_kos.Models;

namespace management_kos.Repositories
{
    public class KosRepositoryTest
    {
        private readonly Mock<IKosRepository> _mockRepository;

        public KosRepositoryTest()
        {
            _mockRepository = new Mock<IKosRepository>();
        }

        [Fact]
        public void GetAll_ShouldReturnAllKos()
        {
            var list = new List<Kos>
            {
                new Kos { Id = 1, NamaKos = "Kos Melati", Alamat = "Jl. Mawar No. 1", HargaDasar = 800_000, JumlahKamar = 10, NamaPemilik = "Budi", NomorTelepon = "081234567890" },
                new Kos { Id = 2, NamaKos = "Kos Kenanga", Alamat = "Jl. Kenanga No. 5", HargaDasar = 1_200_000, JumlahKamar = 6, NamaPemilik = "Siti", NomorTelepon = "081298765432" }
            };
            _mockRepository.Setup(repo => repo.GetAll()).Returns(list);

            var result = _mockRepository.Object.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectKos()
        {
            var kos = new Kos { Id = 1, NamaKos = "Kos Melati", Alamat = "Jl. Mawar No. 1", HargaDasar = 800_000, JumlahKamar = 10, NamaPemilik = "Budi", NomorTelepon = "081234567890" };
            _mockRepository.Setup(repo => repo.GetById(1)).Returns(kos);

            var result = _mockRepository.Object.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Kos Melati", result.NamaKos);
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenNotFound()
        {
            _mockRepository.Setup(repo => repo.GetById(99)).Returns((Kos?)null);

            var result = _mockRepository.Object.GetById(99);

            Assert.Null(result);
        }

        [Fact]
        public void Insert_ShouldAddKos()
        {
            var kos = new Kos { NamaKos = "Kos Baru", Alamat = "Jl. Baru No. 1", HargaDasar = 700_000, JumlahKamar = 8, NamaPemilik = "Andi", NomorTelepon = "089876543210" };
            _mockRepository.Setup(repo => repo.Insert(kos)).Verifiable();

            _mockRepository.Object.Insert(kos);

            _mockRepository.Verify(repo => repo.Insert(kos), Times.Once);
        }

        [Fact]
        public void Update_ShouldUpdateKos()
        {
            var kos = new Kos { Id = 1, NamaKos = "Kos Melati Update", Alamat = "Jl. Mawar No. 1", HargaDasar = 900_000, JumlahKamar = 12, NamaPemilik = "Budi", NomorTelepon = "081234567890" };
            _mockRepository.Setup(repo => repo.Update(kos)).Verifiable();

            _mockRepository.Object.Update(kos);

            _mockRepository.Verify(repo => repo.Update(kos), Times.Once);
        }

        [Fact]
        public void Delete_ShouldRemoveKos()
        {
            _mockRepository.Setup(repo => repo.Delete(1)).Verifiable();

            _mockRepository.Object.Delete(1);

            _mockRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
