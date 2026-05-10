using System.Collections.Generic;
using Xunit;
using Moq;
using management_kos.Models;

namespace management_kos.Repositories
{
    public class KamarRepositoryTest
    {
        private readonly Mock<IKamarRepository> _mockRepository;

        public KamarRepositoryTest()
        {
            _mockRepository = new Mock<IKamarRepository>();
        }

        [Fact]
        public void GetAll_ShouldReturnAllKamar()
        {
            var list = new List<Kamar>
            {
                new Kamar { Id = 1, KosId = 1, NomorKamar = "A-01", HargaKamar = 800_000, Status = "Kosong" },
                new Kamar { Id = 2, KosId = 1, NomorKamar = "A-02", HargaKamar = 900_000, Status = "Terisi" }
            };
            _mockRepository.Setup(repo => repo.GetAll()).Returns(list);

            var result = _mockRepository.Object.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectKamar()
        {
            var kamar = new Kamar { Id = 1, KosId = 1, NomorKamar = "A-01", HargaKamar = 800_000, Status = "Kosong" };
            _mockRepository.Setup(repo => repo.GetById(1)).Returns(kamar);

            var result = _mockRepository.Object.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("A-01", result.NomorKamar);
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenNotFound()
        {
            _mockRepository.Setup(repo => repo.GetById(99)).Returns((Kamar?)null);

            var result = _mockRepository.Object.GetById(99);

            Assert.Null(result);
        }

        [Fact]
        public void GetByKosId_ShouldReturnKamarByKos()
        {
            var list = new List<Kamar>
            {
                new Kamar { Id = 1, KosId = 3, NomorKamar = "B-01", Status = "Kosong" },
                new Kamar { Id = 2, KosId = 3, NomorKamar = "B-02", Status = "Terisi" }
            };
            _mockRepository.Setup(repo => repo.GetByKosId(3)).Returns(list);

            var result = _mockRepository.Object.GetByKosId(3);

            Assert.Equal(2, result.Count);
            Assert.All(result, k => Assert.Equal(3, k.KosId));
        }

        [Fact]
        public void Insert_ShouldAddKamar()
        {
            var kamar = new Kamar { KosId = 1, NomorKamar = "A-01", HargaKamar = 800_000, Status = "Kosong" };
            _mockRepository.Setup(repo => repo.Insert(kamar)).Verifiable();

            _mockRepository.Object.Insert(kamar);

            _mockRepository.Verify(repo => repo.Insert(kamar), Times.Once);
        }

        [Fact]
        public void Update_ShouldUpdateKamar()
        {
            var kamar = new Kamar { Id = 1, KosId = 1, NomorKamar = "A-01", HargaKamar = 950_000, Status = "Terisi" };
            _mockRepository.Setup(repo => repo.Update(kamar)).Verifiable();

            _mockRepository.Object.Update(kamar);

            _mockRepository.Verify(repo => repo.Update(kamar), Times.Once);
        }

        [Fact]
        public void Delete_ShouldRemoveKamar()
        {
            _mockRepository.Setup(repo => repo.Delete(1)).Verifiable();

            _mockRepository.Object.Delete(1);

            _mockRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
