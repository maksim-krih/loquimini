import {BaseApi} from "..";
import {IError, User} from "../types";
import {Login} from "./types";
import {login as loginUrl, users as usersUrl} from "./urls";

class AuthApi {
  public login = async (model: Login): Promise<User> => {
    return BaseApi.post(loginUrl, model)
      .then((response: any) => response.data)
      .catch((e: IError) => console.log("", e));
  };

  public getUsers = async (): Promise<User[]> => {
    return BaseApi.get(usersUrl)
      .then((response: any) => response.data)
      .catch((e: IError) => console.log("", e))
  }
}

export default AuthApi;
