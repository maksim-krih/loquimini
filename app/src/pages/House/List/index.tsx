import { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { GridRequest, House, User } from "../../../services/types";
import Api from "../../../services";
import { Button, Table } from "antd";
import { useHistory } from "react-router";
import { RouterPaths } from "../../../consts";

const columns = [
  {
    title: 'Street',
    dataIndex: 'street',
  },
  {
    title: 'Number',
    dataIndex: 'number',
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
    getHousesData();
  }, []);

  const getHousesData = () => {
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

    Api.House.getAllGrid(request)
    .then((response: any) => {
      setData(response.data);
    })
    .catch(e => {
      
    })
    .finally(() => {
      setLoading(false);
    });
  };

  const handleTableChange = (pagination: any) => {
    setPager(pagination);
    getHousesData();
  };

  const onCreate = () => {
    history.push(RouterPaths.CreateHouse);
  }

  return (
    <div className={classes.container}>
      <Button onClick={onCreate}>
        Create
      </Button>
      <Table
        columns={columns}
        rowKey={(record: House) => record.id}
        dataSource={data}
        pagination={pager}
        loading={loading}
        onChange={handleTableChange}
      />
    </div>
  );
}

export default List;