import { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { GridPager, GridRequest, User } from "../../../services/types";
import Api from "../../../services";
import { Button, Table, TablePaginationConfig, Typography } from "antd";
import { useHistory } from "react-router";
import { DefaultGridRequest, DefaultPager, RouterPaths } from "../../../consts";

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
      render: (text: string, record: User)=> `${record.firstName} ${record.lastName}`
    },
    {
      title: 'Email',
      dataIndex: 'email'
    },
    {
      title: '',
      dataIndex: '',
      render: (_: any, record: User) => (
        <Link onClick={(e) => {
          e.stopPropagation();
          onDelete(record.id)
        }}>
          Delete
        </Link>
      )
    },
  ];

  useEffect(() => {
    getUsersData(pager);
  }, []);

  const getUsersData = (pager: GridPager) => {
    setLoading(true);

    const request = DefaultGridRequest(pager);

    Api.User.getAllGrid(request)
    .then((response: any) => {
      setData(response.data);
      setPager({...pager, total: response.total});
    })
    .finally(() => {
      setLoading(false);
    });
  };

  const handleTableChange = (pagination: TablePaginationConfig) => {
    setPager({...pager, current: pagination.current!});
    getUsersData({...pager, current: pagination.current!});
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
        <Button onClick={onCreate}>
          Create
        </Button>
      </div>
      <Table
        columns={columns}
        rowKey={(record: User) => record.id}
        dataSource={data}
        pagination={pager}
        loading={loading}
        onChange={handleTableChange}
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