import { GridPager, GridRequest } from "../services/types";

export const DefaultPageSize = 10;

export const DefaultGridRequest = (pager: GridPager): GridRequest => ({
  filters: [],
  pager,
  search: {
    fields: [],
    value: ""
  },
  sorter: []
});

export const DefaultPager: GridPager = {
  current: 1,
  total: 0,
  pageSize: DefaultPageSize
};
