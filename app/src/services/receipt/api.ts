import {AuthService, BaseApi} from "..";
import {CreateHouse, GridRequest, GridResponse, House, IError} from "../types";
import * as Url from "./urls";

class ReceiptApi {
  public generateReceipts = async (): Promise<boolean> => {
    return BaseApi.get(Url.GenerateReceipts, { headers: { Authorization: `Bearer ${AuthService.Token}` } })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("ReceiptApi.generateReceipts: ", e);
        throw e;
      });
  };

}

export default ReceiptApi;
