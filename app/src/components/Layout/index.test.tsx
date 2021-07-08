import Layout from ".";
import { shallow } from "enzyme";

const setUp = () => shallow(<Layout />);

describe("Layout", () => {
  it("should render Layout Component", () => {
    const component = setUp();
    expect(component).toMatchSnapshot();
  });
});