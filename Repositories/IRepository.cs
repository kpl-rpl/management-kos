namespace management_kos.Repositories;

public interface IRepository<T>
{
    List<T> GetAll();
    T? GetById(int id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(int id);
}
