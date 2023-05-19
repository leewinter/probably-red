import { useMutation, useQuery } from '@tanstack/react-query';

import { CalculationResult } from '../types/calculation-result.interface';
import { StrategyCalculator } from '../types/strategy-calculator.interface';
import axios from 'axios';
import { useState } from 'react';

export const useProbabilityCalculatorLibrary = () => {
  const [availableCalculators, setAvailableCalculators] = useState<StrategyCalculator[]>([]);
  const [calculationResult, setCalculationResult] = useState<CalculationResult>();
  const [error, setError] = useState<string>();

  const API_URL = `${process.env.REACT_APP_API_BASE_URL}/Probability`;

  const { isLoading: calculatorListLoading } = useQuery({
    queryKey: ['probabilityCalculators'],
    queryFn: async () => {
      return await axios.get(API_URL);
    },
    refetchOnWindowFocus: false,
    onSuccess: async (response) => {
      if (response.request.statusText === 'OK') {
        setAvailableCalculators(response.data);
      }
    },
    onError: (error) => {
      console.error(error);
      setError(`${error}`);
    },
    retry: false,
  });

  const { mutate: requestCalculation, isLoading: calculationLoading } = useMutation({
    mutationFn: async (request: StrategyCalculator) => {
      return await axios.post(API_URL, request);
    },
    onSuccess: async (response) => {
      if (response.request.statusText === 'OK') {
        setCalculationResult(response.data);
      }
    },
    onError: (error) => {
      console.error(error);
      setError(`${error}`);
    },
    retry: false,
  });

  return {
    availableCalculators,
    requestCalculation,
    calculationResult,
    calculatorListLoading,
    calculationLoading,
    error,
  };
};
