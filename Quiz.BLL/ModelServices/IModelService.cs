using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace Quiz.BLL.ModelServices
{
    public interface IModelService<TModel> where TModel : class
    {
        IList<TModel> GetAll();

        TModel GetById(int id);

        int Create(TModel model);

        bool Update(int id, TModel model);

        bool Delete(int id);

    }
}
