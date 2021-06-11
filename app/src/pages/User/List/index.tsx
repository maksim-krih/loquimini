import React, { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { GridPager, GridRequest, GridSearch, GridSorter, User } from "../../../services/types";
import Api from "../../../services";
import { Button, TablePaginationConfig, Typography } from "antd";
import { useHistory } from "react-router";
import { DefaultGridRequest, DefaultPager, RouterPaths } from "../../../consts";
import { DeleteOutlined } from "@ant-design/icons";
import { Table } from "../../../components";

const { Link } = Typography;

const List: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const history = useHistory();
  const [data, setData] = useState([]);
  const [pager, setPager] = useState(DefaultPager);
  const [loading, setLoading] = useState(false);

  const columns = [
    {
      title: 'Name',
      dataIndex: 'userName',
      sorter: true,
      search: true,
      render: (text: string, record: User)=> `${record.firstName} ${record.lastName}`
    },
    {
      title: 'Email',
      sorter: true,
      dataIndex: 'email',
      search: true,
    },
    {
      title: '',
      dataIndex: '',
      render: (_: any, record: User) => (
        <Button 
          onClick={(e) => {
            e.stopPropagation();
            onDelete(record.id)
          }}
          className={classes.gridButton}
        >
          <DeleteOutlined />
        </Button>
      )
    },
  ];

  useEffect(() => {
    getUsersData(pager);
  }, []);

  const getUsersData = (pager: GridPager, sorter?: GridSorter, search?: GridSearch) => {
    setLoading(true);

    const request = DefaultGridRequest(pager, sorter, search);

    Api.User.getAllGrid(request)
    .then((response: any) => {
      setData(response.data);
      setPager({...pager, total: response.total});
    })
    .finally(() => {
      setLoading(false);
    });
  };

  const handleTableChange = (pagination: TablePaginationConfig, filters: any, sorter: any) => {
    setPager({...pager, current: pagination.current!});
    getUsersData({...pager, current: pagination.current!}, sorter);
  };

  const onCreate = () => {
    history.push(RouterPaths.CreateUser);
  }

  const onDelete = (id: string) => {
    setLoading(true);

    Api.User.deleteById(id)
    .then((response: any) => {
      if (response) {
        setPager({...pager, current: 1});
        getUsersData({...pager, current: 1});
      }
    })
    .catch(e => {
    
    })
    .finally(() => {
      setLoading(false);
    });
  }

  return (
    <div className={classes.container}>
      <div className={classes.actionButtons}>
        <Button onClick={onCreate} style={{ borderRadius: 5 }} type="primary">
          Create
        </Button>
      </div>
      <Table
        columns={columns}
        data={data}
        pager={pager}
        loading={loading}
        getData={getUsersData}
        setPager={setPager}
        onRow={(record: User) => {
          return {
            onClick: () => history.push(RouterPaths.GeneralUser(record.id)),
          };
        }}
      />
    </div>
  );
}

export default List;