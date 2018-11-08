using JobPortal.Model;

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
        
        public override bool Create(Users obj)
        {
            bool result = false;
            using (SqlConnection objConn = new SqlConnection("Data Source=JAKUB\\SQLEXPRESS; Initial Catalog=JobPortal;Integrated Security=True"))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {
                    _context.GetTable<Logging>().InsertOnSubmit(new Logging
                    {
                        UserName = obj.Logging.UserName,
                        Password = obj.Logging.Password,
                    });
                    _context.SubmitChanges();

                    var addressExists = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == obj.AddressTable.Postcode);
                    if (addressExists == null)
                    {
                        _context.GetTable<AddressTable>().InsertOnSubmit(new AddressTable
                        {
                            Postcode = obj.AddressTable.Postcode,
                            City = obj.AddressTable.City,
                            Region = obj.AddressTable.Region,
                        });
                    }

                    //TODO:if there is already city postcode and region just add reference instead of adding duplication.
                    _context.SubmitChanges();



                    _context.GetTable<Users>().InsertOnSubmit(new Users
                    {
                        PhoneNumber = obj.PhoneNumber,
                        FirstName = obj.FirstName,
                        LastName = obj.LastName,
                        Email = obj.Email,
                        Logging_ID = _context.GetTable<Logging>().FirstOrDefault(
                           t => t.UserName == obj.Logging.UserName).ID,
                        Gender_ID = _context.GetTable<Repository.DbConnection.Gender>().FirstOrDefault(
                            t => t.Gender1 == obj.Gender.Gender1.ToString()).ID,
                        AddressLine = obj.AddressLine,
                        City_ID = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == obj.AddressTable.Postcode).ID,

                });

                    

                    _context.SubmitChanges();
                    sql.Commit();
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

        public override bool Delete(Expression<Func<Users, bool>> predicate)
        {
            bool result = false;
            using (SqlConnection objConn = new SqlConnection("Data Source=JAKUB\\SQLEXPRESS;Initial Catalog=JobPortal;Integrated Security=True"))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {

                    Users found = _context.GetTable<Users>().FirstOrDefault(predicate);
                    _context.GetTable<Users>().DeleteOnSubmit(found);


                    
                    Logging foundLogging = _context.GetTable<Logging>().FirstOrDefault(t => t.ID.ToString() == found.Logging_ID.ToString());
                    _context.GetTable<Logging>().DeleteOnSubmit(foundLogging);

                 


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
            IQueryable<Users> allUsers =  _context.GetTable<Users>();
            return allUsers; 
        }

        public override Users Get(Expression<Func<Users, bool>> predicate)
        {
            try
            {
                Users found = _context.GetTable<Users>().FirstOrDefault(predicate);
                return found;
            }
            catch
            {
                return null;
            }
        }

        public override IQueryable<Users> List(Expression<Func<Users, bool>> predicate)
        {
            try
            {
                IQueryable<Users> found = _context.GetTable<Users>().Where(predicate);
                return found;
            }
            catch
            {
                return null;
            }
        }

        public Users Update(Users newInformation, string phoneNumber)
        {
            Users result = null;
            using (SqlConnection objConn = new SqlConnection("Data Source=JAKUB\\SQLEXPRESS;Initial Catalog=JobPortal;Integrated Security=True"))
            {
                
                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {
                    Users found = _context.GetTable<Users>().FirstOrDefault(u => u.PhoneNumber == phoneNumber);
                    int oldCity_ID = found.City_ID;
                    var oldPostCode = found.AddressTable.Postcode;
                    found.PhoneNumber = newInformation.PhoneNumber;
                    found.FirstName = newInformation.FirstName;
                    found.LastName = newInformation.LastName;
                    found.Email = newInformation.Email;
                    found.Logging.UserName = newInformation.Logging.UserName;
                    found.Logging.Password = newInformation.Logging.Password;
                    found.AddressLine = newInformation.AddressLine;
                    found.Gender.Gender1 = newInformation.Gender.Gender1;

                    var addressExists = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == newInformation.AddressTable.Postcode);
                    if (addressExists == null)
                    {
                        // nie działa
                        _context.GetTable<AddressTable>().InsertOnSubmit(new AddressTable
                        {
                            Postcode = newInformation.AddressTable.Postcode,
                            City = newInformation.AddressTable.City,
                            Region = newInformation.AddressTable.Region,
                            

                        });
                        string newPhoneNumber = newInformation.PhoneNumber;
                        _context.SubmitChanges();


                        /*Users found2 = _context.GetTable<Users>().FirstOrDefault(u => u.PhoneNumber == found.PhoneNumber);
                        var incoming_ID = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == newInformation.AddressTable.Postcode).ID;
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
                        found.AddressTable.Postcode = newInformation.AddressTable.Postcode;
                        found.AddressTable.City = newInformation.AddressTable.City;
                        found.AddressTable.Region = newInformation.AddressTable.Region;
                    }

                    //delete old address reference, however check if there is more people with the same city if yes leave it
                    

                    _context.SubmitChanges();
                    sql.Commit();
                    result =  found;
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


        public Users UpdateWeb(Users newInformation)
        {
            Users result = null;
            using (SqlConnection objConn = new SqlConnection("Data Source=JAKUB\\SQLEXPRESS;Initial Catalog=JobPortalTestDB;Integrated Security=True"))
            {

                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {
                    Users found = _context.GetTable<Users>().FirstOrDefault(u => u.ID == newInformation.ID);
                    /*int oldCity_ID = found.City_ID;
                    var oldPostCode = found.AddressTable.Postcode;*/

                    found.PhoneNumber = newInformation.PhoneNumber;
                    found.FirstName = newInformation.FirstName;
                    found.LastName = newInformation.LastName;
                    found.AddressLine = newInformation.AddressLine;
                    found.Gender.Gender1 = newInformation.Gender.Gender1;

                    var addressExists = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == newInformation.AddressTable.Postcode);
                    /*if (addressExists == null)
                    {
                        // nie działa
                        _context.GetTable<AddressTable>().InsertOnSubmit(new AddressTable
                        {
                            Postcode = newInformation.AddressTable.Postcode,
                            City = newInformation.AddressTable.City,
                            Region = newInformation.AddressTable.Region,


                        });
                        string newPhoneNumber = newInformation.PhoneNumber;
                        _context.SubmitChanges();


                        /*Users found2 = _context.GetTable<Users>().FirstOrDefault(u => u.PhoneNumber == found.PhoneNumber);
                        var incoming_ID = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == newInformation.AddressTable.Postcode).ID;
                        found2.City_ID = incoming_ID; tutaj STOP


                        int numberOfAddressRecords = _context.GetTable<Users>().Where(t => t.City_ID == oldCity_ID).Count();
                        if (numberOfAddressRecords < 2)
                        {
                            var addressToDelete = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == oldPostCode);
                            _context.GetTable<AddressTable>().DeleteOnSubmit(addressToDelete);
                        }
                    }
                    else
                    {*/
                        found.AddressTable.Postcode = newInformation.AddressTable.Postcode;
                        found.AddressTable.City = newInformation.AddressTable.City;
                        found.AddressTable.Region = newInformation.AddressTable.Region;
                    //}

                    //delete old address reference, however check if there is more people with the same city if yes leave it


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
