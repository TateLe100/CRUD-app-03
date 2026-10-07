"use client";

import {FormEvent, useState} from 'react'

export default function SearchSection() {
    const [search, setSearch] = useState('');

    async function handleSearch(e: FormEvent<HTMLFormElement>) {
        e.preventDefault();

    }


    return (
        <div>
            <form onSubmit={handleSearch}>
                <label>Search Book</label>
                <input type={"text"}/>
                <button>Search</button>
            </form>
        </div>
    )
}
