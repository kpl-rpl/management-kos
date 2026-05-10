using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services
{
    public class KontrakSewaServiceTest
    {
        private readonly Mock<IKontrakSewaRepository> _mockRepo;
        private readonly Mock<IPenghuniRepository> _mockPenghuniRepo;
        private readonly Mock<IKamarRepository> _mockKamarRepo;
        private readonly KontrakSewaService _service;

        public KontrakSewaServiceTest()
        {
            _mockRepo         = new Mock<IKontrakSewaRepository>();
            _mockPenghuniRepo = new Mock<IPenghuniRepository>();
            _mockKamarRepo    = new Mock<IKamarRepository>();
            _service = new KontrakSewaService(
                _mockRepo.Object,
                _mockPenghuniRepo.Object,
                _mockKamarRepo.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnAllKontrak()
        {
            var list = new List<KontrakSewa>
            {
                new KontrakSewa { Id = 1, PenghuniId = 1, KamarId = 1, Status = "Aktif" },
                new KontrakSewa { Id = 2, PenghuniId = 2, KamarId = 2, Status = "Selesai" }
            };
            _mockRepo.Setup(r => r.GetAll()).Returns(list);

            var result = _service.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        public static IEnumerable<object[]> InvalidKontrakCases()
        {
            yield return new object[]
            {
                new KontrakSewa { PenghuniId = 0, KamarId = 1, HargaSewaBulanan = 1_000_000,
                    TanggalMulai = DateTime.Today, TanggalSelesai = DateTime.Today.AddMonths(1), Status = "Aktif" },
                "PenghuniId nol harus ditolak"
            };
            yield return new object[]
            {
                new KontrakSewa { PenghuniId = 1, KamarId = 0, HargaSewaBulanan = 1_000_000,
                    TanggalMulai = DateTime.Today, TanggalSelesai = DateTime.Today.AddMonths(1), Status = "Aktif" },
                "KamarId nol harus ditolak"
            };
            yield return new object[]
            {
                new KontrakSewa { PenghuniId = 1, KamarId = 1, HargaSewaBulanan = 0,
                    TanggalMulai = DateTime.Today, TanggalSelesai = DateTime.Today.AddMonths(1), Status = "Aktif" },
                "Harga sewa nol harus ditolak"
            };
            yield return new object[]
            {
                new KontrakSewa { PenghuniId = 1, KamarId = 1, HargaSewaBulanan = 1_000_000,
                    TanggalMulai = DateTime.Today, TanggalSelesai = DateTime.Today, Status = "Aktif" },
                "TanggalSelesai sama dengan TanggalMulai harus ditolak"
            };
            yield return new object[]
            {
                new KontrakSewa { PenghuniId = 1, KamarId = 1, HargaSewaBulanan = 1_000_000,
                    TanggalMulai = DateTime.Today, TanggalSelesai = DateTime.Today.AddMonths(1), Status = "StatusTidakValid" },
                "Status tidak valid harus ditolak"
            };
        }

        [Theory]
        [MemberData(nameof(InvalidKontrakCases))]
        public void TambahKontrak_ShouldThrow_WhenInputNotValid(KontrakSewa k, string _)
        {
            Assert.Throws<ArgumentException>(() => _service.TambahKontrak(k));
        }

        [Fact]
        public void TambahKontrak_ShouldCallInsert_WhenInputValid()
        {
            var k = new KontrakSewa
            {
                PenghuniId = 1, KamarId = 1,
                HargaSewaBulanan = 1_500_000,
                TanggalMulai = DateTime.Today,
                TanggalSelesai = DateTime.Today.AddMonths(6),
                Status = "Aktif"
            };
            _mockPenghuniRepo.Setup(r => r.GetById(1)).Returns(new Penghuni { Id = 1 });
            _mockKamarRepo.Setup(r => r.GetById(1)).Returns(new Kamar { Id = 1 });

            _service.TambahKontrak(k);

            _mockRepo.Verify(r => r.Insert(k), Times.Once);
        }

        [Fact]
        public void SelesaikanKontrak_ShouldSetStatusSelesai()
        {
            var k = new KontrakSewa { Id = 1, Status = "Aktif" };
            _mockRepo.Setup(r => r.GetById(1)).Returns(k);

            _service.SelesaikanKontrak(1);

            Assert.Equal("Selesai", k.Status);
            _mockRepo.Verify(r => r.Update(k), Times.Once);
        }

        [Fact]
        public void BatalkanKontrak_ShouldSetStatusDibatalkan()
        {
            var k = new KontrakSewa { Id = 1, Status = "Aktif" };
            _mockRepo.Setup(r => r.GetById(1)).Returns(k);

            _service.BatalkanKontrak(1);

            Assert.Equal("Dibatalkan", k.Status);
            _mockRepo.Verify(r => r.Update(k), Times.Once);
        }

        [Fact]
        public void HapusKontrak_ShouldCallDelete_WhenIdValid()
        {
            _service.HapusKontrak(1);

            _mockRepo.Verify(r => r.Delete(1), Times.Once);
        }

        [Fact]
        public void HapusKontrak_ShouldThrow_WhenIdNol()
        {
            Assert.Throws<ArgumentException>(() => _service.HapusKontrak(0));
        }
    }
}
