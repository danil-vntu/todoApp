export interface TaskListQuery {
  page: number;
  pageSize: number;
  search?: string;
  searchCategoryId?: number;
}