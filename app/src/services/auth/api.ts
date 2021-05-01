import {BaseApi} from "..";
import {IAccount, IError, User} from "../types";
import {Login} from "./types";
import * as Url from "./urls";

class AuthApi {
  public login = async (model: Login): Promise<IAccount> => {
    return BaseApi.post(Url.Login, model)
      .then((response: any) => response.data)
      .catch((e: IError) => console.log("", e));
  };

  public getUsers = async (): Promise<User[]> => {
    return BaseApi.get(Url.Users)
      .then((response: any) => response.data)
      .catch((e: IError) => console.log("", e))
  }
}

export default AuthApi;
