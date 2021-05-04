import React, { Component } from "react";
import {
  BrowserRouter,
  Switch,
  Route
} from "react-router-dom";
import Login from "./pages/Login";
import { onlyAuth } from "./hocs";
import { RouterPaths } from "./consts";
import { UserList } from "./pages/User";
import { Layout } from "./components";
import UserGeneral from "./pages/User/General";
import { HouseList } from "./pages/House";
import HouseGeneral from "./pages/House/General";

const PrivateRoute = onlyAuth(RouterPaths.Login)(Route);

const Router = () => {
  return (
    <BrowserRouter>
      <Switch>
        <Route path={RouterPaths.Login} exact>
          <Login />
        </Route>

        <PrivateRoute path={RouterPaths.Main} exact>
          <Layout />
        </PrivateRoute>
        
        <PrivateRoute path={RouterPaths.UserList} exact>
          <Layout>
            <UserList />
          </Layout>
        </PrivateRoute>
        
        <PrivateRoute path={RouterPaths.CreateUser} exact>
          <Layout>
            <UserGeneral isCreate />
          </Layout>
        </PrivateRoute>

        <PrivateRoute path={RouterPaths.HouseList} exact>
          <Layout>
            <HouseList />
          </Layout>
        </PrivateRoute>

        <PrivateRoute path={RouterPaths.CreateHouse} exact>
          <Layout>
            <HouseGeneral isCreate />
          </Layout>
        </PrivateRoute>
      </Switch>
    </BrowserRouter>
  );
};

export default Router;
