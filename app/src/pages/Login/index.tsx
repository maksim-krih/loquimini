import React, { FC, useState } from "react";
import { Button, Form, Input, Typography } from "antd";
import { IProps } from "./types";
import { useStyles } from "./styles";
import Api, { AuthService } from "../../services";
import { useHistory } from "react-router-dom";
import { RouterPaths, validateMessages } from "../../consts";
import { Login as LoginModel } from "../../services/auth/types";
import logoImg from "../../assets/img/logo.png";

const { Password } = Input;
const { Title } = Typography;

const Login: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const history = useHistory();
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();

  const onFinish = (values: any) => {
    setLoading(true);
    const model = values as LoginModel;
    Api.Auth.login(model)
    .then(accountInfo => {
      AuthService.SetAccount(accountInfo);
      history.push(RouterPaths.Main);
    })
    .catch(e => {
      form.setFields([
        {
          name: 'email',
          errors: ['Wrong email or password'],
        },
     ]);
    })
    .finally(() => {
      setLoading(false);
    });
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log('Failed:', errorInfo);
  };

  return (
    <div className={classes.container}>
      <div className={classes.login}>
        <div style={{ display: "flex", justifyContent: "center", alignItems: "center", marginBottom: 10 }}>
          <img src={logoImg} alt="logo" style={{ width: 48, marginRight: 10 }}/>
          <Title level={3} style={{color: "#001529"}}>Loquimini</Title>
        </div>
        
        <Form
          labelCol={{ span: 6 }}
          wrapperCol={{ span: 16 }}
          form={form}
          name="login"
          validateMessages={validateMessages}
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
        >
          <Form.Item
            label="Email"
            name="email"
            rules={[{ required: true }]}
          >
            <Input  style={{ borderRadius: 5 }}/>
          </Form.Item>

          <Form.Item
            label="Password"
            name="password"

            rules={[{ required: true }]}
          >
            <Password  style={{ borderRadius: 5 }}/>
          </Form.Item>
        </Form>
        <div style={{marginBottom: 10}}></div>

        <Button 
          onClick={() => form.submit()} 
          style={{ width: "100%", }}
          loading={loading}
          type="primary"
        >
          Sign in
        </Button>
      </div>
    </div>
    
  );
}

export default Login;