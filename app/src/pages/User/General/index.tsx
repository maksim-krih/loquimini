import { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { CreateUser } from "../../../services/types";
import Api from "../../../services";
import { Button, Form, Input } from "antd";
import { useHistory } from "react-router-dom";
import { RouterPaths, validateMessages } from "../../../consts";

const FormItem = Form.Item;
const { Password } = Input;

const General: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const history = useHistory();
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    getUsersData();
  }, []);

  const getUsersData = () => {
  };

  const onCancel = () => {
    history.goBack()
  }

  const onFinish = (values: CreateUser) => {
    values.roles = ["User"];
    Api.User.create(values)
    .then(() => {
      history.push(RouterPaths.UserList);
    });
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log('Failed:', errorInfo);
  };

  const renderCreate = (
    <div>
      <Button onClick={() => form.submit()}>
        Create
      </Button>
      <Button onClick={onCancel}>
        Cancel
      </Button>
      <Form
        labelCol={{ span: 3 }}
        wrapperCol={{ span: 10 }}
        form={form}
        name="user"
        validateMessages={validateMessages}
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
      >
        <FormItem
          label="First Name"
          name="firstName"
          rules={[{ required: true }]}
        >
          <Input />
        </FormItem>
    
        <FormItem
          label="Last Name"
          name="lastName"
          rules={[{ required: true }]}
        >
          <Input />
        </FormItem>

        <FormItem
          label="Email"
          name="email"
          rules={[{ required: true }]}
        >
          <Input type="email" />
        </FormItem>

        <FormItem
          label="Password"
          name="password"
          rules={[{ required: true }]}
        >
          <Password />
        </FormItem>
      </Form>
    </div>
  );

  return (
    <div className={classes.container}>
      {props.isCreate ? renderCreate
      : (
        <div></div>
      )}
    </div>
  );
}

export default General;