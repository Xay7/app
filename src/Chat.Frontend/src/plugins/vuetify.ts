import "vuetify/styles";
import { aliases, mdi } from "vuetify/lib/iconsets/mdi-svg";
import { createVuetify } from "vuetify";
import SortDescending from "@/assets/sortDescending.vue";
import SortDescendingOn from "@/assets/sortDescendingOn.vue";
import SortStatus from "@/assets/sortStatus.vue";
import SortStatusOn from "@/assets/sortStatusOn.vue";
import { md1, md2, md3 } from "vuetify/blueprints";

const lightTheme = {
  dark: false,
  colors: {
    background: "#eeeeee",
    surface: "#222831",
    primary: "#222831",
    "primary-1": "#393E46",
    "primary-2": "#2a313d",
    "primary-3": "#1E88E5",
    "primary-4": "#2962FF",
    secondary: "#00ADB5",
    "secondary-darken-1": "#018786",
    error: "#B00020",
    info: "#2196F3",
    success: "#4CAF50",
    warning: "#FB8C00",
  },
};

const darkTheme = {
  dark: true,
  colors: {
    background: "#222831",
    surface: "#222831",
    primary: "#222831",
    "primary-1": "#393E46",
    "primary-2": "#2a313d",
    "primary-3": "#1E88E5",
    "primary-4": "#2962FF",
    secondary: "#00ADB5",
    "secondary-darken-1": "#018786",
    error: "#B00020",
    info: "#2196F3",
    success: "#4CAF50",
    warning: "#FB8C00",
  },
};

export default createVuetify({
  theme: {
    options: {
      customProperties: true,
    },
    defaultTheme: "lightTheme",
    themes: {
      darkTheme,
      lightTheme,
    },
  },
  icons: {
    defaultSet: "mdi",
    aliases: {
      ...aliases,
      sortDescending: SortDescending,
      sortDescendingOn: SortDescendingOn,
      sortStatus: SortStatus,
      sortStatusOn: SortStatusOn,
    },
    sets: {
      mdi,
    },
  },
});
