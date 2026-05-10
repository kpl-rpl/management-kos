using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services
{
    public class KosServiceTest
    {
        private readonly Mock<IKosRepository> _mockRepository;
        private readonly KosService _service;

        public KosServiceTest()
        {
            _mockRepository = new Mock<IKosRepository>();
            _service = new KosService(_mockRepository.Object);
        }

        [Fact]
        public void GetAllKos_ShouldReturnAllKos()
        {
            var list = new List<Kos>
            {
                new Kos { Id = 1, NamaKos = "Kos Melati", Alamat = "Jl. Mawar No. 1", HargaDasar = 800_000, JumlahKamar = 10, NamaPemilik = "Budi", NomorTelepon = "081234567890" },
                new Kos { Id = 2, NamaKos = "Kos Kenanga", Alamat = "Jl. Kenanga No. 5", HargaDasar = 1_200_000, JumlahKamar = 6, NamaPemilik = "Siti", NomorTelepon = "081298765432" }
            };
            _mockRepository.Setup(r => r.GetAll()).Returns(list);

            var result = _service.GetAllKos();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void TambahKos_ShouldCallInsert_WhenDataValid()
        {
            var kos = new Kos
            {
                NamaKos = "Kos Melati",
                Alamat = "Jl. Mawar No. 1",
                HargaDasar = 800_000,
                JumlahKamar = 10,
                NamaPemilik = "Budi Santoso",
                NomorTelepon = "081234567890"
            };

            _service.TambahKos(kos);

            _mockRepository.Verify(r => r.Insert(kos), Times.Once);
        }

        [Fact]
        public void TambahKos_ShouldThrow_WhenNamaKosKosong()
        {
            var kos = new Kos { NamaKos = "   ", Alamat = "Jl. Test", HargaDasar = 500_000, JumlahKamar = 5, NamaPemilik = "Test", NomorTelepon = "081234567890" };

            Assert.Throws<ArgumentException>(() => _service.TambahKos(kos));
        }

        [Fact]
        public void TambahKos_ShouldThrow_WhenHargaDasarNol()
        {
            var kos = new Kos { NamaKos = "Kos Test", Alamat = "Jl. Test", HargaDasar = 0, JumlahKamar = 5, NamaPemilik = "Test", NomorTelepon = "081234567890" };

            Assert.Throws<ArgumentException>(() => _service.TambahKos(kos));
        }

        [Fact]
        public void TambahKos_ShouldThrow_WhenJumlahKamarNol()
        {
            var kos = new Kos { NamaKos = "Kos Test", Alamat = "Jl. Test", HargaDasar = 500_000, JumlahKamar = 0, NamaPemilik = "Test", NomorTelepon = "081234567890" };

            Assert.Throws<ArgumentException>(() => _service.TambahKos(kos));
        }

        [Fact]
        public void TambahKos_ShouldThrow_WhenNomorTeleponFormatSalah()
        {
            var kos = new Kos { NamaKos = "Kos Test", Alamat = "Jl. Test", HargaDasar = 500_000, JumlahKamar = 5, NamaPemilik = "Test", NomorTelepon = "abc-xyz" };

            Assert.Throws<ArgumentException>(() => _service.TambahKos(kos));
        }

        [Fact]
        public void UbahKos_ShouldThrow_WhenIdNolAtauNegatif()
        {
            var kos = new Kos { Id = 0, NamaKos = "Kos Test", Alamat = "Jl. Test", HargaDasar = 500_000, JumlahKamar = 5, NamaPemilik = "Test", NomorTelepon = "081234567890" };

            Assert.Throws<ArgumentException>(() => _service.UbahKos(kos));
        }

        [Fact]
        public void UbahKos_ShouldThrow_WhenKosTidakDitemukan()
        {
            _mockRepository.Setup(r => r.GetById(99)).Returns((Kos?)null);
            var kos = new Kos { Id = 99, NamaKos = "Kos Test", Alamat = "Jl. Test", HargaDasar = 500_000, JumlahKamar = 5, NamaPemilik = "Test", NomorTelepon = "081234567890" };

            Assert.Throws<ArgumentException>(() => _service.UbahKos(kos));
        }

        [Fact]
        public void UbahKos_ShouldCallUpdate_WhenDataValid()
        {
            var kos = new Kos { Id = 1, NamaKos = "Kos Melati Update", Alamat = "Jl. Mawar No. 1", HargaDasar = 900_000, JumlahKamar = 12, NamaPemilik = "Budi Santoso", NomorTelepon = "081234567890" };
            _mockRepository.Setup(r => r.GetById(1)).Returns(kos);

            _service.UbahKos(kos);

            _mockRepository.Verify(r => r.Update(kos), Times.Once);
        }

        [Fact]
        public void HapusKos_ShouldThrow_WhenIdNegatif()
        {
            Assert.Throws<ArgumentException>(() => _service.HapusKos(-1));
        }

        [Fact]
        public void HapusKos_ShouldThrow_WhenKosTidakDitemukan()
        {
            _mockRepository.Setup(r => r.GetById(99)).Returns((Kos?)null);

            Assert.Throws<ArgumentException>(() => _service.HapusKos(99));
        }

        [Fact]
        public void HapusKos_ShouldCallDelete_WhenIdValid()
        {
            _mockRepository.Setup(r => r.GetById(1)).Returns(new Kos { Id = 1 });

            _service.HapusKos(1);

            _mockRepository.Verify(r => r.Delete(1), Times.Once);
        }
    }
}
