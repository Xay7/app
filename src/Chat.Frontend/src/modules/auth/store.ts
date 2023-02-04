import { JwtToken } from '@/modules/auth/model';
import { defineStore } from 'pinia';

export type AuthStore = ReturnType<typeof useAuthStore>;

export const useAuthStore = defineStore('auth', {
  state: () =>
    ({
      accessToken: undefined,
    } as State),
  getters: {
    user: (state): UserInfo => {
      if (!state.accessToken) {
        return {
          name: '',
          roles: [],
          isAuthorized: false,
        };
      } else {
        return {
          name: `${decodeURIComponent(escape(state.accessToken.payload?.preferred_username   ?? 'err'))}`,
          isAuthorized: state.accessToken.isValid,
        } as UserInfo;
      }
    },
  },
  actions: {
    async updateToken(token: string): Promise<void> {
      this.accessToken = new JwtToken(token);
    },
    async logout(): Promise<void> {
      this.accessToken = undefined;
    },
  },
});

export interface UserInfo {
  name: string;
  roles: string[];
  isAuthorized: boolean;
}

export interface State {
  accessToken?: JwtToken;
  refreshToken?: string;
}
