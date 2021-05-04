import {createUseStyles} from 'react-jss'

export const useStyles = createUseStyles({
  trigger: {
    color: "#fff",
    padding: "0 26px",
    fontSize: 18,
    lineHeight: "64px",
    cursor: "pointer",
    transition: "color 0.3s",
    "&:hover": {
      color: "#1890ff"
    }
  },
  logo: {
    height: 32,
    margin: 16,
    textAlign: "center",
    overflow: "hidden",
    "& .ant-typography": {
      color: "#fff"
  }
  },
  layout: {
    background: "#fff"
  },
  header: {
    padding: 0,
    paddingRight: 24,
    height: 68,
    display: "flex",
    justifyContent: "space-between"
  },
  content: {
    margin: '24px 16px',
    padding: 24,
    minHeight: 280,
  },
  layoutContainer: {
    minHeight: '100vh'
  },
  profile: {
    "& .ant-typography, & .anticon": {
      color: "#fff"
    }
  },
  user: {
    marginRight: 8
  }
})