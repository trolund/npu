import { Header } from "./header";

import { ReactNode } from "react";

type LayoutProps = {
  children: ReactNode;
};

export const Layout = ({ children }: LayoutProps) => {
  return (
    <>
      <Header />
      <div className="relative">
        <div className="fixed inset-0 -z-10 h-96 w-96 animate-float rounded-full bg-[conic-gradient(from_180deg_at_50%_50%,#00FFC2_0deg,#00F0FF_120deg,#0077FF_180deg,#FF0099_242deg,#00FFC2_360deg)] opacity-15 blur-3xl" />
        <main className="relative z-10">{children}</main>
      </div>
    </>
  );
};
