using Quiz.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DAL
{
    public interface IUnitOfWork
    {
        GenericRepository<T> GetRepository<T>() where T : class;
        /// <summary>
        /// Save method.
        /// </summary>
        void Save();
    }
}
