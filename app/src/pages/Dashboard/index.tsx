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
  const date = new Date().toLocaleDateString(undefined, {
    month: "long",
    year: "numeric",
  });

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
      <Title level={3}>{date}</Title>
      {info && 
        <>
          <Title level={4}>Total sum</Title>
          <Title level={5}>${info.totalSum}</Title>
          <br />
          <Title level={4}>Total debts</Title>
          <Title level={5}>${info.totalDebts}</Title>
          <br />
          <Title level={4}>Filled</Title>
          <Title level={5}>{info.currentFilled}/{info.totalFilled}</Title>
          <br />
          <Title level={4}>Paid</Title>
          <Title level={5}>{info.currentPaid}/{info.totalPaid}</Title>
          <br />
        </>
      }
    </div>
  );
}

export default Dashboard;