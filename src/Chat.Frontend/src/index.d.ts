/* eslint-disable */
declare module '*.vue' {
  import type { DefineComponent } from 'vue';
  const component: DefineComponent<{}, {}, any>;
  export default component;
}

declare module 'vuetify';
declare module 'vuetify/lib/components';
declare module 'vuetify/lib/directives';

declare module '*.png' {
  const value: any;
  export = value;
}

declare module '*.svg' {
  const filePath: string;

  export default filePath;
}
