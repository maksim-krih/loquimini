import React from "react";
import {
  BrowserRouter,
  Switch,
  Route
} from "react-router-dom";
import Login from "./pages/login";
import Quizzes from "./pages/quizzes";
import { ScrollToTop } from "./components";
import { onlyAuth, withLayout } from "./hocs";

const PrivateRoute = onlyAuth("/login")(Route);

const Router = () => {
  return (
    <BrowserRouter>
      <ScrollToTop>
        <Switch>
          <PrivateRoute path="/quizzes" exact component={withLayout(Quizzes)} />
          <Route path="/login" exact component={Login} />
        </Switch>
      </ScrollToTop>
    </BrowserRouter>
  );
};

export default Router;
