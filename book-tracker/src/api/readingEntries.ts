import type { ReadingEntry } from "../types/ReadingEntry";

const BASE_URL = `${import.meta.env.VITE_API_URL}/api/books`;

export async function getEntriesForBook(id: number): Promise<ReadingEntry[]> {

    const response = await fetch(`${BASE_URL}/${id}/entries`);

    if (!response.ok) {
        throw new Error(`Failed to load entries for book id: ${id} (status ${response.status})`);
    }

    return response.json();
}