export interface TaskListQuery {
  page: number;
  pageSize: number;
  search?: string;
  searchCategoryIds?: number[];
}
