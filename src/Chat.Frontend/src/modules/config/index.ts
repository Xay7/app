import { useConfigStore } from './store';
import { AppModule } from '@/core/modules';

const module: AppModule = {
  plugins: {
    init: async () => {
      const configStore = useConfigStore();
      await configStore.init();
    },
  },
};

export default module;
