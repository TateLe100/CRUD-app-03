import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import type { Book } from "../types/Book";
import { getBooks } from "../api/books.ts";
import { accentFor } from "../utils/accent";
import "./HomePage.css";

function HomePage() {
    const [books, setBooks] = useState<Book[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchBooks = async () => {
            try {
                const data = await getBooks();
                setBooks(data);
            } catch (err) {
                setError(err instanceof Error ? err.message : "Something went wrong");
            } finally {
                setLoading(false);
            }
        };

        fetchBooks();
    }, []);

    if (loading) {
        return (
            <div className="home-page">
                <div className="home-container">
                    <p className="home-status">Loading your books…</p>
                </div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="home-page">
                <div className="home-container">
                    <h1 className="home-status-title">Your books didn't load</h1>
                    <p className="home-status">{error}. Check that the API is running, then refresh.</p>
                </div>
            </div>
        );
    }

    return (
        <div className="home-page">
            <div className="home-container">
                <header className="home-header">
                    <h1 className="home-title">Your books</h1>
                    <p className="home-count">
                        {books.length} {books.length === 1 ? "book" : "books"}
                    </p>
                </header>

                {books.length === 0 ? (
                    <div className="home-empty">
                        <p className="home-empty-title">No books yet</p>
                        <p className="home-status">Books you add will show up here.</p>
                    </div>
                ) : (
                    <ul className="book-list">
                        {books.map((book) => (
                            <li key={book.id}>
                                <Link to={`/book/${book.id}`} className="book-row">
                                    <span
                                        className="book-tile"
                                        style={{ backgroundColor: accentFor(book.id) }}
                                        aria-hidden="true"
                                    >
                                        {book.title.charAt(0).toUpperCase()}
                                    </span>
                                    <span className="book-text">
                                        <span className="book-title">{book.title}</span>
                                        <span className="book-author">{book.author}</span>
                                    </span>
                                </Link>
                            </li>
                        ))}
                    </ul>
                )}
            </div>
        </div>
    );
}

export default HomePage;