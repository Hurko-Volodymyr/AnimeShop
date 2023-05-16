import { observer } from "mobx-react-lite";
import React, { FC, } from "react";
import { Button, Spinner } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { myBasketStore } from "../../App";
import { config } from "../../constants/api-constants";

const BasketComponent: FC = observer(() => {
  const navigate = useNavigate();

  return (
    <>
      <div className="container-sm">
        {myBasketStore.isLoading ? (
          <div className="container">
            <div className="row min-vh-100">
              <div className="col d-flex flex-column justify-content-center align-items-center">
                <Spinner animation="border" />
              </div>
            </div>
          </div>
        ) : (
          <>
            <h2>Basket</h2>
            {myBasketStore.items.length ? (
              <section className="pt-4 pb-3">
                <div className="container">
                  <div className="row w-100">
                    <div className="col-sm-12 col-sm-12 col-12">
                      <table
                        id="shoppingCart"
                        className="table table-condensed table-responsive align-middle"
                      >
                        <thead>
                          <tr>
                            <th style={{ width: "20%" }}></th>
                            <th style={{ width: "20%" }}>Name</th>
                            <th style={{ width: "20%" }}>Quantity</th>
                            <th style={{ width: "20%" }}></th>
                          </tr>
                        </thead>
                        {myBasketStore.items &&
                          myBasketStore.items.map((item, index) => (
                            <tbody key={index}>
                              <tr>
                                <td data-th="">
                                  <div className="text-right">
                                    <img
                                      src={`${item.pictureUrl}`}
                                      alt={item.name}
                                      className="img-fluid rounded"
                                    />
                                  </div>
                                </td>
                                <td data-th="Name">
                                  <div>{item.name}</div>
                                </td>

                                <td data-th="Quantity">
                                  <div className="d-flex justify-content-center">
                                    {
                                      <div>{item.count}</div>
                                    }
                                  </div>
                                </td>
                                <td className="actions" data-th="">
                                  <div>
                                    <button 
                                    style={{ backgroundColor: "#a2a8d3", color: "black" }}
                                      onClick={() =>
                                        myBasketStore.deleteItem(item.id)
                                       }
                                    >
                                    Delete
                                    </button>
                                  </div>
                                </td>
                              </tr>
                            </tbody>
                          ))}
                      </table>
                    </div>
                    <div className="d-flex justify-content-end">
                      <h4>Total: {myBasketStore.getTotalCountOfBasketItems()} characters</h4>
                    </div>
                  </div>
                  <div className="d-flex justify-content-start">
                    <button
                    style={{ backgroundColor: "#a2a8d3", color: "black" }}
                      className="btn btn-outline-dark flex-shrink-0 me-3"
                      onClick={() => navigate("/new-order")}
                    >
                      Make an order
                    </button>
                  </div>
                </div>
              </section>
            ) : (
              <div>Basket is empty</div>
            )}
          </>
        )}
      </div>
    </>
  )
});

export default BasketComponent;
