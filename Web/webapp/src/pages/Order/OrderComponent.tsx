import { observer } from "mobx-react-lite";
import React, { FC, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { Link, Navigate } from "react-router-dom";
import { authStore, myBasketStore, orderStore } from "../../App";
import { config } from "../../constants/api-constants";
import OrderModel from "../../models/orderModel";
import { Box } from "@mui/material";


const OrderComponent: FC = observer(() => {
  const [orderMessage, setOrderMessage] = useState<number | null>();

  useEffect(() => {
    if (orderMessage) {
      if ((orderMessage as number) !== 0) {
        (async () => {
          await myBasketStore.clearBasket();
        })()
        myBasketStore.totalSum = 0;
        alert(`Order was confirmed with id = ${orderMessage}`);
        <Navigate to="/orders" />;
      }
      else {
        alert("Failed to order");
      }
    }
  }, [orderMessage]);

  const {
    register,
    formState: { errors },
    handleSubmit,
  } = useForm({ mode: "onBlur" });

  const [formData, setFormData] = useState<OrderModel>({    
    name: authStore.user?.profile.given_name,
    lastName: authStore.user?.profile.family_name,
    email: "",
    gameAccountId: "",
  } as OrderModel);

  function handleChange(e: any) {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  }

  const onSubmit = () => {
    (async () => {
      setOrderMessage(
        await orderStore.AddOrder(
          {
            userId: authStore.user?.profile.sub,           
            name: formData.name,
            lastName: formData.lastName,
            email: formData.email,
            gameAccountId: formData.gameAccountId,
          } as OrderModel));
    })();
  };

  if (myBasketStore.items.length > 0) {
    return (
      <Box
            sx={{
                flexGrow: 1,
                backgroundColor: 'whitesmoke',
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
            }}
        >
      <div className="container p-2">
        <div className="row mx-2">
          <div className="pt-2 mb-2 col-md-5 col-lg-4 order-md-first bg-white rounded border border-dark">
            <h4 className="d-flex justify-content-between align-items-center mb-3 ">
              <span>Your Basket</span>
              <span className="badge bg-dark rounded-pill">
                {myBasketStore.items!.length}
              </span>
            </h4>
            <div className="container">
              <div className="row">
                <div className="col d-flex flex-column justify-content-center align-items-end">
                  <span>
                  </span>
                </div>
              </div>
            </div>
            <div className="d-md-flex mt-2">
              <div
                className="overflow-auto pt-2 mb-3 mb-md-0"
                style={{ maxWidth: "500px", maxHeight: "310px" }}
              >
                {myBasketStore.items!.map((product, index) => (
                  <>
                    <div className="card me-2 shadow-0 border mb-2">
                      <div className="card-body">
                        <div className="row">
                          <div className="col-md-2">
                            <img
                              key={index}
                              src={`${product.pictureUrl}`}
                              className="img-fluid"
                              alt="product"
                            />
                          </div>
                          <div className="col-md-4 text-center d-flex justify-content-center align-items-center">
                            <p className="text-muted mb-0">
                              {product.name}
                            </p>
                          </div>
                          <div className="col-md-3 text-center d-flex justify-content-center align-items-center">
                            <p className="text-muted mb-0 small">
                              {product.count}
                            </p>
                          </div>
                        </div>
                      </div>
                    </div>
                  </>
                ))}
              </div>
            </div>

            <h5 className="d-flex justify-content-end mt-3">
              Total: {myBasketStore.totalSum} 
            </h5>
          </div>
          <div className="col-md-7 col-lg-8">
            <h4 className="mb-3">Your order</h4>
            <form
              className="needs-validation"
              onSubmit={handleSubmit(onSubmit)}
            >
              <div className="row g-3">
                <div className="col-sm-4">
                  <label htmlFor="name" className="form-label">
                    Name
                  </label>
                  <input
                    {...register("name", {
                      required: "Name can not be empty",
                      minLength: { value: 3, message: "Min 3 symbols" },
                    })}
                    defaultValue={formData.name}
                    type="text"
                    className="form-control"
                    placeholder="Enter name"
                    onChange={(e) => handleChange(e)}
                  />
                  <div style={{ height: 20 }}>
                    {errors?.firstName && (
                      <p className="text-danger">
                        {errors?.name?.message?.toString()}
                      </p>
                    )}
                  </div>
                </div>

                <div className="col-sm-4">
                  <label htmlFor="lastName" className="form-label">
                    Surname
                  </label>
                  <input
                    {...register("lastName", {
                      required: "Last Name can not be empty",
                      minLength: { value: 2, message: "Min 2 symbols" },
                    })}
                    defaultValue={formData.lastName}
                    type="text"
                    className="form-control"
                    placeholder="Enter last Name"
                    onChange={(e) => handleChange(e)}
                  />
                  <div style={{ height: 20 }}>
                    {errors?.lastName && (
                      <p className="text-danger">
                        {errors?.lastName?.message?.toString()}
                      </p>
                    )}
                  </div>
                </div> 

                <div className="col-md-5">
                  <label htmlFor="email" className="form-label">
                    Email
                  </label>
                  <input
                    {...register("email", {
                      required: "Email can not be empty",
                      pattern: {
                        value: /^[a-zA-Z0-9].+@[a-zA-Z0-9]+\.[A-Za-z]+$/,
                        message: "Invalid email format",
                      },
                    })}
                    type="email"
                    className="form-control"
                    placeholder="Enter email"
                    onChange={(e) => handleChange(e)}
                  />
                  <div style={{ height: 20 }}>
                    {errors?.email && (
                      <p className="text-danger">
                        {errors?.email?.message?.toString()}
                      </p>
                    )}
                  </div>
                </div>

                <div className="col-md-5">
                  <label htmlFor="gameAccountId" className="form-label">
                    Game Account Id
                  </label>
                  <input
                    {...register("gameAccountId", {
                      required: "Game Account Id can not be empty",
                      pattern: {
                        value: /^[0-9]+$/,
                        message: "Invalid Game Account Id format",
                      },
                    })}
                    type="text"
                    className="form-control"
                    placeholder="Enter Game Account Id"
                    onChange={(e) => handleChange(e)}
                  />
                  <div style={{ height: 20 }}>
                    {errors?.gameAccountId && (
                      <p className="text-danger">
                        {errors?.gameAccountId?.message?.toString()}
                      </p>
                    )}
                  </div>
                </div>
              </div>

              <hr className="my-4" />

              <button type="submit" className="w-100 btn btn-primary btn-lg" style={{ backgroundColor: "#a2a8d3", color: "black" }}>
                
                Confirm order
              </button>
            </form>
          </div>
        </div>
      </div>
      </Box>
    );
  }
  else {
    return <Navigate to="/characters" />
  }
});

export default OrderComponent;