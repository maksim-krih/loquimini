import { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { GridPager, GridSorter, Receipt } from "../../services/types";
import Api, { AuthService } from "../../services";
import { Table, TablePaginationConfig, Typography } from "antd";
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

const { Link } = Typography;

const Receipts: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const [data, setData] = useState([]);
  const [fillModalVisible, setFillModalVisible] = useState(false);
  const [payModalVisible, setPayModalVisible] = useState(false);
  const [receiptId, setReceiptId] = useState("");
  const [pager, setPager] = useState(DefaultPager);
  const [loading, setLoading] = useState(false);
  
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
    },
    {
      title: 'Debt',
      dataIndex: 'debt',
      sorter: true,
    },
    {
      title: 'Total',
      dataIndex: 'total',
      sorter: true,
    },
    {
      title: 'Paid',
      dataIndex: 'paid',
      sorter: true,
    },
    {
      title: '',
      dataIndex: '',
      render: (_: any, record: Receipt) => {
        switch (record.status) {
          case ReceiptStatus.Created:
            return (
              <Link onClick={(e) => {
                setReceiptId(record.id);
                setFillModalVisible(true);
              }}>
                Fill
              </Link>
            );
          case ReceiptStatus.Filled:
            return (
              <Link onClick={(e) => {
                setReceiptId(record.id);
                setPayModalVisible(true);
              }}>
                Pay
              </Link>
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