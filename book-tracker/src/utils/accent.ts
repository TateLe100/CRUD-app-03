const ACCENTS = ["#3B5BDB", "#0C8599", "#2B8A3E", "#E8590C", "#C2255C", "#7048E8"];

export function accentFor(id: number) {
    return ACCENTS[Math.abs(id) % ACCENTS.length];
}