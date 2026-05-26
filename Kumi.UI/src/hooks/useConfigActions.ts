import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import agent from '../lib/api/Agent';
import type { Config } from '../lib/types/Config';

export function useConfig(search: string) {
  return useQuery({
    queryKey: ['config'],
    queryFn: async () => {
      const response = await agent.get<Config>(`/config?search=${search}`);
      return response.data;
    },
  });
}

export function useUpdateConfig() {
  const client = useQueryClient();
  return useMutation({
    mutationFn: async (config: Config) => {
      const response = await agent.post('/config', config);
      return response.data;
    },
    onSuccess: async () => {
      await client.invalidateQueries({
        queryKey: ['config'],
      });
    },
  });
}
