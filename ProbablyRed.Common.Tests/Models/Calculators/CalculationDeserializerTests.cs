using Newtonsoft.Json;
using ProbablyRed.Common.Models.Calculators;
using ProbablyRed.Common.Models.Calculators.Calculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbablyRed.Common.Tests.Models.Calculators
{
    [TestClass]
    public class CalculationDeserializerTests
    {
        [TestMethod]
        public void Deserialize_NormalInput_ShouldSucceed()
        {
            var expectedCalculator = new CombinedProbability() { ProbabilityA = 0.25m, ProbabilityB = 0.25m };
            var expectedBody = JsonConvert.SerializeObject(expectedCalculator);
            dynamic? payload = JsonConvert.DeserializeObject<dynamic>(expectedBody);

            var result = CalculationDeserializer.Deserialize(payload) as CombinedProbability ?? throw new ArgumentNullException("Deserialize of CombinedProbability should not be null");

            Assert.AreEqual(expectedCalculator.ProbabilityA, result.ProbabilityA);
            Assert.AreEqual(expectedCalculator.ProbabilityB, result.ProbabilityB);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Unable to identify StrategyCalculator CalculationType during deserialization. Ensure a valid payload was provided")]
        public void Deserialize_UnexpectedInput_ShouldThrowArgumentNullException()
        {
            var unexpectedInput = new CalculationResult() { };
            var expectedBody = JsonConvert.SerializeObject(unexpectedInput);
            dynamic? payload = JsonConvert.DeserializeObject<dynamic>(expectedBody);

            CalculationDeserializer.Deserialize(payload);
        }
    }
}
