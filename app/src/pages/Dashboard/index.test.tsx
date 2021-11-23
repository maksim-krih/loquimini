import Dashboard from ".";
import { shallow } from "enzyme";
import { BaseApi } from "../../services";
import renderer, { act } from 'react-test-renderer';

beforeEach(() => {
  jest
  .useFakeTimers('modern')
  .setSystemTime(new Date('2020-01-01').getTime());
});

afterEach(() => {
  jest.clearAllMocks();
});

jest.mock('axios', () => {
  return {
    create: jest.fn(() => ({
      get: jest.fn(),
      interceptors: {
        request: { use: jest.fn(), eject: jest.fn() },
        response: { use: jest.fn(), eject: jest.fn() }
      }
    }))
  }
});

describe("Dashboard", () => {
  it("should render Dashboard Component", () => {
    const component = shallow(<Dashboard />);
    expect(component).toMatchSnapshot();
  });
  
  it("test get dashboard info", async () => {
      const data = {
        totalSum: 1,
        totalDebts: 1,
        currentFilled: 1,
        totalFilled: 1,
        currentPaid: 1,
        totalPaid: 1
      };
      
      BaseApi.get = jest.fn().mockResolvedValue({
        data
      });
      
      let component;
      await act(async () => {
        component = renderer.create(<Dashboard />);
      });

      expect(component).toMatchSnapshot();
  });
});