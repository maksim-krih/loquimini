import { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { GridRequest, User } from "../../../services/types";
import Api from "../../../services";
import { Button, Table } from "antd";
import { useHistory } from "react-router";
import { RouterPaths } from "../../../consts";

const columns = [
  {
    title: 'Name',
    dataIndex: 'userName',
    render: (text: string, record: User)=> `${record.firstName} ${record.lastName}`
  },
  {
    title: 'Email',
    dataIndex: 'email'
  },
];

const List: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const history = useHistory();
  const [data, setData] = useState([]);
  const [pager, setPager] = useState({
    current: 1,
    pageSize: 10
  });
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    getUsersData();
  }, []);

  const getUsersData = () => {
    setLoading(true);

    const request: GridRequest = {
      filters: [],
      pager,
      search: {
        fields: [],
        value: ""
      },
      sorter: []
    };

    Api.User.getAllGrid(request)
    .then((response: any) => {
      setData(response.data);
    })
    .finally(() => {
      setLoading(false);
    });
  };

  const handleTableChange = (pagination: any) => {
    setPager(pagination);
    getUsersData();
  };

  const onCreate = () => {
    history.push(RouterPaths.CreateUser);
  }

  return (
    <div className={classes.container}>
      <Button onClick={onCreate}>
        Create
      </Button>
      <Table
        columns={columns}
        rowKey={(record: User) => record.id}
        dataSource={data}
        pagination={pager}
        loading={loading}
        onChange={handleTableChange}
      />
    </div>
  );
}

export default List;