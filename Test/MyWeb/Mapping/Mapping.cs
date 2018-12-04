using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using WebJobPortal.Models;

namespace MyWeb.Mapping
{
    public static class Mapping
    {

        public static User Map_SetPropertiesViewModel_To_User(SetPropertiesViewModel model)
        {
            return new User
            {
                AddressLine = model.AddressLine,
                CityName = model.CityName,
                FirstName = model.FirstName,
                Gender = model.Gender,
                LastName = model.LastName,
                PayPalMail = model.PayPalMail,
                Postcode = model.Postcode,
                Region = model.Region,

            };
        }

        public static ChangeEmailViewModel Map_User_To_ChangeEmailViewModel(User model)
        {
            return new ChangeEmailViewModel
            {
                NewEmail = model.Email,
            };
        }

        public static DescriptionViewModel Map_User_To_DescriptionViewModel(User model)
        {
            return new DescriptionViewModel
            {
                Description = model.Description,
            };
        }

        public static User Map_DescriptionViewModel_To_User(DescriptionViewModel model)
        {
            return new User
            {
                ID = model.ID,
                Description = model.Description,
            };
        }

        public static User Map_ChangeEmailViewModel_To_User(ChangeEmailViewModel model)
        {
            return new User
            {
                ID = model.Id,
                Email = model.NewEmail,
            };
        }

        public static UserProfileViewModel Map_User_To_UserProfileViewModel(User user)
        {
            return new UserProfileViewModel

            {
                ID = user.ID.ToString(),
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                CityName = user.CityName,
                AddressLine = user.AddressLine,
                Postcode = user.Postcode,
                Region = user.Region,
                PayPalMail = user.PayPalMail,
                UserName = user.UserName,
                Description = user.Description,
            };
        }

        public static User Map_UserProfileViewModel_To_User(UserProfileViewModel userProfileViewModel)
        {
            return new User
            {
                ID = Int32.Parse(userProfileViewModel.ID),
                FirstName = userProfileViewModel.FirstName,
                LastName = userProfileViewModel.LastName,
                PhoneNumber = userProfileViewModel.PhoneNumber,
                Gender = userProfileViewModel.Gender,
                CityName = userProfileViewModel.CityName,
                AddressLine = userProfileViewModel.AddressLine,
                Postcode = userProfileViewModel.Postcode,
                Region = userProfileViewModel.Region,
                PayPalMail = userProfileViewModel.PayPalMail,
                UserName = userProfileViewModel.UserName,
                Description = userProfileViewModel.Description,
            };
        }

        public static Offer Map_AddServiceOfferModel_To_Offer(AddServiceOfferModel model, string authorID)
        {
            return new Offer
            {
                AuthorId = authorID,
                Description = model.ManageOffers.Description,
                Title = model.ManageOffers.Title,
                RatePerHour = model.ManageOffers.RatePerHour,
                Category = model.ManageOffers.Category,
                Subcategory = model.ManageOffers.Subcategory,
            };
        }

        public static Offer Map_ViewDetails_To_Offer(ViewDetails edited)
        {
            return new Offer
            {
                Id = edited.Id,
                Title = edited.Title,
                RatePerHour = edited.RatePerHour,
                Description = edited.Description,
                WorkingTimes = edited.Dates,
            };
        }

        public static BoughtOffers Map_Offer_To_BoughtOffers(Offer x)
        {
            return new BoughtOffers
            {
                Id = x.Id,
                Author = x.AuthorId,
                Description = x.Description,
                RatePerHour = x.RatePerHour,
                Title = x.Title,
                Subcategory = x.Subcategory,
                Category = x.Category,
                Date = x.WorkingTime.Date,
                HourFrom = x.WorkingTime.HoursFrom,
                HourTo = x.WorkingTime.HoursTo
            };
        }

        public static ManageOffers Map_Offer_To_ManageOffers(Offer x)
        {
            return new ManageOffers
            {
                Id = x.Id,
                Author = x.AuthorId,
                Description = x.Description,
                RatePerHour = x.RatePerHour,
                Title = x.Title,
                Subcategory = x.Subcategory,
                Category = x.Category,
            };
        }

        public static WebJobPortal.Models.JobOffer Map_JobOffer_JPModel_To_WebJobPortal_JobOffer(JobPortal.Model.JobOffer x, DateTime date, UserProfileViewModel user)
        {
            return new WebJobPortal.Models.JobOffer
            {
                CurrentDate = (DateTime)date,
                TotalPrice = x.TotalPrice,
                Customer = new CustomerViewModel { AddressLine = user.AddressLine, CityName = user.CityName, FirstName = user.FirstName, Gender = user.Gender, LastName = user.LastName, PayPalMail = user.PayPalMail, Email = user.Email, PhoneNumber = user.PhoneNumber, Postcode = user.Postcode, Region = user.Region },
                Offer = new BoughtOffers
                {
                    Id = x.Offer.Id,
                    Author = x.Offer.AuthorId,
                    Description = x.Offer.Description,
                    RatePerHour = x.Offer.RatePerHour,
                    Title = x.Offer.Title,
                    Subcategory = x.Offer.Subcategory,
                    Category = x.Offer.Category,
                    Date = x.Offer.WorkingTime.Date,
                    HourFrom = x.Offer.WorkingTime.HoursFrom,
                    HourTo = x.Offer.WorkingTime.HoursTo
                }
            };
        }
    }
}