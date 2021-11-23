import Triggers from ".";
import { shallow } from "enzyme";

const setUp = () => shallow(<Triggers />);

describe("Triggers", () => {
  it("should render Triggers Component", () => {
    const component = setUp();
    expect(component).toMatchSnapshot();
  });

  it("should Generate receipts Triggers Component", () => {
    const component = setUp();
    const button = component.find("#generate-receipts");
    button.simulate("click");
  });
});