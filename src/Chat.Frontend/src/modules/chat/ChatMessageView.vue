<template>
  <v-app-bar density="compact" class="bg-primary" elevation="1">
    <v-btn icon @click="router.go(-1)">
      <v-icon :icon="mdiArrowLeft"></v-icon>
    </v-btn>
    <v-app-bar-title>Wiadomości</v-app-bar-title>
    <v-spacer />
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
  </v-app-bar>
  <v-container class="pa-0 header" fluid app> </v-container>
  <v-container
    ref="messageContainer"
    class="pt-6 px-0 d-flex d-flex flex-column align-end container"
    fluid
    @scroll.self="handleScroll"
  >
    <div
      v-for="(value, index) in chatStore.messages"
      :key="index"
      no-gutters
      class="d-flex align-self-start float-left elevation-1 mx-4 my-2 rounded"
      :class="{
        'align-self-end': value.username === authStore.user.name ? true : false,
      }"
    >
      <v-container class="message-container bg-primary-2 pa-3 rounded">
        <v-row no-gutters class="mb-1">
          <caption
            class="text-accent font-weight-medium text-caption"
            :class="value.username === authStore.user.name ? '' : 'text-yellow'"
          >
            {{
              value.username === authStore.user.name ? "Ja" : value.username
            }}
          </caption>
          <v-spacer />
          <time class="text-grey">{{ value.created }}</time>
        </v-row>
        <v-row no-gutters>
          <p
            v-if="parseBody(value.body) === Body.text"
            class="text-wrap text-subtitle-2 font-weight-light"
          >
            {{ value.body }}
          </p>
          <p
            v-if="parseBody(value.body) === Body.link"
            class="text-wrap text-subtitle-2 font-weight-light text-blue"
          >
            <a :href="value.body" target="_blank">{{ value.body }}</a>
          </p>
          <v-img
            v-if="parseBody(value.body) === Body.img"
            :src="value.body"
          ></v-img>
        </v-row>
      </v-container>
    </div>
  </v-container>
  <v-container
    class="d-flex flex-row pa-0 py-2 pl-4 message-input bg-primary-2 align-center elevation-2"
    fluid
  >
    <v-text-field
      v-model="message"
      placeholder="Wiadomość"
      persistent-placeholder
      hide-details
      single-line
      density="compact"
      @keyup.enter.prevent="sendMessage"
    ></v-text-field>
    <v-btn variant="text" @click="sendMessage">
      <v-icon :icon="mdiSend" size="24" color="white" />
    </v-btn>
  </v-container>
</template>
<script setup lang="ts">
import { mdiSend, mdiDotsVertical, mdiArrowLeft } from "@mdi/js";
import { Message, useChatStore } from "./store";
import { onMounted } from "vue";
import { Ref, ref } from "vue";
import { useAuthStore } from "@/modules/auth/store";
import { useRoute, useRouter } from "vue-router";

enum Body {
  text = 0,
  img = 1,
  link = 2,
}

function isValidURL(string) {
  const res = string.match(
    /(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/g
  );
  return res !== null;
}

function parseBody(body: string) {
  if (body.match(/\.(jpeg|jpg|gif|png)$/) != null) {
    return Body.img;
  } else if (isValidURL(body)) {
    return Body.link;
  } else return Body.text;
}

const chatStore = useChatStore();
const authStore = useAuthStore();
const message = ref("");
const messageContainer = ref<any>(null);
const router = useRouter();
const route = useRoute();
const showGroupSettings = ref(false);
const messages: Ref<Message[]> = ref([
  {
    user: "Jan Kowalski",
    text: "Hello",
    created: "21-22-2017 12:04",
  },
  {
    user: "Emil Cw",
    text: "Hello",
    created: "21-22-2017 12:05",
  },
]);

onMounted(async () => {
  await chatStore.getMessages({
    RoomId: route.params.id as string,
  });
  scrollToBottom();
});

const scrollToBottom = () => {
  messageContainer.value!.$el.scrollTop =
    messageContainer.value!.$el.scrollHeight;
};

chatStore.$onAction(({ name, after }) => {
  after(() => {
    if (name === "messageAdded") {
      scrollToBottom();
    }
  });
});

const sendMessage = async () => {
  if (message.value === "") {
    return;
  }
  // await chatStore.addMessage({
  //   RoomId: route.params.id as string,
  //   Text: message.value,
  //   Username: authStore.user.name
  // })
  await chatStore.sendMessage(
    authStore.user.name,
    message.value,
    route.params.id as string
  );

  message.value = "";
  // await chatStore.getMessages({
  //   RoomId: route.params.id as string
  // })
};

async function handleScroll(e: UIEvent) {
  const target = e.target as HTMLDivElement;

  if (target.scrollTop === 0) {
    const previousScrollHeight = target.scrollHeight;
    await chatStore.fetchMessages();
    chatStore.messages.forEach(async (element) => {
      await ConfirmMessageRead(element.id);
    });
    target.scrollTo(0, target.scrollHeight - previousScrollHeight);
  }
}
function goBack() {
  router.go(-1);
}
</script>
<style scoped>
.icon-container {
  width: 48px !important;
  height: 48px !important;
}

.input-container {
  bottom: 48px;
  height: 56px !important;
  padding: 4px 0;
  padding-left: 4px;
  display: flex;
}

.message-container {
  width: 256px !important;
}

time {
  font-size: 10px;
  letter-spacing: 0.31px;
  font-stretch: condensed;
}

p {
  word-break: break-all;
}

.icon-container:hover {
  background-color: rgb(var(--v-theme-primary-1));
}

:deep(.v-field__field) {
  padding-top: 6px !important;
}

.message-input {
  position: absolute;
}

.container {
  min-height: calc(100vh - 206px) !important;
  height: 1px !important;
  overflow-y: auto !important;
  display: flex !important;
  flex-flow: column nowrap !important;
}

.container > :first-child {
  margin-top: auto !important;
}
</style>
