import {BaseApi} from "..";
import {UserCredentials, IError} from "../types";
import {Login} from "./types";
import * as Url from "./urls";

class AuthApi {
  public login = async (model: Login): Promise<UserCredentials> => {
    return BaseApi.post(Url.Login, model)
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("AuthApi.login: ", e)
        throw e;
      });
  };
}

export default AuthApi;
