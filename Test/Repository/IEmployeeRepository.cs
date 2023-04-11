using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public interface IEmployeeRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object EmployeeID);
        void Add(T Employee);
        void Update(T Employee);
        void Delete(object Employee);
        void Save();
    }
}
