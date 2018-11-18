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
        private readonly string connection = "Data Source=kraka.ucn.dk;Initial Catalog=dmai0917_1067677;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework";
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
                    try
                    {
                       //if (loggingId == null)
                       //{
                       //    AspNetUsers logging = new AspNetUsers
                       //    {
                       //        UserName = obj.AspNetUsers.UserName,
                       //        PasswordHash = obj.AspNetUsers.PasswordHash,
                       //        Email = obj.AspNetUsers.Email,
                       //        PhoneNumber = obj.AspNetUsers.PhoneNumber,
                       //    };
                       //
                       //    _context.GetTable<AspNetUsers>().InsertOnSubmit(logging);
                       //    _context.SubmitChanges();
                       //    loggingId = logging.Id.ToString();
                       //}

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
                        objConn.Close();
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
        public  AspNetUsers Login(AspNetUsers account)
        {
            return base.Login(account);
        }

        public override bool Update(Users obj)
        {
            {
                bool result = false;
                using (SqlConnection objConn = new SqlConnection(connection))
                {

                    objConn.Open();
                    sql = objConn.BeginTransaction();
                    try
                    {
                        Users found = _context.GetTable<Users>().FirstOrDefault(u => u.ID == obj.ID);
                        int oldCity_ID = found.City_ID;
                        var oldPostCode = found.AddressTable.Postcode;
                        found.AspNetUsers.PhoneNumber = obj.AspNetUsers.PhoneNumber;
                        found.FirstName = obj.FirstName;
                        found.LastName = obj.LastName;
                        found.AspNetUsers.Email = obj.AspNetUsers.Email;
                        found.AspNetUsers.UserName = obj.AspNetUsers.UserName;
                       // found.AspNetUsers.Password = obj.AspNetUsers.AspNetUsers.Password;
                        found.AddressLine = obj.AddressLine;
                        if(obj.Gender.Gender1 == "Male")
                        {
                            found.Gender_ID = 1;
                        }
                        else
                        {
                            found.Gender_ID = 2;
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
                            string newPhoneNumber = obj.AspNetUsers.PhoneNumber;
                            _context.SubmitChanges();


                            /*Users found2 = _context.GetTable<Users>().FirstOrDefault(u => u.PhoneNumber == found.PhoneNumber);
                            var incoming_ID = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == obj.AddressTable.Postcode).ID;
                            found2.City_ID = incoming_ID;*/


                            int numberOfAddressRecords = _context.GetTable<Users>().Where(t => t.City_ID == oldCity_ID).Count();
                            if (numberOfAddressRecords < 2)
                            {
                                var addressToDelete = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == oldPostCode);
                                _context.GetTable<AddressTable>().DeleteOnSubmit(addressToDelete);
                            }
                        }
                        else
                        {
                            found.AddressTable.Postcode = obj.AddressTable.Postcode;
                            found.AddressTable.City = obj.AddressTable.City;
                            found.AddressTable.Region = obj.AddressTable.Region;
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
                    found.Description= newInformation.Description;
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
