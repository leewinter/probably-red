using ProbablyRed.Common.Models.Calculators;
using ProbablyRed.Common.Models.Calculators.Calculations;

namespace ProbablyRed.Common.Tests.Models.Calculators
{
    [TestClass]
    public class StrategyCalculatorTests
    {
        [TestMethod]
        [DataRow("0.0", "0.0", "0.0")]
        [DataRow("0.5", "0.5", "0.25")]
        [DataRow("0.5", "0.75", "0.375")]
        [DataRow("0.9999999991", "0.9999999991", "0.99999999820000000081")]
        public void Calculate_NormalParameters_SuccessfulResult(string paramA, string paramB, string calcResult)
        {
            var parameterA = decimal.Parse(paramA);
            var parameterB = decimal.Parse(paramB);
            var expectedResult = decimal.Parse(calcResult);
            var knownStrategy = "CombinedProbability";

            IStrategyCalculator sut = new CombinedProbability() { ProbabilityA = parameterA, ProbabilityB = parameterB };

            var result = sut.Calculate();

            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Result, expectedResult);
            Assert.IsFalse(result.ValidationErrors.Any());
            Assert.AreEqual(result.CalculationStrategy, knownStrategy);
        }

        [TestMethod]
        [DataRow("-0.1", "0.0", "0.0", 1)]
        [DataRow("0.0", "1.5", "0.0", 1)]
        [DataRow("-0.5", "1.75", "0.0", 2)]
        public void Calculate_InvalidParameters_UnsuccessfulResult(string paramA, string paramB, string calcResult, int errorCount)
        {
            var parameterA = decimal.Parse(paramA);
            var parameterB = decimal.Parse(paramB);
            var expectedResult = decimal.Parse(calcResult);
            var knownStrategy = "CombinedProbability";

            IStrategyCalculator sut = new CombinedProbability() { ProbabilityA = parameterA, ProbabilityB = parameterB };

            var result = sut.Calculate();

            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Result, expectedResult);
            Assert.AreEqual(result.ValidationErrors.Count(), errorCount);
            Assert.AreEqual(result.CalculationStrategy, knownStrategy);
        }

        [TestMethod]
        public void Calculate_InvalidParameterA_ExpectedValidation()
        {
            IStrategyCalculator sut = new CombinedProbability() { ProbabilityA = -0.5m, ProbabilityB = 0.0m };

            var result = sut.Calculate();

            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Result, 0.0m);
            Assert.AreEqual(result.ValidationErrors.Count(), 1);
            Assert.AreEqual(result.ValidationErrors.FirstOrDefault().ErrorMessage, "ProbabilityA must be within 0 and 1");
        }

        [TestMethod]
        public void Calculate_InvalidParameterB_ExpectedValidation()
        {
            IStrategyCalculator sut = new CombinedProbability() { ProbabilityA = 0.5m, ProbabilityB = -0.5m };

            var result = sut.Calculate();

            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Result, 0.0m);
            Assert.AreEqual(result.ValidationErrors.Count(), 1);
            Assert.AreEqual(result.ValidationErrors.FirstOrDefault().ErrorMessage, "ProbabilityB must be within 0 and 1");
        }

        /// <summary>
        /// Currently including validation as part of calculate methods, however it could be left out or called independently
        /// </summary>
        [TestMethod]
        [DataRow("-0.1", "0.0", 1)]
        [DataRow("0.0", "1.5", 1)]
        [DataRow("-0.5", "1.75", 2)]
        public void Validate_InvalidParameters_ExpectedValidation(string paramA, string paramB, int errorCount)
        {
            var parameterA = decimal.Parse(paramA);
            var parameterB = decimal.Parse(paramB);

            IStrategyCalculator sut = new CombinedProbability() { ProbabilityA = parameterA, ProbabilityB = parameterB };

            var result = sut.Validate();

            Assert.IsTrue(result.Any());
            Assert.AreEqual(result.Count(), errorCount);
        }
    }
}
