import {AuthService, BaseApi} from "..";
import {CreateHouse, FillReceipt, GridRequest, GridResponse, House, IError, PayReceipt, Receipt} from "../types";
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

  public fillReceipt = async (model: FillReceipt): Promise<boolean> => {
    return BaseApi.post(Url.FillReceipt, model, { headers: { Authorization: `Bearer ${AuthService.Token}` } })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("ReceiptApi.fillReceipt: ", e);
        throw e;
      });
  };

  public payReceipt = async (model: PayReceipt): Promise<boolean> => {
    return BaseApi.post(Url.PayReceipt, model, { headers: { Authorization: `Bearer ${AuthService.Token}` } })
      .then((response: any) => response.data)
      .catch((e: IError) => {
        console.log("ReceiptApi.payReceipt: ", e);
        throw e;
      });
  };
}

export default ReceiptApi;
