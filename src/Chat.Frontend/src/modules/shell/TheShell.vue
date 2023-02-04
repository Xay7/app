<template>
  <v-app>
    <TheAppBar />
    <TheNavigationDrawerVue />
    <v-main>
      <v-snackbar
        v-model="shellStore.message.visible"
        centered
        top
        :timeout="5000"
        :color="shellStore.message.severity"
      >
        <v-icon
          v-if="shellStore.message.severity === 'info'"
          :icon="mdiInformationOutline"
        />
        <v-icon
          v-if="shellStore.message.severity === 'success'"
          :icon="mdiCheckCircleOutline"
        />
        <v-icon
          v-if="shellStore.message.severity === 'warning'"
          :icon="mdiAlertCircleOutline"
        />
        <v-icon
          v-if="shellStore.message.severity === 'error'"
          :icon="mdiAlertCircleOutline"
        />
        {{ shellStore.message.text }}
      </v-snackbar>
      <router-view :key="$route.path" />
    </v-main>
    <TheFooter />
  </v-app>
</template>

<script setup lang="ts">
import TheAppBar from "./TheAppBar.vue";
import TheFooter from "./TheFooter.vue";
import TheNavigationDrawerVue from "@/modules/shell/TheNavigationDrawer.vue";
import { mdiInformationOutline } from "@mdi/js";
import { mdiCheckCircleOutline } from "@mdi/js";
import { mdiAlertCircleOutline } from "@mdi/js";
import { useShellStore } from "@/modules/shell/store";

const shellStore = useShellStore();
</script>
