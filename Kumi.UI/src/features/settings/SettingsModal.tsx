import { useForm } from 'react-hook-form';
import Input from '../../components/Input';
import Modal from '../../components/Modal';
import { useConfig, useUpdateConfig } from '../../hooks/useConfigActions';
import type { Config } from '../../lib/types/Config';

interface Props {
  closeModal: () => void;
}

export default function SettingsModal({ closeModal }: Props) {
  const updateConfig = useUpdateConfig();
  const { data: config } = useConfig('SYSTEM');
  const { register, handleSubmit } = useForm<Config>();

  async function onSubmit(config: Config) {
    await updateConfig.mutateAsync(config, {
      onSuccess: () => closeModal(),
    });
  }

  return (
    <Modal
      title="Settings"
      onSubmit={handleSubmit(onSubmit)}
      onCancel={closeModal}
    >
      <Input
        label="Provider"
        register={register}
        field="type"
        placeholder="Ollama"
        defaultValue={config?.type}
      />
      <Input
        label="Model"
        register={register}
        field="model"
        placeholder="gemma4:26b"
        defaultValue={config?.model}
      />
      <Input label="Api Key" register={register} field="apiKey" />
    </Modal>
  );
}
