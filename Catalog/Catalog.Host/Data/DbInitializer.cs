using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.CatalogWeapons.Any())
        {
            await context.CatalogWeapons.AddRangeAsync(GetPreconfiguredCatalogBrands());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogRarities.Any())
        {
            await context.CatalogRarities.AddRangeAsync(GetPreconfiguredCatalogTypes());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogItems.Any())
        {
            await context.CatalogItems.AddRangeAsync(GetPreconfiguredItems());

            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<CatalogWeapon> GetPreconfiguredCatalogBrands()
    {
        return new List<CatalogWeapon>()
        {
            new CatalogWeapon() { Weapon = "Spear" },
            new CatalogWeapon() { Weapon = "Claymor" },
            new CatalogWeapon() { Weapon = "Sword" },
            new CatalogWeapon() { Weapon = "Catalyst" },
            new CatalogWeapon() { Weapon = "Bow" },
        };
    }

    private static IEnumerable<CatalogRarity> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogRarity>()
        {
            new CatalogRarity() { Rarity = 4 },
            new CatalogRarity() { Rarity = 5 }
        };
    }

    private static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>()
        {
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 3, Name = "Kamysato Ayaka", Region = "Inazuma", Birthday = "28.09", PictureFile = "Ayaka.png" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 3, Name = "Kamysato Ayato", Region = "Inazuma", Birthday = "26.03", PictureFile = "Ayato.png" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 1, Name = "Raiden Shogun", Region = "Inazuma", Birthday = "26.06", PictureFile = "Raiden.png" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 4, Name = "Yae Miko", Region = "Inazuma", Birthday = "27.06", PictureFile = "Miko.png" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 3, Name = "Nilou", Region = "Sumeru", Birthday = "03.12", PictureFile = "Nilou.png" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 1, Name = "Shenhe", Region = "Liyue", Birthday = "10.03", PictureFile = "Shenhe.png" },
            new CatalogItem { CatalogRarityId = 1, CatalogWeaponId = 2, Name = "Chongyun", Region = "Liyue", Birthday = "07.09", PictureFile = "Chongyun.png" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 1, Name = "Zhongli", Region = "Liyue", Birthday = "31.12", PictureFile = "Zhongli.png" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 3, Name = "Jean", Region = "Mondstadt", Birthday = "14.03", PictureFile = "Jean.png" },
            new CatalogItem { CatalogRarityId = 1, CatalogWeaponId = 5, Name = "Kujou Sara", Region = "Inazuma", Birthday = "14.07", PictureFile = "Sara.png" },
            new CatalogItem { CatalogRarityId = 1, CatalogWeaponId = 3, Name = "Layla", Region = "Sumery", Birthday = "19.12", PictureFile = "Layla.png" },
        };
    }
}