import General from "./General";
import List from "./List";
import { shallow } from "enzyme";
import { Route } from "react-router-dom";
import { IProps as IGeneralProps } from "./General/types";

const setUpGeneral = (props: IGeneralProps) => shallow(
  <Route path="/user/:id" >
    <General {...props} />
  </Route>
);
const setUpList = () => shallow(<List />);

describe("User", () => {
  describe("Edit", () => {
    it("should render Edit Component", () => {
      const component = setUpGeneral({
        isCreate: false
      });
      expect(component).toMatchSnapshot();
    });
  });

  describe("Create", () => {
    it("should render Create Component", () => {
      const component = setUpGeneral({
        isCreate: true
      });
      expect(component).toMatchSnapshot();
    });
  });

  describe("List", () => {
    it("should render List Component", () => {
      const component = setUpList();
      expect(component).toMatchSnapshot();
    });
  });
});