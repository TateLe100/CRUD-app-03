import type { ReadingEntry } from "../types/ReadingEntry.ts";
import "./ReadingEntries.css";

type ReadingEntriesProps = {
    entries: ReadingEntry[];
};

function ReadingEntries({ entries }: ReadingEntriesProps) {
    return entries.length === 0 ? (
        <div className="entries-empty">
            <p className="entries-empty-title">No entries yet</p>
            <p className="entries-empty-text">
                Log the pages you read and a quick summary to start tracking this book.
            </p>
        </div>
    ) : (
        <ol className="entry-list">
            {entries.map((entry) => (
                <li key={entry.id} className="entry">
                    <div className="entry-header">
                        <span className="entry-pages">
                            Pages {entry.startPage}–{entry.endPage}
                        </span>
                        <time className="entry-date" dateTime={entry.dateRead}>
                            {new Date(entry.dateRead).toLocaleDateString()}
                        </time>
                    </div>
                    <p className="entry-summary">{entry.summary}</p>
                </li>
            ))}
        </ol>
    );
}

export default ReadingEntries;