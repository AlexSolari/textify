using sORM.Core;
using sORM.Core.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textify.Core.Api;
using Textify.Core.Data.Models;
using Textify.Core.Dependencies;
using Textify.Core.Extensions;

namespace Textify.Core.Data.Providers
{
    public class DataProvider<TEntity>
        where TEntity : Entity
    {
        public DataProvider()
        {

        }

        public virtual TEntity GetByField(string field, object value)
        {
            TEntity result = null;

            var key = "{0}-{1}-{2}/{3}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name, field, value);
            if (SiteApi.Cache.HasKey(key))
            {
                return SiteApi.Cache.Get<TEntity>(key);
            }

            result = SimpleORM.Current.Get<TEntity>(Condition.Equals(field, value)).FirstOrDefault();

            SiteApi.Cache.Add(result, key);
            return result;
        }

        public virtual IList<TEntity> GetListByField(string field, object value)
        {
            IList<TEntity> result = Resolver.GetInstance<IList<TEntity>>();

            var key = "{0}-{1}-{2}/{3}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name, field, value);
            if (SiteApi.Cache.HasKey(key))
            {
                return SiteApi.Cache.Get<IList<TEntity>>(key);
            }

            result = SimpleORM.Current.Get<TEntity>(Condition.Equals(field, value)).ToList();

            SiteApi.Cache.Add(result, key);
            return result;
        }

        public virtual IList<TEntity> GetList(int page = 0, int pagesize = 50)
        {
            IList<TEntity> result = Resolver.GetInstance<IList<TEntity>>();

            var key = "{0}-{1}-{2}/{3}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name, page, pagesize);
            if (SiteApi.Cache.HasKey(key))
            {
                return SiteApi.Cache.Get<IList<TEntity>>(key);
            }

            result = SimpleORM.Current.Get<TEntity>(options: new DataEntityListLoadOptions(pagesize, page)).ToList();

            SiteApi.Cache.Add(result, key);
            return result;
        }

        public virtual IList<TEntity> All()
        {
            IList<TEntity> result = Resolver.GetInstance<IList<TEntity>>();

            var key = "{0}-{1}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name);
            if (SiteApi.Cache.HasKey(key))
            {
                return SiteApi.Cache.Get<IList<TEntity>>(key);
            }

            result = SimpleORM.Current.Get<TEntity>(options: new DataEntityListLoadOptions()).ToList();

            SiteApi.Cache.Add(result, key);
            return result;
        }

        public TEntity GetById(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                return GetById(guid);
            }
            return null;
        }

        public TEntity GetById(Guid id)
        {
            TEntity result = null;

            var key = "{0}-{1}-{2}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name, id);
            if (SiteApi.Cache.HasKey(key))
            {
                return SiteApi.Cache.Get<TEntity>(key);
            }

            result = SimpleORM.Current.Get<TEntity>(Condition.Equals("Id", id)).FirstOrDefault();

            SiteApi.Cache.Add(result, key);
            return result;
        }

        public void CreateOrUpdate(TEntity model)
        {
            SimpleORM.Current.CreateOrUpdate(model);
            SiteApi.Cache.DropWhere(x => x.Contains("GetById") && x.Contains(model.Id.ToString()) && x.Contains(typeof(TEntity).Name));
            SiteApi.Cache.DropWhere(x => x.Contains("All") && x.Contains(typeof(TEntity).Name));
            SiteApi.Cache.DropWhere(x => x.Contains("List") && x.Contains(typeof(TEntity).Name));
            SiteApi.Cache.DropWhere(x => x.Contains("GetByField") && x.Contains(typeof(TEntity).Name));
        }

        public void Delete(TEntity model)
        {
            SimpleORM.Current.Delete(model);
            SiteApi.Cache.DropWhere(x => x.Contains("GetById") && x.Contains(model.Id.ToString()) && x.Contains(typeof(TEntity).Name));
            SiteApi.Cache.DropWhere(x => x.Contains("All") && x.Contains(typeof(TEntity).Name));
            SiteApi.Cache.DropWhere(x => x.Contains("List") && x.Contains(typeof(TEntity).Name));
            SiteApi.Cache.DropWhere(x => x.Contains("GetByField") && x.Contains(typeof(TEntity).Name));
        }
    }
}
