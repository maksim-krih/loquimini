import React, { FC, useState } from "react";
import { Button, Input } from "antd";
import { IProps } from "./types";
import { useStyles } from "./styles";
import Api from "../../services";

const { Password } = Input;

const Login: FC<IProps> = (props: IProps) => {
  const classes = useStyles();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const onLogin = () => {
    Api.Auth.login({
      email,
      password
    });
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