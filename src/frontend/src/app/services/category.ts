import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { CategoryResponse } from '../interfaces/category/category-response';
import { CategoryCreateUpdate } from '../interfaces/category/category-create-update';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient) {}

  getCategories() {
    return this.http.get<CategoryResponse[]>(
      `${environment.apiUrl}/category`
    )
  }

  getCategoryById(id: number) {
    return this.http.get<CategoryResponse>(
      `${environment.apiUrl}/category/${id}`
    )
  }

  createCategory(body: CategoryCreateUpdate) {
    return this.http.post<CategoryResponse>(
      `${environment.apiUrl}/category`,
      body
    )
  }

  updateCategory(body: CategoryCreateUpdate, id: number) {
    return this.http.put<CategoryResponse>(
      `${environment.apiUrl}/category/${id}`,
      body
    )
  }

  deleteCategory(id:number) {
    return this.http.delete(
      `${environment.apiUrl}/category/${id}`
    )
  }
}
