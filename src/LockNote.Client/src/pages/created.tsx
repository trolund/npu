import { FunctionComponent } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowLeft, faClipboard } from "@fortawesome/free-solid-svg-icons";
import Lottie from "lottie-react";
import okAnimation from "../assets/ok2.json";
import { Link, Navigate, useLocation, useParams } from "react-router-dom";

interface CreatedPageProps {}

const CreatedPage: FunctionComponent<CreatedPageProps> = () => {
  const { noteId } = useParams();
  const location = useLocation();

  if (!location.state?.fromRoot) {
    return <Navigate to="/" replace />;
  }

  if (!noteId) {
    return <Navigate to="/not-found" replace />;
  }

  const copyToClipboard = async (noteId: string) => {
    if (!navigator.clipboard) {
      return;
    }

    // get base url from window object
    const baseUrl = window.location.origin;
    await navigator.clipboard.writeText(`${baseUrl}/note/${noteId}`);
  };

  return (
    <div className="flex min-h-8 w-fit max-w-96 flex-col gap-4">
      <div className="flex flex-col gap-3">
        <Link
          type="button"
          data-testid="back-btn"
          to="/"
          className="mt-5 rounded-lg bg-blue-500 px-4 py-2 text-sm font-medium text-white hover:bg-blue-600 hover:text-white"
        >
          <FontAwesomeIcon icon={faArrowLeft} className="mr-2" />
          Create new note
        </Link>
        {okAnimation && (
          <Lottie animationData={okAnimation} loop={false} allowTransparency />
        )}
        <h2 className="mb-12 text-xl text-green-600">
          Your note has been created!
        </h2>
        <p className="text-slate-400">You can access it at:</p>

        <Link
          data-testid="note-link"
          to={`/note/${noteId}`}
        >{`${window.location.origin}/note/${noteId}`}</Link>
        <button
          type="button"
          data-testid="clipboard-btn"
          className="mt-5 rounded-lg bg-blue-500 px-4 py-2 text-sm font-medium text-white hover:bg-blue-600"
          onClick={() => copyToClipboard(noteId)}
        >
          <FontAwesomeIcon icon={faClipboard} className="mr-2" />
          Copy link to clipboard
        </button>
      </div>
    </div>
  );
};

export default CreatedPage;
