using JobPortal.Model;
using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;


namespace JobPortal.Model
{
    [DataContract]
    [KnownType(typeof(Category))]
    [KnownType(typeof(SubCategory))]
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
        public virtual User Author{ get; set; }
        [DataMember]
        public virtual Category Category { get; set; }
        [DataMember]
        public virtual SubCategory Subcategory { get; set; }
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
        /*[EnumMember]
        Art,*/
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
