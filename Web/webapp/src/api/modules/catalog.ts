import { config } from "../../constants/api-constants";
import { ItemsByWeaponRequest } from "../../models/requests/itemsByWeaponRequest";
import { ItemsByRarityRequest } from "../../models/requests/itemsByRarityRequest";
import apiClient from "../client";

export const getCatalogItemById = (id: string) =>
  apiClient({
    url: config.catalogUrl,
    path: `itemById/${id}`,
    method: "POST",
  });

export const getCatalogItems = (pageIndex: number, pageSize: number, filter: object) =>
  apiClient({
    url: config.catalogUrl,
    path: `items`,
    method: "POST",
    data: { pageIndex, pageSize, filter }
  });

export const getCatalogItemsByWeapon = (request: ItemsByWeaponRequest) =>
  apiClient({
    url: config.catalogUrl,
    path: `itemsByWeapon`,
    method: "POST",
    data: request
  });

export const getWeapons = () =>
  apiClient({
    url: config.catalogUrl,
    path: `weapons`,
    method: "POST",
  });

  export const getCatalogItemsByRarity = (request: ItemsByRarityRequest) =>
  apiClient({
    url: config.catalogUrl,
    path: `itemsByRarity`,
    method: "POST",
    data: request
  });

export const getRarities = () =>
  apiClient({
    url: config.catalogUrl,
    path: `rarities`,
    method: "POST",
  });