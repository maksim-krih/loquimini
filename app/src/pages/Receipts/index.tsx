import { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { GridPager, Receipt } from "../../services/types";
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

  const getAddress = (receipt: Receipt) => {
    if (receipt.house)
    {
      return `${receipt.house.street}, ${receipt.house.number}`
    }
    else if (receipt.flat){
      return `${receipt.flat.street}, ${receipt.flat.houseNumber}, fl. ${receipt.flat.number}`
    }
  }
  
  const columns = [
    {
      title: 'Date',
      dataIndex: 'date',
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
      render: (_: any, record: Receipt) => (
        getAddress(record)
      )
    },
    {
      title: 'House Type',
      dataIndex: 'houseType',
      render: (houseType: HouseType, record: Receipt) => (
        HouseTypeLabel(houseType)
      )
    },
    {
      title: 'Receipt Type',
      dataIndex: 'type',
      render: (type: ReceiptType, record: Receipt) => (
        ReceiptTypeLabel(type)
      )
    },
    {
      title: 'Status',
      dataIndex: 'status',
      render: (status: ReceiptStatus, record: Receipt) => (
        ReceiptStatusLabel(status)
      )
    },
    {
      title: 'Old Indicator',
      dataIndex: 'oldIndicator',
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
    },
    {
      title: 'Debt',
      dataIndex: 'debt',
    },
    {
      title: 'Total',
      dataIndex: 'total',
    },
    {
      title: 'Paid',
      dataIndex: 'paid',
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

  const getReceiptsData = (pager: GridPager) => {
    setLoading(true);

    const request = DefaultGridRequest(pager);

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

  const handleTableChange = (pagination: TablePaginationConfig) => {
    setPager({...pager, current: pagination.current!});
    getReceiptsData({...pager, current: pagination.current!});
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