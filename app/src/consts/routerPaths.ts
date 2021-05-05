class RouterPaths {
  public static Login = "/login";
  public static Main = "/";
  public static UserList = "/users";
  public static CreateUser = "/user/create";
  public static HouseList = "/houses";
  public static CreateHouse = "/house/create";
  public static GeneralHouseTemplate = "/house/:id";
  public static GeneralHouse = (id: string) => `/house/${id}`;
  public static GeneralUserTemplate = "/user/:id";
  public static GeneralUser = (id: string) => `/user/${id}`;
}

export default RouterPaths;