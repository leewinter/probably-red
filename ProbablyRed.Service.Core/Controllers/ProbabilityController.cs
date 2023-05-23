using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProbablyRed.Common.Models.Calculators;
using static System.Net.Mime.MediaTypeNames;

namespace ProbablyRed.Service.Core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProbabilityController : ControllerBase
    {
        private readonly ILogger<ProbabilityController> _logger;
        public ProbabilityController(ILogger<ProbabilityController> logger) =>
        _logger = logger;
        private void LogCalculation(CalculationResult result, dynamic calculator) {
            try
            {
                _logger.LogInformation(@"IStrategyCalculator.Calculate() called. Response: CalculationStrategy[{CalculationStrategy}] Properties[{Properties}] Result[{Result}]", result.CalculationStrategy, string.Join(" ", ((IEnumerable<CalculatorInput>)calculator.InputProperties).Select(n => string.Format("{0} - {1}", n.Name, calculator.GetType().GetProperty(n.Name).GetValue(calculator, null))).ToList()), result.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured attempting to construct the IStrategyCalculator.Calculate() log response", ex);
            }
        }

        [HttpPost(Name = "ProcessCalculation")]
        public CalculationResult Post([FromBody] dynamic body)
        {
            var calculator = CalculationDeserializer.Deserialize(body);
            CalculationResult result = calculator.Calculate();
            LogCalculation(result, calculator);
            return result;
        }

        [HttpGet(Name = "ListCalculations")]
        public string Get()
        {
            return JsonConvert.SerializeObject(CalculationLibrary.GetAvailableCalculators());
        }
    }
}
