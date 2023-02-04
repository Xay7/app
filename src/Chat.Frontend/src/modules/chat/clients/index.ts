import * as signalR from "@microsoft/signalr";
import {
  Conference,
  Contact,
  Message,
  Room,
  useChatStore,
} from "@/modules/chat/store";
import { useAuthStore } from "@/modules/auth/store";
import { useWebsocketStore } from "@/modules/chat/plugins/store";

class Chathub {
  private connection: null | signalR.HubConnection = null;
  constructor() {
    this.startConnection();
  }

  async startConnection() {
    const authStore = useAuthStore();
    const accessToken = authStore.accessToken?.value;
    if (accessToken === undefined) {
      return;
    }
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5288/hub", {
        transport: signalR.HttpTransportType.WebSockets,
        skipNegotiation: true,
        accessTokenFactory: () => {
          return accessToken;
        },
      })
      .configureLogging(signalR.LogLevel.Information)
      .build();

    try {
      await this.connection.start();
      this.GetMessage();
    } catch (error) {
      console.log(error);
    }
  }

  async GetMessage() {
    const chatStore = useChatStore();
    try {
      this.connection?.on(
        "GetMessage",
        async (res: { msg: Message; room: Room }) => {
          await chatStore.messageAdded(res);
        }
      );
    } catch (error) {
      console.log(error);
    }
  }

  async JoinChatroom(roomId: string) {
    try {
      await this.connection?.invoke("JoinChatroom", roomId);
    } catch (error) {
      console.log(error);
    }
  }

  async CreateChatroom(roomId: string, description: string, username: string) {
    try {
      await this.connection?.invoke(
        "AddChatroom",
        roomId,
        description,
        username
      );
    } catch (error) {
      console.log(error);
    }
  }

  async SendMessage(username: string, message: string, roomId: string) {
    try {
      await this.connection?.invoke("SendMessage", username, message, roomId);
    } catch (error) {
      console.log(error);
    }
  }
}

let chathub: Chathub | null = null;

export function useChatHub() {
  if (chathub === null || chathub === undefined) {
    chathub = new Chathub();
  }
  return chathub;
}
