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
}

export default HouseApi;
