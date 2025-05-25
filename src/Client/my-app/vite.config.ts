import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import flowbiteReact from "flowbite-react/plugin/vite";

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin(), flowbiteReact()],
    server: {
        port: 56217,
    }
})