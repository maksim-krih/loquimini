import { User } from "../types";

export interface Login {
  email: string;
  password: string;
}

export interface LoginResponse {
  user: User;
}