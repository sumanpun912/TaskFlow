import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { HealthPage } from "./features/health/HealthPage";

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <HealthPage />
    </QueryClientProvider>
  );
}

export default App;