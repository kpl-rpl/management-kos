using System;
using System.Collections.Generic;
using System.Linq;
using management_kos.Models;

namespace management_kos.Repositories;

public class KamarRepositoryDummy : IKamarRepository
{
    private static readonly List<Kamar> Items = new List<Kamar>();
    private static int NextId = 1;

    public List<Kamar> GetAll()
    {
        return Items
            .OrderByDescending(x => x.Id)
            .Select(Clone)
            .ToList();
    }

    public Kamar? GetById(int id)
    {
        var item = Items.FirstOrDefault(x => x.Id == id);
        return item == null ? null : Clone(item);
    }

    public List<Kamar> GetByKosId(int kosId)
    {
        return Items
            .Where(x => x.KosId == kosId)
            .OrderByDescending(x => x.Id)
            .Select(Clone)
            .ToList();
    }

    public void Insert(Kamar kamar)
    {
        if (kamar == null) throw new ArgumentNullException(nameof(kamar));

        var copy = Clone(kamar);
        copy.Id = NextId++;
        Items.Add(copy);
    }

    public void Update(Kamar kamar)
    {
        if (kamar == null) throw new ArgumentNullException(nameof(kamar));

        var index = Items.FindIndex(x => x.Id == kamar.Id);
        if (index < 0)
        {
            throw new KeyNotFoundException("Data Kamar dengan Id " + kamar.Id + " tidak ditemukan.");
        }

        Items[index] = Clone(kamar);
    }

    public void Delete(int id)
    {
        var index = Items.FindIndex(x => x.Id == id);
        if (index >= 0)
        {
            Items.RemoveAt(index);
        }
    }

    private static Kamar Clone(Kamar src)
    {
        return new Kamar
        {
            Id = src.Id,
            KosId = src.KosId,
            NomorKamar = src.NomorKamar,
            Status = src.Status
        };
    }
}