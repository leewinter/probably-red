import { CalculationHistory, CalculationResult } from '../types/calculation-result.interface';
import { ChangeEvent, useEffect, useState } from 'react';

import CalculationHistoryDisplay from './CalculationHistoryDisplay';
import CalculationResultDisplay from './CalculationResultDisplay';
import Calculator from './Calculator';
import CalculatorSelect from './CalculatorSelect';
import ErrorOutput from './ErrorOutput';
import { StrategyCalculator } from '../types/strategy-calculator.interface';
import { useProbabilityCalculatorLibrary } from '../hooks/use-probability-calculator-library';

const ProbabilityCalculator = () => {
  const [chosenCalculator, setChosenCalculator] = useState<StrategyCalculator>();
  const [selectedCalculator, setSelectedCalculator] = useState<string>('');
  // Our request to mutate and send
  const [calculationRequest, setCalculationRequest] = useState<StrategyCalculator>();
  // Our history of calculations
  const [calculationResults, setCalculationResults] = useState<CalculationHistory[]>([]);
  // Holds last result or overriden by history
  const [resultToDisplay, setResultToDisplay] = useState<CalculationResult>();
  // Hold any api errors
  const [requestErrors, setRequestErrors] = useState<string[]>([]);

  const {
    availableCalculators,
    requestCalculation,
    calculationResult,
    calculationLoading,
    error: apiError,
  } = useProbabilityCalculatorLibrary();

  useEffect(() => {
    if (calculationResult && calculationRequest) {
      setCalculationResults([...calculationResults, { calculationRequest, calculationResult }]);
      setResultToDisplay(calculationResult);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [calculationResult]);

  useEffect(() => {
    if (apiError) setRequestErrors([...requestErrors, apiError]);
  }, [apiError]);

  const handleCalculatorChange = (event: ChangeEvent<HTMLSelectElement>) => {
    const value = event.target.value;
    setSelectedCalculator(value);
    const foundCalc = availableCalculators.find((calc) => calc.CalculationName === value);
    setChosenCalculator(foundCalc);
    setCalculationRequest(foundCalc);
  };

  const handleParameterChange = (parameterName: string, event: ChangeEvent<HTMLInputElement>) => {
    const value = event.target.value;

    if (calculationRequest)
      setCalculationRequest({
        ...calculationRequest,
        [parameterName]: value.length ? value : 0,
      });
  };

  const handleCalculate = () => {
    if (calculationRequest) requestCalculation(calculationRequest);
  };

  const handleHistorySelect = ({
    calculationRequest: calculationRequestHistoryItem,
    calculationResult: calculationResultHistoryItem,
  }: CalculationHistory) => {
    setSelectedCalculator(calculationRequestHistoryItem.CalculationName);
    const foundCalc = availableCalculators.find(
      (calc) => calc.CalculationName === calculationRequestHistoryItem.CalculationName,
    );
    setChosenCalculator(foundCalc);
    setCalculationRequest(calculationRequestHistoryItem);
    setResultToDisplay(calculationResultHistoryItem);
  };

  return (
    <div className="feature">
      <CalculatorSelect
        selectedCalculator={selectedCalculator}
        handleCalculatorChange={handleCalculatorChange}
        calculationLoading={calculationLoading}
        availableCalculators={availableCalculators}
      />
      <div className="content">
        <Calculator
          chosenCalculator={chosenCalculator}
          calculationRequest={calculationRequest}
          handleParameterChange={handleParameterChange}
          calculationLoading={calculationLoading}
          handleCalculate={handleCalculate}
        />
        <CalculationResultDisplay calculationResult={resultToDisplay} />
      </div>
      <CalculationHistoryDisplay
        calculationHistory={calculationResults}
        handleHistorySelect={handleHistorySelect}
      />
      <ErrorOutput errors={requestErrors} />
    </div>
  );
};

export default ProbabilityCalculator;
