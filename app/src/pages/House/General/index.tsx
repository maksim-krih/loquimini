import { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { CreateHouse, House, User } from "../../../services/types";
import Api from "../../../services";
import { Button, Form, Input, InputNumber, Select, Typography } from "antd";
import { useHistory, useParams } from "react-router-dom";
import { RouterPaths, validateMessages } from "../../../consts";
import { HouseType, HouseTypeSelect } from "../../../enums";
import { MinusCircleOutlined, PlusOutlined } from "@ant-design/icons";
import { useToggle } from "ahooks";

const FormItem = Form.Item;
const { Title } = Typography;

const General: FC<IProps> = (props: IProps) => {
  const { isCreate } = props;
  const classes = useStyles();
  const history = useHistory();
  const { id } = useParams<{id: string}>();
  const [form] = Form.useForm();
  const [users, setUsers] = useState<Array<User>>([]);
  const [data, setData] = useState<House>(Object);
  const [houseType, setHouseType] = useState(HouseType.Private);
  const [loading, setLoading] = useState(false);
  const [isEdit, { toggle }] = useToggle(false);
  const readonly = !isCreate && !isEdit;

  useEffect(() => {
    getUsers();

    if (!isCreate) {
      getHouse();
    }
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

  const getHouse = () => {
    Api.House.getById(id)
    .then((response: House) => {
      setData(response);
      setHouseType(response.type);
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
      const model = values as CreateHouse;

      Api.House.create(model)
      .then(() => {
        history.push(RouterPaths.HouseList);
      });
    }
    else {
      const model = values as House;
      model.id = data.id;

      Api.House.update(model)
      .then(() => {
        history.push(RouterPaths.HouseList);
      });
    }
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log('Failed:', errorInfo);
  };

  const onTypeChange = (value: HouseType) => {
    setHouseType(value);
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
        name="house"
        validateMessages={validateMessages}
        initialValues={data.id ? data : {
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
          <Input disabled={readonly}/>
        </FormItem>
    
        <FormItem
          label="Number"
          name="number"
          rules={[{ required: true }]}
        >
          <Input disabled={readonly}/>
        </FormItem>

        <FormItem
          label="Type"
          name="type"
          rules={[{ required: true }]}
        >
          <Select
            options={HouseTypeSelect}
            onChange={onTypeChange}
            disabled={readonly}
          />
        </FormItem>
        {houseType === HouseType.Private ? (
          <>
            <FormItem
              label="User"
              name="userId"
              rules={[{ required: true }]}
            >
              <Select
                options={users.map(x => ({
                  value: x.id,
                  label: x.email
                }))}
                disabled={readonly}
              />
            </FormItem>
            <FormItem
              label="Area"
              name={["info", "area"]}
              rules={[{ required: true }]}
            >
              <InputNumber disabled={readonly} />
            </FormItem>
            <Title level={5}>Default Indicators</Title>
            <FormItem
              label="Cold Water"
              name="coldWater"
            >
              <InputNumber disabled={readonly} />
            </FormItem>
            <FormItem
              label="Hot Water"
              name="hotWater"
            >
              <InputNumber disabled={readonly} />
            </FormItem>
            <FormItem
              label="Electricity"
              name="electricity"
            >
              <InputNumber disabled={readonly} />
            </FormItem>
            <FormItem
              label="Gas"
              name="gas"
            >
              <InputNumber disabled={readonly} />
            </FormItem>
          </>
        ) : (
          <>
            <Title level={4}>Flats</Title>
            <Form.List name="flats">
              {(fields, { add, remove }) => (
                <>
                  {fields.map(({ key, name, fieldKey, ...restField }, index) => (
                    <>
                      <Title level={5}>#{index + 1}</Title>
                      <MinusCircleOutlined onClick={() => remove(name)} />
                      <Form.Item
                        {...restField}
                        name={[name, 'number']}
                        fieldKey={[fieldKey, 'number']}
                        label="Number"
                        rules={[{ required: true }]}
                      >
                        <Input disabled={readonly} />
                      </Form.Item>
                      <Form.Item
                        {...restField}
                        name={[name, 'userId']}
                        fieldKey={[fieldKey, 'userId']}
                        label="User"
                        rules={[{ required: true }]}
                      >
                        <Select
                          options={users.map(x => ({
                            value: x.id,
                            label: x.email
                          }))}
                          disabled={readonly}
                        />
                      </Form.Item>
                      <Form.Item
                        {...restField}
                        name={[name, 'info', 'area']}
                        fieldKey={[fieldKey, 'info', 'area']}
                        label="Area"
                        rules={[{ required: true }]}
                      >
                        <InputNumber disabled={readonly} />
                      </Form.Item>
                      <Title level={5}>Default Indicators</Title>
                      <FormItem
                        label="Cold Water"
                        name="coldWater"
                      >
                        <InputNumber disabled={readonly} />
                      </FormItem>
                      <FormItem
                        label="Hot Water"
                        name="hotWater"
                      >
                        <InputNumber disabled={readonly} />
                      </FormItem>
                      <FormItem
                        label="Electricity"
                        name="electricity"
                      >
                        <InputNumber disabled={readonly} />
                      </FormItem>
                      <FormItem
                        label="Gas"
                        name="gas"
                      >
                        <InputNumber disabled={readonly} />
                      </FormItem>
                    </>
                  ))}
                  <Form.Item>
                    <Button type="dashed" onClick={() => add()} block icon={<PlusOutlined />}>
                      Add field
                    </Button>
                  </Form.Item>
                </>
              )}
            </Form.List>
          </>
        )}
      </Form>
    </div>
  );
}

export default General;