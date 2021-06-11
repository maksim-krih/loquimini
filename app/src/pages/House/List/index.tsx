import React, { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { GridPager, GridRequest, GridSearch, GridSorter, House, User } from "../../../services/types";
import Api from "../../../services";
import { Button, TablePaginationConfig, Typography } from "antd";
import { useHistory } from "react-router";
import { DefaultGridRequest, DefaultPager, RouterPaths } from "../../../consts";
import { Table } from "../../../components";
import { SorterResult } from "antd/lib/table/interface";
import { DeleteOutlined } from "@ant-design/icons";
import { HouseType, HouseTypeLabel } from "../../../enums";

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
      search: true
    },
    {
      title: 'Number',
      dataIndex: 'number',
      sorter: true,
      search: true
    },
    {
      title: 'Type',
      dataIndex: 'type',
      sorter: true,
      filters: [
        {
          text: 'Private',
          value: '0',
        },
        {
          text: 'Apartment',
          value: '1',
        },
      ],
      render: (type: HouseType, record: House) => (
        HouseTypeLabel(type)
      )
    },
    {
      title: '',
      dataIndex: '',
      render: (_: any, record: House) => (
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
    getHousesData(pager);
  }, []);

  const getHousesData = (pager: GridPager, sorter?: GridSorter, search?: GridSearch) => {
    setLoading(true);

    const request = DefaultGridRequest(pager, sorter, search);

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
        <Button onClick={onCreate} type="primary" className={classes.actionButton}>
          Create
        </Button>
      </div>
      <Table
        columns={columns}
        data={data}
        pager={pager}
        loading={loading}
        getData={getHousesData}
        setPager={setPager}
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