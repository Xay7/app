import { defineStore } from "pinia";

export interface State {
  version: string;
  endpoints: {
    identity: string;
  };
}

export const useConfigStore = defineStore("config", {
  state: () =>
    ({
      endpoints: {
        identity: "",
      },
      version: "",
    } as State),
  actions: {
    async init() {
      if (
        process.env.NODE_ENV === "production" ||
        process.env.NODE_ENV === "staging"
      ) {
        this.endpoints = {
          identity: "http://localhost:8080",
        };
        this.version = "DEVELOP";
      } else {
        this.endpoints = {
          identity: "http://localhost:8080",
        };
        this.version = "DEVELOP";
      }
    },
  },
});
