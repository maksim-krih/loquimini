import {AuthService, BaseApi} from "..";
import {CreateUser, GridRequest, GridResponse, IError, User} from "../types";
import * as Url from "./urls";

class UserApi {
  public getAllGrid = async (model: GridRequest): Promise<GridResponse<User>> => {
    return BaseApi.post(Url.GetAllGrid, model, { headers: { Authorization: `Bearer ${AuthService.Token}` } })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("UserApi.getAllGrid: ", e);
        throw e;
      });
  };

  public getAll = async (): Promise<Array<User>> => {
    return BaseApi.get(Url.GetAll, { headers: { Authorization: `Bearer ${AuthService.Token}` } })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("UserApi.getAll: ", e);
        throw e;
      });
  };

  public create = async (model: CreateUser): Promise<User> => {
    return BaseApi.post(Url.Create, model, { headers: { Authorization: `Bearer ${AuthService.Token}` } })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("UserApi.create: ", e);
        throw e;
      });
  };

  public deleteById = async (id: string): Promise<boolean> => {
    const params = {
      id
    };
    
    return BaseApi.delete(Url.DeleteById, { params, headers: { Authorization: `Bearer ${AuthService.Token}` } })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("UserApi.deleteById: ", e);
        throw e;
      });
  };

  public update = async (model: User): Promise<User> => {
    return BaseApi.post(Url.Update, model, { headers: { Authorization: `Bearer ${AuthService.Token}` } })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("UserApi.update: ", e);
        throw e;
      });
  };

  public getById = async (id: string): Promise<User> => {
    const params = {
      id
    };

    return BaseApi.get(Url.GetById, { params, headers: { Authorization: `Bearer ${AuthService.Token}` } })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("UserApi.getById: ", e);
        throw e;
      });
  };
}

export default UserApi;
