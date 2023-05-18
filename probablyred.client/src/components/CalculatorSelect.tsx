const CalculatorSelect = (props: {
  selectedCalculator: any;
  handleCalculatorChange: any;
  calculationLoading: any;
  availableCalculators: any;
}) => {
  const { selectedCalculator, handleCalculatorChange, calculationLoading, availableCalculators } =
    props;
  return (
    <div>
      <select
        value={selectedCalculator}
        onChange={handleCalculatorChange}
        disabled={calculationLoading}
      >
        <option value=""> - Choose a Calculator - </option>
        {availableCalculators.map((calc: any) => {
          return (
            <option key={calc.CalculationType} value={calc.CalculationName}>
              {calc.CalculationName}
            </option>
          );
        })}
      </select>
    </div>
  );
};

export default CalculatorSelect;
