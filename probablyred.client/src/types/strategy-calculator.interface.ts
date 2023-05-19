export interface InputProperty {
  Name: string;
  DataType: string;
}

export interface StrategyCalculator {
  CalculationName: string;
  CalculationDescription: string;
  CalculationType: string;
  InputProperties: InputProperty[];
}
