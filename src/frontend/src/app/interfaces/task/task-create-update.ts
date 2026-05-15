export interface TaskCreateUpdate {
  title: string;
  description: string | null;
  isCompleted: boolean;
  dueDate: string | null;
  categoryId: number | null;
}
