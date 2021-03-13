import { IAccount, User } from "../types";

const userLocalStorageKey = "user";

class AuthService {
  public get User() {
    return this.getUser() as User;
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
      userLocalStorageKey,
      JSON.stringify(data.user as User)
    );
  };

  private getUser() {
    const userLocalStorage = localStorage.getItem(userLocalStorageKey);

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
}

const service = new AuthService();
export default service as AuthService;
