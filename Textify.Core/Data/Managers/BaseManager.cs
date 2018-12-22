using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textify.Core.Data.Models;
using Textify.Core.Data.Providers;

namespace Textify.Core.Data.Managers
{
    public class BaseManager<TModel>
            where TModel : Entity
    {
        protected DataProvider<TModel> provider;

        public BaseManager()
        {
            provider = new DataProvider<TModel>();
        }

        public virtual TModel GetById(string id)
        {
            return provider.GetById(id);
        }

        public virtual IList<TModel> GetList(int page = 0, int pagesize = 50)
        {
            return provider.GetList(page, pagesize);
        }

        public virtual IList<TModel> All()
        {
            return provider.All();
        }

        public virtual void CreateOrUpdate(TModel model)
        {
            provider.CreateOrUpdate(model);
        }

        public virtual void Delete(TModel model)
        {
            provider.Delete(model);
        }

        public virtual TModel GetByField(string field, object value)
        {
            return provider.GetByField(field, value);
        }

        public virtual IList<TModel> GetListByField(string field, object value)
        {
            return provider.GetListByField(field, value);
        }
    }
}
