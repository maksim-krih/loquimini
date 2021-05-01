export interface User {
  firstName: string;
  lastName: string;
  id: string;
  email: string;
  role: Role;
}

export interface Role {
  id: string;
  name: string;
}

export interface IAccount {
  user: User;
  token: string;
}

export interface IError {
  code: string;
  desription: string;
}
