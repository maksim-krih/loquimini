import React from "react";
import {
  BrowserRouter,
  Switch,
  Route
} from "react-router-dom";
import Login from "./pages/Login";
//import Quizzes from "./pages/quizzes";
import { onlyAuth, withLayout } from "./hocs";

const PrivateRoute = onlyAuth("/login")(Route);

const Router = () => {
  return (
    <BrowserRouter>
      <Switch>
        {/* <PrivateRoute path="/quizzes" exact component={withLayout(Quizzes)} /> */}
        <Route path="/login" exact component={Login} />
      </Switch>
    </BrowserRouter>
  );
};

export default Router;
