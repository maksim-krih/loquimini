import * as React from "react";

import { AuthService } from "../services";
import { Redirect } from "react-router";

export const onlyAuth = (pathToRedirect: string) => <P extends object>(
  Component: React.ComponentType<P>
) =>
  (props: P) => AuthService.IsAuthenticated ? (
    <Component {...props} />
  ) : (
      <Redirect to={pathToRedirect} />
    );
