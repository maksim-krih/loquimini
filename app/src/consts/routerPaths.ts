class RouterPaths {
  public static Login = "/login";
  public static Main = "/";
  public static UserList = "/users";
  public static CreateUser = "/user/create";
  public static HouseList = "/houses";
  public static CreateHouse = "/house/create";
  public static GeneralHouseTemplate = "/house/:id";
  public static GeneralHouse = (id: string) => `/house/${id}`;
}

export default RouterPaths;