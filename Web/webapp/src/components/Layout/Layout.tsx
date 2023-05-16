import React, { FC, ReactNode } from "react";
import Navbar from "../Navbar";
import Footer from "../Footer";
import { Box, CssBaseline } from "@mui/material";

interface LayoutProps {
  children: ReactNode;
}

const Layout: FC<LayoutProps> = ({ children }) => {
  return (
    <>
      <CssBaseline />
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          justifyContent: "flex-start",
          minHeight: "100vh",
          maxWidth: "100vw",
          flexGrow: 1,
        }}
      >
        {children}
        <Footer />
      </Box>
    </>
  );
};

export default Layout;