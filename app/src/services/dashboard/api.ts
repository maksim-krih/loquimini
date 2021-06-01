import {AuthService, BaseApi} from "..";
import {DashboardInfo, IError} from "../types";
import * as Url from "./urls";

class DashboardApi {
  public getInfo = async (): Promise<DashboardInfo> => {
    return BaseApi.get(Url.GetInfo, { headers: { Authorization: `Bearer ${AuthService.Token}` } })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("DashboardApi.getinfo: ", e);
        throw e;
      });
  };
}

export default DashboardApi;
