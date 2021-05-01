import {createUseStyles} from 'react-jss'

export const useStyles = createUseStyles({
  trigger: {
    padding: "0 24px",
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
    background: "rgba(255, 255, 255, 0.3)"
  },
  layout: {
    background: "#fff"
  },
  header: {
    padding: 0
  },
  content: {
    margin: '24px 16px',
    padding: 24,
    minHeight: 280,
  }
})