using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;


namespace JobPortal.Model
{
    [DataContract]
    [KnownType(typeof(Category))]
    [KnownType(typeof(SubCategory))]
    [KnownType(typeof(WorkingDetails))]
    public class Offer
    {
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual decimal RatePerHour { get; set; }
        [DataMember]
        public virtual string Title { get; set; }
        [DataMember]
        public virtual string Description { get; set; }
        [DataMember]
        public virtual string AuthorId{ get; set; }
        [DataMember]
        public virtual Category Category { get; set; }
        [DataMember]
        public virtual SubCategory Subcategory { get; set; }
        [DataMember]
        public virtual WorkingDetails WorkingTime { get; set; }
        [DataMember]
        public virtual IEnumerable<WorkingTime> WorkingTimes { get; set; }
    }
    [DataContract]
    public class PayPalOffer
    {
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual decimal RatePerHour { get; set; }
        [DataMember]
        public virtual string Title { get; set; }
        [DataMember]
        public virtual TimeSpan HoursFrom { get; set; }
        [DataMember]
        public virtual TimeSpan HoursTo { get; set; }
    }
    [DataContract]
    public class WorkingDetails
    {
        [DataMember]
        public virtual TimeSpan HoursFrom { get; set; }
        [DataMember]
        public virtual TimeSpan HoursTo { get; set; }
        [DataMember]
        public virtual DateTime Date{ get; set; }
        [DataMember]
        public virtual DayOfWeek WeekDay { get; set; }
    }

    /*public class OrderedOffer
    {
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual decimal RatePerHour { get; set; }
        [DataMember]
        public virtual string Title { get; set; }
        [DataMember]
        public virtual string Description { get; set; }
        [DataMember]
        public virtual string AuthorId { get; set; }
        [DataMember]
        public virtual Category Category { get; set; }
        [DataMember]
        public virtual SubCategory Subcategory { get; set; }
        [DataMember]
        public virtual TimeSpan HoursFrom { get; set; }
        [DataMember]
        public virtual TimeSpan HoursTo { get; set; }
        [DataMember]
        public virtual DayOfWeek WeekDay { get; set; }
        [DataMember]
        public virtual DateTime Date { get; set; }
    }*/
    [DataContract]
    public class WorkingTime
    {
        [DataMember]
        public virtual TimeSpan Start { get; set; }
        [DataMember]
        public virtual TimeSpan End { get; set; }
        [DataMember]
        public virtual DayOfWeek WeekDay { get; set; }
        [DataMember]
        public virtual int OfferId { get; set; }
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual string Text { get; set; }
    }
    [DataContract]
    public class WorkingDate
    {
        [DataMember]
        public virtual DateTime Start { get; set; }
        [DataMember]
        public virtual DateTime End { get; set; }
        [DataMember]
        public virtual DayOfWeek WeekDay { get; set; }
        [DataMember]
        public virtual int OfferId { get; set; }
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual string Text { get; set; }
    }


    [DataContract(Name = "Category")]
    public enum Category
    {
        [EnumMember]
        Home,
        [EnumMember]
        Tutoring,
        [EnumMember]
        IT,
        [EnumMember]
        Repairs,
        [EnumMember]
        Architecture,
        [EnumMember]
        Media

    }
    [DataContract(Name = "SubCategory")]
    public enum SubCategory
    {
        [EnumMember]
        [SubcategoryOf(Category.Home)]
        Cleaning,
        [EnumMember]
        [SubcategoryOf(Category.Home)]
        Gardening,
        [EnumMember]
        [SubcategoryOf(Category.Home)]
        Babysitting,

        [EnumMember]
        [SubcategoryOf(Category.Tutoring)]
        Languages,
        [EnumMember]
        [SubcategoryOf(Category.Tutoring)]
        Science,

        [EnumMember]
        [SubcategoryOf(Category.IT)]
        WebPrgramming,
        [EnumMember]
        [SubcategoryOf(Category.IT)]
        AppPrgramming,
        [EnumMember]
        [SubcategoryOf(Category.IT)]
        Design,

        [EnumMember]
        [SubcategoryOf(Category.Repairs)]
        Cars,
        [EnumMember]
        [SubcategoryOf(Category.Repairs)]
        Bikes,
        [EnumMember]
        [SubcategoryOf(Category.Repairs)]
        HouseholdGoods,
        [EnumMember]
        [SubcategoryOf(Category.Repairs)]
        Electronics,

        [EnumMember]
        [SubcategoryOf(Category.Architecture)]
        Buildings,
        [EnumMember]
        [SubcategoryOf(Category.Architecture)]
        InteriorDesign,
        [EnumMember]
        [SubcategoryOf(Category.Architecture)]
        Landscape,

        [EnumMember]
        [SubcategoryOf(Category.Media)]
        Video,
        [EnumMember]
        [SubcategoryOf(Category.Media)]
        Audio,
        [EnumMember]
        [SubcategoryOf(Category.Media)]
        Marketing,

    }
}
