import React, { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { GridPager, GridSorter, Receipt } from "../../services/types";
import Api, { AuthService } from "../../services";
import { Button, Table, TablePaginationConfig, Typography } from "antd";
import { DefaultGridRequest, DefaultPager } from "../../consts";
import { 
  HouseType, 
  HouseTypeLabel, 
  ReceiptStatus, 
  ReceiptStatusLabel,
  ReceiptType, 
  ReceiptTypeLabel 
} from "../../enums";
import FillModal from "./fillModal";
import PayModal from "./payModal";
import { CheckCircleOutlined, CloseCircleOutlined, DollarCircleOutlined, EditOutlined, ExclamationCircleOutlined } from "@ant-design/icons";

const { Link } = Typography;

const Receipts: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const [data, setData] = useState([]);
  const [fillModalVisible, setFillModalVisible] = useState(false);
  const [payModalVisible, setPayModalVisible] = useState(false);
  const [receiptId, setReceiptId] = useState("");
  const [pager, setPager] = useState(DefaultPager);
  const [loading, setLoading] = useState(false);

  const getStatus = (status: ReceiptStatus) => {
    switch (status) {
      case ReceiptStatus.Created:
        return (
          <div>
            <span><CloseCircleOutlined /></span>
            <span>{ReceiptStatusLabel(status)}</span>
          </div>
        );
      case ReceiptStatus.Filled:
        return (
          <div>
            <span><ExclamationCircleOutlined /></span>
            <span>{ReceiptStatusLabel(status)}</span>
          </div>
        );
      case ReceiptStatus.Paid:
        return (
          <div>
            <span><CheckCircleOutlined /></span>
            <span>{ReceiptStatusLabel(status)}</span>
          </div>
        );
    }
  }
  
  const columns = [
    {
      title: 'Date',
      dataIndex: 'date',
      sorter: true,
      render: (date: Date, record: Receipt) => (
        new Date(date).toLocaleDateString(undefined, {
          month: "long",
          year: "numeric",
        })
      )
    },
    {
      title: 'Address',
      dataIndex: 'address',
      sorter: true,
    },
    {
      title: 'House Type',
      dataIndex: 'houseType',
      sorter: true,
      render: (houseType: HouseType, record: Receipt) => (
        HouseTypeLabel(houseType)
      )
    },
    {
      title: 'Receipt Type',
      dataIndex: 'type',
      sorter: true,
      render: (type: ReceiptType, record: Receipt) => (
        ReceiptTypeLabel(type)
      )
    },
    {
      title: 'Status',
      dataIndex: 'status',
      sorter: true,
      render: (status: ReceiptStatus, record: Receipt) => (
        ReceiptStatusLabel(status)
      )
    },
    {
      title: 'Old Indicator',
      dataIndex: 'oldIndicator',
      sorter: true,
      render: (oldIndicator: number, record: Receipt) => {
        return record.type === ReceiptType.ColdWater ||
          record.type === ReceiptType.Electricity ||
          record.type === ReceiptType.HotWater ||
          record.type === ReceiptType.Gas
        ? oldIndicator : "—";
      }
    },
    {
      title: 'New Indicator',
      dataIndex: 'newIndicator',
      sorter: true,
      render: (newIndicator: number, record: Receipt) => {
        return record.type === ReceiptType.ColdWater ||
          record.type === ReceiptType.Electricity ||
          record.type === ReceiptType.HotWater ||
          record.type === ReceiptType.Gas
        ? newIndicator : "—";
      }
    },
    {
      title: 'Rate',
      dataIndex: 'rate',
      sorter: true,
      render: (rate: number, record: Receipt) => {
        return `$${rate}`;
      }
    },
    {
      title: 'Debt',
      dataIndex: 'debt',
      sorter: true,
      render: (debt: number, record: Receipt) => {
        return `$${debt}`;
      }
    },
    {
      title: 'Total',
      dataIndex: 'total',
      sorter: true,
      render: (total: number, record: Receipt) => {
        return `$${total}`;
      }
    },
    {
      title: 'Paid',
      dataIndex: 'paid',
      sorter: true,
      render: (paid: number, record: Receipt) => {
        return `$${paid}`;
      }
    },
    {
      title: '',
      dataIndex: '',
      render: (_: any, record: Receipt) => {
        switch (record.status) {
          case ReceiptStatus.Created:
            return (
              <Button 
                onClick={(e) => {
                  setReceiptId(record.id);
                  setFillModalVisible(true);
                }}
                className={classes.gridButton}
              >
                <EditOutlined />
              </Button>
            );
          case ReceiptStatus.Filled:
            return (
              <Button 
                onClick={(e) => {
                  setReceiptId(record.id);
                  setPayModalVisible(true);
                }}
                className={classes.gridButton}
              >
                <DollarCircleOutlined />
              </Button>
            );
        }
      },
    }
  ];

  useEffect(() => {
    getReceiptsData(pager);
  }, []);

  const getReceiptsData = (pager: GridPager, sorter?: GridSorter) => {
    setLoading(true);

    const request = DefaultGridRequest(pager, sorter);

    Api.Receipt.getByUserIdGrid(AuthService.User.id, request)
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
    setPager({...pager, current: pagination.current!});
    getReceiptsData({...pager, current: pagination.current!}, sorter);
  };

  const fillModalCancel = () => {
    setFillModalVisible(false);
  };

  const payModalCancel = () => {
    setPayModalVisible(false);
  };

  return (
    <div className={classes.container}>
      {fillModalVisible && (
        <FillModal 
          visible={fillModalVisible} 
          onCancel={fillModalCancel}
          receiptId={receiptId}
          reloadGrid={() => getReceiptsData(pager)}
        />
      )}
      {payModalVisible && (
        <PayModal 
          visible={payModalVisible} 
          onCancel={payModalCancel}
          receiptId={receiptId}
          reloadGrid={() => getReceiptsData(pager)}
        />
      )}
      <Table
        columns={columns}
        rowKey={(record: Receipt) => record.id}
        dataSource={data}
        pagination={pager}
        loading={loading}
        onChange={handleTableChange}
      />
    </div>
  );
}

export default Receipts;