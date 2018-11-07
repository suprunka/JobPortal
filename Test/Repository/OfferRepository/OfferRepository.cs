using AppJobPortal.Model;
using Repositories;
using Repository.DbConnection;
using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;


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

      

        public bool Create(Advertisement offer)
        {
            bool result = false;
            using (SqlConnection objConn = new SqlConnection("Data Source=DESKTOP-GQ6AKJT\\SA;Initial Catalog=JobPortal;Integrated Security=True"))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {
                    _context.GetTable<Advertisement>().InsertOnSubmit(new Advertisement
                    {
                        Description = offer.Description,
                        RatePerHour = offer.RatePerHour,
                        Title = offer.Title,
                        Employee_Phone = offer.Employee_Phone,
                        Service_ID = _context.GetTable<ServiceTable>().FirstOrDefault(x => x.Name.Equals(offer.Service_ID)).ID
               
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

        public bool Delete(System.Linq.Expressions.Expression<Func<Offer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Offer Get(System.Linq.Expressions.Expression<Func<Offer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Offer> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Offer> List(System.Linq.Expressions.Expression<Func<Offer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(Offer obj)
        {
            throw new NotImplementedException();
        }
    }
}
