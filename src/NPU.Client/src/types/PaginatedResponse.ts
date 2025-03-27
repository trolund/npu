export interface PaginatedResponse<T> {
  items?: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  numberOfPages: number;
}
