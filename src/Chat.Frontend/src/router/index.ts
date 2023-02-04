import { createRouter, createWebHistory, RouteRecordRaw, Router } from 'vue-router';
import { AppModule } from '@/core/modules';

export default (modules: AppModule[]): Router => {
  const routes: RouteRecordRaw[] = [];

  modules.forEach((m) => {
    if (m.plugins?.router) {
      m.plugins.router(routes);
    }
  });

  return createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
  });
};
