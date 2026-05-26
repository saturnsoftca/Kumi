import { useState } from 'react';
import SidebarItem from './SidebarItem';
import SettingsModal from './SettingsModal';

export default function Sidebar() {
  const [isSettingsOpen, setIsSettingsOpen] = useState<boolean>(false);

  return (
    <>
      {isSettingsOpen && (
        <SettingsModal closeModal={() => setIsSettingsOpen(false)} />
      )}
      <div>
        <aside className="fixed w-64 h-full top-0 z-40 bg-neutral-900">
          <div className="h-full px-3 py-4 overflow-y-auto">
            <ul className="space-y-2 font-medium">
              <SidebarItem name="Tools" to="/tools" />
              <SidebarItem name="Chat" to="/" />
              <li
                onClick={() => setIsSettingsOpen(true)}
                className="flex w-full text-neutral-400 items-center p-2 rounded-lg cursor-pointer hover:bg-neutral-800 hover:text-white"
              >
                Settings
              </li>
            </ul>
          </div>
        </aside>
      </div>
    </>
  );
}
