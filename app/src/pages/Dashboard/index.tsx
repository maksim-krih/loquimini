import { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { DashboardInfo } from "../../services/types";
import Api from "../../services";
import { Typography } from "antd";

const { Title } = Typography;

const Dashboard: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const [info, setInfo] = useState<DashboardInfo | undefined>(undefined);

  useEffect(() => {
    getDashboard();
  }, []);

  const getDashboard = () => {
    Api.Dashboard.getInfo()
    .then((response: DashboardInfo) => {
      setInfo(response);
    })
  };

  return (
    <div className={classes.container}>
      {info && 
        <>
          <Title level={4}>Total sum</Title>
          <div>{info.totalSum}</div>
          <br />
          <Title level={4}>Total debts</Title>
          <div>{info.totalDebts}</div>
          <br />
          <Title level={4}>Filled</Title>
          <div>{info.currentFilled}/{info.totalFilled}</div>
          <br />
          <Title level={4}>Paid</Title>
          <div>{info.currentPaid}/{info.totalPaid}</div>
          <br />
        </>
      }
    </div>
  );
}

export default Dashboard;