import { defineStore } from "pinia";
import {
  addChat,
  getChatrooms,
  getMessages,
  useChatHub,
} from "@/modules/chat/clients/index";
import { formatDate } from "@/modules/chat/utils/formatDate";
import { useAuthStore } from "@/modules/auth/store";
import { timeDifferenceNow } from "@/modules/chat/utils/timeDifferenceNow";
import axios from "axios";

export const useChatStore = defineStore("chat", {
  state: (): ChatState => ({
    rooms: [],
    messages: [],
  }),
  actions: {
    async sendMessage(username: string, message: string, roomId: string) {
      await useChatHub().SendMessage(username, message, roomId);
    },
    async joinChatroom(roomId: string) {
      await useChatHub().JoinChatroom(roomId);
    },
    async addMessage(req: AddMessageRequest) {
      try {
        await axios.post("http://localhost:5288/api/rooms/messages", req);
      } catch (error) {
        console.log(error);
      }
    },
    async messageAdded(res: { msg: Message; room: Room }) {
      (this.rooms as []).map((el) => {
        if (el.id === res.room.id) {
          el.unread += 1;
        }
      });
      this.messages.push(res.msg);
    },
    async getMessages(req: GetMessagesRequest) {
      console.log(req);
      try {
        const res = await axios.get(
          "http://localhost:5288/api/rooms/messages",
          {
            params: {
              RoomId: +req.RoomId,
            },
          }
        );
        this.messages = res.data;
      } catch (error) {
        console.log(error);
      }
    },
    async addRoomHub(req: AddRoomRequest) {
      await useChatHub().CreateChatroom(
        req.name,
        req.description,
        req.username
      );
    },
    async addRoom(req: AddRoomRequest) {
      try {
        await axios.post("http://localhost:5288/api/rooms", req);
      } catch (error) {
        console.log(error);
      }
    },
    async getRooms(req: GetRoomsRequest) {
      try {
        const res = await axios.get("http://localhost:5288/api/rooms", {
          params: {
            Username: req.Username,
          },
        });
        res.data.forEach((el) => {
          el.unread = 0;
        });
        this.rooms = res.data;
      } catch (error) {
        console.log(error);
      }
    },
  },
});

export interface AddMessageRequest {
  RoomId: string;
  Username: string;
  Text: string;
}

export interface GetMessagesRequest {
  RoomId: string;
}

export interface AddRoomRequest {
  username: string;
  name: string;
  description: string;
}

export interface GetRoomsRequest {
  Username: string;
}

export type ChatState = {
  rooms: Room[];
  messages: Message[];
};

export interface Room {
  id: number;
  name: string;
  description: string;
  created: string;
  updated: string;
  unread: number;
}

export interface Message {
  roomId: string;
  body: string;
  created: string;
  username: string;
}

export interface User {
  name: string;
}
