import { QueryClient, useMutation, useQuery } from "react-query";
import { NoteDto } from "../types/NoteDto";
import { isProduction } from "./node";
import { FetchError } from "./models/FetchError";

const baseUrl: string = isProduction()
  ? window.location.origin
  : import.meta.env.VITE_BACKEND_BASE_URL;

const fetchNotes = async () => {
  const res = await fetch(`${baseUrl}/api/notes`);
  return res.json();
};

export const useGetNotes = () => {
  return useQuery<NoteDto[], Error>("notes", fetchNotes);
};

const fetchNoteByIdAndPassword = async (
  id: string,
  password?: string | null,
) => {
  const url = new URL(`${baseUrl}/api/notes/${id}`);

  const response = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ password: password }),
  });

  const data = await response.json();

  if (response.ok) {
    return data;
  } else {
    throw new FetchError(response);
  }
};

export const useGetNoteById = (id: string, password?: string | null) => {
  return useQuery<NoteDto, Error>(
    ["note", id],
    () => fetchNoteByIdAndPassword(id, password),
    { retryOnMount: false, enabled: false },
  );
};

const createNote = async (note: NoteDto) => {
  const res = await fetch(`${baseUrl}/api/notes`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(note),
  });
  return res.json() as Promise<NoteDto>;
};

export const useCreateNote = (queryClient: QueryClient) => {
  return useMutation(createNote, {
    onSuccess: () => {
      queryClient.invalidateQueries("note");
    },
  });
};

const deleteNote = async (noteId: string) => {
  const res = await fetch(`${baseUrl}/api/notes/${noteId}`, {
    method: "DELETE",
  });

  if (!res.ok) {
    throw new Error("Failed to delete note");
  }
};

export const useDeleteNote = (id: string, queryClient: QueryClient) => {
  return useMutation(() => deleteNote(id), {
    onSuccess: () => {
      queryClient.invalidateQueries("note");
    },
  });
};
