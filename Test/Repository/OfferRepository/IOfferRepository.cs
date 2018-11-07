using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AppJobPortal.Model;
using Repositories;
using Repository.DbConnection;

namespace Repository.OfferRepository
{
   public interface IOfferRepository : IRepository<Offer>
    {
        bool Create(ServiceOffer obj);

        ServiceOffer Get(Expression<Func<ServiceOffer, bool>> predicate);

        bool Update(ServiceOffer obj);

        bool Delete(Expression<Func<ServiceOffer, bool>> predicate);

        IQueryable<ServiceOffer> GetAll();

        IQueryable<ServiceOffer> List(Expression<Func<ServiceOffer, bool>> predicate);
    }
}
