import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import type { Book } from "../types/Book";
import { getBook } from "../api/books.ts";
import "./BookDetailsPage.css";
import { accentFor } from "../utils/accent";
import type {ReadingEntry} from "../types/ReadingEntry.ts";
import {getEntriesForBook} from "../api/readingEntries.ts";
import ReadingEntries from "../components/ReadingEntries.tsx";


function BookDetailsPage() {
    const { bookId } = useParams();
    const id = Number(bookId);

    const [book, setBook] = useState<Book | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    const [entries, setEntries] = useState<ReadingEntry[]>([]);
    const [entriesLoading, setEntriesLoading] = useState<boolean>(true);
    const [entriesError, setEntriesError] = useState<string | null>(null);

    useEffect(() => {
        if (Number.isNaN(id)) return;

        const fetchBook = async () => {
            setLoading(true);
            setError(null);

            try {
                const data = await getBook(id);
                setBook(data);
            } catch (err) {
                setError(err instanceof Error ? err.message : "Something went wrong");
            } finally {
                setLoading(false);
            }
        };

        fetchBook();
    }, [id]);


    useEffect(() => {
        if (Number.isNaN(id)) return;

        const fetchEntries = async () => {
            setEntriesLoading(true);
            setEntriesError(null);

            try {
                const data = await getEntriesForBook(id);
                setEntries(data);
            } catch (err) {
                setEntriesError(err instanceof Error ? err.message : "Something went wrong");
            } finally {
                setEntriesLoading(false);
            }
        };

        fetchEntries();
    }, [id]);

    if (Number.isNaN(id)) {
        return <StatusMessage title="That isn't a valid book link" message="Go back to your books and pick one from the list." />;
    }

    if (loading) {
        return <div className="detail-page"><p className="status-text">Loading book…</p></div>;
    }

    if (error) {
        return <StatusMessage title="This book didn't load" message={`${error}. Check that the API is running, then refresh.`} />;
    }

    if (!book) {
        return <StatusMessage title="No book with that id" message="It may have been deleted. Go back to your books to find another." />;
    }

    return (
        <div className="detail-page">
            <div className="detail-container">
                <Link to="/" className="back-link">Back to your books</Link>

                <header className="detail-header">
                    <div
                        className="detail-monogram"
                        style={{ backgroundColor: accentFor(book.id) }}
                        aria-hidden="true"
                    >
                        {book.title.charAt(0).toUpperCase()}
                    </div>

                    <div>
                        <h1 className="detail-title">{book.title}</h1>
                        <p className="detail-author">{book.author}</p>
                    </div>
                </header>

                <dl className="detail-fields">
                    <div>
                        <dt>Book number</dt>
                        <dd>{book.id}</dd>
                    </div>
                </dl>
                <section className="entries-section">
                    <h2>Reading entries</h2>

                    {entriesLoading ? (
                        <p className="status-text">Loading entries…</p>
                    ) : entriesError ? (
                        <p className="status-text">{entriesError}</p>
                    ) : (
                        <ReadingEntries entries={entries} />
                    )}
                </section>
            </div>
        </div>
    );
}
function StatusMessage({ title, message }: { title: string; message: string }) {
    return (
        <div className="detail-page">
            <div className="detail-container">
                <Link to="/" className="back-link">Back to your books</Link>
                <h1 className="status-title">{title}</h1>
                <p className="status-text">{message}</p>
            </div>
        </div>
    );
}
export default BookDetailsPage;