import { Link } from "react-router-dom";

export const Header = () => {
  return (
    <>
      <div className="fixed left-0 top-0 z-50 w-screen bg-black bg-opacity-50 pb-2 pl-5 pt-2 backdrop-blur-[15px]">
        <div className="flex gap-2 align-middle">
          <h1>
            <Link className="align-middle text-xl text-white" to="/">
              NPU
            </Link>
          </h1>
        </div>
      </div>
      <div className="h-12" />
    </>
  );
};
