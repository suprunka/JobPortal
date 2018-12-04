using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace UnitTestProject1.App_test
{
    [TestClass]
    public class UserTests
    {
        //read

       /*[TestMethod]
        public void Get_Will_Return_Valid_Object()
        {
            var userMock = new Mock<AppJobPortal.UserServiceReferenceTcp.User>();
            userMock.Object.ID = 6;
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.FindUser(It.IsAny<string>())).Returns(userMock.Object);

            var sut = new UserController(userServiceStub.Object);
            UserAppModel user = sut.Get(6);
            Assert.IsTrue(
                userMock.Object.ID == user.ID
            );
        }

/*

        [TestMethod]
        public void Get_Will_Return_Proper_Amount_Of_Users()
        {
            User[] u = { new User(), new User() };
            IQueryable<User> listOfUsers = u.AsQueryable<User>();

            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetAll()).Returns(() =>
            {
                return u;
            });

            var sut = new UserController(userServiceStub.Object);
            var result = sut.GetAll();
            Assert.AreEqual(result.Length, 2);
        }

        #endregion

        //update
        #region
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, true, DisplayName = "valid all data")] //valid all data
        [DataRow("123456789", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid phonenumber (too many characters)")] //invalid phonenumber (too many characters)
        [DataRow("12345ść", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid phonenumber (not allowed characters)")] //
        [DataRow("12345678", "Adaś", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid firstname (not allowed characters)")] //
        [DataRow("12345678", "Adam", "Adamżść", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid lastname (not allowed characters)")] //invalid lastname (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid email")] //invalid email
        [DataRow("12345678", "Adam", "Adam", "Adaśgmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid email without '@'")] //invalid email without '@'
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmailcom", "AdamMana", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid email without '.'")] //invalid email without '.'
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamManaż", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid userName (not allowed characters)")] //invalid userName (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "he", "Qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "too short userName")] //too short userName
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "qwerty1", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "//invalid password no capital letter")] //invalid password no capital letter
        [DataRow("12345678", "Adam", "Adam", "Adaś@gmail.com", "AdamMana", "Qwerty", "Streetline", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid password without number")] //invalid password without number
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetlineżć", "Cityname", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid addressline (not allowed characters)")] //invalid addressline (not allowed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Citynamę", "2154", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid city name (not allwed characters)")] //invalid city name (not allwed characters)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "215214", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid postcode (too long)")] //invalid postcode (too long)
        [DataRow("12345678", "Adam", "Adam", "Adam@gmail.com", "AdamMana", "Qwerty1", "Streetline", "Cityname", "śćęż", Region.Hovedstaden, Gender.Male, false, DisplayName = "invalid postcode (not allowed characters)")] //invalid postcode (not allowed characters)
        
        [TestMethod]
        public void Update_OfferService_Verify_If_Returns_Valid_Object(string phoneNumber, string firstName,
         string lastName, string email, string userName, string password, string addressLine,
         string cityName, string postCode, Region region, Gender gender, bool shouldValidate)
        {
            var serviceMock = new Mock<IUserService>();
            UserAppModel user = new UserAppModel
            {
                FirstName = firstName,
                PhoneNumber = phoneNumber,
                LastName = lastName,
                Email = email,
                UserName = userName,
                Password = password,
                AddressLine = addressLine,
                CityName = cityName,
                Postcode = postCode,
                Region = region,
                Gender = gender
            };
            var subject = new UserController(serviceMock.Object);
            bool result = subject.Edit(user);
            
            Assert.IsTrue(result== shouldValidate);
        }
        */
    }
  
}
