import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { CategoryService } from '../../services/category';

import { CategoryResponse } from '../../interfaces/category/category-response';

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

  isCategoryNameInvalid() {
    return this.categoryName.trim().length === 0;
  }

  isEditingCategoryNameInvalid() {
    return this.editingCategoryName.trim().length === 0;
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
      },
    });
  }

  createCategory() {
    const name = this.categoryName.trim();

    if (!name) return;

    const body = {
      name,
    };

    this.categoryService.createCategory(body).subscribe({
      next: (response) => {
        this.categories.update((categories) => [...categories, response]);

        this.categoryName = '';
      },
      error: (error) => {
        console.log(error);
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
    const name = this.editingCategoryName.trim();

    if (!name) return;

    const body = {
      name,
    };

    this.categoryService.updateCategory(body, id).subscribe({
      next: () => {
        this.categories.update((categories) =>
          categories.map((category) => (category.id === id ? { ...category, name } : category)),
        );
        this.cancelEdit();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  deleteCategory(id: number) {
    this.categoryService.deleteCategory(id).subscribe({
      next: () => {
        this.categories.update((categories) => categories.filter((category) => category.id !== id));
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
