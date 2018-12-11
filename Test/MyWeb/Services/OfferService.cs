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
        private readonly IUnitOfWork _unitOfWork;

        public OfferService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            new NotificationService();

        }

        public OfferService()
        {
            _unitOfWork = new UnitOfWork(new JobPortalDatabaseDataContext());
            new NotificationService().Init();

        }

        public bool AddHoursToOffer(WorkingTime days)
        {
            return _unitOfWork.Offers.AddWorkingDates(new WorkingDates
            {
                NameOfDay = days.WeekDay.ToString(),
                HourFrom = days.Start,
                HourTo = days.End,
                ServiceOffer_ID = days.OfferId,
            });
        }

        public bool CreateServiceOffer(Offer offer)
        {
            try
            {
                if (RegexMatch.DoesOfferMatch(offer) && (offer.RatePerHour > 0))
                {
                    var serviceOffer = _unitOfWork.Offers.Create(new ServiceOffer
                    {

                        SubCategory = new Repository.DbConnection.SubCategory
                        {
                            Name = offer.Subcategory.ToString(),
                            Category = new Repository.DbConnection.Category
                            {
                                Name = offer.Category.ToString(),
                            },
                        },


                        Title = offer.Title,
                        Description = offer.Description,
                        RatePerHour = offer.RatePerHour,
                        Employee_ID = offer.AuthorId,

                    });
                    //      AddtoOffer(serviceOffer, offer);

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
            var dbResult = _unitOfWork.Offers.Get(x => x.ID == ID);
            if (dbResult != null)
            {

                offer = new Offer
                {
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
                    return _unitOfWork.Offers.Delete(t => t.ID == ID);

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
                if (RegexMatch.DoesOfferMatch(serviceOffer) && (serviceOffer.RatePerHour > 0))
                {
                    _unitOfWork.Offers.Update(new ServiceOffer
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
                        },
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
            foreach (var item in _unitOfWork.Offers.GetAll())
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
        public IQueryable<WorkingTime> GetAllWorkingDays()
        {
            IList<WorkingTime> resultToReturn = new List<WorkingTime>();
            foreach (var item in _unitOfWork.Offers.GetAllWorkingDays())
            {

                resultToReturn.Add(new WorkingTime
                {
                    Start = item.HourFrom,
                    End = item.HourTo,
                    OfferId = item.ServiceOffer_ID,
                    WeekDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), item.NameOfDay),

                });
            }
            return resultToReturn.AsQueryable<WorkingTime>();
        }
        public IQueryable<Offer> GetAllBought(string id)
        {
            IList<Offer> resultToReturn = new List<Offer>();
            foreach (var i in _unitOfWork.Offers.GetAllBought(id))
            {
                resultToReturn.Add(new Offer
                {
                    Id = i.ServiceOffer.ID,
                    AuthorId = id,
                    Description = i.ServiceOffer.Description,
                    Title = i.ServiceOffer.Title,
                    RatePerHour = i.ServiceOffer.RatePerHour,
                    Category = (Category)Enum.Parse(typeof(Category), i.ServiceOffer.SubCategory.Category.Name),
                    Subcategory = (SubCategory)Enum.Parse(typeof(SubCategory), i.ServiceOffer.SubCategory.Name),
                    WorkingTime = new WorkingDetails
                    {
                        Date = i.BookedDate.BookedDate1,
                        HoursFrom = i.BookedDate.HourFrom,
                        HoursTo = i.BookedDate.HourTo,
                        WeekDay = i.BookedDate.BookedDate1.DayOfWeek,
                    },


                });
            }
            return resultToReturn.AsQueryable<Offer>();
        }

        public bool AddReview(OfferReview review)
        {
            if ((review.Rate <= 5 || review.Rate >= 0) && review.Comment.Length <= 255)
            {
                try
                {
                    return _unitOfWork.Offers.AddReview(new Review
                    {
                        Comment = review.Comment,
                        Customer_ID = review.CustomerId,
                        RateValue = review.Rate,
                        ServiceOffer_ID = review.ServiceOfferId
                    }
                 );
                }
                catch
                {
                    return false;
                }
            }
            return false;


        }

        public IQueryable<OfferReview> GetServiceReviews(int serviceId)
        {
            if (_unitOfWork.Offers.GetServiceReviews(serviceId).Count() < 1)
            {
                return null;
            }
            else
            {
                return _unitOfWork.Offers.GetServiceReviews(serviceId).Select(x => new OfferReview { CustomerId = x.Customer_ID, Comment = x.Comment, Rate = (double)x.RateValue, ServiceOfferId = serviceId });
            }
        }

        public double GetAvgOfServiceRates(int serviceId)
        {
            try
            {
                return GetServiceReviews(serviceId).Select(x => x.Rate).Average();
            }
            catch
            {
                return 0;
            }
            
        }
    }
}