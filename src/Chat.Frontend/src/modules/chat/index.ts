import { AppModule } from '@/core/modules';
import ChatMessageView from '@/modules/chat/ChatMessageView.vue';

const module: AppModule = {
  plugins: {
    init: async () => {
      // ...
    },
    router: (routes) => {
      routes.push({
        path: '/czat/:id/wiadomosc',
        name: 'chatMessage',
        component: ChatMessageView,
      });
    },
  },
};

export default module;
