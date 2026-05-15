import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

import { TaskService } from '../../services/task';
import { CategoryService } from '../../services/category';
import { CategoryResponse } from '../../interfaces/category/category-response';
import { TaskResponse } from '../../interfaces/task/task-response';
import { PagedResult } from '../../interfaces/paging/paged-result';
import { TaskCreateUpdate } from '../../interfaces/task/task-create-update';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-tasks',
  imports: [FormsModule, DatePipe],
  templateUrl: './tasks.html',
  styleUrl: './tasks.css',
})
export class Tasks implements OnInit {
  categories = signal<CategoryResponse[]>([]);

  tasks = signal<PagedResult<TaskResponse> | null>(null);

  page = 1;
  pageSize = 10;
  search = '';
  searchCategoryId: number | undefined = undefined;
  selectedCategoryIds: number[] = [];
  isCategoryFilterOpen = false;

  id = signal<number | null>(null);
  title = '';
  description = '';
  isCompleted = false;
  dueDate = '';
  categoryId: number | null = null;
  isTaskModalOpen = false;

  // newTitle = '';
  // newDescription = '';
  // newIsCompleted = false;
  // newDueDate = '';
  // newCategoryId: number | null = null;

  //taskId: number | null = null;

  successMessage = '';

  errorMessage = '';

  private clearForm() {
    this.id.set(null);
    this.title = '';
    this.description = '';
    this.isCompleted = false;
    this.dueDate = '';
    this.categoryId = null;
  }

  private mapForm(): TaskCreateUpdate {
    return {
      title: this.title,
      description: this.description,
      isCompleted: this.isCompleted,
      dueDate: this.dueDate || null,
      categoryId: this.categoryId,
    };
  }
  private handleSuccess(message: string) {
    this.loadTasks();
    this.successMessage = `${message} Successfully!`;

    //if(this.id !== null) { this.id = null }

    setTimeout(() => {
      this.successMessage = '';
    }, 5000);

    this.clearForm();
    this.isTaskModalOpen = false;
  }

  private showError(error: HttpErrorResponse) {
    console.log(error);

    this.errorMessage = error.error.message;
    setTimeout(() => {
      this.errorMessage = '';
    }, 5000);
  }

  constructor(
    private taskService: TaskService,
    private categoryService: CategoryService,
  ) {}

  ngOnInit() {
    this.loadCategories();
    this.loadTasks();
  }

  nextPage() {
    this.page++;
    this.loadTasks();
  }

  previousPage() {
    if (this.page === 1) return;
    this.page--;
    this.loadTasks();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (response) => {
        this.categories.set(response);
      },
      error: (error) => {
        this.showError(error);
      },
    });
  }

  getCategoryName(categoryId: number | null) {
    if (categoryId === null) return 'No category';

    const category = this.categories().find((c) => c.id === categoryId);

    return category?.name || 'Unknown category';
  }

  filteredTasks() {
    const items = this.tasks()?.items ?? [];

    if (this.selectedCategoryIds.length === 0) {
      return items;
    }

    return items.filter(
      (task) => task.categoryId !== null && this.selectedCategoryIds.includes(task.categoryId),
    );
  }

  selectedCategoryLabel() {
    if (this.selectedCategoryIds.length === 0) return 'All categories';

    if (this.selectedCategoryIds.length === this.categories().length) {
      return 'All categories selected';
    }

    if (this.selectedCategoryIds.length === 1) {
      return this.getCategoryName(this.selectedCategoryIds[0]);
    }

    return `${this.selectedCategoryIds.length} categories`;
  }

  applyFilters() {
    this.page = 1;
    this.searchCategoryId =
      this.selectedCategoryIds.length === 1 ? this.selectedCategoryIds[0] : undefined;
    this.loadTasks();
  }

  toggleCategoryDropdown() {
    this.isCategoryFilterOpen = !this.isCategoryFilterOpen;
  }

  toggleSearchCategory(categoryId: number) {
    if (this.selectedCategoryIds.includes(categoryId)) {
      this.selectedCategoryIds = this.selectedCategoryIds.filter((id) => id !== categoryId);
    } else {
      this.selectedCategoryIds = [...this.selectedCategoryIds, categoryId];
    }

    this.applyFilters();
  }

  selectAllCategories() {
    this.selectedCategoryIds = this.categories().map((category) => category.id);
    this.applyFilters();
  }

  clearAllCategories() {
    this.selectedCategoryIds = [];
    this.applyFilters();
  }

  loadTasks() {
    const query = {
      page: this.page,
      pageSize: this.pageSize,
      search: this.search,
      searchCategoryId: this.searchCategoryId,
    };

    this.taskService.getTasks(query).subscribe({
      next: (response) => {
        this.tasks.set(response);
      },
      error: (error) => {
        this.showError(error);
      },
    });
  }

  createTask() {
    const body = this.mapForm();

    this.taskService.createTask(body).subscribe({
      next: () => {
        const message = 'Created';
        this.handleSuccess(message);
      },
      error: (error) => {
        this.showError(error);
      },
    });
  }

  openCreateModal() {
    this.clearForm();
    this.isTaskModalOpen = true;
  }

  startUpdate(task: TaskResponse) {
    this.id.set(task.id);
    this.title = task.title;
    this.description = task.description || '';
    this.isCompleted = task.isCompleted;
    this.dueDate = task.dueDate || '';
    this.categoryId = task.categoryId;
    this.isTaskModalOpen = true;
  }

  cancelEdit() {
    this.clearForm();
    this.isTaskModalOpen = false;
  }

  updateTask(id: number) {
    if (id === null) return;

    const body = this.mapForm();

    //const id = this.id

    this.taskService.updateTask(body, id).subscribe({
      next: () => {
        const message = 'Updated';
        this.handleSuccess(message);
      },
      error: (error) => {
        this.showError(error);
      },
    });
  }

  toggleCompleted(task: TaskResponse) {
    const body = {
      title: task.title,
      description: task.description,
      isCompleted: !task.isCompleted,
      dueDate: task.dueDate,
      categoryId: task.categoryId,
    };

    this.taskService.updateTask(body, task.id).subscribe({
      next: () => {
        this.loadTasks();
      },
      error: (error) => {
        this.showError(error);
      },
    });
  }

  deleteTask(id: number) {
    if (id === null) return;

    this.taskService.deleteTask(id).subscribe({
      next: () => {
        const message = 'Deleted';
        this.handleSuccess(message);
      },
      error: (error) => {
        this.showError(error);
      },
    });
  }
}
