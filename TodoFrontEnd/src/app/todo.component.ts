import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TodoService } from './services/todo.service';
import { TodoItems } from './models/todo.models';

@Component({
  selector: 'app-todo',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit {

  todos: TodoItems[] = [];

  selectedTodoId: number | null = null;
  selectedTodo: TodoItems | null = null;

  currentFeature: 'add' | 'update' | 'delete' | null = null;

  newTodo = {
    title: '',
    description: '',
    isCompleted: false
  };

  updateTodoModel = {
    title: '',
    description: '',
    isCompleted: false
  };

  constructor(private todoService: TodoService) {}

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.todoService.getTodos().subscribe(data => {
      this.todos = data;
    });
  }

  toggleTodo(id: number): void {
    if (this.selectedTodoId === id) {
      this.selectedTodoId = null;
      this.selectedTodo = null;
    } else {
      this.selectedTodoId = id;
      this.selectedTodo = this.todos.find(t => t.id === id) || null;
    }
  }

  showFeature(feature: 'add' | 'update' | 'delete'): void {
    this.currentFeature = feature;

    if (feature === 'update' && this.selectedTodo) {
      this.updateTodoModel = {
        title: this.selectedTodo.title,
        description: this.selectedTodo.description || '',
        isCompleted: this.selectedTodo.isCompleted
      };
    }
  }

  addTodo(): void {
    if (!this.newTodo.title.trim()) return;

    this.todoService.addTodo(this.newTodo).subscribe({
      next: createdTodo => {
        this.todos.push(createdTodo);
        this.newTodo = {
          title: '',
          description: '',
          isCompleted: false
        };
      },
      error: err => {
        console.error(err);
        alert('Thêm công việc thất bại');
      }
    });
  }

  updateTodo(): void {
    if (!this.selectedTodoId) return;

    this.todoService.updateTodo(this.selectedTodoId, {
      id: this.selectedTodoId,
      title: this.updateTodoModel.title,
      description: this.updateTodoModel.description,
      isCompleted: this.updateTodoModel.isCompleted
    }).subscribe({
      next: () => {
        const index = this.todos.findIndex(t => t.id === this.selectedTodoId);
        if (index !== -1) {
          this.todos[index] = {
            ...this.todos[index],
            ...this.updateTodoModel
          };
        }

        alert('Cập nhật thành công');
        this.currentFeature = null;
      },
      error: err => {
        console.error(err);
        alert('Cập nhật thất bại');
      }
    });
  }

  deleteTodo(): void {
  if (!this.selectedTodoId) return;

  const confirmDelete = confirm('Bạn có chắc muốn xóa công việc này không?');
  if (!confirmDelete) return;

  this.todoService.deleteTodo(this.selectedTodoId).subscribe({
    next: () => {
      // xóa khỏi danh sách UI
      this.todos = this.todos.filter(
        t => t.id !== this.selectedTodoId
      );

      // reset trạng thái
      this.selectedTodoId = null;
      this.selectedTodo = null;
      this.currentFeature = null;

      alert('Xóa công việc thành công');
    },
    error: err => {
      console.error(err);
      alert('Xóa công việc thất bại');
    }
  });
}

}
