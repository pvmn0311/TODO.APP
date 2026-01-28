import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TodoItems } from '../models/todo.models';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  private apiUrl = '/api/todo';

  constructor(private http: HttpClient){}

  getTodos(): Observable<TodoItems[]>{
    return this.http.get<TodoItems[]>(this.apiUrl)
  }
  addTodo(todo : TodoItems): Observable<TodoItems>{ 
    return this.http.post<TodoItems>(`${this.apiUrl}/create`, todo);
  }
  updateTodo(id : number, todo : TodoItems): Observable<void>{
    return this.http.put<void>(`${this.apiUrl}/update/${id}`, todo);
  }
  deleteTodo(id : number): Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/delete/${id}`);
  }
}
