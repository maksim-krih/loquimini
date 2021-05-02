import React, { Component } from "react";
import {
  BrowserRouter,
  Switch,
  Route
} from "react-router-dom";
import Login from "./pages/Login";
//import Quizzes from "./pages/quizzes";
import { onlyAuth, withLayout } from "./hocs";
import { RouterPaths } from "./consts";

const PrivateRoute = onlyAuth(RouterPaths.Login)(Route);

const Router = () => {
  return (
    <BrowserRouter>
      <Switch>
        <PrivateRoute path={RouterPaths.Main} exact component={withLayout(Component)} />
        <Route path={RouterPaths.Login} exact component={Login} />
      </Switch>
    </BrowserRouter>
  );
};

export default Router;
