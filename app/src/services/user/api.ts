import {BaseApi} from "..";
import {CreateUser, GridRequest, GridResponse, IError, User} from "../types";
import * as Url from "./urls";

class UserApi {
  public getAllGrid = async (model: GridRequest): Promise<GridResponse<User>> => {
    return BaseApi.post(Url.GetAllGrid, model)
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("UserApi.getAllGrid: ", e);
        throw e;
      });
  };

  public getAll = async (): Promise<Array<User>> => {
    return BaseApi.get(Url.GetAll)
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("UserApi.getAll: ", e);
        throw e;
      });
  };

  public create = async (model: CreateUser): Promise<User> => {
    return BaseApi.post(Url.Create, model)
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("UserApi.create: ", e);
        throw e;
      });
  };
}

export default UserApi;
