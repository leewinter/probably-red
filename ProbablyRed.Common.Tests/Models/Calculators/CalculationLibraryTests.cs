using ProbablyRed.Common.Models.Calculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
