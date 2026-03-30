using System;
using System.Collections.Generic;
using System.Linq;
using management_kos.Models;

namespace management_kos.Repositories;
public class KamarRepository : IKamarRepository
{
    private static readonly List<Kamar> _items = new List<Kamar>();
    private static int _nextId = 1;

    public List<Kamar> GetAll()
    {
        return _items
            .OrderByDescending(x => x.Id)
            .Select(Clone)
            .ToList();
    }

    public Kamar? GetById(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        return item == null ? null : Clone(item);
    }

    public List<Kamar> GetByKosId(int kosId)
    {
        return _items
            .Where(x => x.KosId == kosId)
            .OrderByDescending(x => x.Id)
            .Select(Clone)
            .ToList();
    }

    public void Insert(Kamar kamar)
    {
        if (kamar == null) throw new ArgumentNullException(nameof(kamar));

        var newItem = Clone(kamar);
        newItem.Id = _nextId++;
        _items.Add(newItem);
    }

    public void Update(Kamar kamar)
    {
        if (kamar == null) throw new ArgumentNullException(nameof(kamar));

        var index = _items.FindIndex(x => x.Id == kamar.Id);
        if (index < 0)
        {
            throw new KeyNotFoundException("Data Kamar dengan Id " + kamar.Id + " tidak ditemukan.");
        }

        _items[index] = Clone(kamar);
    }

    public void Delete(int id)
    {
        var index = _items.FindIndex(x => x.Id == id);
        if (index >= 0)
        {
            _items.RemoveAt(index);
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
