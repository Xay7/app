import { defineStore } from 'pinia';

export type WsState = {
  connected: boolean;
};

export const useWebsocketStore = defineStore('ws', {
  state: (): WsState => ({
    connected: false,
  }),
});
