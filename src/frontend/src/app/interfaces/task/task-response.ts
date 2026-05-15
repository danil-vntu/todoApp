export interface TaskResponse {
  id: number;
  title: string;
  description: string | null;
  isCompleted: boolean;
  createdAt: string;
  dueDate: string | null;
  categoryId: number | null;
}
