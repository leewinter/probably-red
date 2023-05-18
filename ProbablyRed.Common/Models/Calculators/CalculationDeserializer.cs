using Newtonsoft.Json;
using System.Reflection;

namespace ProbablyRed.Common.Models.Calculators
{
    public class CalculationDeserializer
    {
        public static IStrategyCalculator Deserialize(dynamic calc)
        {
            try
            {
                dynamic data = JsonConvert.DeserializeObject<dynamic>(calc.ToString());
                var calculationType = data.CalculationType.ToString();
                Assembly assem = typeof(IStrategyCalculator).Assembly;
                var type = assem.GetType(calculationType);
                IStrategyCalculator calculator = JsonConvert.DeserializeObject(calc.ToString(), type);
                return calculator;
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Cannot perform runtime binding on a null reference")) {
                    throw new ArgumentNullException("Unable to identify StrategyCalculator CalculationType during deserialization. Ensure a valid payload was provided", ex);
                }
                
                throw;
            }            
        }
    }
}
