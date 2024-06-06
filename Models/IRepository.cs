using System.Collections.Generic;
namespace WebApplication2.Models
{
    public interface IRepository<TEntity>
    {


        public void Add(TEntity entity);
        public void delete(TEntity entity);

        public  List<TEntity> GetAll();


        public List<TEntity> GetById(int c_id);

    }
}
