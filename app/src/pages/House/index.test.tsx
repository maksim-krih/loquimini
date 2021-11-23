import General from "./General";
import List from "./List";
import { shallow } from "enzyme";
import Api, { BaseApi } from "../../services";
import renderer, { act } from 'react-test-renderer';
import { HouseType } from "../../enums";
import { House } from "../../services/types";

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useParams: () => ({ id :"1" }),
}));

describe("House", () => {
  describe("Edit", () => {
    it("should render Edit Component", () => {
      const component = shallow(<General />);
      
      expect(component).toMatchSnapshot();
    });

    it("should get House", async () => {
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
      
      const userData = [
        {
          firstName: "Max",
          lastName: "1",
          id: "1",
          email: "max1@gmail.com",
          roles: ["User"]
        },
        {
          firstName: "Max",
          lastName: "2",
          id: "2",
          email: "max2@gmail.com",
          roles: ["User"]
        }
      ];

      const houseData: House = {
        id: "1",
        number: "1",
        street: "1",
        type: HouseType.Private,
        userId: "1",
        info: {
          area: 61,
          defaultIndicators: []
        },
        flats: []
      };
      
      Api.User.getAll = jest.fn().mockResolvedValue({
        data: userData
      });

      Api.House.getById = jest.fn().mockResolvedValue({
        data: houseData
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