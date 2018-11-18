﻿using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebJobPortal.Models;

namespace MyWeb.Mapping
{
    public static class UserMapping
    {

        public static User MapSetPropertiesViewModelToUser(SetPropertiesViewModel model)
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

        public static UserProfileViewModel Map_User_To_UserProfileViewModel(User user)
        {
            return new UserProfileViewModel

            {
                ID = user.ID.ToString(),
                CityName = user.CityName,
                Email = user.Email,
                FirstName = user.FirstName,
                Gender = user.Gender,
                LastName = user.LastName,
               // Password = user.Password,
                PayPalMail = user.PayPalMail,
                PhoneNumber = user.PhoneNumber,
                Postcode = user.Postcode,
                Region = user.Region,
                AddressLine = user.AddressLine,
                UserName = user.UserName,
            };
        }


        public static User Map_UserProfileViewModel_To_User(UserProfileViewModel userProfileViewModel)
        {
            return new User
            {
                ID = Int32.Parse(userProfileViewModel.ID),
                AddressLine = userProfileViewModel.AddressLine,
                CityName = userProfileViewModel.CityName,
                Email = userProfileViewModel.Email,
                FirstName = userProfileViewModel.FirstName,
                Gender = userProfileViewModel.Gender,
                LastName = userProfileViewModel.LastName,
                PayPalMail = userProfileViewModel.PayPalMail,
                PhoneNumber = userProfileViewModel.PhoneNumber,
                Postcode = userProfileViewModel.Postcode,
                Region = userProfileViewModel.Region,
                UserName = userProfileViewModel.UserName,
            };
        }
    }
}