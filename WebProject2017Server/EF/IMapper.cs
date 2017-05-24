using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebProject2017Server.EF.Mappers;

namespace WebProject2017Server.EF
{
    public interface IMapper<T> where T : WithID
    {
        List<T> GetAll();
        List<T> FindBy(Expression<Func<T, bool>> predicate);
        T FindBy(int id);
        List<T> AddorUpdate(List<T> entity);
        T AddorUpdate(T entity);
        void Delete(int key);
        void Save();
    }
}
