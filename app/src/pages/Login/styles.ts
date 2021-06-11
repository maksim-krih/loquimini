import {createUseStyles} from 'react-jss'

export const useStyles = createUseStyles({
  container: {
    backgroundColor: "#001529",
    position: "fixed",
    width: "100%",
    height: "100%",
    display: "flex"
  },
  login: {
    backgroundColor: "#fff",
    padding: "16px 24px",
    width: 500,
    borderRadius: "20px",
    margin: "auto",
  }
})