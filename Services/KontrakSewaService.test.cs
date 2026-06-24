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
                new KontrakSewa { Id = 1, PenghuniId = 1, KamarId = 1, Status = KontrakStatus.Aktif },
                new KontrakSewa { Id = 2, PenghuniId = 2, KamarId = 2, Status = KontrakStatus.Selesai }
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
                    TanggalMulai = DateTime.Today, TanggalSelesai = DateTime.Today.AddMonths(1), Status = KontrakStatus.Aktif },
                "PenghuniId nol harus ditolak"
            };
            yield return new object[]
            {
                new KontrakSewa { PenghuniId = 1, KamarId = 0, HargaSewaBulanan = 1_000_000,
                    TanggalMulai = DateTime.Today, TanggalSelesai = DateTime.Today.AddMonths(1), Status = KontrakStatus.Aktif },
                "KamarId nol harus ditolak"
            };
            yield return new object[]
            {
                new KontrakSewa { PenghuniId = 1, KamarId = 1, HargaSewaBulanan = 0,
                    TanggalMulai = DateTime.Today, TanggalSelesai = DateTime.Today.AddMonths(1), Status = KontrakStatus.Aktif },
                "Harga sewa nol harus ditolak"
            };
            yield return new object[]
            {
                new KontrakSewa { PenghuniId = 1, KamarId = 1, HargaSewaBulanan = 1_000_000,
                    TanggalMulai = DateTime.Today, DurasiBulanInput = 0, Status = KontrakStatus.Aktif },
                "Durasi nol harus ditolak"
            };
            yield return new object[]
            {
                new KontrakSewa { PenghuniId = 1, KamarId = 1, HargaSewaBulanan = 1_000_000,
                    TanggalMulai = DateTime.Today, TanggalSelesai = DateTime.Today.AddMonths(1), Status = (KontrakStatus)999 },
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
                Status = KontrakStatus.Aktif
            };
            _mockPenghuniRepo.Setup(r => r.GetById(1)).Returns(new Penghuni { Id = 1 });
            _mockKamarRepo.Setup(r => r.GetById(1)).Returns(new Kamar { Id = 1, Status = KamarStatus.Kosong });
            _mockRepo.Setup(r => r.GetByKamarId(1)).Returns(new List<KontrakSewa>());

            _service.TambahKontrak(k);

            _mockRepo.Verify(r => r.Insert(k), Times.Once);
        }

        [Fact]
        public void TambahKontrak_ShouldRoundDurationUpAndCalculateTotal()
        {
            var k = new KontrakSewa
            {
                PenghuniId = 1,
                KamarId = 1,
                HargaSewaBulanan = 1_500_000,
                DurasiBulanInput = 2.5m,
                TanggalMulai = new DateTime(2026, 1, 10),
                Status = KontrakStatus.Aktif
            };
            _mockPenghuniRepo.Setup(r => r.GetById(1)).Returns(new Penghuni { Id = 1 });
            _mockKamarRepo.Setup(r => r.GetById(1)).Returns(new Kamar { Id = 1, Status = KamarStatus.Kosong });
            _mockRepo.Setup(r => r.GetByKamarId(1)).Returns(new List<KontrakSewa>());

            _service.TambahKontrak(k);

            Assert.Equal(3, k.JumlahBulanTagihan);
            Assert.Equal(4_500_000, k.TotalTagihan);
            Assert.Equal(new DateTime(2026, 4, 10), k.TanggalSelesai);
        }

        [Fact]
        public void TambahKontrak_ShouldAllowSamePenghuniForDifferentRooms()
        {
            var first = new KontrakSewa { Id = 1, PenghuniId = 1, KamarId = 1, HargaSewaBulanan = 1_000_000, Status = KontrakStatus.Aktif };
            var second = new KontrakSewa { PenghuniId = 1, KamarId = 2, HargaSewaBulanan = 900_000, Status = KontrakStatus.Aktif };
            _mockPenghuniRepo.Setup(r => r.GetById(1)).Returns(new Penghuni { Id = 1 });
            _mockKamarRepo.Setup(r => r.GetById(2)).Returns(new Kamar { Id = 2, Status = KamarStatus.Kosong });
            _mockRepo.Setup(r => r.GetByKamarId(2)).Returns(new List<KontrakSewa>());

            _service.TambahKontrak(second);

            _mockRepo.Verify(r => r.Insert(second), Times.Once);
        }

        [Fact]
        public void Search_ShouldDelegateToRepository()
        {
            _mockRepo.Setup(r => r.Search("andi")).Returns(new List<KontrakSewa>());

            var result = _service.Search("andi");

            Assert.Empty(result);
            _mockRepo.Verify(r => r.Search("andi"), Times.Once);
        }

        [Fact]
        public void SelesaikanKontrak_ShouldSetStatusSelesai()
        {
            var k = new KontrakSewa { Id = 1, Status = KontrakStatus.Aktif };
            _mockRepo.Setup(r => r.GetById(1)).Returns(k);

            _service.SelesaikanKontrak(1);

            Assert.Equal(KontrakStatus.Selesai, k.Status);
            _mockRepo.Verify(r => r.Update(k), Times.Once);
        }

        [Fact]
        public void BatalkanKontrak_ShouldSetStatusDibatalkan()
        {
            var k = new KontrakSewa { Id = 1, Status = KontrakStatus.Aktif };
            _mockRepo.Setup(r => r.GetById(1)).Returns(k);

            _service.BatalkanKontrak(1);

            Assert.Equal(KontrakStatus.Dibatalkan, k.Status);
            _mockRepo.Verify(r => r.Update(k), Times.Once);
        }

        [Fact]
        public void HapusKontrak_ShouldCallDelete_WhenIdValid()
        {
            _mockRepo.Setup(r => r.GetById(1)).Returns(new KontrakSewa { Id = 1, KamarId = 1, Status = KontrakStatus.Aktif });
            _mockKamarRepo.Setup(r => r.GetById(1)).Returns(new Kamar { Id = 1, Status = KamarStatus.Terisi });
            _mockRepo.Setup(r => r.GetByKamarId(1)).Returns(new List<KontrakSewa>());

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
