import React, { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { GridFilter, GridPager, GridRequest, GridSorter, User } from "../../../services/types";
import Api from "../../../services";
import { Table } from "antd";

const columns = [
  {
    title: 'UserName',
    dataIndex: 'userName',
    render: (text: string, record: User)=> `${record.firstName} ${record.lastName}`
  },
];

const General: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
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

    Api.User.getAll(request).finally(() => {
      setLoading(false);
    });
  };

  const handleTableChange = (pagination: any, filters: any, sorting: any, extra: any) => {
    setPager(pagination);
    getUsersData();
  };

  return (
    <div className={classes.container}>
    </div>
  );
}

export default General;