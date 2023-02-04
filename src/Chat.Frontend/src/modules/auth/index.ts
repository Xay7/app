import { AppModule } from "@/core/modules";
import { RouteLocationNormalizedLoaded } from "vue-router";
import { AuthStore, useAuthStore } from "@/modules/auth/store";
import Keycloak from "keycloak-js";
const module: AppModule = {
  plugins: {
    init: async ({ router }) => {
      const authStore = useAuthStore();
      const keycloak = new Keycloak({
        url: "http://localhost:8080",
        realm: "chat",
        clientId: "chat-client",
      });

      const state = await keycloak.init({ onLoad: "login-required" });

      authStore.$onAction(({ name, after }) => {
        if (name === "logout") {
          after(async () => {
            await keycloak.logout();
          });
        }
      });

      if (state) {
        if (!keycloak.token) {
          window.location.reload();
        } else {
          await authStore.updateToken(keycloak.token);
        }
      }

      setInterval(() => {
        keycloak
          .updateToken(70)
          .then((refreshed) => {
            if (refreshed && keycloak.token) {
              authStore.updateToken(keycloak.token);
            }
          })
          .catch(() => {
            console.log("err");
          });
      });

      router.beforeEach(async (to, from, next) => {
        try {
          const auth = useAuthStore();
          if (await ensureRouteIsAuthorized(auth, to)) {
            next();
          } else {
            next({
              path: "/",
              force: true,
            });
          }
        } catch (error) {
          next({
            path: "/",
            force: true,
          });
        }
      });
    },
  },
};

async function ensureRouteIsAuthorized(
  store: AuthStore,
  target: RouteLocationNormalizedLoaded
): Promise<boolean> {
  let result = store.user.isAuthorized;
  if (result && target?.meta?.roles && target.meta.roles.length > 0) {
    result =
      target.meta.roles.find((r: string) => store.user.roles.includes(r)) !==
      undefined;
  }
  return result;
}

export default module;

declare module "vue-router" {
  interface RouteMeta {
    roles?: string[];
  }
}
