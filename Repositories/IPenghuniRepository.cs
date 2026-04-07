using management_kos.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace management_kos.Repositories
{
    public interface IPenghuniRepository
    {
        List<Penghuni> GetAll();
        Penghuni? GetById(int id);
        List<Penghuni> GetByKamarId(int kamarId);

        void Insert(Penghuni penghuni);
        void Update(Penghuni penghuni);
        void Delete(int id);
    }
}
