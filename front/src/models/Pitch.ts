import { Idea } from "./Idea";

export interface Pitch{
    id: string,
    title: string,
    content: string,
    ideas: Idea[]
}