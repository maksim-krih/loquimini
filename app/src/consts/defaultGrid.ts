import { GridPager, GridRequest, GridSorter } from "../services/types";

export const DefaultPageSize = 10;

export const DefaultGridRequest = (pager: GridPager, sorter?: GridSorter): GridRequest => ({
  filters: [],
  pager,
  search: {
    fields: [],
    value: ""
  },
  sorter: sorter ? [{ ...sorter, order: sorter.order === "ascend" ? "asc" : "desc" }] : []
});

export const DefaultPager: GridPager = {
  current: 1,
  total: 0,
  pageSize: DefaultPageSize
};
