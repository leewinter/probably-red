import { ChangeEvent } from 'react';

const CalculationHistory = (props: { calculationHistory: any; handleHistorySelect: any }) => {
  const { calculationHistory, handleHistorySelect } = props;

  if (!calculationHistory.length) return null;

  return (
    <div>
      <h4>History</h4>
      <select
        onChange={(event: ChangeEvent<HTMLSelectElement>) => {
          const value = event.target.value;
          handleHistorySelect(calculationHistory[value]);
        }}
      >
        {calculationHistory.map((hist: any, index: number) => {
          return (
            <option
              key={index}
              value={index}
              className={hist.calculationResult.success ? 'success' : 'error'}
            >
              {hist.calculationResult.calculationStrategy}
              {`(${hist.calculationRequest.InputProperties.map((p: any) => {
                return hist.calculationRequest[p.Name];
              })})`}
            </option>
          );
        })}
      </select>
    </div>
  );
};

export default CalculationHistory;
