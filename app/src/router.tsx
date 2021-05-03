import React, { Component } from "react";
import {
  BrowserRouter,
  Switch,
  Route
} from "react-router-dom";
import Login from "./pages/Login";
import { onlyAuth, withLayout } from "./hocs";
import { RouterPaths } from "./consts";
import { UserList } from "./pages/User";

const PrivateRoute = onlyAuth(RouterPaths.Login)(Route);

const Router = () => {
  return (
    <BrowserRouter>
      <Switch>
        <PrivateRoute path={RouterPaths.Main} exact component={withLayout(React.Fragment)} />
        <Route path={RouterPaths.Login} exact component={Login} />
        <Route path={RouterPaths.UserList} exact component={withLayout(UserList)} />
      </Switch>
    </BrowserRouter>
  );
};

export default Router;
