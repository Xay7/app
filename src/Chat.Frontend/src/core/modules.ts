import { Router, RouteRecordRaw } from 'vue-router';

export interface AppPluginContext {
  router: Router;
  modules: AppModule[];
}

export interface AppModulePlugins {
  [key: string]: any;
  router?: (routes: Array<RouteRecordRaw>) => void;
  init?: (context: AppPluginContext) => Promise<void>;
}

export interface AppModule {
  plugins?: AppModulePlugins;
}
