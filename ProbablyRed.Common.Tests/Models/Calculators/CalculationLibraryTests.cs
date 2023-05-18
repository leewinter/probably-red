using ProbablyRed.Common.Models.Calculators;

namespace ProbablyRed.Common.Tests.Models.Calculators
{
    [TestClass]
    public class CalculationLibraryTests
    {
        [TestMethod]
        public void GetAvailableCalculators_Get_ShouldReturnAsExpected()
        {
            var availableCalculators = CalculationLibrary.GetAvailableCalculators();

            Assert.IsNotNull(availableCalculators);
            Assert.IsTrue(availableCalculators.Any());
        }
    }
}
