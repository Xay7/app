<template>
  <v-app>
    <the-app-bar />
    <the-navigation-drawer />
    <v-main scrollable class="bg-background">
      <router-view />
    </v-main>
    <the-footer />
  </v-app>
</template>

<script lang="ts" setup>
import TheAppBar from "@/modules/shell/TheAppBar.vue";
import TheNavigationDrawer from "@/modules/shell/TheNavigationDrawer.vue";
import TheFooter from "@/modules/shell/TheFooter.vue";
</script>
<style>
html,
body {
  overflow: hidden;
}
</style>
