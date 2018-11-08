
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

        public override ServiceOffer Create(ServiceOffer offer)
        {
            ServiceOffer result = null;
            using (SqlConnection objConn = new SqlConnection("Data Source=JAKUB\\SQLEXPRESS;Initial Catalog=JobPortal;Integrated Security=True"))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {
                    ServiceOffer s = new ServiceOffer
                    {
                        Description = offer.Description,
                        RatePerHour = offer.RatePerHour,
                        Title = offer.Title,
                        Employee_Phone = offer.Employee_Phone,
                        Subcategory_ID = _context.GetTable<SubCategory>().FirstOrDefault(x => x.Name.Equals(offer.SubCategory.Name)).ID

                    };

                    _context.GetTable<ServiceOffer>().InsertOnSubmit(s);
                    _context.SubmitChanges();
                    result = s;
                    sql.Commit();
                }
                catch (Exception e)
                {
                    sql.Rollback();
                    result = null;
                    throw e;
                }
                finally
                {
                    objConn.Close();
                }
            }
            return result;
        }


        
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
            return base.Delete(predicate);
        }

        //TO DO
        public override bool Update(ServiceOffer obj)
        {
            return base.Update(obj);
        }
    }
}
