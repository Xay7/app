import { defineStore } from 'pinia';

export type MenuLink = {
  id: string;
  title: string;
  icon?: string;
  type?: 'link';
  route?: {
    name: string;
  };
};

export type ShellState = {
  navigationLinks: MenuLink[];
  message: {
    visible: boolean;
    text: string;
    severity: string;
  };
  showNavbar: boolean;
};

export type ShellMessage = {
  visible: boolean;
  text: string;
  severity: MessageSeverity;
  timeoutId?: number;
};

type MessageSeverity = 'info' | 'warning' | 'error' | 'success';

export const useShellStore = defineStore('shell', {
  state: (): ShellState => ({
    navigationLinks: [],
    message: {
      visible: false,
      text: '',
      severity: 'info',
    },
    showNavbar: false,
  }),
  actions: {
    addNavigationLink(navLink: MenuLink) {
      this.navigationLinks.push(navLink);
    },
    showMessage(text: string, severity: MessageSeverity = 'info') {
      this.message.visible = true;
      this.message.text = text;
      this.message.severity = severity;
    },

    hideMessage() {
      this.message.visible = false;
    },
  },
});
