import {AuthService, BaseApi} from "..";
import {CreateHouse, GridRequest, GridResponse, House, IError, Receipt} from "../types";
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

  public getByUserIdGrid = async (userId: string, model: GridRequest): Promise<GridResponse<Receipt>> => {
    return BaseApi.post(Url.GetByUserIdGrid(userId), model, { headers: { Authorization: `Bearer ${AuthService.Token}` } })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("ReceiptApi.getByUserIdGrid: ", e);
        throw e;
      });
  };

}

export default ReceiptApi;
