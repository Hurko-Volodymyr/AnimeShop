import { CatalogRarityDto } from "./catalogRarityDto";
import { CatalogWeaponDto } from "./catalogWeaponDto";

export interface CatalogItemDto {
  'id': number,
  'name': string,
  'region': string,
  'birthday': string,
  'pictureUrl': string,
  'catalogWeapon': CatalogWeaponDto,
  'catalogRarity': CatalogRarityDto,
}