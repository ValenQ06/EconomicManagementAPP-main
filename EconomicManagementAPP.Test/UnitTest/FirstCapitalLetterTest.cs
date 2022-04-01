using EconomicManagementAPP.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Test
{
    [TestClass]
    public class FirstCapitalLetterTest
    {
        [TestMethod]
        public void FirstLetterLower_ReturnError() //la primera letra en miniscula debe de retornar un error
        {
            var firstCapitalLetter = new FirstCapitalLetter();
            var data = "tarjeta";

            var context = new ValidationContext(new { Name = data });

            var testResult = firstCapitalLetter.GetValidationResult(data, context);

            Assert.AreEqual("The first letter must be in uppercase", testResult?.ErrorMessage);
        }

        [TestMethod]
        public void nullData_NoErrorMessage() //valimos que si el campo es nulo no tiene que hacer nada
        {
            var firstCapitalLetter = new FirstCapitalLetter();
            string data = null;

            var context = new ValidationContext(new { Name = data });

            var testResult = firstCapitalLetter.GetValidationResult(data, context);

            Assert.IsNull(testResult);
        }
    }
}