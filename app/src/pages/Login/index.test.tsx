import Login from ".";
import { mount, shallow } from "enzyme";
import renderer, { act } from 'react-test-renderer';

const setUp = () => shallow(<Login />);

describe("Login", () => {
  it("should render Login Component", () => {
    const component = setUp();
    expect(component).toMatchSnapshot();
  });

  it("should simulate submit click", () => {
    const component = setUp();
    const button = component.find("#submit");
    button.simulate("click");
  });

  it("should simulate submit form", async () => {
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
    
    // const data = {
    //     firstName: "Max",
    //     lastName: "1",
    //     id: "1",
    //     email: "max1@gmail.com",
    //     roles: ["User"]
    // }

    // Api.User.getById = jest.fn().mockResolvedValue({
    //   data
    // });

    let component: any;
    await act(async () => {
      component = renderer.create(<Login />);
    });
    const form = component!.find("#form");
    form.simulate("submit");
  });

});