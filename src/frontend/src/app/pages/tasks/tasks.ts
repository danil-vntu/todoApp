import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

export interface Todo {
  isCompleted: boolean;
  taskTitle: string;
  taskDescription: string | null;
}

@Component({
  selector: 'app-tasks',
  imports: [FormsModule],
  templateUrl: './tasks.html',
  styleUrl: './tasks.css',
})

export class Tasks {
  tasks: Todo[] = 
  [
    { isCompleted: false, taskTitle: "Learn Angular", taskDescription: "Finish Todo frontend MPV"},
  ]

  isCompleted = false
  taskTitle =""
  taskDescription=""

  addTask() {
    const todo =
      {
        isCompleted: this.isCompleted, 
        taskTitle: this.taskTitle, 
        taskDescription: this.taskDescription
      }

    this.tasks.push(todo)
    this.taskTitle=""
    this.taskDescription=""
  }

  deleteTask(task:Todo) {
    this.tasks = this.tasks.filter(item => item !== task);
  }
}
