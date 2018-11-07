using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AppJobPortal.Model;
using Repositories;

namespace Repository.OfferRepository
{
   public interface IOfferRepository : IRepository<Offer>
    {
        bool Create(Offer obj);

        Offer Get(Expression<Func<Offer, bool>> predicate);

        bool Update(Offer obj);

        bool Delete(Expression<Func<Offer, bool>> predicate);

        IQueryable<Offer> GetAll();

        IQueryable<Offer> List(Expression<Func<Offer, bool>> predicate);
    }
}
