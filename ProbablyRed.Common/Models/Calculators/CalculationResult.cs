using System.ComponentModel.DataAnnotations;

namespace ProbablyRed.Common.Models.Calculators
{
    public class CalculatorValidationResult : ValidationResult
    {
        public CalculatorValidationResult() : base("")
        {

        }

        public CalculatorValidationResult(string message) : base(message)
        {

        }
    }

    [Serializable]
    public struct CalculationResult
    {
        public bool Success { get; set; }

        public string CalculationStrategy { get; set; }

        public IEnumerable<CalculatorValidationResult> ValidationErrors { get; set; }

        public decimal Result { get; set; }
    }
}
