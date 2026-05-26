import { useForm } from 'react-hook-form';
import Input from '../../components/Input';
import Modal from '../../components/Modal';
import { useConfig } from '../../hooks/useConfigActions';
import type { Config } from '../../lib/types/Config';

interface Props {
  closeModal: () => void;
}

export default function SettingsModal({ closeModal }: Props) {
  const { data: config } = useConfig('SYSTEM');
  const { register } = useForm<Config>();

  return (
    <Modal
      title="Settings"
      onSubmit={() => console.log('Done')}
      onCancel={closeModal}
    >
      <Input
        label="Provider"
        register={register}
        field="for"
        placeholder="Ollama"
        value={config?.type}
      />
      <Input
        label="Model"
        register={register}
        field="model"
        placeholder="gemma4:26b"
        value={config?.model}
      />
      <Input label="Api Key" register={register} field="apiKey" />
    </Modal>
  );
}
