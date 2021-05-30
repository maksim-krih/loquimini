export enum ReceiptStatus {
  Created,
  Filled,
  Paid,
}


export const ReceiptStatusLabel = (status: ReceiptStatus) => {
  switch (status) {
    case ReceiptStatus.Created:
      return "Created";
    case ReceiptStatus.Filled:
      return "Filled";
    case ReceiptStatus.Paid:
      return "Paid";
  }
}