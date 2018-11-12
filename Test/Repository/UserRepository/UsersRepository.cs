﻿using JobPortal.Model;

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
        //private readonly string  connection = ConfigurationManager.ConnectionStrings["JobPortalDatabaseTesting"].ConnectionString;
        public UsersRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public override Users Create(Users obj)
        {
            Users result = null;
            using (SqlConnection objConn = new SqlConnection("Data Source = JAKUB\\SQLEXPRESS; Initial Catalog = JobPortalTestDB; Integrated Security = True"))
            {
                objConn.Open();
                using (var myTran = new TransactionScope())
                {
                    try
                    {
                        Logging logging = new Logging
                        {
                            UserName = obj.Logging.UserName,
                            Password = obj.Logging.Password,
                        };

                        _context.GetTable<Logging>().InsertOnSubmit(logging);
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
                            PhoneNumber = obj.PhoneNumber,
                            FirstName = obj.FirstName,
                            LastName = obj.LastName,
                            Email = obj.Email,
                            Logging_ID = logging.ID,
                            Gender_ID = _context.GetTable<Repository.DbConnection.Gender>().FirstOrDefault(
                                t => t.Gender1 == obj.Gender.Gender1.ToString()).ID,
                            AddressLine = obj.AddressLine,
                            City_ID = addressExists.ID
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
            using (SqlConnection objConn = new SqlConnection("Data Source = JAKUB\\SQLEXPRESS; Initial Catalog = JobPortalTestDB; Integrated Security = True"))
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

        public override bool Update(Users obj)
        {
            {
                bool result = false;
                using (SqlConnection objConn = new SqlConnection("Data Source = JAKUB\\SQLEXPRESS; Initial Catalog = JobPortalTestDB; Integrated Security = True"))
                {

                    objConn.Open();
                    sql = objConn.BeginTransaction();
                    try
                    {
                        Users found = _context.GetTable<Users>().FirstOrDefault(u => u.ID == obj.ID);
                        int oldCity_ID = found.City_ID;
                        var oldPostCode = found.AddressTable.Postcode;
                        found.PhoneNumber = obj.PhoneNumber;
                        found.FirstName = obj.FirstName;
                        found.LastName = obj.LastName;
                        found.Email = obj.Email;
                        found.Logging.UserName = obj.Logging.UserName;
                        found.Logging.Password = obj.Logging.Password;
                        found.AddressLine = obj.AddressLine;
                        found.Gender.Gender1 = obj.Gender.Gender1;

                        var addressExists = _context.GetTable<AddressTable>().FirstOrDefault(t => t.Postcode == obj.AddressTable.Postcode);
                        if (addressExists == null)
                        {

                            _context.GetTable<AddressTable>().InsertOnSubmit(new AddressTable
                            {
                                Postcode = obj.AddressTable.Postcode,
                                City = obj.AddressTable.City,
                                Region = obj.AddressTable.Region,


                            });
                            string newPhoneNumber = obj.PhoneNumber;
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
