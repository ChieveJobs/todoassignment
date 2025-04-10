<template>
  <div>
    <h2>To-Do List</h2>
    <ul>
      <li v-for="todo in todos" :key="todo.id">
        {{ todo.title }} - {{ todo.isCompleted ? '✅' : '❌' }}
      </li>
    </ul>
  </div>
</template>


<script setup lang="ts">
  import { ref, onMounted } from 'vue'
  import axios from 'axios'

  // Define a reactive variable to store the to-do items
  const todos = ref([])

  // Function to fetch to-dos from the API
  const fetchTodos = async () => {
    try {
      const response = await axios.get('https://localhost:5000/todo')
      todos.value = response.data
    } catch (error) {
      console.error('Error fetching to-dos:', error)
    }
  }

  // Fetch the data when the component is mounted
  onMounted(fetchTodos)
</script>
