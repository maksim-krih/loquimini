import { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { CreateHouse, Flat, House, User } from "../../../services/types";
import Api from "../../../services";
import { Button, Form, Input, InputNumber, Select, Typography } from "antd";
import { useHistory, useParams } from "react-router-dom";
import { RouterPaths, validateMessages } from "../../../consts";
import { HouseType, HouseTypeSelect, ReceiptType } from "../../../enums";
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
      const model = response as any;
      
      if (model.type === HouseType.Private) {
        model.coldWater = response.info!.defaultIndicators.find(x => x.type === ReceiptType.ColdWater)?.value;
        model.hotWater = response.info!.defaultIndicators.find(x => x.type === ReceiptType.HotWater)?.value;
        model.gas = response.info!.defaultIndicators.find(x => x.type === ReceiptType.Gas)?.value;
        model.electricity = response.info!.defaultIndicators.find(x => x.type === ReceiptType.Electricity)?.value;
      }
      else if (model.type === HouseType.Apartment) {
        model.flats.forEach((x: any, index: number) => {
          x.coldWater = response.flats[index].info.defaultIndicators.find(x => x.type === ReceiptType.ColdWater)?.value;
          x.hotWater = response.flats[index].info.defaultIndicators.find(x => x.type === ReceiptType.HotWater)?.value;
          x.gas = response.flats[index].info.defaultIndicators.find(x => x.type === ReceiptType.Gas)?.value;
          x.electricity = response.flats[index].info.defaultIndicators.find(x => x.type === ReceiptType.Electricity)?.value;
        })
      }

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

      if (model.type === HouseType.Private) {
        model.info!.defaultIndicators = [
          {
            type: ReceiptType.ColdWater,
            value: values.coldWater
          },
          {
            type: ReceiptType.HotWater,
            value: values.hotWater
          },
          {
            type: ReceiptType.Gas,
            value: values.gas
          },
          {
            type: ReceiptType.Electricity,
            value: values.electricity
          }
        ];
      }
      else if (model.type === HouseType.Apartment) {
        model.flats.forEach((x: Flat, index: number) => {
          x.info.defaultIndicators = [
            {
              type: ReceiptType.ColdWater,
              value: values.flats[index].coldWater
            },
            {
              type: ReceiptType.HotWater,
              value: values.flats[index].hotWater
            },
            {
              type: ReceiptType.Gas,
              value: values.flats[index].gas
            },
            {
              type: ReceiptType.Electricity,
              value: values.flats[index].electricity
            }
          ]
        })
      }
      
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
      <div className={classes.actionButtons}>
        {isCreate ? (
          <Button 
            onClick={() => form.submit()} 
            className={classes.actionButton} 
            type="primary" 
            style={{ marginRight: 5 }}
          >
            Create
          </Button>
        ) : (
          <Button 
            onClick={() => isEdit ? form.submit() : toggle()} 
            className={classes.actionButton}
            type="primary" 
            style={{ marginRight: 5 }}  
          >
            Edit
          </Button>
        )}
        <Button onClick={onCancel} className={classes.actionButton}>
          Cancel
        </Button>
      </div>
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
          <Input disabled={readonly} style={{ borderRadius: 5 }}/>
        </FormItem>
    
        <FormItem
          label="Number"
          name="number"
          rules={[{ required: true }]}
        >
          <Input disabled={readonly} style={{ borderRadius: 5 }} />
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
            style={{ borderRadius: 5 }}
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
                style={{ borderRadius: 5 }}
              />
            </FormItem>
            <FormItem
              label="Area"
              name={["info", "area"]}
              rules={[{ required: true }]}
            >
              <InputNumber disabled={readonly} style={{ borderRadius: 5 }} min={1}/>
            </FormItem>
            <Title level={5}>Default Indicators</Title>
            <FormItem
              label="Cold Water"
              name="coldWater"
            >
              <InputNumber disabled={readonly || !isCreate} style={{ borderRadius: 5 }} min={0}/>
            </FormItem>
            <FormItem
              label="Hot Water"
              name="hotWater"
            >
              <InputNumber disabled={readonly || !isCreate} style={{ borderRadius: 5 }} min={0}/>
            </FormItem>
            <FormItem
              label="Electricity"
              name="electricity"
            >
              <InputNumber disabled={readonly || !isCreate} style={{ borderRadius: 5 }} min={0}/>
            </FormItem>
            <FormItem
              label="Gas"
              name="gas"
            >
              <InputNumber disabled={readonly || !isCreate} style={{ borderRadius: 5 }} min={0}/>
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
                      <div style={{ display: "flex" }}>
                        <Title level={5} style={{ marginRight: 5 }}>#{index + 1}</Title>
                        <MinusCircleOutlined onClick={() => remove(name)} width={32} style={{ marginTop: 6 }}/>
                      </div>
                      <Form.Item
                        {...restField}
                        name={[name, 'number']}
                        fieldKey={[fieldKey, 'number']}
                        label="Number"
                        rules={[{ required: true }]}
                      >
                        <Input disabled={readonly} style={{ borderRadius: 5 }} />
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
                          style={{ borderRadius: 5 }}
                        />
                      </Form.Item>
                      <Form.Item
                        {...restField}
                        name={[name, 'info', 'area']}
                        fieldKey={[fieldKey, 'info', 'area']}
                        label="Area"
                        rules={[{ required: true }]}
                      >
                        <InputNumber disabled={readonly} style={{ borderRadius: 5 }} min={1}/>
                      </Form.Item>
                      <Title level={5}>Default Indicators</Title>
                      <FormItem
                        {...restField}
                        name={[name, 'coldWater']}
                        fieldKey={[fieldKey, 'coldWater']}
                        label="Cold Water"
                      >
                        <InputNumber disabled={readonly || !isCreate} style={{ borderRadius: 5 }} min={0}/>
                      </FormItem>
                      <FormItem
                        {...restField}
                        name={[name, 'hotWater']}
                        fieldKey={[fieldKey, 'hotWater']}
                        label="Hot Water"
                      >
                        <InputNumber disabled={readonly || !isCreate} style={{ borderRadius: 5 }} min={0} />
                      </FormItem>
                      <FormItem
                        {...restField}
                        name={[name, 'electricity']}
                        fieldKey={[fieldKey, 'electricity']}
                        label="Electricity"
                      >
                        <InputNumber disabled={readonly || !isCreate} style={{ borderRadius: 5 }} min={0}/>
                      </FormItem>
                      <FormItem
                        {...restField}
                        name={[name, 'gas']}
                        fieldKey={[fieldKey, 'gas']}
                        label="Gas"
                      >
                        <InputNumber disabled={readonly || !isCreate} style={{ borderRadius: 5 }} min={0}/>
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