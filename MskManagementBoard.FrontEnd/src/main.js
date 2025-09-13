import './assets/main.css'
import { createApp } from 'vue'
import App from './App.vue'
import { registerLicense } from '@syncfusion/ej2-base'
import ToastPlugin from 'vue-toast-notification';

registerLicense("Ngo9BigBOggjGyl/Vkd+XU9FcVRDXXxIf0x0RWFcb1t6d1JMYFlBJAtUQF1hTH5ad0RiXnxZc3FdRWFdWkd3")

const app = createApp(App);
app.use(ToastPlugin, {
    position: 'top-right',
    duration: 4000,
    dismissible: true,
    pauseOnHover: true
});
app.mount('#app');
