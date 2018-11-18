﻿
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
        private readonly string connection = "Data Source=DESKTOP-GQ6AKJT\\SA;Initial Catalog=JobPortal;User ID=sa;Password=root";



        public OfferRepository(DataContext context) : base(context)
        {
            _context = context;


        }

        public OfferRepository()
        {

        }

        public override ServiceOffer Create(ServiceOffer offer)
        {
            ServiceOffer result = null;
            using (SqlConnection objConn = new SqlConnection(connection))
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
                        Employee_ID = offer.Employee_ID,
                        Subcategory_ID = _context.GetTable<SubCategory>().FirstOrDefault(x => x.ID == offer.Subcategory_ID).ID
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


        public override bool Delete(Expression<Func<ServiceOffer, bool>> predicate)
        {
            bool result = false;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                try
                {
                    var toDelete = _context.GetTable<ServiceOffer>().SingleOrDefault(predicate);
                    _context.GetTable<ServiceOffer>().DeleteOnSubmit(toDelete);
                    _context.SubmitChanges();
                    result = true;

                }
                catch (Exception e)
                {
                    result = false;
                    throw e;
                }
                return result;
            }
        }


        public override bool Update(ServiceOffer modified)
        {
            bool result = false;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                try
                {
                    var foundService = _context.GetTable<ServiceOffer>().FirstOrDefault(x => x.ID == modified.ID);
                    if (foundService != null)
                    {
                        foundService.RatePerHour = modified.RatePerHour;
                        foundService.Title = modified.Title;
                        foundService.Description = modified.Description;
                        foundService.Subcategory_ID = modified.Subcategory_ID;
                        result = true;
                        _context.SubmitChanges();
                    }
                }
                catch
                {
                    result = false;

                }
                finally
                {
                    objConn.Close();
                }
                return result;

            }

        }
    }
}
