export interface CredentialsInfo {
  firstName: string;
  lastName: string;
  id: string;
  email: string;
  roles: Array<Role>;
}

export interface Role {
  id: string;
  name: string;
}

export interface UserCredentials {
  credentialsInfo: CredentialsInfo;
  accessToken: AccessToken;
}

export interface AccessToken {
  token: string;
  expiresIn: number;
}

export interface IError {
  code: string;
  desription: string;
}
