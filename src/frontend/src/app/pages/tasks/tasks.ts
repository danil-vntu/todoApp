import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { finalize } from 'rxjs';

import { TaskService } from '../../services/task.service';
import { CategoryService } from '../../services/category.service';
import { CategoryResponse } from '../../interfaces/category/category-response';
import { TaskResponse } from '../../interfaces/task/task-response';
import { PagedResult } from '../../interfaces/paging/paged-result';
import { TaskCreateUpdate } from '../../interfaces/task/task-create-update';
import { DatePipe } from '@angular/common';
import { getErrorMessage } from '../../utils/http-error-message';

@Component({
  selector: 'app-tasks',
  imports: [FormsModule, DatePipe],
  templateUrl: './tasks.html',
  styleUrl: './tasks.css',
})
export class Tasks implements OnInit {
  categories = signal<CategoryResponse[]>([]);

  tasks = signal<PagedResult<TaskResponse> | null>(null);
  tasksCount = 0;

  page = 1;
  pageSize = 10;
  search = '';
  searchCategoryIds: number[] = [];
  // searchCategoryId: number | undefined = undefined;
  // selectedCategoryIds: number[] = [];
  isCategoryFilterOpen = false;

  id = signal<number | null>(null);
  title = '';
  description = '';
  isCompleted = false;
  dueDate = '';
  categoryId: number | null = null;
  isTaskModalOpen = false;
  isSavingTask = false;
  deletingTaskIds = new Set<number>();
  togglingTaskIds = new Set<number>();

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

    this.errorMessage = getErrorMessage(error);
    setTimeout(() => {
      this.errorMessage = '';
    }, 5000);
  }

  isTaskFormInvalid() {
    return this.title.trim().length === 0 ||
      this.title.length > 200 ||
      this.description.length > 2000;
  }

  constructor(
    private taskService: TaskService,
    private categoryService: CategoryService,
  ) {}

  ngOnInit() {
    this.loadCategories();
    this.loadTasks();
  }

  allowNextPage() : boolean {
      if(this.page * this.pageSize < this.tasksCount) {
        return true;
      }

    return false;
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

  listTasks() { return this.tasks()?.items ?? []; }

  selectedCategoryLabel() {
    if (this.searchCategoryIds.length === 0) return 'All categories';

    if (this.searchCategoryIds.length === this.categories().length) {
      return 'All categories selected';
    }

    if (this.searchCategoryIds.length === 1) {
      return this.getCategoryName(this.searchCategoryIds[0]);
    }

    return `${this.searchCategoryIds.length} categories`;
  }

  applyFilters() {
    this.page = 1;
    this.loadTasks();
  }

  toggleCategoryDropdown() {
    this.isCategoryFilterOpen = !this.isCategoryFilterOpen;
  }

  toggleSearchCategory(categoryId: number) {
    if (this.searchCategoryIds.includes(categoryId)) {
      this.searchCategoryIds = this.searchCategoryIds.filter((id) => id !== categoryId);
    } else {
      this.searchCategoryIds = [...this.searchCategoryIds, categoryId];
    }

    this.applyFilters();
  }

  selectAllCategories() {
    this.searchCategoryIds = this.categories().map((category) => category.id);
    this.applyFilters();
  }

  clearAllCategories() {
    this.searchCategoryIds = [];
    this.applyFilters();
  }

  loadTasks() {
    const query = {
      page: this.page,
      pageSize: this.pageSize,
      search: this.search,
      searchCategoryIds: this.searchCategoryIds,
    };

    this.taskService.getTasks(query).subscribe({
      next: (response) => {
        this.tasks.set(response);
        this.tasksCount = response.totalCount;
      },
      error: (error) => {
        this.showError(error);
      },
    });
  }

  createTask() {
    if (this.isSavingTask || this.isTaskFormInvalid()) return;

    this.isSavingTask = true;
    this.errorMessage = '';

    const body = this.mapForm();

    this.taskService.createTask(body)
    .pipe(finalize(() => this.isSavingTask = false))
    .subscribe({
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
    if (id === null || this.isSavingTask || this.isTaskFormInvalid()) return;

    this.isSavingTask = true;
    this.errorMessage = '';

    const body = this.mapForm();

    //const id = this.id

    this.taskService.updateTask(body, id)
    .pipe(finalize(() => this.isSavingTask = false))
    .subscribe({
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
    if (this.togglingTaskIds.has(task.id)) return;

    this.togglingTaskIds.add(task.id);
    this.errorMessage = '';

    const body = {
      isCompleted: !task.isCompleted,
    };

    this.taskService.toggleCompleted(body, task.id)
    .pipe(finalize(() => this.togglingTaskIds.delete(task.id)))
    .subscribe({
      next: () => {
        this.loadTasks();
      },
      error: (error) => {
        this.showError(error);
      },
    });
  }

  deleteTask(id: number) {
    if (id === null || this.deletingTaskIds.has(id)) return;

    this.deletingTaskIds.add(id);
    this.errorMessage = '';

    this.taskService.deleteTask(id)
    .pipe(finalize(() => this.deletingTaskIds.delete(id)))
    .subscribe({
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
