import { useShellStore } from '@/modules/shell/store';
import { AppModule } from '@/core/modules';
import SettingsView from '@/modules/settings/SettingsView.vue';
import ChatView from '@/modules/chat/ChatView.vue';

const module: AppModule = {
  plugins: {
    init: async () => {
      const shellStore = useShellStore();

      shellStore.addNavigationLink({
        id: 'czat',
        title: 'Czat',
        route: {
          name: 'czat',
        },
      });
      shellStore.addNavigationLink({
        id: 'ustawienia',
        title: 'Ustawienia',
        route: {
          name: 'ustawienia',
        },
      });
    },
    router: (routes) => {
      routes.push(
        {
          path: '/ustawienia',
          name: 'ustawienia',
          component: SettingsView,
        },
        {
          path: '/',
          name: 'czat',
          component: ChatView,
        }
      );
    },
  },
};

export default module;
