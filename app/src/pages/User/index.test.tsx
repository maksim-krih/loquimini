import General from "./General";
import List from "./List";
import { shallow } from "enzyme";
import Api from "../../services";
import renderer, { act } from 'react-test-renderer';

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useParams: () => ({ id :"1" }),
}));

describe("User", () => {
  describe("Edit", () => {
    it("should render Edit Component", () => {
      const component = shallow(<General />);

      expect(component).toMatchSnapshot();
    });

    it("should get User", async () => {
      Object.defineProperty(window, 'matchMedia', {
        writable: true,
        value: jest.fn().mockImplementation(query => ({
          matches: false,
          media: query,
          onchange: null,
          addListener: jest.fn(), // Deprecated
          removeListener: jest.fn(), // Deprecated
          addEventListener: jest.fn(),
          removeEventListener: jest.fn(),
          dispatchEvent: jest.fn(),
        })),
      });
      
      const data = {
          firstName: "Max",
          lastName: "1",
          id: "1",
          email: "max1@gmail.com",
          roles: ["User"]
      }

      Api.User.getById = jest.fn().mockResolvedValue({
        data
      });

      let component;
      await act(async () => {
        component = renderer.create(<General />);
      });

      expect(component).toMatchSnapshot();
    });
  });

  describe("Create", () => {
    it("should render Create Component", () => {
      const component = shallow(<General isCreate />);
      expect(component).toMatchSnapshot();
    });
  });

  describe("List", () => {
    it("should render List Component", () => {
      const component = shallow(<List />);
      expect(component).toMatchSnapshot();
    });
  });
});