<template>
  <v-app-bar density="compact" elevation="1">
    <v-btn icon @click="router.go(-1)">
      <v-icon :icon="mdiArrowLeft"></v-icon>
    </v-btn>
    <v-app-bar-title>Ustawienia</v-app-bar-title>
    <v-spacer />
    <v-row justify="end" align="center" class="fill-height" no-gutters>
      <div
        class="fill-height icon-container d-flex flex-row justify-center align-center"
        @click="showGroupSettings = !showGroupSettings"
      >
        <v-icon
          :icon="mdiDotsVertical"
          size="24"
          :color="showGroupSettings ? 'green' : 'white'"
        />
        <v-menu
          activator="parent"
          anchor="bottom end"
          origin="auto"
          class="white"
          @click:outside="showGroupSettings = false"
        >
          <v-container
            class="py-3 pr-12 options-container pl-4 d-flex flex-row justify-center align-center"
            style="height: 64px"
          >
            <p
              style="width: 150px"
              class="text-body-2 font-weight-regular text-white"
            >
              Informacje o grupie
            </p>
          </v-container>
        </v-menu>
      </div>
    </v-row>
  </v-app-bar>
  <v-tabs class="bg-primary">
    <v-tab class="text-secondary">Ogólne</v-tab>
  </v-tabs>
  <v-container fluid>
    <v-row align="center" justify="center">
      <v-col cols="9">
        <p class="pl-4 text-high-emphasis">Motyw</p>
        <p class="pl-4 font-weight-light">Zmień motyw jasny/ciemny</p>
      </v-col>
      <v-col cols="3">
        <v-switch
          class="float-right"
          hide-details
          color="accent"
          @click="toggleTheme"
        ></v-switch>
      </v-col>
    </v-row>
  </v-container>
</template>
<script setup lang="ts">
import { useSettingsStore } from "@/modules/settings/store";
import { mdiDotsVertical, mdiArrowLeft } from "@mdi/js";
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useTheme } from "vuetify";
const router = useRouter();
let showGroupSettings = ref(false);

const settingsStore = useSettingsStore();
const theme = useTheme();

function toggleTheme() {
  theme.global.name.value = theme.global.current.value.dark
    ? "lightTheme"
    : "darkTheme";
}

function goBack() {
  router.go(-1);
}
</script>
<style scoped>
p {
  letter-spacing: 0.5px;
}
.settings-row {
  height: 70px !important;
}
.options-container {
  border: 1px solid black;
}

:deep(label) {
  opacity: 1 !important;
  height: 48px !important;
  padding-left: 10px;
  font-size: 14px;
  color: rgba(255, 255, 255, 0.87);
}
@media (hover: hover) {
  .icon-container:hover {
    background-color: rgb(var(--v-theme-primary-darken-2));
  }
}

.header {
  height: 48px !important;
}
</style>
