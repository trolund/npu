import { QueryClient, QueryClientProvider } from "react-query";
import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { PageNotFound } from "./pages/page-not-found";
import { Layout } from "./component/layout";
import { Index } from "./pages";

function App() {
  const queryClient = new QueryClient();
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Layout>
          <Routes>
            <Route index element={<Index />}></Route>
            <Route path="*" element={<PageNotFound />} />
            <Route path="not-found" element={<PageNotFound />} />
          </Routes>
        </Layout>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;
