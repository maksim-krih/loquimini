export interface CredentialsInfo {
  firstName: string;
  lastName: string;
  id: string;
  email: string;
  roles: Array<Role>;
}

export interface Role {
  id: string;
  name: string;
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
  roles: Array<Role>;
}
