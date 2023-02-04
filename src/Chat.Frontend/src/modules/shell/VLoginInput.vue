<template>
  <div class="input-container">
    <label :for="props.labelText" class="text-high-emphasis">{{ props.labelText }}</label>
    <v-text-field
      :id="props.labelText"
      v-bind="$attrs"
      :placeholder="props.placeholder"
      :type="props.type"
      persistent-placeholder
      variant="outlined"
      hide-details
      single-line
      density="comfortable"
      class="input mr-3 text-high-emphasis"
      color="accent"
      bg-color="grey-3"
      :autocomplete="props.type === 'password' ? 'current-password' : 'on'"
    />
  </div>
</template>
<script setup lang="ts">
const props = defineProps({
  labelText: {
    type: String,
    default: '',
  },
  placeholder: {
    type: String,
    default: '',
  },
  type: {
    type: String,
    default: '',
  },
});
</script>
<style scoped>
.input-container {
  width: 100%;
}

.input-container:focus-within label {
  color: #ffbb00;
}

.input {
  margin-bottom: 20px;
  width: 100%;
}

/* :deep(.v-field__outline) {
  --v-field-border-width: none !important;
  --v-field-border-opacity: 0 !important;
  border: 1px solid rgba(255, 255, 255, 0.27) !important;
  outline: none !important;
} */

:deep(.v-field__field) {
  padding: 0px !important;
}

:deep(.v-field__input) {
  padding: 0px !important;
}

:deep(.v-field__input) :first-child {
  padding: 12px 16px !important;
  border-radius: 4px !important;
}

.input:focus {
  border: 2px solid #ffbb00;
  outline: none;
}

label {
  color: #ffffff99;
  text-transform: uppercase;
  letter-spacing: 1.07px;
  font-size: 12px;
}
</style>
