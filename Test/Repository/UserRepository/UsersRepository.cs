using JobPortal.Model;

using Repository.DbConnection;
using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace Repository
{
    public class UsersRepository : Repository<Users>, IUserRepository
    {
        private DataContext _context;
        private SqlTransaction sql = null;
        private readonly string connection = "Data Source=kraka.ucn.dk;Initial Catalog=dmai0917_1067712;User ID=dmai0917_1067712;Password=Password1!";
        public UsersRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public override Users Create(Users obj)
        {
            Users result = null;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                using (var myTran = new TransactionScope())
                {

                    if (obj.Logging_ID == null)
                    {
                        try
                        {
                            AspNetUsers logging = new AspNetUsers
                            {
                                Id = obj.AspNetUsers.UserName,
                                EmailConfirmed = false,
                                PhoneNumberConfirmed = false,
                                TwoFactorEnabled = false,
                                LockoutEnabled = false,
                                AccessFailedCount = 0,
                                UserName = obj.AspNetUsers.UserName,
                                PasswordHash = obj.AspNetUsers.PasswordHash,
                                Email = obj.AspNetUsers.Email,
                                PhoneNumber = obj.AspNetUsers.PhoneNumber,

                            };
                            _context.GetTable<AspNetUsers>().InsertOnSubmit(logging);
                            _context.SubmitChanges();

                            var addressExists = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == obj.AddressTable.Postcode);
                            if (addressExists == null)
                            {
                                addressExists = new AddressTable
                                {
                                    Postcode = obj.AddressTable.Postcode,
                                    City = obj.AddressTable.City,
                                    Region = obj.AddressTable.Region,
                                };
                                _context.GetTable<AddressTable>().InsertOnSubmit(addressExists);


                                _context.SubmitChanges();
                            }

                            Users u = new Users
                            {
                                FirstName = obj.FirstName,
                                LastName = obj.LastName,
                                Logging_ID = logging.Id,
                                Gender_ID = _context.GetTable<Repository.DbConnection.Gender>().FirstOrDefault(
                                    t => t.Gender1 == obj.Gender.Gender1.ToString()).ID,
                                AddressLine = obj.AddressLine,
                                City_ID = addressExists.ID,
                                PayPalMail = obj.PayPalMail,
                             
                            };

                            _context.GetTable<Users>().InsertOnSubmit(u);
                            _context.SubmitChanges();

                            myTran.Complete();
                            result = u;


                        }
                        catch
                        {
                            result = null;
                            throw new DuplicateKeyException(this);
                        }
                        finally
                        {
                            myTran.Dispose();
                            objConn.Close();
                        }
                    }
                    else
                    {
                        try
                        {
                            var addressExists = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == obj.AddressTable.Postcode);
                            if (addressExists == null)
                            {
                                addressExists = new AddressTable
                                {
                                    Postcode = obj.AddressTable.Postcode,
                                    City = obj.AddressTable.City,
                                    Region = obj.AddressTable.Region,
                                };
                                _context.GetTable<AddressTable>().InsertOnSubmit(addressExists);


                                _context.SubmitChanges();
                            }

                            Users u = new Users
                            {
                                FirstName = obj.FirstName,
                                LastName = obj.LastName,
                                Logging_ID = obj.Logging_ID,
                                Gender_ID = _context.GetTable<Repository.DbConnection.Gender>().FirstOrDefault(
                                    t => t.Gender1 == obj.Gender.Gender1.ToString()).ID,
                                AddressLine = obj.AddressLine,
                                City_ID = addressExists.ID,
                                PayPalMail = obj.PayPalMail,
                            };

                            _context.GetTable<Users>().InsertOnSubmit(u);
                            _context.SubmitChanges();

                            myTran.Complete();
                            result = u;
                        }
                        catch
                        {
                            result = null;
                            throw new DuplicateKeyException(this);
                        }
                        finally
                        {
                            myTran.Dispose();
                            objConn.Close();
                        }

                    }


                }
            }
            return result;
        }


        public override bool Delete(Expression<Func<Users, bool>> predicate)
        {
            bool result = false;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {

                    Users found = _context.GetTable<Users>().FirstOrDefault(predicate);
                    _context.GetTable<Users>().DeleteOnSubmit(found);

                    AspNetUsers foundLogging = _context.GetTable<AspNetUsers>().FirstOrDefault(t => t.Id.ToString() == found.Logging_ID.ToString());
                    _context.GetTable<AspNetUsers>().DeleteOnSubmit(foundLogging);



                    var services = _context.GetTable<ServiceOffer>().Where(t => t.Employee_ID == foundLogging.Id);
                    foreach (var t in services)
                    {
                        _context.GetTable<ServiceOffer>().DeleteOnSubmit(t);
                    }



                    //delete however check if there is more people with the same city if not leave the city
                    int numberOfAddressRecords = _context.GetTable<Users>().Where(t => t.City_ID == found.City_ID).Count();
                    if (numberOfAddressRecords < 2)
                    {

                        var addressToDelete = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == found.AddressTable.Postcode);
                        _context.GetTable<AddressTable>().DeleteOnSubmit(addressToDelete);
                    }

                    _context.SubmitChanges();

                    sql.Commit();
                    result = true;

                }
                catch (Exception)
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
            return base.GetAll();
        }

        public override Users Get(Expression<Func<Users, bool>> predicate)
        {
            return base.Get(predicate);
        }

        public override IQueryable<Users> List(Expression<Func<Users, bool>> predicate)
        {
            return base.List(predicate);
        }
        public AspNetUsers Login(AspNetUsers account)
        {
            return base.Login(account);
        }

        public override bool Update(Users obj)
        {
            
                bool result = false;
                Users found = _context.GetTable<Users>().FirstOrDefault(u => u.ID == obj.ID);
                using (SqlConnection objConn = new SqlConnection(connection))
                {
                    objConn.Open();
                    if (found.LastUpdate == obj.LastUpdate)
                    {
                        using (var myTran = new TransactionScope())
                        {
                            try
                            {

                                int oldCity_ID = found.City_ID;
                                var oldPostCode = found.AddressTable.Postcode;
                                found.AspNetUsers.PhoneNumber = obj.AspNetUsers.PhoneNumber;
                                found.FirstName = obj.FirstName;
                                found.LastName = obj.LastName;
                                found.AddressLine = obj.AddressLine;
                                found.PayPalMail = obj.PayPalMail;
                                if (obj.Gender.Gender1 == "Male")
                                {
                                    found.Gender = _context.GetTable<DbConnection.Gender>().Single(x => x.Gender1 == "Male");
                                }
                                else
                                {
                                    found.Gender = _context.GetTable<DbConnection.Gender>().Single(x => x.Gender1 == "Female");
                                }

                                var addressExists = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == obj.AddressTable.Postcode);
                                if (addressExists == null)
                                {

                                    _context.GetTable<AddressTable>().InsertOnSubmit(new AddressTable
                                    {
                                        Postcode = obj.AddressTable.Postcode,
                                        City = obj.AddressTable.City,
                                        Region = obj.AddressTable.Region,
                                    });

                                    _context.SubmitChanges();

                                    found.AddressTable = _context.GetTable<AddressTable>().Single(x => x.Postcode == obj.AddressTable.Postcode);
                                    _context.SubmitChanges();
                                }
                                else
                                {
                                    found.AddressTable = _context.GetTable<AddressTable>().Single(x => x.Postcode == addressExists.Postcode);
                                }

                                int numberOfAddressRecords = _context.GetTable<Users>().Where(t => t.City_ID == oldCity_ID).Count();
                                if (numberOfAddressRecords < 2)
                                {
                                    var addressToDelete = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == oldPostCode);
                                    _context.GetTable<AddressTable>().DeleteOnSubmit(addressToDelete);
                                }
                                _context.SubmitChanges();



                                _context.SubmitChanges();


                                myTran.Complete();
                                result = true;
                            }


                            catch(Exception e)
                            {

                                result = false;
                                throw new InvalidOperationException();
                            }
                            finally
                            {
                                objConn.Close();
                            }
                        }
                    }

                    return result;
                }
            
        }

        public Users UpdateUserMail(Users newInformation)
        {
            Users result = null;
            using (SqlConnection objConn = new SqlConnection(connection))
            {

                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {
                    Users found = _context.GetTable<Users>().FirstOrDefault(u => u.ID == newInformation.ID);
                    found.AspNetUsers.Email = newInformation.AspNetUsers.Email;
                    _context.SubmitChanges();
                    sql.Commit();
                    result = found;
                }
                catch
                {
                    sql.Rollback();
                    result = null;
                    throw new InvalidOperationException();
                }
                finally
                {
                    objConn.Close();
                }
            }
            return result;
        }

        public Users AddDescription(Users newInformation)
        {
            Users result = null;
            using (SqlConnection objConn = new SqlConnection(connection))
            {

                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {
                    Users found = _context.GetTable<Users>().FirstOrDefault(u => u.ID == newInformation.ID);
                    found.Description = newInformation.Description;
                    _context.SubmitChanges();
                    sql.Commit();
                    result = found;
                }
                catch
                {
                    sql.Rollback();
                    result = null;
                    throw new InvalidOperationException();
                }
                finally
                {
                    objConn.Close();
                }
            }
            return result;
        }
    }
}
