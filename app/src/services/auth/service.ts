import { IAccount, User } from "../types";

const LocalStorageKeys = {
  User: "user",
  Token: "token"
}

class AuthService {
  public get User() {
    return this.getUser() as User;
  }

  public get Token() {
    return this.getToken() as string;
  }

  public get IsAuthenticated() {
    return !!this.User.id;
  }

  public get IsAdmin() {
    return this.User.role.name === 'Admin'
  }

  public get IsStudent() {
    return this.User.role.name === 'Student'
  }

  public SignOut = () => {
    localStorage.clear();

    window.location.reload();
  };

  public SetAccount = (data: IAccount) => {
    localStorage.setItem(
      LocalStorageKeys.User,
      JSON.stringify(data.user as User)
    );
  };

  private getUser() {
    const userLocalStorage = localStorage.getItem(LocalStorageKeys.User);

    const user = userLocalStorage ? JSON.parse(userLocalStorage) as User : null;

    if (!user) {
      return {
        firstName: "",
        lastName: "",
        email: "",
        id: "",
      };
    }

    return user;
  }

  private getToken() {
    const tokenLocalStorage = localStorage.getItem(LocalStorageKeys.Token);

    const user = tokenLocalStorage ? JSON.parse(tokenLocalStorage) as string : null;

    if (!user) {
      return "";
    }

    return user;
  }
}

const service = new AuthService();
export default service as AuthService;
