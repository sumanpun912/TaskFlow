import { useQuery } from "@tanstack/react-query";
import { apiClient } from "../../shared/api/apiClient";

type HealthResponse = {
  data: {
    status: string;
    service: string;
    architecture: string;
    timestamp: string;
  };
  errors: string[];
  success: boolean;
};

export function HealthPage() {
  const { data, isLoading, isError } = useQuery({
    queryKey: ["health"],
    queryFn: async () => {
      const response = await apiClient.get<HealthResponse>("/api/health");
      return response.data;
    },
  });

  if (isLoading) {
    return <p>Checking API health...</p>;
  }

  if (isError) {
    return <p>API is not reachable.</p>;
  }

  return (
    <main>
      <h1>TaskFlow</h1>
      <p>Production-grade multi-tenant project management SaaS.</p>

      <section>
        <h2>API Status</h2>
        <p>Status: {data?.data.status}</p>
        <p>Service: {data?.data.service}</p>
        <p>Architecture: {data?.data.architecture}</p>
        <p>Time: {data?.data.timestamp}</p>
      </section>
    </main>
  );
}