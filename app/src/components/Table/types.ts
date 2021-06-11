import { GridPager, GridSearch } from "../../services/types";

export interface IProps {
  onRow: (record: any) => any;
  columns: any[];
  data: any[];
  pager: GridPager;
  loading: boolean;
  setPager: (_: any) => void;
  getData: (_: any, sorter: any, search?: GridSearch) => void;
}