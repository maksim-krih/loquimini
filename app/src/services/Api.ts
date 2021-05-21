import axios from "axios";
import AuthApi from "./auth";
import HouseApi from "./house";
import UserApi from "./user";
import ReceiptApi from "./receipt";
import { AuthService } from ".";
import { RouterPaths } from "../consts";

const backendUrl = "https://localhost:44320/api";

export const BaseApi = axios.create({
  baseURL: backendUrl,
  headers: {
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "*",
    "Access-Control-Allow-Headers": "*"
  },
});

BaseApi.interceptors.response.use(response => {
  return response;
  }, error => {
    if (error.response.status === 401) {
      AuthService.SignOut();
      window.location.replace(RouterPaths.Login);
    }
    return error;
});

class Api {
  static get Auth() {
    return new AuthApi();
  }

  static get House() {
    return new HouseApi();
  }

  static get User() {
    return new UserApi();
  }

  static get Receipt() {
    return new ReceiptApi();
  }
}

export default Api;
