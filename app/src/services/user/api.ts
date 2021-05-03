import {BaseApi} from "..";
import {GridRequest, GridResponse, IError, User} from "../types";
import * as Url from "./urls";

class UserApi {
  public getAll = async (model: GridRequest): Promise<GridResponse<User>> => {
    return BaseApi.post(Url.GetAll, model)
      .then((response: any) => {console.log(response);return response.data})
      .catch((e: IError) => console.log("", e));
  };
}

export default UserApi;
