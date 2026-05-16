import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';

import { CategoryService } from '../../services/category.service';

import { CategoryResponse } from '../../interfaces/category/category-response';
import { getErrorMessage } from '../../utils/http-error-message';

@Component({
  selector: 'app-categories',
  imports: [FormsModule],
  templateUrl: './categories.html',
  styleUrl: './categories.css',
})
export class Categories implements OnInit {
  constructor(private categoryService: CategoryService) {}

  categories = signal<CategoryResponse[]>([]);
  categoryName = '';
  editingCategoryId: number | null = null;
  editingCategoryName = '';
  errorMessage = '';
  isCreatingCategory = false;
  isSavingCategory = false;
  deletingCategoryIds = new Set<number>();

  isCategoryNameInvalid() {
    return this.categoryName.trim().length === 0 || this.categoryName.length > 100;
  }

  isEditingCategoryNameInvalid() {
    return this.editingCategoryName.trim().length === 0 || this.editingCategoryName.length > 100;
  }

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (response) => {
        this.categories.set(response);
      },
      error: (error) => {
        console.log(error);
        this.errorMessage = getErrorMessage(error);
      },
    });
  }

  createCategory() {
    if (this.isCreatingCategory || this.isCategoryNameInvalid()) return;

    this.isCreatingCategory = true;
    this.errorMessage = '';

    const name = this.categoryName.trim();

    if (!name) return;

    const body = {
      name,
    };

    this.categoryService.createCategory(body)
    .pipe(finalize(() => this.isCreatingCategory = false))
    .subscribe({
      next: (response) => {
        this.categories.update((categories) => [...categories, response]);

        this.categoryName = '';
      },
      error: (error) => {
        console.log(error);
        this.errorMessage = getErrorMessage(error);
      },
    });
  }

  startEdit(category: CategoryResponse) {
    this.editingCategoryId = category.id;

    this.editingCategoryName = category.name;
  }

  cancelEdit() {
    this.editingCategoryId = null;
    this.editingCategoryName = '';
  }

  saveEdit(id: number) {
    if (this.isSavingCategory || this.isEditingCategoryNameInvalid()) return;

    this.isSavingCategory = true;
    this.errorMessage = '';

    const name = this.editingCategoryName.trim();

    if (!name) return;

    const body = {
      name,
    };

    this.categoryService.updateCategory(body, id)
    .pipe(finalize(() => this.isSavingCategory = false))
    .subscribe({
      next: () => {
        this.categories.update((categories) =>
          categories.map((category) => (category.id === id ? { ...category, name } : category)),
        );
        this.cancelEdit();
      },
      error: (error) => {
        console.log(error);
        this.errorMessage = getErrorMessage(error);
      },
    });
  }

  deleteCategory(id: number) {
    if (this.deletingCategoryIds.has(id)) return;

    this.deletingCategoryIds.add(id);
    this.errorMessage = '';

    this.categoryService.deleteCategory(id)
    .pipe(finalize(() => this.deletingCategoryIds.delete(id)))
    .subscribe({
      next: () => {
        this.categories.update((categories) => categories.filter((category) => category.id !== id));
      },
      error: (error) => {
        console.log(error);
        this.errorMessage = getErrorMessage(error);
      },
    });
  }
}
