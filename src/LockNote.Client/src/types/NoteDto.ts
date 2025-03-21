export interface NoteDto {
  id: string;
  readBeforeDelete: number;
  content: string | null;
  createdAt: string;
  password: string | null;
}
