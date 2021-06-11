import React, { FC, useEffect, useState } from "react";
import { IProps } from "./types";
import { useStyles } from "./styles";
import { InputNumber, Modal } from "antd";
import Api from "../../../services";
import {CardElement, useStripe, useElements} from '@stripe/react-stripe-js';

const PayModal: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const stripe = useStripe();
  const elements = useElements();
  const { visible, onCancel, receiptId, reloadGrid } = props;
  const [value, setValue] = useState<number | undefined>(undefined);
  const [loading, setLoading] = useState(false);

  const handleOk = async () => {
    setLoading(true);
    if (!stripe || !elements) {
      return;
    }

    const cardElement = elements.getElement(CardElement);
    const {error, paymentMethod} = await stripe.createPaymentMethod({
      type: 'card',
      card: cardElement!,
    });

    if (error) {
      console.log('[error]', error);
      setLoading(false);
    } else {
      Api.Receipt.payReceipt({
        value: value!,
        receiptId
      })
      .then((response: any) => {
        reloadGrid();
        onCancel();
      })
      .catch(e => {
      
      })
      .finally(() => {
        setLoading(false);
      });
    }
  }

  const onChange = (value: number) => {
    setValue(value);
  }

  return (
    <Modal 
      title="Pay Receipt" 
      visible={visible} 
      confirmLoading={loading}
      onOk={handleOk} 
      onCancel={onCancel}
    >
      <div style={{ marginBottom: 6}}>
        <CardElement />
      </div>
      <InputNumber value={value} onChange={onChange} style={{ borderRadius: 5 }}/>
    </Modal>
  );
}

export default PayModal;