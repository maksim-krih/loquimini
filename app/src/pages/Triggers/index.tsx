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

  const generateReceipts = async () => {
    Api.Receipt.generateReceipts()
    .then((result: any) => {
      
    })
    .catch((e: any) => {

    });
  };

  return (
    <div className={classes.container}>
      <Button onClick={generateReceipts}>Generate Receipts</Button>
    </div>
  );
}

export default Trigger;