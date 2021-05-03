import axios from "axios";
import AuthApi from "./auth";
import UserApi from "./user";

const backendUrl = "https://localhost:44320/api";

export const BaseApi = axios.create({
  baseURL: backendUrl,
  headers: {
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "*",
    "Access-Control-Allow-Headers": "*"
  },
});

class Api {
  static get Auth() {
    return new AuthApi();
  }

  static get User() {
    return new UserApi();
  }
}

export default Api;
