import { ChangeEvent } from 'react';
import { StrategyCalculator } from '../types/strategy-calculator.interface';

const CalculatorSelect = (props: {
  selectedCalculator: string;
  handleCalculatorChange: (event: ChangeEvent<HTMLSelectElement>) => void;
  calculationLoading: boolean;
  availableCalculators: StrategyCalculator[];
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
        {availableCalculators.map((calc: StrategyCalculator) => {
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
