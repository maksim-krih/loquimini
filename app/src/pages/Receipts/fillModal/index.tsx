import React, { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { InputNumber, Modal } from "antd";

const FillModal: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const { visible, onCancel } = props;
  const [value, setValue] = useState<number | undefined>(undefined);

  const handleOk = () => {

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
      <InputNumber value={value} onChange={onChange}/>
    </Modal>
  );
}

export default FillModal;