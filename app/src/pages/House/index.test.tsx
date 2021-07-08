import General from "./General";
import List from "./List";
import { shallow } from "enzyme";
import { Route } from "react-router-dom";
import { IProps as IGeneralProps } from "./General/types";

const setUpGeneral = (props: IGeneralProps) => shallow(
  <General {...props} />
);
const setUpList = () => shallow(<List />);

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useParams: () => ({ id :"1" }),
}));

describe("House", () => {
  describe("Edit", () => {
    it("should render Edit Component", () => {
      const component = setUpGeneral({ isCreate: false })
      
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