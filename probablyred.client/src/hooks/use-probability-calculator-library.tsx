import { useMutation, useQuery } from '@tanstack/react-query';

import { CalculationResult } from '../types/calculation-result.interface';
import { StrategyCalculator } from '../types/strategy-calculator.interface';
import { useState } from 'react';

export const useProbabilityCalculatorLibrary = () => {
  const [availableCalculators, setAvailableCalculators] = useState<StrategyCalculator[]>([]);
  const [calculationResult, setCalculationResult] = useState<CalculationResult>();

  const API_URL = `${process.env.REACT_APP_API_BASE_URL}/Probability`;

  const { isLoading: calculatorListLoading } = useQuery({
    queryKey: ['probabilityCalculators'],
    queryFn: async () => {
      return await fetch(API_URL);
    },
    refetchOnWindowFocus: false,
    onSuccess: async (response) => {
      if (response.ok) {
        const queryResult = await response.json();
        setAvailableCalculators(queryResult);
      }
    },
    onError: (error) => {
      console.error(error);
    },
  });

  const { mutate: requestCalculation, isLoading: calculationLoading } = useMutation({
    mutationFn: async (request: StrategyCalculator) => {
      return await fetch(API_URL, {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(request),
      });
    },
    onSuccess: async (response) => {
      if (response.ok) {
        const queryResult = await response.json();
        setCalculationResult(queryResult);
      }
    },
    onError: (error) => {
      console.error(error);
    },
  });

  return {
    availableCalculators,
    requestCalculation,
    calculationResult,
    calculatorListLoading,
    calculationLoading,
  };
};
