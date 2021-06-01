import { HouseType, ReceiptStatus, ReceiptType } from "../enums";

export interface CredentialsInfo {
  firstName: string;
  lastName: string;
  id: string;
  email: string;
  roles: Array<string>;
}
 
export interface UserCredentials {
  credentialsInfo: CredentialsInfo;
  accessToken: AccessToken;
}

export interface AccessToken {
  token: string;
  expiresIn: number;
}

export interface IError {
  code: string;
  desription: string;
}

export interface GridFilter {
  operator: string;
  field: string;
  value: string;
  logic: string;
}

export interface GridPager {
  current: number;
  total: number;
  pageSize: number;
}

export interface GridSorter {
  field: string;
  order: string;
}

export interface GridSearch {
  fields: Array<string>;
  value: string;
}

export interface GridRequest {
  filters: Array<GridFilter>;
  pager: GridPager;
  sorter: Array<GridSorter>;
  search: GridSearch;
}

export interface GridResponse<T> {
  total: number;
  list: T[];
}

export interface User {
  firstName: string;
  lastName: string;
  id: string;
  email: string;
  roles: Array<string>;
}

export interface House {
  id: string;
  number: string;
  street: string;
  type: HouseType;
  userId?: string;
  info?: BuildingInfo;
  flats: Array<Flat>
}

export interface Flat {
  id: string;
  houseId: string;
  houseNumber: string;
  street: string;
  number: string;
  info: BuildingInfo;
  userId: string;
}

export interface BuildingInfo {
  area: number;
  defaultIndicators: Array<DefaultIndicator>;
}

export interface DefaultIndicator {
  type: ReceiptType;
  value: number;
}

export interface CreateUser {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  roles: Array<string>;
}

export interface CreateHouse {
  number: string;
  street: string;
  type: HouseType;
  userId?: string;
  info?: BuildingInfo;
  flats: Array<Flat>;
}

export interface Receipt {
  id: string;
  type: ReceiptType;
  status: ReceiptStatus;
  rate: number;
  oldIndicator: number;
  newIndictor: number;
  total: number;
  paid: number;
  debt: number;
  date: Date;
  house: House;
  flat: Flat;
  houseType: HouseType;
}

export interface FillReceipt {
  receiptId: string;
  newIndicator: number;
}

export interface PayReceipt {
  receiptId: string;
  value: number;
}

export interface DashboardInfo {
  totalSum: number;
  totalDebts: number;
  currentFilled: number;
  totalFilled: number;
  currentPaid: number;
  totalPaid: number;
}