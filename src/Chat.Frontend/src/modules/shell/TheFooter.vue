<template>
  <v-footer
    fixed
    bottom
    width="100%"
    height="48px"
    fluid
    app
    class="bg-primary"
  >
    <v-container width="100%">
      <v-row class="" height="48px">
        <p class="font-weight-light nav-text"></p>
        <v-spacer />
        <p class="font-weight-light nav-text"></p>
      </v-row>
    </v-container>
  </v-footer>
</template>
<script setup lang="ts">
import { onMounted, ref, watch } from "vue";
import { useRoute } from "vue-router";
import { mdiCircle } from "@mdi/js";

import FooterLogo from "@/assets/footerLogo.png";
import { useSettingsStore } from "@/modules/settings/store";

const route = useRoute();
const settingsStore = useSettingsStore();
let timestamp = ref("00:00:00");
let showGps = ref(true);
let showLogo = ref(false);

watch(
  () => route.name,
  (name) => {
    {
      showGps.value = false;
      showLogo.value = false;
      if (name === "login") {
        showLogo.value = true;
      } else {
        showGps.value = true;
      }
    }
  }
);

onMounted(() => {
  showGps.value = false;
  showLogo.value = false;
  if (route.name === "login") {
    showLogo.value = true;
  } else {
    showGps.value = true;
  }
});
</script>
<style scoped>
.login-btn {
  width: 158px !important;
  height: 40px !important;
}

.gps-icon {
  margin-right: 4px;
}

p {
  font-size: 0.75rem;
}

.icon-container {
  width: 24px;
  height: 24px;
}
</style>
