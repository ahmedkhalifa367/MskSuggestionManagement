<script lang="ts">
import { Component, Vue, toNative } from "vue-facing-decorator";
import KanbanBoard from "./components/kanban-board/KanbanBoard.vue";

@Component({ 
  name: "App",
	components: { KanbanBoard },
})
class App extends Vue {
}
export default toNative(App);
</script>
<template>
  <div class="app">
    <header class="header">
      <div class="header-content">
        <h1 class="header-title">MSK Suggestion Management Board</h1>
        <p>A regional director of health, safety and wellbeing at a client has been tasked with reducing rates of MSK absence in the company. They've rolled out VIDA to help them achieve it. Now they need to ensure employees are reducing their risk based on suggestions made by VIDA.</p>
      </div>
    </header>

    <main class="main">
      <kanban-board />
    </main>

    <footer class="footer">
      <div class="footer-content">
        <p><strong>Ahmed Khalifa</strong> |
          <a href="mailto:ahmedkhalifa367@gmail.com">ahmedkhalifa367@gmail.com</a> |
          &copy; 2025 All rights reserved
        </p>
      </div>
    </footer>
  </div>
</template>

<style scoped>
.app {
  height: 100dvh;
  display: flex;
  flex-direction: column;
  background: linear-gradient(135deg,#f5f7fa 0%,#c3cfe2 100%);
}

.header { 
  position: sticky; 
  top: 0; 
  z-index: 20;
  background: linear-gradient(135deg,#2c3e50 0%,#34495e 100%); 
  color:#fff; 
}

.footer { 
  position: sticky; 
  bottom: 0; 
  z-index: 20;
  background: linear-gradient(135deg,#2c3e50 0%,#34495e 100%); 
  color:#fff; 
}

.header-content,.footer-content { 
  padding: 0rem 1rem; 
  text-align: center; 

}

.header-title { 
  font-weight: 700; 
  margin: .25rem 0; 
}

.main{
  flex: 1;
  min-height: 0;
  overflow: auto;
  padding: 1rem;
  display: flex;
  align-items: stretch;
  justify-content: center;
}

.footer-content a { 
  color:#74b9ff; 
  text-decoration: none; 
}
</style>