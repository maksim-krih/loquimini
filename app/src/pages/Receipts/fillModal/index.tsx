import React, { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { InputNumber, Modal } from "antd";
import Api from "../../../services";

const FillModal: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const { visible, onCancel, receiptId, reloadGrid } = props;
  const [value, setValue] = useState<number | undefined>(undefined);

  const handleOk = () => {
    Api.Receipt.fillReceipt({
      newIndicator: value!,
      receiptId
    })
    .then((response: any) => {
      reloadGrid();
      onCancel();
    })
    .catch(e => {
      
    });
  }

  const onChange = (value: number) => {
    setValue(value);
  }

  return (
    <Modal 
      title="Fill Receipt" 
      visible={visible} 
      onOk={handleOk} 
      onCancel={onCancel}
    >
      <InputNumber value={value} onChange={onChange} style={{ borderRadius: 5 }}/>
    </Modal>
  );
}

export default FillModal;