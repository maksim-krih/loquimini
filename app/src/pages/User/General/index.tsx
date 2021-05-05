import { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { CreateUser, User } from "../../../services/types";
import Api from "../../../services";
import { Button, Form, Input } from "antd";
import { useHistory, useParams } from "react-router-dom";
import { RouterPaths, validateMessages } from "../../../consts";
import { useToggle } from "ahooks";

const FormItem = Form.Item;
const { Password } = Input;

const General: FC<IProps> = (props: IProps) => {
  const { isCreate } = props;
  const classes = useStyles();
  const history = useHistory();
  const { id } = useParams<{id: string}>();
  const [form] = Form.useForm();
  const [data, setData] = useState<User>(Object);
  const [loading, setLoading] = useState(false);
  const [isEdit, { toggle }] = useToggle(false);
  const readonly = !isCreate && !isEdit;

  useEffect(() => {
    if (!isCreate) {
      getUser();
    }
  }, []);

  const getUser = () => {
    Api.User.getById(id)
    .then((response: User) => {
      setData(response);
      form.resetFields();
    })
  };

  const onCancel = () => {
    if (isCreate || !isEdit)
    {
      history.goBack();
    }
    else {
      form.resetFields();
      toggle();
    }
  }

  const onFinish = (values: any) => {
    if(isCreate) {
      const model = values as CreateUser;
    
      values.roles = ["User"];
      Api.User.create(model)
      .then(() => {
        history.push(RouterPaths.UserList);
      });
    }
    else {
      const model = values as User;
      model.id = data.id;

      Api.User.update(model)
      .then(() => {
        history.push(RouterPaths.UserList);
      });
    }
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log('Failed:', errorInfo);
  };

  return (
    <div className={classes.container}>
      {isCreate ? (
        <Button onClick={() => form.submit()}>
          Create
        </Button>
      ) : (
        <Button onClick={() => isEdit ? form.submit() : toggle()}>
          Edit
        </Button>
      )}
      <Button onClick={onCancel}>
        Cancel
      </Button>
      <Form
        labelCol={{ span: 3 }}
        wrapperCol={{ span: 10 }}
        form={form}
        name="user"
        initialValues={data.id ? data : {}}
        validateMessages={validateMessages}
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
      >
        <FormItem
          label="First Name"
          name="firstName"
          rules={[{ required: true }]}
        >
          <Input disabled={readonly} />
        </FormItem>
    
        <FormItem
          label="Last Name"
          name="lastName"
          rules={[{ required: true }]}
        >
          <Input disabled={readonly} />
        </FormItem>

        <FormItem
          label="Email"
          name="email"
          rules={[{ required: true }]}
        >
          <Input type="email" disabled={readonly} />
        </FormItem>

        {isCreate && (
          <FormItem
            label="Password"
            name="password"
            rules={[{ required: true }]}
          >
            <Password />
          </FormItem>
        )}
      </Form>
    </div>
  );
}

export default General;