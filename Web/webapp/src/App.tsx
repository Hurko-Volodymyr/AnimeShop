import React, { FC, } from 'react';
import './App.css';
import { observer } from 'mobx-react-lite';
import { Navigate, Route, Router, Routes } from 'react-router-dom';
import "bootstrap/dist/css/bootstrap.min.css";
import NavBarComponent from './components/Navbar/NavbarComponent';
import Layout from "./components/Layout/Layout";
import ExceptionComponent from './pages/ExceptionPage/ExceptionComponent';
import HomeStore from './pages/Home/components/HomeStore';
import CatalogItemList from './pages/Home/components/HomeComponent';
import CatalogItem from './pages/CatalogItem';
import { BasketStore } from './pages/Basket/Basket.store';
import BasketComponent from './pages/Basket/BasketComponent';
import OrderComponent from './pages/Order/OrderComponent';
import OrderHistoryComponent from './pages/Order/OrderHistoryComponent';
import AuthStore from './stores/Auth.store';
import Callback from './components/CallbackComponent';
import { OrderStore } from './pages/Order/order.store';
import { CssBaseline, createTheme } from '@mui/material';
import { ThemeProvider } from 'react-bootstrap';

export const authStore = new AuthStore();
export const homeStore = new HomeStore();
export const myBasketStore = new BasketStore();
export const orderStore = new OrderStore();

const theme = createTheme({
  palette: {
    primary: {
      light: "#98B2D1",
      main: "#98B2D1",
      dark: "#98B2D1",
      contrastText: "#000",
    },
    secondary: {
      main: "#a2a8d3",
      light: "#a2a8d3",
      dark: "#a2a8d3",
      contrastText: "#000",
    },
  },
});
const App: FC = observer(() => {
  return (
    <ThemeProvider theme={theme}>
    <CssBaseline />
          <Layout>
    <div className="App">
      <Routes>
        <Route path="/" element={<NavBarComponent />}>
          <Route index element={<Navigate replace to="characters" />} />
          <Route
            path="characters"
            element={<CatalogItemList />} />
          <Route path="characters/:id" element={<CatalogItem />} />
          <Route path="callback" element={<Callback />} />
          <Route path="new-order" element={<OrderComponent />} />
          {authStore.user && (
            <>
              <Route path="basket" element={<BasketComponent />} />
              <Route path="orders" element={<OrderHistoryComponent />} />
            </>
          )}
          :
          {
            <>
              <Route path="basket" element={<Navigate replace to="/characters" />} />
            </>
          }
        </Route>
        <Route path="*" element={<ExceptionComponent />} />
      </Routes>
    </div >
              </Layout>
          </ThemeProvider>
  );
});

export default App;

