import type { Book } from "../types/Book";

const BASE_URL = `${import.meta.env.VITE_API_URL}/api/books`;

export async function getBooks(): Promise<Book[]> {
    const response = await fetch(BASE_URL);

    if (!response.ok) {
        throw new Error(`Failed to load books (status ${response.status})`);
    }

    return response.json();
}

export async function getBook(id: number): Promise<Book> {
    const response = await fetch(`${BASE_URL}/${id}`);

    if (!response.ok) {
        throw new Error(`Failed to load book (status ${response.status})`);
    }

    return response.json();
}