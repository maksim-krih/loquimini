import React, { createRef, FC, useRef, useState } from 'react';
import { IProps } from './types';
import { useStyles } from './styles';
import { Button, Input, Space, Table as AntTable, TablePaginationConfig } from 'antd';
import { SearchOutlined } from '@ant-design/icons';

const Table: FC<IProps> = (props: IProps) => {
  const { onRow, columns, data, pager, loading, setPager, getData } = props;
  const classes = useStyles();
  const searchInputRef = createRef<Input>();
  const [searchText, setSearchText] = useState("");
  const [searchedColumn, setSearchedColumn] = useState("");
  
  const handleTableChange = (pagination: TablePaginationConfig, filters: any, sorter: any) => {
    setPager({...pager, current: pagination.current! });
    getData({...pager, current: pagination.current!}, sorter);
  };

  const getColumnSearchProps = ( dataIndex: any ) => ({
    filterDropdown: ({ setSelectedKeys, selectedKeys, confirm, clearFilters }: any) => (
      <div style={{ padding: 8 }}>
        <Input
          ref={searchInputRef}
          placeholder={`Search ${dataIndex}`}
          value={selectedKeys[0]}
          onChange={e => setSelectedKeys(e.target.value ? [e.target.value] : [])}
          onPressEnter={() => handleSearch(selectedKeys, confirm, dataIndex)}
          style={{ marginBottom: 8, display: 'block' }}
        />
        <Space>
          <Button
            type="primary"
            onClick={() => handleSearch(selectedKeys, confirm, dataIndex)}
            icon={<SearchOutlined />}
            size="small"
            style={{ width: 90 }}
          >
            Search
          </Button>
          <Button onClick={() => handleReset(clearFilters)} size="small" style={{ width: 90 }}>
            Reset
          </Button>
        </Space>
      </div>
    ),
    filterIcon: (filtered: any) => <SearchOutlined style={{ color: filtered ? '#1890ff' : undefined }} />,
    onFilter: (value: any, record: any) =>
      record[dataIndex]
        ? record[dataIndex].toString().toLowerCase().includes(value.toLowerCase())
        : '',
    onFilterDropdownVisibleChange: (visible: any) => {
      if (visible) {
        setTimeout(() => searchInputRef.current!.select(), 100);
      }
    }
  });

  const handleSearch = (selectedKeys: any, confirm: any, dataIndex: any) => {
    confirm();
    getData({...pager, current: 1}, null, { fields: [dataIndex], value: selectedKeys[0] });
    setSearchText(selectedKeys[0]);
    setSearchedColumn(dataIndex);
  };

  const handleReset = (clearFilters: any) => {
    clearFilters();
    setSearchText('');
  };

  const columnsModel = columns.map(x => x.search ? {...x, ...getColumnSearchProps(x.dataIndex) } : x);

  return (
    <div className={classes.container}>
      <AntTable
        columns={columnsModel}
        rowKey={(record: any) => record.id}
        dataSource={data}
        pagination={pager}
        loading={loading}
        onChange={handleTableChange}
        onRow={onRow}
      />
    </div>
  );
};

export default Table;