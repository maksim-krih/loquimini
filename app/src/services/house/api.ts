import {BaseApi} from "..";
import {CreateHouse, GridRequest, GridResponse, House, IError} from "../types";
import * as Url from "./urls";

class HouseApi {
  public getAllGrid = async (model: GridRequest): Promise<GridResponse<House>> => {
    return BaseApi.post(Url.GetAllGrid, model)
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("HouseApi.getAllGrid: ", e);
        throw e;
      });
  };

  public create = async (model: CreateHouse): Promise<House> => {
    return BaseApi.post(Url.Create, model)
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("HouseApi.create: ", e);
        throw e;
      });
  };

  public update = async (model: House): Promise<House> => {
    return BaseApi.post(Url.Update, model)
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("HouseApi.update: ", e);
        throw e;
      });
  };

  public getById = async (id: string): Promise<House> => {
    const params = {
      id
    };

    return BaseApi.get(Url.GetById, { params })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("HouseApi.getById: ", e);
        throw e;
      });
  };

  public deleteById = async (id: string): Promise<boolean> => {
    const params = {
      id
    };
    
    return BaseApi.delete(Url.DeleteById, { params })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("HouseApi.deleteById: ", e);
        throw e;
      });
  };
}

export default HouseApi;
