import { Link } from "react-router-dom";

export const Header = () => {
  return (
    <>
      <div className="fixed left-0 top-0 z-50 h-14 w-screen bg-black bg-opacity-70 backdrop-blur-[15px]">
        <div className="flex gap-2 align-middle">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="50"
            height="50"
            viewBox="0 0 200 200"
          >
            <rect
              x="50"
              y="90"
              width="100"
              height="80"
              rx="10"
              ry="10"
              fill="#2a9df4"
            />

            <path
              d="M70 90 V60 A30 30 0 0 1 130 60 V90"
              fill="none"
              stroke="#2a9df4"
              strokeWidth="8"
            />

            <rect
              x="85"
              y="110"
              width="50"
              height="40"
              rx="5"
              ry="5"
              fill="#fff"
              stroke="#333"
              strokeWidth="2"
            />
            <line
              x1="90"
              y1="120"
              x2="130"
              y2="120"
              stroke="#333"
              strokeWidth="2"
            />
            <line
              x1="90"
              y1="130"
              x2="130"
              y2="130"
              stroke="#333"
              strokeWidth="2"
            />
            <line
              x1="90"
              y1="140"
              x2="120"
              y2="140"
              stroke="#333"
              strokeWidth="2"
            />
          </svg>

          <h1>
            <Link className="align-middle text-xl text-white" to="/">
              Lock-note
            </Link>
          </h1>
        </div>
      </div>
      <div className="h-12" />
    </>
  );
};
