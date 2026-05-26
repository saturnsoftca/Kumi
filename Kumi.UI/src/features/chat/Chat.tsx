import { useState } from 'react';
import ChatMessageBox from './ChatMessageBox';
import ChatInput from './ChatInput';
import { useSendPrompt } from '../../hooks/useChatActions';
import type { ChatMessage } from '../../lib/types/ChatMessage';
import { useConfig } from '../../hooks/useConfigActions';
import SettingsModal from '../settings/SettingsModal';

export default function Chat() {
  const sendPrompt = useSendPrompt();
  const [isSettingsOpen, setIsSettingsOpen] = useState<boolean>(false);
  const [messages, setMessages] = useState<ChatMessage[]>([]);

  async function sendMessage(msg: string) {
    const prompt: ChatMessage = {
      type: 'PROMPT',
      content: msg,
    };
    setMessages([...messages, prompt]);
    await sendPrompt.mutateAsync(prompt, {
      onSuccess: (chatMessages) => {
        setMessages([...messages, ...chatMessages]);
      },
      onError: () => setIsSettingsOpen(true),
    });

    //setMessages([...messages, msg])
  }

  return (
    <>
      {isSettingsOpen && (
        <SettingsModal closeModal={() => setIsSettingsOpen(false)} />
      )}
      <div className="h-screen w-full flex items-center justify-center">
        <div className="w-2/3 h-11/12">
          <div
            className={
              'w-full h-full ' +
              (messages.length > 0 ? '' : 'flex items-center')
            }
          >
            <ChatMessageBox
              chatMessages={messages}
              pending={sendPrompt.isPending}
            />
            <div
              className={
                'w-full h-1/2 flex justify-center ' +
                (messages.length > 0 ? 'items-end' : 'items-center')
              }
            >
              <ChatInput sendMessage={sendMessage} />
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
