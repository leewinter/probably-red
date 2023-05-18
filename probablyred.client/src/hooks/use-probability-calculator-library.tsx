import { useMutation, useQuery } from '@tanstack/react-query';

import { useState } from 'react';

export const useProbabilityCalculatorLibrary = () => {
  const [availableCalculators, setAvailableCalculators] = useState<any[]>([]);
  const [calculationResult, setCalculationResult] = useState<any>();

  const { isLoading: calculatorListLoading } = useQuery({
    queryKey: ['probabilityCalculators'],
    queryFn: async () => {
      return await fetch('http://localhost:5199/Probability');
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
    mutationFn: async (request: any) => {
      return await fetch('http://localhost:5199/Probability', {
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
