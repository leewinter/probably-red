import { StrategyCalculator } from './strategy-calculator.interface';

export interface ValidationResult {
  errorMessage: string;
}

export interface CalculationResult {
  success: boolean;
  calculationStrategy: string;
  result: number;
  validationErrors: ValidationResult[];
}

export interface CalculationHistory {
  calculationRequest: StrategyCalculator;
  calculationResult: CalculationResult;
}
