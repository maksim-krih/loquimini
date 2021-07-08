import Login from ".";
import { shallow } from "enzyme";

const setUp = () => shallow(<Login />);

describe("Login", () => {
  it("should render Login Component", () => {
    const component = setUp();
    expect(component).toMatchSnapshot();
  });
});