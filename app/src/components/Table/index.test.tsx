import Table from ".";
import { shallow } from "enzyme";
import { IProps } from "./types";

const setUp = (props: IProps) => shallow(<Table {...props} />);

describe("Table", () => {
  it("should render Table Component", () => {
    const component = setUp({
      columns: [],
      data: [],
      getData: () => {},
      loading: false,
      onRow: () => {},
      pager: {
        current: 0,
        pageSize: 0,
        total: 0
      },
      setPager: () => {}
    });
    expect(component).toMatchSnapshot();
  });
});