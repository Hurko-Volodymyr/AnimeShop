import { FC, ReactElement } from "react";
import { Button, Card } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { authStore, myBasketStore } from "../../../App";
import { config } from "../../../constants/api-constants";
import { CatalogItemModel } from "../../../models/catalogItemModel";
import { CatalogItemDto } from "../../../models/dtos/catalogItemsDto";
import React from "react";
import { Box } from "@mui/material";

const CatalogItemCard: FC<CatalogItemDto> = (props): ReactElement => {

  const navigation = useNavigate();
  return (

    <div className="col">
      <Card className="mt-1 ms-4 h-100">
        <Card.Img
          variant="top"
          height={400}
          src={`${props.pictureUrl}`}
          alt={props.name}
          onClick={() => {
            navigation(`/characters/${props.id}`);
          }}
        />
        <Card.Body>
          <Card.Title>{props.name}</Card.Title>
          {authStore.user && <Button
            className="btn-info d-flex"
            onClick={async () =>
              await myBasketStore.addItem({
                id: props.id,
                name: props.name,
                region: props.region,
                birthday: props.birthday,
                pictureUrl: props.pictureUrl,
                catalogWeaponId: props.catalogWeapon.id,
                catalogRarityId: props.catalogRarity.id,
                price: props.catalogRarity.rarity === 4
                  ? Math.floor(Math.random() * 10) + 1
                  : props.catalogRarity.rarity === 5
                    ? Math.floor(Math.random() * 90) + 1
                    : 0
              } as CatalogItemModel)
            } style={{ backgroundColor: "#a2a8d3", color: "black" }}
          >

            Add to Basket
          </Button>}
        </Card.Body>
      </Card>
    </div>
  );
}

export default CatalogItemCard
