export interface TaskCreateUpdate {
    title: string;
    description: string | null;
    isCompleted: boolean;
    dueDate: string;
    categoryId: number | null;
}