using AcademiaFS.HomeJourney.WebAPI._Features.Auth;
using HomeJourne.UnitTest.DomainServiceAuthTest.DataTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomeJourne.UnitTest.DomainServiceAuthTest
{
    public class DomarinServiceAuthTest
    {
        private readonly DomainServiceAuth _service;
        public DomarinServiceAuthTest()
        {
            _service = new DomainServiceAuth();
        }

        [Theory]
        [ClassData(typeof(DomainServiceAuthData))]               
        public void GivenValidatePassword_WhenInputProvided_ThenReturnExpectedResult(string inputPassword, string passwordForHash, bool expectedResult)
        {
            // Arrange
            byte[] storedHash;

            using (var sha256 = SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(passwordForHash);
                storedHash = sha256.ComputeHash(inputBytes);
            }

            // Act
            bool result = _service.ValidatePassword(inputPassword, storedHash);

            // Assert
            Assert.Equal(expectedResult, result);
        }

    }
}
