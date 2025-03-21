import { useCreateNote } from "../api/client";
import { useQueryClient } from "react-query";
import NoteForm from "../component/note-form";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Features from "../component/fatures";

export const Index = () => {
  const queryClient = useQueryClient();
  const { mutate, data, isError, isLoading, isSuccess } =
    useCreateNote(queryClient);
  const navigate = useNavigate();

  useEffect(() => {
    if (data?.id && isSuccess) {
      navigate(`created/${data.id}`, {
        replace: true,
        state: { fromRoot: true },
      });
    }
  }, [data, navigate, isSuccess]);

  return (
    <div className="flex flex-col items-center gap-4">
      <div className="min-h-8 w-full max-w-2xl">
        <NoteForm mutate={mutate} isWaitingForCreation={isLoading} />
        {isError && <p className="text-red-800">An error occurred</p>}
      </div>
      <Features />
    </div>
  );
};
