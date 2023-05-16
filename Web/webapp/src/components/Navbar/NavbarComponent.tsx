import { observer } from "mobx-react-lite";
import React, { FC } from "react";
import { Container, Nav, Navbar } from "react-bootstrap";
import { Link, Outlet } from "react-router-dom";
import { authStore, myBasketStore, orderStore } from "../../App";

const NavBarComponent: FC = observer(() => {
  return (
    <>
      <Navbar sticky="top" style={{ backgroundColor: '#a2a8d3' }} expand="lg" variant="light">
        <Container fluid>
          <Navbar.Brand>Anime Shop</Navbar.Brand>
          <Navbar.Toggle aria-controls="navbarScroll" />
          <Navbar.Collapse id="navbarScroll">
            <Nav
              className="me-auto my-2 my-lg-0"
              style={{ maxHeight: "100px" }}
              navbarScroll
            >
              <Nav.Link
                as={Link}
                to="/characters"
                className="text-decoration-none text-white cursor-pointer"
              >
                Characters
              </Nav.Link>
              {authStore.user && <Nav.Link as={Link} to="/basket" onClick={async () => await myBasketStore.prefetchData()}>
                Basket
                <span className="ms-1">{myBasketStore.getTotalCountOfBasketItems()}</span>
              </Nav.Link>
              }
              {authStore.user && (
                <Nav.Link className="" as={Link} to="orders" onClick={async () => await orderStore.prefetchData()}>
                  Orders
                </Nav.Link>
              )}
            </Nav>
            {!authStore.user && (
              <Nav.Link
                className="text-white me-3"
                onClick={() => authStore.login()
                }
              >
                Login
              </Nav.Link>)}
            {authStore.user && (
              <Nav.Link
                className="text-white me-3"
                onClick={() => authStore.login()
                }
              >
                {authStore.user.profile.name}
              </Nav.Link>)}
            {authStore.user && (
              <Nav.Link
                className="text-white me-3"
                onClick={() => authStore.logout()
                }
              >
                Logout
              </Nav.Link>
            )}
          </Navbar.Collapse>
        </Container>
      </Navbar>
      <Outlet />
    </>
  );
});

export default NavBarComponent;