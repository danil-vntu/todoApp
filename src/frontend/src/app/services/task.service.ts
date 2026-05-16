import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { TaskListQuery } from '../interfaces/task/task-list-query';
import { PagedResult } from '../interfaces/paging/paged-result';
import { environment } from '../environments/environment';
import { TaskCreateUpdate } from '../interfaces/task/task-create-update';
import { TaskResponse } from '../interfaces/task/task-response';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  constructor(private http: HttpClient) {}

  getTasks(query: TaskListQuery) {
    return this.http.get
    <PagedResult<TaskResponse>>(
      `${environment.apiUrl}/Task`,
      {
        params: {
          page: query.page,
          pageSize: query.pageSize,
          search: query.search ?? "",
          categoryId: query.searchCategoryId ?? ""
        }
      }
    )
  }

  createTask(body: TaskCreateUpdate) {
    return this.http.post
    <TaskListQuery>(
      `${environment.apiUrl}/Task`,
      body
    )
  }

  updateTask(body: TaskCreateUpdate, id: number | null) {
    return this.http.put
    <TaskListQuery>(
      `${environment.apiUrl}/Task/${id}`,
      body
    )
  }

  deleteTask(id: number | null) {
    return this.http.delete(
      `${environment.apiUrl}/Task/${id}`
    )
  }
}
