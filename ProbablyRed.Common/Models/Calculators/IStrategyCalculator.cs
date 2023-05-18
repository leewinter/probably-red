namespace ProbablyRed.Common.Models.Calculators
{
    public interface IStrategyCalculator
    {
        CalculationResult Calculate();
        string CalculationName { get; }
        string CalculationDescription { get; }
        IEnumerable<CalculatorValidationResult> Validate();
        IEnumerable<CalculatorInput> InputProperties { get; }
    }
}
