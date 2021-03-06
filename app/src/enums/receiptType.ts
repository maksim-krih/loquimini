export enum ReceiptType {
  Rent,
  Garbage,
  Gas,
  Electricity,
  Intercom,
  HotWater,
  ColdWater,
  Sewerage
}

export const ReceiptTypeLabel = (type: ReceiptType) => {
  switch (type) {
    case ReceiptType.Rent:
      return "Rent";
    case ReceiptType.Garbage:
      return "Garbage";
    case ReceiptType.Gas:
      return "Gas";
    case ReceiptType.Electricity:
      return "Electricity";
    case ReceiptType.Intercom:
      return "Intercom";
    case ReceiptType.HotWater:
      return "Hot Water";
    case ReceiptType.ColdWater:
      return "Cold Water";
    case ReceiptType.Sewerage:
      return "Sewerage";
  }
}