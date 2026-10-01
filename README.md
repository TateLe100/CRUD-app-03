# 📚 Book Tracker

A full-stack reading tracker built with an **ASP.NET Core Web API** and a **Next.js** frontend. Search for books using the Open Library API, add them to a personal shelf, and track your reading progress.

This is the third project in my CRUD learning series, focused on building more complex API calls from a Next.js frontend.

## Tech Stack

**Backend**
- ASP.NET Core Web API (C#)
- Entity Framework Core
- SQL Server (LocalDB for development)

**Frontend**
- Next.js (App Router) + TypeScript
- Tailwind CSS

**External API**
- [Open Library API](https://openlibrary.org/developers/api)

## Features

- [ ] Search books via Open Library with debounced search-as-you-type
- [ ] Add books to a personal shelf
- [ ] Track reading status (Want to Read / Reading / Finished)
- [ ] Filter, sort, and paginate the shelf
- [ ] Log reading sessions and notes per book
- [ ] Optimistic UI updates when changing status
- [ ] Reading stats dashboard

## Project Structure

```
CRUD-app-03/
├── BookTracker.Api/    # ASP.NET Core Web API
└── book-tracker/       # Next.js frontend
```

## Getting Started

### Prerequisites
- .NET SDK
- Node.js
- SQL Server LocalDB

### Backend

```bash
cd BookTracker.Api
dotnet ef database update
dotnet run
```

### Frontend

Create a `.env.local` file in `book-tracker/`:

```
NEXT_PUBLIC_API_URL=https://localhost:<api-port>
```

Then run:

```bash
cd book-tracker
npm install
npm run dev
```

The app runs at `http://localhost:3000`.

## What I'm Learning

- Building a typed API client layer in Next.js
- Calling third-party APIs alongside my own backend
- URL-driven filtering, sorting, and pagination with `searchParams`
- Optimistic updates with `useOptimistic`
- Surfacing ASP.NET validation errors (ProblemDetails) in forms
