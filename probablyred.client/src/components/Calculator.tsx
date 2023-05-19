import { ChangeEvent, Fragment } from 'react';
import { InputProperty, StrategyCalculator } from '../types/strategy-calculator.interface';

const Calculator = (props: {
  chosenCalculator: StrategyCalculator | undefined;
  calculationRequest: StrategyCalculator | any; // overload with any due to dynamic input property indexing. TODO: use a dictionary next time
  handleParameterChange: (parameterName: string, event: ChangeEvent<HTMLInputElement>) => void;
  calculationLoading: boolean;
  handleCalculate: () => void;
}) => {
  const {
    chosenCalculator,
    calculationRequest,
    handleParameterChange,
    calculationLoading,
    handleCalculate,
  } = props;
  return (
    <div>
      {chosenCalculator ? (
        <div className="container">
          {chosenCalculator.InputProperties.map((input: InputProperty) => {
            return (
              <Fragment key={input.Name}>
                <label>
                  {input.Name}{' '}
                  <input
                    type="number"
                    value={calculationRequest[input.Name]}
                    onChange={(event) => handleParameterChange(input.Name, event)}
                  />
                </label>{' '}
                <br />
              </Fragment>
            );
          })}
          <button disabled={!calculationRequest || calculationLoading} onClick={handleCalculate}>
            Calculate
          </button>
        </div>
      ) : null}
    </div>
  );
};

export default Calculator;
