using JobPortal.Model;
using Repositories;
using Repository.DbConnection;
using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public class UsersRepository : Repository<Users>, IUserRepository
    {
        private DataContext _context;
        private SqlTransaction sql = null;
        private Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


        public UsersRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public bool Create(User model)
        {
            bool result = false;
            using (SqlConnection objConn = new SqlConnection("Data Source=JAKUB\\SQLEXPRESS; Initial Catalog=JobPortalTestDB;Integrated Security=True"))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {
                    _context.GetTable<Logging>().InsertOnSubmit(new Logging
                    {
                        ID = Int32.Parse(model.PhoneNumber),
                        UserName = model.UserName,
                        Password = model.Password,
                    });
                    _context.SubmitChanges();

                    var addressExists = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == model.Postcode);
                    if (addressExists == null)
                    {
                        _context.GetTable<AddressTable>().InsertOnSubmit(new AddressTable
                        {
                            Postcode = model.Postcode,
                            City = model.CityName,
                            Region = model.Region.ToString(),
                        });
                    }

                    //TODO:if there is already city postcode and region just add reference instead of adding duplication.
                    _context.SubmitChanges();



                    _context.GetTable<Users>().InsertOnSubmit(new Users
                    {
                        PhoneNumber = model.PhoneNumber,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Logging_ID = _context.GetTable<Logging>().FirstOrDefault(
                           t => t.UserName == model.UserName).ID,
                        Gender_ID = _context.GetTable<Repository.DbConnection.Gender>().FirstOrDefault(
                            t => t.Gender1 == model.Gender.ToString()).ID,
                        AddressLine = model.AddressLine,
                        Postcode = model.Postcode,

                    });

                    _context.SubmitChanges();
                    sql.Commit();
                    result = true;
                }
                catch
                {
                    sql.Rollback();
                    result = false;
                    throw new DuplicateKeyException(model);
                }
                finally
                {
                    objConn.Close();
                }
            }
            return result;
        }


        public override bool Delete(Expression<Func<Users, bool>> predicate)
        {
            bool result = false;
            using (SqlConnection objConn = new SqlConnection("Data Source=JAKUB\\SQLEXPRESS;Initial Catalog=JobPortalDBTest;Integrated Security=True"))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {

                    Users found = _context.GetTable<Users>().FirstOrDefault(predicate);
                    _context.GetTable<Users>().DeleteOnSubmit(found);

                    Logging foundLogging = _context.GetTable<Logging>().FirstOrDefault(t => t.ID.ToString() == found.PhoneNumber);
                    _context.GetTable<Logging>().DeleteOnSubmit(foundLogging);


                    //delete however check if there is more people with the same city if not leave the city
                    int numberOfAddressRecords = _context.GetTable<Users>().Where(t => t.Postcode == found.Postcode).Count();
                    if (numberOfAddressRecords < 2)
                    {

                        var addressToDelete = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == found.Postcode);
                        _context.GetTable<AddressTable>().DeleteOnSubmit(addressToDelete);
                    }
                    _context.SubmitChanges();

                    sql.Commit();
                    result = true;

                }
                catch
                {
                    sql.Rollback();
                    result = false;
                    throw new InvalidOperationException();
                }
                finally
                {
                    objConn.Close();
                }
            }
            return result;
        }


        public override IQueryable<Users> GetAll()
        {
        
            IQueryable<Users> allUsers =  _context.GetTable<Users>();
            return allUsers;
             
        }
    }
}
