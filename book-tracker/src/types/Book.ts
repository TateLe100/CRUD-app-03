export type Book = {
    id: number;
    external: string;
    title: string;
    author: string | null;
    currentPage: number;
    totalPages: number | null;
    isFinished: boolean;
    dateAdded: string;
    dateFinished: string | null;
}