import { UserCredentials, CredentialsInfo, AccessToken } from "../types";

const LocalStorageKeys = {
  User: "user",
  Token: "token"
}

class AuthService {
  public get User() {
    return this.getUser() as CredentialsInfo;
  }

  public get Token() {
    return this.getToken() as string;
  }

  public get IsAuthenticated() {
    return !!this.Token;
  }

  public get IsAdmin() {
    return this.User.roles.some(x => x.name === 'Admin');
  }

  public SignOut = () => {
    localStorage.clear();
  };

  public SetAccount = (data: UserCredentials) => {
    debugger;
    localStorage.setItem(
      LocalStorageKeys.User,
      JSON.stringify(data.credentialsInfo as CredentialsInfo)
    );
    localStorage.setItem(
      LocalStorageKeys.Token,
      JSON.stringify(data.accessToken as AccessToken)
    );
  };

  private getUser() {
    const userLocalStorage = localStorage.getItem(LocalStorageKeys.User);

    const user = userLocalStorage ? JSON.parse(userLocalStorage) as CredentialsInfo : null;

    if (!user) {
      return {
        firstName: "",
        lastName: "",
        email: "",
        id: "",
        roles: []
      };
    }

    return user;
  }

  private getToken() {
    const tokenLocalStorage = localStorage.getItem(LocalStorageKeys.Token);

    const accessToken = tokenLocalStorage ? JSON.parse(tokenLocalStorage) as AccessToken : null;

    if (!accessToken) {
      return "";
    }

    return accessToken.token;
  }
}

const service = new AuthService();
export default service as AuthService;
