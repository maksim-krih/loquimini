import { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { GridPager, GridRequest, GridSorter, House, User } from "../../../services/types";
import Api from "../../../services";
import { Button, Table, TablePaginationConfig, Typography } from "antd";
import { useHistory } from "react-router";
import { DefaultGridRequest, DefaultPager, RouterPaths } from "../../../consts";
import { SorterResult } from "antd/lib/table/interface";

const { Link } = Typography;

const List: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const history = useHistory();
  const [data, setData] = useState([]);
  const [pager, setPager] = useState(DefaultPager);
  const [loading, setLoading] = useState(false);

  const columns = [
    {
      title: 'Street',
      dataIndex: 'street',
      sorter: true,
    },
    {
      title: 'Number',
      dataIndex: 'number',
      sorter: true,
    },
    {
      title: '',
      dataIndex: '',
      render: (_: any, record: House) => (
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
    getHousesData(pager);
  }, []);

  const getHousesData = (pager: GridPager, sorter?: GridSorter) => {
    setLoading(true);

    const request = DefaultGridRequest(pager, sorter);

    Api.House.getAllGrid(request)
    .then((response: any) => {
      setData(response.data);
      setPager({...pager, total: response.total});
    })
    .catch(e => {
      
    })
    .finally(() => {
      setLoading(false);
    });
  };

  const handleTableChange = (pagination: TablePaginationConfig, filters: any, sorter: any) => {
    setPager({...pager, current: pagination.current! });
    getHousesData({...pager, current: pagination.current!}, sorter);
  };

  const onCreate = () => {
    history.push(RouterPaths.CreateHouse);
  }

  const onDelete = (id: string) => {
    setLoading(true);

    Api.House.deleteById(id)
    .then((response: any) => {
      if (response) {
        setPager({...pager, current: 1});
        getHousesData({...pager, current: 1});
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
        rowKey={(record: House) => record.id}
        dataSource={data}
        pagination={pager}
        loading={loading}
        onChange={handleTableChange}
        onRow={(record: House) => {
          return {
            onClick: () => history.push(RouterPaths.GeneralHouse(record.id)),
          };
        }}
      />
    </div>
  );
}

export default List;