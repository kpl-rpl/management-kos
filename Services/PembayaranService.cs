using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

// Penerapan State / Automata itu di sini
public enum StatusPembayaran
{
    BelumBayar,
    Dicicil,
    Lunas
}

public enum PaymentEvent
{
    TidakBayar,
    BayarSebagian,
    BayarLunas
}

public class PembayaranService
{
    private readonly IPembayaranRepository _pembayaranRepository;

    // Penerapan Table Driven itu di sini
    //Current State + Event => Next State

    private readonly Dictionary<(StatusPembayaran, PaymentEvent), StatusPembayaran>
        _transitionTable =
        new()
        {
            {
                (StatusPembayaran.BelumBayar, PaymentEvent.TidakBayar),
                StatusPembayaran.BelumBayar
            },

            {
                (StatusPembayaran.BelumBayar, PaymentEvent.BayarSebagian),
                StatusPembayaran.Dicicil
            },

            {
                (StatusPembayaran.BelumBayar, PaymentEvent.BayarLunas),
                StatusPembayaran.Lunas
            },

            {
                (StatusPembayaran.Dicicil, PaymentEvent.BayarSebagian),
                StatusPembayaran.Dicicil
            },

            {
                (StatusPembayaran.Dicicil, PaymentEvent.BayarLunas),
                StatusPembayaran.Lunas
            },

            {
                (StatusPembayaran.Lunas, PaymentEvent.BayarLunas),
                StatusPembayaran.Lunas
            }
        };

    public PembayaranService(IPembayaranRepository pembayaranRepository)
    {
        _pembayaranRepository = pembayaranRepository;
    }


    public List<Pembayaran> GetAll()
    {
        return _pembayaranRepository.GetAll();
    }

    public List<Pembayaran> GetByKontrak(int kontrakId)
    {
        if (kontrakId <= 0)
            throw new ArgumentException("ID Kontrak tidak valid.");

        return _pembayaranRepository.GetByKontrakSewaId(kontrakId);
    }

    public void TambahTagihan(Pembayaran p)
    {
        Validate(p);

        var currentState = StatusPembayaran.BelumBayar;

        var paymentEvent = DetermineEvent(
            p.JumlahDibayar,
            p.JumlahTagihan
        );

        p.Status = GetNextState(currentState, paymentEvent).ToString();

        _pembayaranRepository.Insert(p);
    }

    public void BayarTagihan(int id, decimal nominal, string metode)
    {
        if (nominal <= 0)
            throw new ArgumentException("Nominal pembayaran harus lebih dari 0.");

        if (string.IsNullOrWhiteSpace(metode))
            throw new ArgumentException("Metode pembayaran wajib diisi.");

        var pembayaran = _pembayaranRepository.GetById(id);

        if (pembayaran == null)
            throw new Exception("Data tagihan tidak ditemukan.");

        pembayaran.JumlahDibayar += nominal;
        pembayaran.MetodePembayaran = metode;
        pembayaran.TanggalBayar = DateTime.Now;

        var currentState = Enum.Parse<StatusPembayaran>(pembayaran.Status);

        var paymentEvent = DetermineEvent(
            pembayaran.JumlahDibayar,
            pembayaran.JumlahTagihan
        );

        var nextState = GetNextState(currentState, paymentEvent);

        pembayaran.Status = nextState.ToString();

        _pembayaranRepository.Update(pembayaran);
    }

    public void UbahPembayaran(Pembayaran p)
    {
        if (p.Id <= 0)
            throw new ArgumentException("ID Pembayaran tidak valid.");

        Validate(p);

        var currentState = Enum.Parse<StatusPembayaran>(p.Status);

        var paymentEvent = DetermineEvent(
            p.JumlahDibayar,
            p.JumlahTagihan
        );

        p.Status = GetNextState(currentState, paymentEvent).ToString();

        _pembayaranRepository.Update(p);
    }

    public void HapusPembayaran(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID tidak valid.");

        _pembayaranRepository.Delete(id);
    }

    private PaymentEvent DetermineEvent(
        decimal jumlahDibayar,
        decimal jumlahTagihan
    )
    {
        if (jumlahDibayar <= 0)
            return PaymentEvent.TidakBayar;

        if (jumlahDibayar < jumlahTagihan)
            return PaymentEvent.BayarSebagian;

        return PaymentEvent.BayarLunas;
    }

    private StatusPembayaran GetNextState(
        StatusPembayaran currentState,
        PaymentEvent paymentEvent
    )
    {
        if (_transitionTable.TryGetValue(
            (currentState, paymentEvent),
            out var nextState))
        {
            return nextState;
        }

        throw new Exception(
            $"Transisi state tidak valid: {currentState} -> {paymentEvent}"
        );
    }
    // Penerapan Defensive Programming itu di sini
    private void Validate(Pembayaran p)
    {
        if (p == null)
            throw new ArgumentNullException(nameof(p));

        if (p.KontrakSewaId <= 0)
            throw new ArgumentException("Kontrak sewa harus dipilih.");

        if (string.IsNullOrWhiteSpace(p.Periode))
            throw new ArgumentException("Periode wajib diisi.");

        if (p.JumlahTagihan <= 0)
            throw new ArgumentException("Jumlah tagihan harus lebih dari 0.");

        if (p.JumlahDibayar < 0)
            throw new ArgumentException("Jumlah dibayar tidak boleh negatif.");
    }
}