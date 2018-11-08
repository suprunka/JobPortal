
using Repository.DbConnection;
using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public class OfferRepository : Repository<ServiceOffer>, IOfferRepository
    {
        private DataContext _context;
        private SqlTransaction sql = null;
        private Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


        public OfferRepository(DataContext context) : base(context)
        {
            _context = context;


        }

        public override bool Create(ServiceOffer offer)
        {
            bool result = false;
            using (SqlConnection objConn = new SqlConnection("Data Source=JAKUB\\SQLEXPRESS;Initial Catalog=JobPortal;Integrated Security=True"))
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
                        Subcategory_ID = _context.GetTable<SubCategory>().FirstOrDefault(x => x.Name.Equals(offer.SubCategory.Name)).ID

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


        /*public override bool Create(ServiceOffer offer)
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
        }*/

        public override ServiceOffer Get(Expression<Func<ServiceOffer, bool>> predicate)
        {
            return base.Get(predicate);
        }

        public override IQueryable<ServiceOffer> GetAll()
        {
            return base.GetAll();
        }

        public override IQueryable<ServiceOffer> List(Expression<Func<ServiceOffer, bool>> predicate)
        {
            return base.List(predicate);
        }

        //TO DO
        public override bool Delete(Expression<Func<ServiceOffer, bool>> predicate)
        {
            bool result = false;
            using (SqlConnection objConn = new SqlConnection("Data Source=JAKUB\\SQLEXPRESS;Initial Catalog=JobPortal;Integrated Security=True"))
            {
                objConn.Open();
                try
                {
                    var toDelete = _context.GetTable<ServiceOffer>().SingleOrDefault(predicate);
                    _context.GetTable<ServiceOffer>().DeleteOnSubmit(toDelete);
                    result = true;
                }
                catch (Exception e )
                {
                    result = false;
                    throw e;
                }
                return result;
            }
            }

        //TO DO
        public override bool Update(ServiceOffer obj)
        {
            return base.Update(obj);
        }
    }
}
