import logo from './logo.svg';
import './App.css';
import { Container, Typography } from "@material-ui/core";
import Order from "./components/Application";

function App() {
  return (
    <Container maxWidth="md">
      <Typography
        gutterBottom
        variant="h3"
        align="center">
        STUDENT APP By Amisi Jlak
      </Typography>
      <Order />
    </Container>
  );
}

export default App;
