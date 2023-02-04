<template>
  <v-app-bar density="compact" color="primary" elevation="1">
    <v-app-bar-title>Pokoje</v-app-bar-title>
    <v-spacer></v-spacer>
    <v-btn icon>
      <v-icon :icon="mdiSync" @click="getRooms"></v-icon>
    </v-btn>
    <div
      class="fill-height icon-container d-flex flex-row justify-center align-center"
      @click="showAddDialog = !showAddDialog"
    >
      <v-icon :icon="mdiPlus" />
    </div>
    <div
      class="fill-height icon-container d-flex flex-row justify-center align-center"
      @click="showJoinDialog = !showJoinDialog"
    >
      <v-icon :icon="mdiChatPlus" />
    </div>
    <div
      class="fill-height icon-container d-flex flex-row justify-center align-center"
      @click="descendingMenu"
    >
      <v-icon>{{
        showDescendingMenu ? "$sortDescendingOn" : "$sortDescending"
      }}</v-icon>
      <v-menu
        activator="parent"
        anchor="bottom end"
        origin="auto"
        class="white"
        @click:outside="showDescendingMenu = false"
      >
        <v-container class="bg-surface elevation-2" style="min-width: 200px">
          <v-radio-group v-model="activeRadioBtn" hide-details>
            <v-radio
              label="Po aktywności"
              value="activity"
              class="white"
              color="success"
              @change="fetchChatsByActivity"
            />
            <v-radio
              label="Alfabetycznie"
              value="alphabetically"
              color="success "
              @change="fetchChatsAlphabetically"
            />
          </v-radio-group>
        </v-container>
      </v-menu>
    </div>
  </v-app-bar>

  <v-list class="pa-0 bg-background">
    <v-list-item
      v-for="(value, index) in chatStore.rooms"
      class="bg-primary-2 elevation-1 ma-2 my-4 rounded"
      :key="index"
      @click.once="goToMessageView(value.id)"
    >
      <template #prepend>
        <v-badge
          class="mr-4"
          color="red"
          :content="value.unread"
          v-if="value.unread !== 0"
        >
          <v-avatar color="secondary">
            <span class="text-h5">{{ value.name.substring(0, 2) }}</span>
          </v-avatar>
        </v-badge>
        <v-avatar color="secondary" v-if="value.unread === 0">
          <span class="text-h5">{{ value.name.substring(0, 2) }}</span>
        </v-avatar>
      </template>
      <v-list-item-title class="text-h6">{{ value.name }}</v-list-item-title>
      <v-list-item-subtitle class="text-body-1"
        >{{ value.id }} {{ value.description }}</v-list-item-subtitle
      >
      <template #append>
        <v-btn @click="showAddDialog = false" color="green" icon variant="text">
          <v-icon :icon="mdiCircleSmall" size="48"></v-icon>
        </v-btn>
      </template>
    </v-list-item>
  </v-list>
  <v-dialog v-model="showAddDialog" width="400">
    <v-card>
      <v-card-title>Dodaj czat</v-card-title>
      <v-card-text>
        <v-text-field v-model="newChat.name" label="Nazwa" />
        <v-text-field v-model="newChat.description" label="Opis" />
      </v-card-text>

      <v-card-actions>
        <v-spacer />
        <v-btn @click="showAddDialog = false" color="red">Cofnij</v-btn>
        <v-btn @click="addChat" color="green">Zatwierdź</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <v-dialog v-model="showJoinDialog" width="400">
    <v-card>
      <v-card-title>Dołącz do pokoju</v-card-title>
      <v-card-text>
        <v-text-field v-model="joinRoomId" label="Id pokoju" />
      </v-card-text>

      <v-card-actions>
        <v-spacer />
        <v-btn @click="showJoinDialog = false" color="red">Cofnij</v-btn>
        <v-btn @click="joinRoom" color="green">Zatwierdź</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script setup lang="ts">
import { mdiPlus, mdiChatPlus, mdiSync, mdiCircleSmall } from "@mdi/js";
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useAuthStore } from "../auth/store";
import { useChatStore } from "./store";

const chatStore = useChatStore();
const router = useRouter();
const authStore = useAuthStore();
const showStatusMenu = ref(false);
const showDescendingMenu = ref(false);
const activeRadioBtn = ref(0);
const showAddDialog = ref(false);
const showJoinDialog = ref(false);
const joinRoomId = ref("");
const newChat = ref({
  name: "",
  description: "",
});

async function getRooms() {
  await chatStore.getRooms({ Username: authStore.user.name });
}

async function addChat() {
  showAddDialog.value = false;

  await chatStore.addRoomHub({
    username: authStore.user.name,
    name: newChat.value.name,
    description: newChat.value.description,
  });
  await getRooms();
}

async function joinRoom() {
  showJoinDialog.value = false;
  await chatStore.joinChatroom(joinRoomId.value);
  await getRooms();
}

onMounted(async () => {
  await getRooms();
});

async function goToMessageView(id: number) {
  console.log(id);
  router.push({ name: "chatMessage", params: { id: id } });
}
function descendingMenu() {
  showStatusMenu.value = false;
  chatStore.statusSort = false;
  showDescendingMenu.value = !showDescendingMenu.value;
}

function fetchChatsByActivity() {
  chatStore.rooms = chatStore.rooms.sort((a, b) => {
    return a.name.localeCompare(b.name);
  });
}

function fetchChatsAlphabetically() {
  chatStore.rooms = chatStore.rooms.sort((a, b) => {
    return a.name.localeCompare(b.name);
  });
}

function sortByStatus() {
  activeRadioBtn.value = 0;
  showStatusMenu.value = !showStatusMenu.value;
  chatStore.sort = "status";
  chatStore.fetchChatsByStatus();
}
</script>
<style scoped>
.container {
  min-height: calc(100vh - 190px) !important;
  height: 1px !important;
  padding-bottom: 48px !important;
  overflow-y: auto !important;
}
.chat-col {
  cursor: pointer;
}
p {
  word-break: break-all;
}

.header {
  height: 48px !important;
}

.icon-container {
  width: 48px;
}

.options-container {
  border: 1px solid black;
}

@media (hover: hover) {
  .icon-container:hover {
    background-color: rgb(var(--v-theme-primary-darken-2));
  }
}
</style>
