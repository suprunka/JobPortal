using AppJobPortal.Model;
using Repositories;
using Repository.DbConnection;
using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.OfferRepository
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        private DataContext _context;
        private SqlTransaction sql = null;
        private Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private JobPortalDatabaseDataContext jobPortalDatabaseDataContext;

        public OfferRepository(DataContext context) : base(context)
        {
            _context = context;
        }

      

        public bool Create(ServiceOffer offer)
        {
            bool result = false;
            using (SqlConnection objConn = new SqlConnection("Data Source=DESKTOP-GQ6AKJT\\SA;Initial Catalog=JobPortal;Integrated Security=True"))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {
                    _context.GetTable<ServiceOffer>().InsertOnSubmit(new ServiceOffer
                    {
                        Description = offer.Description,
                        RatePerHour = offer.RatePerHour,
                        Title = offer.Title,
                        Employee_Phone = offer.Employee_Phone,
                        Subcategory_ID= _context.GetTable<DbConnection.SubCategory>().FirstOrDefault(x => x.Name.Equals(offer.SubCategory)).ID
              
                    });
                    result = true;
                }
                catch (Exception e)
                {
                    sql.Rollback();
                    result = false;
                    throw e;
                }
                finally
                {
                    objConn.Close();
                }
            }
            return result;
        }

        public bool Delete(System.Linq.Expressions.Expression<Func<ServiceOffer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ServiceOffer Get(System.Linq.Expressions.Expression<Func<ServiceOffer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(ServiceOffer obj)
        {
            throw new NotImplementedException();
        }

        IQueryable<ServiceOffer> IOfferRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        IQueryable<ServiceOffer> IOfferRepository.List(Expression<Func<ServiceOffer, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
