using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProbablyRed.Common.Models.Calculators.Calculations
{
    public class CombinedProbability : CalculatorBase, IStrategyCalculator
    {
        [ReadOnly(true)]
        public string CalculationName { get { return "CombinedProbability"; } }
        [ReadOnly(true)]
        public string CalculationDescription { get { return "Calculates the probability both params coalesce combined (e.g. 0.5 * 0.5 = 0.25)"; } }

        public CalculationResult Calculate()
        {
            var result = new CalculationResult() { CalculationStrategy = CalculationName };
            result.ValidationErrors = Validate();
            if (result.ValidationErrors.Count() < 1)
            {
                result.Result = ProbabilityA * ProbabilityB;
                result.Success = true;
            }
            else result.Success = false;
            return result;
        }

        public IEnumerable<CalculatorValidationResult> Validate()
        {
            var results = new List<CalculatorValidationResult>();
            Func<decimal, bool> invalidRange = param => param < 0.0M || param > 1.0M;
            if (invalidRange(ProbabilityA)) results.Add(new CalculatorValidationResult("ProbabilityA must be within 0 and 1"));
            if (invalidRange(ProbabilityB)) results.Add(new CalculatorValidationResult("ProbabilityB must be within 0 and 1"));
                        
            return results;
        }

        [DataInput]
        [Required]
        [DefaultValue(typeof(Decimal), "0.0")]
        [Range(0, 1, ErrorMessage = "Range should be between 0 and 1")]
        public decimal ProbabilityA { get; set; }
        [DataInput]
        [Required]
        [DefaultValue(typeof(Decimal), "0.0")]
        [Range(0, 1, ErrorMessage = "Range should be between 0 and 1")]
        public decimal ProbabilityB { get; set; }

    }
}
