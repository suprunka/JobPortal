using System;
using System.Linq;
using System.ServiceModel;
using Repository.DbConnection;
using Repository;
using ServiceLibrary.Models;
using System.Collections.Generic;
using JobPortal.Model;
using System.Data.Linq;
using SubCategory = JobPortal.Model.SubCategory;
using Category = JobPortal.Model.Category;
using System.Linq.Expressions;
using System.Collections;

namespace ServiceLibrary
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _database;

        public OfferService(IOfferRepository database)
        {
            _database = database;
        }

        public OfferService()
        {
            _database = new OfferRepository(new JobPortalDatabaseDataContext());

        }

        public bool AddtoOffer(ServiceOffer serviceOffer, Offer offer)
        {
            foreach (var item in offer.ListOfWorkingDays)
            {
                _database.AddToService(serviceOffer, new WorkingDate
                {
                    NameOfDay = item.WeekDay.ToString(),
                    HourFrom = item.HoursFrom,
                    HourTo = item.HoursTo,
                    ServiceOffer = serviceOffer,

                }
                    );
            }

            return false;
        }

        public bool CreateServiceOffer(Offer offer)
        {
            try
            {
                if (RegexMatch.DoesOfferMatch(offer) && (offer.RatePerHour > 0))
                {
                   var serviceOffer= _database.Create(new ServiceOffer
                    {

                        SubCategory = new Repository.DbConnection.SubCategory
                        {
                            Name = offer.Subcategory.ToString(),
                            Category = new Repository.DbConnection.Category
                            {
                                Name = offer.Subcategory.ToString(),
                            },
                        },
                        Title = offer.Title,
                        Description = offer.Description,
                        RatePerHour = offer.RatePerHour,
                        Employee_ID = offer.AuthorId,

                    });
                    AddtoOffer(serviceOffer, offer);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
           
        }

        public Offer FindServiceOffer(int ID)
        {
            Offer offer = null;
            var dbResult = _database.Get(x => x.ID == ID);
            if (dbResult != null)
            {
               
                offer = new Offer {
                    Id = ID,
                    AuthorId = dbResult.Employee_ID,
                    Description = dbResult.Description,
                    Title = dbResult.Title,
                    RatePerHour = dbResult.RatePerHour,
                    Subcategory = (SubCategory)Enum.Parse(typeof(SubCategory), dbResult.SubCategory.Name),
                    Category = (Category)Enum.Parse(typeof(Category), dbResult.SubCategory.Category.Name)
                };

            }
            return offer;
        }

        public bool DeleteServiceOffer(int ID)
        {
            try
            {
                if (ID > -1)
                {
                   return _database.Delete(t => t.ID == ID);
                   
                }
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }

        }

        public bool UpdateServiceOffer(Offer serviceOffer)
        {
            try
            {
                if ((RegexMatch.DoesOfferMatch(serviceOffer)) && (serviceOffer.RatePerHour > 0))
                {
                    _database.Update(new ServiceOffer
                    {
                        ID = serviceOffer.Id,
                        Description = serviceOffer.Description,
                        Title = serviceOffer.Title,
                        RatePerHour = serviceOffer.RatePerHour,
                        SubCategory = new Repository.DbConnection.SubCategory
                        {
                            Name = serviceOffer.Subcategory.ToString(),
                            Category = new Repository.DbConnection.Category
                            {
                                Name = serviceOffer.Category.ToString()
                            }
                        }
                    });
                    return true;
                }
                return false;

            }

            catch (ChangeConflictException e)
            {
                //optimistic concurrency
                throw e;

            }
            catch
            {
                return false;
            }


        }

        public IQueryable<Offer> GetAllOffers()
        {
            IList<Offer> resultToReturn = new List<Offer>();
            foreach (var item in _database.GetAll())
            {
               
                resultToReturn.Add(new Offer
                {
                    Id = item.ID,
                    AuthorId = item.Employee_ID,
                    Description = item.Description,
                    Title = item.Title,
                    RatePerHour = item.RatePerHour,
                    Subcategory = (SubCategory)Enum.Parse(typeof(SubCategory), item.SubCategory.Name),
                    Category = (Category)Enum.Parse(typeof(Category), item.SubCategory.Category.Name),
                });
            }
            return resultToReturn.AsQueryable<Offer>();
        }
       
  

    }
}