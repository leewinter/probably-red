using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProbablyRed.Common.Models.Calculators;

namespace ProbablyRed.Service.Core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProbabilityController : ControllerBase
    {
        [HttpPost(Name = "ProcessCalculation")]
        public CalculationResult Post([FromBody] dynamic body)
        {
            IStrategyCalculator calculator = CalculationDeserializer.Deserialize(body);
            CalculationResult result = calculator.Calculate();
            return result;
        }

        [HttpGet(Name = "ListCalculations")]
        public string Get()
        {
            return JsonConvert.SerializeObject(CalculationLibrary.GetAvailableCalculators());
        }
    }
}
