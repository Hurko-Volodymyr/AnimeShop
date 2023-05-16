import { observer } from "mobx-react-lite";
import { FC, ReactElement, useEffect } from "react";
import { Button } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import { authStore, homeStore, myBasketStore } from "../../App";
import { config } from "../../constants/api-constants";
import { CatalogItemModel } from "../../models/catalogItemModel";
import ExceptionComponent from "../ExceptionPage/ExceptionComponent";
import React from "react";
import { Box } from "@mui/material";

const User: FC<any> = observer((): ReactElement => {
  const { id } = useParams();
  const navigation = useNavigate();

  useEffect(() => {
    (async () => {
      if (id) {
        await homeStore.getSingleCatalogItem(id);
      }
    })();
  }, [id]);

  if (homeStore.singleCatalogItem) {
    if (homeStore.singleCatalogItem.id) {
      return (       
        <>
               <Box
            sx={{
                flexGrow: 1,
                backgroundColor: 'whitesmoke',
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
            }}
        >  
          <section className="my-4">
            <div className="container px-4 px-lg-5">
              <div className="row gx-4 gx-lg-5">
                <div className="col-md-4">
                  <img
                    className="card-img-top mb-5 mb-md-0"
                    src={`${homeStore.singleCatalogItem.pictureUrl}`}
                    alt={homeStore.singleCatalogItem.name}
                  />
                </div>
                <div className="col-md-6">
                  <h1 className="display-4 fw-bolder mb-2">{homeStore.singleCatalogItem.name}</h1>
                  <div className="fs-5 mb-2">
                  </div>
                  <div className="d-flex mb-2"><span className="fw-bolder">Rarity:&nbsp;</span>{homeStore.singleCatalogItem.catalogRarity.rarity}*</div>
                  <div className="d-flex mb-2"><span className="fw-bolder">Region:&nbsp;</span>{homeStore.singleCatalogItem.region}</div>
                  <div className="d-flex mb-2"><span className="fw-bolder">Birthday:&nbsp;</span>{homeStore.singleCatalogItem.birthday}</div>
                  <div className="d-flex mb-2"><span className="fw-bolder">Weapon:&nbsp;</span>{homeStore.singleCatalogItem.catalogWeapon.weapon}</div>
                  <div className="d-flex mt-5">
                    {authStore.user && <button
                      onClick={async () =>
                        await myBasketStore.addItem({
                          id: homeStore.singleCatalogItem.id,
                          name: homeStore.singleCatalogItem.name,
                          region: homeStore.singleCatalogItem.region,
                          birthday: homeStore.singleCatalogItem.birthday,
                          pictureUrl: homeStore.singleCatalogItem.pictureUrl,
                          catalogWeaponId: homeStore.singleCatalogItem.catalogWeapon.id,
                          catalogRarityId: homeStore.singleCatalogItem.catalogRarity.id,
                        }  as CatalogItemModel)
                      }
                      className="btn btn-outline-dark flex-shrink-0"
                      style={{ backgroundColor: "#a2a8d3", color: "black" }}
                    >                      
                      Add to Basket
                    </button>}
                  </div>
                </div>
              </div>
            </div>
          </section>
          </Box>
        </>        
      );
    }
  }
  return <ExceptionComponent />;
});

export default User;
