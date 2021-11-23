import React, { FC, useState } from "react";
import { Button, Input } from "antd";
import { IProps } from "./types";
import { useStyles } from "./styles";
import Api, { AuthService } from "../../services";
import { useHistory } from "react-router-dom";
import { RouterPaths } from "../../consts";

const Trigger: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const history = useHistory();
  const [loading, setLoading] = useState(false);

  const generateReceipts = async () => {
    setLoading(true);
    Api.Receipt.generateReceipts()
    .then((result: any) => {
      
    })
    .catch((e: any) => {

    })
    .finally(() => {
      setLoading(false);
    });
  };

  return (
    <div className={classes.container}>
      <Button 
        id="generate-receipts"
        onClick={generateReceipts} 
        style={{ borderRadius: 5 }} 
        type="primary"
        loading={loading}
      >
        Generate Receipts
      </Button>
    </div>
  );
}

export default Trigger;