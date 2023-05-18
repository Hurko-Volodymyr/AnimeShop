import { observer } from "mobx-react-lite";
import React, { FC } from "react";
import { orderStore } from "../../App";
import { Spinner, Card } from "react-bootstrap";

const OrderHistoryComponent: FC = observer(
  () => {
    const { isLoading, orders } = orderStore;

    return (
      <div className="mb-3">
        {isLoading ? (
          <div className="container">
            <div className="row min-vh-100">
              <div className="col d-flex flex-column justify-content-center align-items-center">
                <Spinner animation="border" />
              </div>
            </div>
          </div>
        ) : (
          <>
            <h2>My orders</h2>
            {orders.length ? (
              <div className="container w-75">
                {orders
                  .slice()
                  .sort((a, b) => b.id - a.id)
                  .map((order) => (
                    <Card key={order.id} className="mb-1">
                      <Card.Header className="d-flex justify-content-between align-items-center">
                        <div className="col-sm-3 me-2 text-center">
                          Order №{order.id}
                        </div>
                        <div className="col-sm-5 text-center">
                          <p className="mb-0">Order price: {order.totalSum}*</p>
                        </div>
                        <div className="col-sm-3 ms-3 text-end">
                          Order owner: {order.gameAccountId}
                        </div>
                      </Card.Header>
                      <Card.Body>
                        <section className="gradient-custom">
                          <div className="container">
                            <div className="row d-flex justify-content-center align-items-center h-100">
                              <div className="card">
                                <div className="card-header px-2 py-3">
                                  <h5 className="text-muted mb-0">Order details</h5>
                                </div>
                                <div className="card-body pe-4 ps-4 pb-1">
                                  <div className="d-flex justify-content-between pt-1 ps-1">
                                    <p className="text-muted mb-0 text-start">
                                      Game Account Id: {order.gameAccountId}
                                    </p>
                                  </div>
                                  <div className="d-flex justify-content-between ps-1">
                                    <p className="text-muted mb-0 text-start">
                                      Fullname: {order.name} {order.lastName}
                                    </p>
                                  </div>
                                  <div className="d-flex justify-content-between ps-1">
                                    <p className="text-muted mb-0 text-start">
                                      Email: {order.email}
                                    </p>
                                  </div>
                                  <div className="d-flex justify-content-between ps-1">
                                    <p className="text-muted mb-0 text-start">
                                      createdAt: {order.createdAt}
                                    </p>
                                  </div>
                                </div>
                              </div>
                            </div>
                          </div>
                        </section>
                      </Card.Body>
                    </Card>
                  ))}
              </div>
            ) : (
              <div>You haven't ordered anything yet</div>
            )}
          </>
        )}
      </div>
    );
  }
);

export default OrderHistoryComponent;
