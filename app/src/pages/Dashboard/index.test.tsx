import Dashboard from ".";
import { shallow } from "enzyme";

const setUp = () => shallow(<Dashboard />);

describe("Dashboard", () => {
  it("should render Dashboard Component", () => {
    const component = setUp();
    expect(component).toMatchSnapshot();
  });
});