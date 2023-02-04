import { defineStore } from 'pinia';

export interface State {
  gps: boolean;
}

export const useSettingsStore = defineStore('settings', {
  state: () =>
    ({
      gps: false,
    } as State),
  actions: {
    toogleGps() {
      this.gps = !this.gps;
    },
  },
});
