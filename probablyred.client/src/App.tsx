import './App.css';

import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

import ProbabilityCalculator from './components/ProbabilityCalculator';

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <div className="App">
        <header className="App-header">
          <h1>Probability Calculators</h1>
        </header>
        <ProbabilityCalculator />
      </div>
    </QueryClientProvider>
  );
}

export default App;
