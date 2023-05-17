using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using ProbablyRed.Common.Models.Calculators;
using ProbablyRed.Common.Models.Calculators.Calculations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProbablyRed.Service.Core.Tests.Controllers
{
    [TestClass]
    public class ProbabillityControllerTests
    {
        public HttpClient Client { get; }

        public ProbabillityControllerTests()
        {
            var factory = new WebApplicationFactory<Program>();
            Client = factory.CreateClient();
        }

        [TestMethod]
        public async Task ListCalculations_Get_ReturnArrayOfValidCalculations()
        {
            var response = await Client.GetAsync("Probability");
            response.EnsureSuccessStatusCode();

            var converter = new ExpandoObjectConverter();
            var jsonString = await response.Content.ReadAsStringAsync();
            dynamic dynamicJsonResult = JsonConvert.DeserializeObject<List<ExpandoObject>>(jsonString, converter);

            foreach (var cal in dynamicJsonResult)
            {
                var CalculationName = cal.CalculationName;
                Assert.IsNotNull(CalculationName, "CalculationName returned from ProbabilityController should not be null");

                var CalculationDescription = cal.CalculationDescription;
                Assert.IsNotNull(CalculationDescription, "CalculationDescription returned from ProbabilityController should not be null");

                var CalculationType = cal.CalculationType;
                Assert.IsNotNull(CalculationType, "CalculationType returned from ProbabilityController should not be null");

                var InputProperties = cal.InputProperties;
                Assert.IsTrue(InputProperties.Count > 0, "Calculation returned from ProbabilityController should have some InputProperties");
            }
        }

        [TestMethod]
        [DataRow("0.0","0.0", "0.0")]
        [DataRow("0.5", "0.5", "0.25")]
        [DataRow("0.5", "0.75", "0.375")]
        public async Task ProcessCalculation_KnownCalcValidParams_ReturnSuccessfulCalculationResult(string paramA, string paramB, string calcResult)
        {
            var parameterA = decimal.Parse(paramA);
            var parameterB = decimal.Parse(paramB);
            var expectedResult = decimal.Parse(calcResult);
            var knownStrategy = "CombinedProbability";

            var getCalculatorsResponse = await Client.GetAsync("Probability");
            getCalculatorsResponse.EnsureSuccessStatusCode();

            var converter = new ExpandoObjectConverter();
            var jsonString = await getCalculatorsResponse.Content.ReadAsStringAsync();
            var dynamicJsonResult = JsonConvert.DeserializeObject<List<ExpandoObject>>(jsonString, converter);

            var combinedProbability = dynamicJsonResult.Where(n=> (n as dynamic).CalculationName == knownStrategy).FirstOrDefault();
            var calculator = JsonConvert.DeserializeObject<CombinedProbability>(JsonConvert.SerializeObject(combinedProbability));

            calculator.ProbabilityA = parameterA;
            calculator.ProbabilityB = parameterB;

            var payload = JsonConvert.SerializeObject(calculator);
            var buffer = Encoding.UTF8.GetBytes(payload);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var postCalculatorResponse = await Client.PostAsync("Probability", byteContent);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, postCalculatorResponse.StatusCode);

            var postJsonString = await postCalculatorResponse.Content.ReadAsStringAsync();
            var serviceCalculationResult = JsonConvert.DeserializeObject<CalculationResult>(postJsonString);

            Assert.AreEqual(serviceCalculationResult.Result, expectedResult);
            Assert.IsTrue(serviceCalculationResult.Success);
            Assert.AreEqual(serviceCalculationResult.ValidationErrors.Count(),0);
            Assert.AreEqual(serviceCalculationResult.CalculationStrategy, knownStrategy);
        }

        [TestMethod]
        [DataRow("-0.1", "0.0", "0.0", 1)]
        [DataRow("0.0", "1.5", "0.0", 1)]
        [DataRow("-0.5", "1.75", "0.0", 2)]
        public async Task ProcessCalculation_KnownCalcInvalidParams_ReturnUnsuccessfulCalculationResult(string paramA, string paramB, string calcResult, int errorCount)
        {
            var parameterA = decimal.Parse(paramA);
            var parameterB = decimal.Parse(paramB);
            var expectedResult = decimal.Parse(calcResult);
            var knownStrategy = "CombinedProbability";

            var getCalculatorsResponse = await Client.GetAsync("Probability");
            getCalculatorsResponse.EnsureSuccessStatusCode();

            var converter = new ExpandoObjectConverter();
            var jsonString = await getCalculatorsResponse.Content.ReadAsStringAsync();
            var dynamicJsonResult = JsonConvert.DeserializeObject<List<ExpandoObject>>(jsonString, converter);

            var combinedProbability = dynamicJsonResult.Where(n => (n as dynamic).CalculationName == knownStrategy).FirstOrDefault();
            var calculator = JsonConvert.DeserializeObject<CombinedProbability>(JsonConvert.SerializeObject(combinedProbability));

            calculator.ProbabilityA = parameterA;
            calculator.ProbabilityB = parameterB;

            var payload = JsonConvert.SerializeObject(calculator);
            var buffer = Encoding.UTF8.GetBytes(payload);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var postCalculatorResponse = await Client.PostAsync("Probability", byteContent);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, postCalculatorResponse.StatusCode);

            var postJsonString = await postCalculatorResponse.Content.ReadAsStringAsync();
            var serviceCalculationResult = JsonConvert.DeserializeObject<CalculationResult>(postJsonString);

            Assert.AreEqual(serviceCalculationResult.Result, expectedResult);
            Assert.IsFalse(serviceCalculationResult.Success);
            Assert.AreEqual(serviceCalculationResult.ValidationErrors.Count(), errorCount);
            Assert.AreEqual(serviceCalculationResult.CalculationStrategy, knownStrategy);
        }
    }
}
