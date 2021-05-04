import { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { CreateHouse, GridRequest, User } from "../../../services/types";
import Api from "../../../services";
import { Button, Dropdown, Form, Input, InputNumber, Select } from "antd";
import { useHistory } from "react-router-dom";
import { RouterPaths, validateMessages } from "../../../consts";
import { HouseType, HouseTypeSelect } from "../../../enums";

const FormItem = Form.Item;

const General: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const history = useHistory();
  const [form] = Form.useForm();
  const [users, setUsers] = useState<Array<User>>([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    getUsers();
  }, []);

  const getUsers = () => {
    setLoading(true);

    Api.User.getAll()
    .then((response: Array<User>) => {
      setUsers(response);
    })
    .finally(() => {
      setLoading(false);
    });
  };

  const onCancel = () => {
    history.goBack()
  }

  const onFinish = (values: CreateHouse) => {
    
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
        name="house"
        validateMessages={validateMessages}
        initialValues={{
          type: HouseType.Private
        }}
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
      >
        <FormItem
          label="Street"
          name="street"
          rules={[{ required: true }]}
        >
          <Input />
        </FormItem>
    
        <FormItem
          label="Number"
          name="number"
          rules={[{ required: true }]}
        >
          <Input />
        </FormItem>

        <FormItem
          label="Type"
          name="type"
        >
          <Select
            options={HouseTypeSelect}
          />
        </FormItem>
        <FormItem
          label="User"
          name="userId"
        >
          <Select
            options={users.map(x => ({
              value: x.id,
              label: x.email
            }))}
          />
        </FormItem>
        <FormItem
          label="Area"
          name={["info", "area"]}
        >
          <InputNumber />
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