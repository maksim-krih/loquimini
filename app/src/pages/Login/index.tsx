import React, { FC, useState } from "react";
import { Button, Input, Typography } from "antd";
import { IProps } from "./types";
import { useStyles } from "./styles";
import Api, { AuthService } from "../../services";
import { useHistory } from "react-router-dom";
import { RouterPaths } from "../../consts";

const { Password } = Input;
const { Title } = Typography;

const Login: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const history = useHistory();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const onLogin = async () => {
    Api.Auth.login({
      email,
      password
    })
    .then(accountInfo => {
      AuthService.SetAccount(accountInfo);
      history.push(RouterPaths.Main);
    })
    .catch(e => {

    });
  };

  return (
    <div className={classes.container}>
      <div className={classes.login}>
        <Title level={3} style={{color: "#fff", textAlign: "center"}}>Loquimini</Title>
        <div style={{marginBottom: 10}}></div>
        <Input value={email} onChange={(e) => setEmail(e.target.value)} />
        <div style={{marginBottom: 10}}></div>
        <Password value={password} onChange={(e) => setPassword(e.target.value)} />
        <div style={{marginBottom: 10}}></div>

        <Button onClick={onLogin} style={{ marginLeft: 85}}>Sign in</Button>
      </div>
    </div>
    
  );
}

export default Login;