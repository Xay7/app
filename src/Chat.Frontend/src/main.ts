import { createApp } from "vue";
import App from "./App.vue";
import vuetify from "./plugins/vuetify";
import { loadFonts } from "./plugins/webfontloader";
import { createPinia } from "pinia";
import { AppModule, AppPluginContext } from "@/core/modules";
import ShellModule from "./modules/shell/index";
import ChatModule from "@/modules/chat/index";
import ConfigModule from "@/modules/config/index";
import AuthModule from "@/modules/auth/index";
import createRouter from "./router/index";
import { useChatHub } from "./modules/chat/clients";

const pinia = createPinia();
const modules: AppModule[] = [
  ConfigModule,
  AuthModule,
  ShellModule,
  ChatModule,
];

const appContext: AppPluginContext = {
  router: createRouter(modules),
  modules,
};

loadFonts();

async function init() {
  const app = createApp(App);
  app.use(pinia);
  app.use(appContext.router);
  app.use(vuetify);
  app.provide("appContext", appContext);
  for (const m of appContext.modules) {
    await m?.plugins?.init?.(appContext);
  }
  app.mount("#app");
  useChatHub();
}

init();
