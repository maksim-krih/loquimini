import React, { FC, useState } from "react";
import { Button, Input } from "antd";
import { IProps } from "./types";
import { useStyles } from "./styles";
import Api, { AuthService } from "../../services";
import { useHistory } from "react-router-dom";
import { RouterPaths } from "../../consts";

const { Password } = Input;

const Login: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const history = useHistory();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const onLogin = async () => {
    try {
      const accountInfo = await Api.Auth.login({
        email,
        password
      });
      
      AuthService.SetAccount(accountInfo);
      history.push(RouterPaths.Main);
    }
    catch {

    }
  };

  return (
    <div className={classes.container}>
      <Input value={email} onChange={(e) => setEmail(e.target.value)} />
      <Password value={password} onChange={(e) => setPassword(e.target.value)} />
      <Button onClick={onLogin}>Sign in</Button>
    </div>
  );
}

export default Login;