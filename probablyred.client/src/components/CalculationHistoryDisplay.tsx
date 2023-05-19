import { CalculationHistory } from '../types/calculation-result.interface';
import { ChangeEvent } from 'react';
import { InputProperty } from '../types/strategy-calculator.interface';

const CalculationHistoryDisplay = (props: {
  calculationHistory: CalculationHistory[];
  handleHistorySelect: (hist: CalculationHistory) => void;
}) => {
  const { calculationHistory, handleHistorySelect } = props;

  if (!calculationHistory.length) return null;

  return (
    <div>
      <h4>History</h4>
      <select
        onChange={(event: ChangeEvent<HTMLSelectElement>) => {
          const value = parseInt(event.target.value);
          handleHistorySelect(calculationHistory[value]);
        }}
      >
        {calculationHistory.map((hist: CalculationHistory, index: number) => {
          return (
            <option
              key={index}
              value={index}
              className={hist.calculationResult.success ? 'success' : 'error'}
            >
              {hist.calculationResult.calculationStrategy}
              {`(${hist.calculationRequest.InputProperties.map((p: InputProperty) => {
                return (hist.calculationRequest as any)[p.Name]; // Once again dynamic overload caught me out
              })})`}
            </option>
          );
        })}
      </select>
    </div>
  );
};

export default CalculationHistoryDisplay;
