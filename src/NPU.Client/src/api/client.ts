import { useQuery } from "react-query";
import { isProduction } from "./node";
import { PaginatedResponse } from "../types/PaginatedResponse";
import { NpuResponse } from "../types/NpuResponse";
import { keepPreviousData } from "@tanstack/react-query";

const baseUrl: string = isProduction()
  ? window.location.origin
  : import.meta.env.VITE_BACKEND_BASE_URL;

export const useGetPaginatedNpus = (
  searchTerm: string,
  pageNumber: number = 1,
  pageSize: number = 10,
  sortOrderKey: string = "deadline",
) => {
  const fetchProjectsPage = async (
    searchTerm: string,
    pageNumber: number,
    pageSize: number,
    sortOrderKey: string,
  ): Promise<PaginatedResponse<NpuResponse>> => {
    const response = await fetch(
      `${baseUrl}/npus/pagination?${new URLSearchParams({
        searchTerm,
        pageNumber: pageNumber.toString(),
        pageSize: pageSize.toString(),
        sortOrderKey,
      })}`,
    );

    return await response.json();
  };

  return useQuery({
    queryKey: ["npu", pageNumber, pageSize, searchTerm, sortOrderKey],
    queryFn: () =>
      fetchProjectsPage(searchTerm, pageNumber, pageSize, sortOrderKey),
    placeholderData: keepPreviousData,
  });
};
