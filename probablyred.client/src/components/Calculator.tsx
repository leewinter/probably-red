import { Fragment } from 'react';

const Calculator = (props: {
  chosenCalculator: any;
  calculationRequest: any;
  handleParameterChange: any;
  calculationLoading: any;
  handleCalculate: any;
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
        <div>
          {chosenCalculator.InputProperties.map((input: any) => {
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
