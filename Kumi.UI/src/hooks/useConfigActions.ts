import { useQuery } from '@tanstack/react-query';
import agent from '../lib/api/Agent';
import type { Config } from 'tailwindcss';

export function useConfig(search: string) {
  return useQuery({
    queryKey: ['config', search],
    queryFn: async () => {
      const response = await agent.get<Config>(`/config?search=${search}`);
      return response.data;
    },
  });
}
