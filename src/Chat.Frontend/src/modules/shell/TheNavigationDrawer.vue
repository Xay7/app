<template>
  <v-navigation-drawer
    v-model="shellStore.showNavbar"
    class="mt-0 navigation fill-height bg-primary"
    width="300"
    elevation="4"
    app
    disable-resize-watcher
    order="-1"
  >
    <v-container
      class="d-flex justify-space-between fill-height flex-column pa-0"
      width="100%"
    >
      <div width="100%">
        <v-row>
          <v-col align="center" justify="center" class="mt-6 mb-4">
            <v-img max-height="128px" max-width="128px" :src="Logo"></v-img>
          </v-col>
        </v-row>
        <v-row>
          <v-col align="center" justify="center">
            <h1 class="font-weight-regular mb-1 text-wrap px-4">
              {{ authStore.user.name }}
            </h1>
            <v-btn
              width="104px"
              height="35px"
              class="mt-3 elevation-0 font-weight-medium"
              color="red"
              @click="logout"
              >Wyloguj</v-btn
            >
          </v-col>
        </v-row>
        <v-row class="pt-6">
          <v-col>
            <v-list class="d-flex justify-center flex-column">
              <v-list-item
                v-for="link in shellStore.navigationLinks"
                :key="link.id"
                class="d-flex justify-center"
              >
                <v-list-item-title class="font-weight-regular">
                  <router-link :to="link.route" class="router-link">{{
                    link.title
                  }}</router-link>
                </v-list-item-title>
              </v-list-item>
            </v-list>
          </v-col>
        </v-row>
      </div>

      <v-container width="100%">
        <v-row width="100%" height="48px" justify="center" class="pb-8">
          <v-img
            max-height="48"
            max-width="128"
            :src="FooterLogo"
            transition="none"
          ></v-img>
        </v-row>
        <v-row class="px-6 py-3 nav-footer" height="48px">
          <p class="font-weight-light nav-text">Chat App</p>
          <v-spacer />
          <p class="font-weight-light nav-text">v0.0.1</p>
        </v-row>
      </v-container>
    </v-container>
  </v-navigation-drawer>
</template>

<script setup lang="ts">
import { mdiBackburger } from "@mdi/js";
import Logo from "@/assets/logo.png";
import FooterLogo from "@/assets/footerLogo.png";
import { useShellStore } from "@/modules/shell/store";
import { useAuthStore } from "@/modules/auth/store";
import { useRouter } from "vue-router";
import { useChatStore } from "@/modules/chat/store";
const router = useRouter();
const shellStore = useShellStore();
const authStore = useAuthStore();
const chatStore = useChatStore();

async function logout() {
  try {
    await authStore.logout();
    shellStore.showNavbar = false;
  } catch (error) {
    console.log(error);
  }
}
</script>

<style>
h1 {
  word-break: break-all;
}
.navigation {
  z-index: 999999 !important;
}
</style>
