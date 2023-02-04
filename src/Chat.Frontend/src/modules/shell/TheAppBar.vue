<template>
  <v-app-bar density="compact" app order="0" flat class="bg-primary">
    <div
      v-if="showToolbarChildren"
      class="icon-container d-flex flex-row justify-center align-center"
      @click="shellStore.showNavbar = !shellStore.showNavbar"
    >
      <v-icon :icon="mdiMenu" size="default" color="white" />
    </div>
    <v-spacer></v-spacer>
  </v-app-bar>
</template>
<script setup lang="ts">
import { mdiMenu, mdiMessageAlert } from "@mdi/js";
import { onMounted, ref, watch } from "vue";
import { useRoute } from "vue-router";
import { useShellStore } from "@/modules/shell/store";
import { useChatStore } from "@/modules/chat/store";
const route = useRoute();
const shellStore = useShellStore();
const chatStore = useChatStore();
let showToolbarChildren = ref(true);
let showMessageAlert = ref(true);

watch(
  () => route.name,
  (name) => {
    {
      if (name === "login") {
        showToolbarChildren.value = false;
        showMessageAlert.value = false;
      } else {
        showToolbarChildren.value = true;
        showMessageAlert.value = true;
      }
    }
  }
);

onMounted(() => {
  if (route.name === "login") {
    showToolbarChildren.value = false;
    showMessageAlert.value = false;
  } else {
    showToolbarChildren.value = true;
  }
});
</script>
<style>
.nav-text {
  font-size: 0.75rem;
}

.nav-footer {
  border-top: 1px solid #00000012;
}

.router-link {
  text-decoration: none;
}

a {
  color: inherit;
}

:deep(.v-toolbar__content) {
  padding: 0 !important;
}

.icon-container {
  width: 48px;
  height: 48px;
}

.icon-container:hover {
  cursor: pointer;
}
</style>
