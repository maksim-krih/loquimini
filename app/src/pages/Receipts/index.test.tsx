import Receipts from ".";
import FillModal from "./fillModal";
import PayModal from "./payModal";
import { shallow } from "enzyme";
import { IProps as IFillModalProps } from "./fillModal/types";
import { IProps as IPayModalProps } from "./payModal/types";
import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";

const stripePromise = loadStripe('pk_test_WC7EYFzpuVCbs8ONcfSrjNE800zRXJe3lw');
const setUpReceipts = () => shallow(<Receipts />);
const setUpFillModal = (props: IFillModalProps) => shallow(<FillModal {...props} />);
const setUpPayModal = (props: IPayModalProps) => shallow(
  <PayModal  {...props} />
);

jest.mock('@stripe/react-stripe-js', () => ({
  ...jest.requireActual('@stripe/react-stripe-js'),
  useStripe: () => ({ }),
  useElements:  () => ({ })
}));


describe("Receipts", () => {
  describe("Index", () => {
    it("should render Receipts Component", () => {
      const component = setUpReceipts();
      expect(component).toMatchSnapshot();
    });
  });

  describe("FillModal", () => {
    it("should render FillModal Component", () => {
      const component = setUpFillModal({
        onCancel: () => {},
        receiptId: "",
        reloadGrid: () => {},
        visible: false
      });
      expect(component).toMatchSnapshot();
    });
  });

  describe("PayModal", () => {
    it("should render PayModal Component", () => {
      const component = setUpPayModal({
        onCancel: () => {},
        receiptId: "",
        reloadGrid: () => {},
        visible: false
      });
      expect(component).toMatchSnapshot();
    });
  });
});