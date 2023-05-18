import { ChangeEvent, useEffect, useState } from 'react';

import CalculationHistory from './CalculationHistory';
import CalculationResult from './CalculationResult';
import Calculator from './Calculator';
import CalculatorSelect from './CalculatorSelect';
import { useProbabilityCalculatorLibrary } from '../hooks/use-probability-calculator-library';

const ProbabilityCalculator = () => {
  const [chosenCalculator, setChosenCalculator] = useState<any>();
  const [selectedCalculator, setSelectedCalculator] = useState<string>('');
  // Our request to mutate and send
  const [calculationRequest, setCalculationRequest] = useState<any>();
  // Our history of calculations
  const [calculationResults, setCalculationResults] = useState<any[]>([]);
  // Holds last result or overriden by history
  const [resultToDisplay, setResultToDisplay] = useState<any>();

  const { availableCalculators, requestCalculation, calculationResult, calculationLoading } =
    useProbabilityCalculatorLibrary();

  useEffect(() => {
    if (calculationResult) {
      setCalculationResults([...calculationResults, { calculationRequest, calculationResult }]);
      setResultToDisplay(calculationResult);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [calculationResult]);

  const handleCalculatorChange = (event: ChangeEvent<HTMLSelectElement>) => {
    const value = event.target.value;
    setSelectedCalculator(value);
    const foundCalc = availableCalculators.find((calc) => calc.CalculationName === value);
    setChosenCalculator(foundCalc);
    setCalculationRequest(foundCalc);
  };

  const handleParameterChange = (parameterName: string, event: ChangeEvent<HTMLInputElement>) => {
    const value = event.target.value;

    setCalculationRequest({
      ...calculationRequest,
      [parameterName]: value.length ? value : 0,
    });
  };

  const handleCalculate = () => {
    requestCalculation(calculationRequest);
  };

  const handleHistorySelect = ({
    calculationRequest: calculationRequestHistoryItem,
    calculationResult: calculationResultHistoryItem,
  }: any) => {
    setSelectedCalculator(calculationRequestHistoryItem.CalculationName);
    const foundCalc = availableCalculators.find(
      (calc) => calc.CalculationName === calculationRequestHistoryItem.CalculationName,
    );
    setChosenCalculator(foundCalc);
    setCalculationRequest(calculationRequestHistoryItem);
    setResultToDisplay(calculationResultHistoryItem);
  };

  return (
    <div className="container">
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
        <CalculationResult calculationResult={resultToDisplay} />
      </div>
      <CalculationHistory
        calculationHistory={calculationResults}
        handleHistorySelect={handleHistorySelect}
      />
    </div>
  );
};

export default ProbabilityCalculator;
