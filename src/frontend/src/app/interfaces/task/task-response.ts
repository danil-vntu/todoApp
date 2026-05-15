export interface TaskResponse {
    id: number;
    title: string;
    description: string | null;
    isCompleted: boolean;
    createdAt: string;
    dueDate: string;
    categoryId: number | null;
}