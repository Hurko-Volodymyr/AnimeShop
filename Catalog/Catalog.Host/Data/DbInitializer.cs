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
            new CatalogRarity() { Rarity = "4*" },
            new CatalogRarity() { Rarity = "5*" }
        };
    }

    private static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>()
        {
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 3, Name = "Kamysato Ayaka", Region = "Inazuma", Birthday = "28.09", PictureFileURL = "https://static.wikia.nocookie.net/gensin-impact/images/d/d0/Character_Kamisato_Ayaka_Full_Wish.png/revision/latest/scale-to-width-down/1000?cb=20221014024207" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 3, Name = "Kamysato Ayato", Region = "Inazuma", Birthday = "26.03", PictureFileURL = "https://static.wikia.nocookie.net/gensin-impact/images/c/ce/Character_Kamisato_Ayato_Full_Wish.png/revision/latest/scale-to-width-down/1000?cb=20221014023913" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 1, Name = "Raiden Shogun", Region = "Inazuma", Birthday = "26.06", PictureFileURL = "https://static.wikia.nocookie.net/gensin-impact/images/a/a3/Character_Raiden_Shogun_Full_Wish.png/revision/latest/scale-to-width-down/1000?cb=20220507154003" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 4, Name = "Yae Miko", Region = "Inazuma", Birthday = "27.06", PictureFileURL = "https://static.wikia.nocookie.net/gensin-impact/images/4/49/Character_Yae_Miko_Full_Wish.png/revision/latest/scale-to-width-down/1000?cb=20220507110641" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 3, Name = "Nilou", Region = "Sumeru", Birthday = "03.12", PictureFileURL = "https://static.wikia.nocookie.net/gensin-impact/images/6/61/Character_Nilou_Full_Wish.png/revision/latest/scale-to-width-down/1000?cb=20221014130045" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 1, Name = "Shenhe", Region = "Liyue", Birthday = "10.03", PictureFileURL = "https://static.wikia.nocookie.net/gensin-impact/images/4/49/Character_Shenhe_Full_Wish.png/revision/latest/scale-to-width-down/1000?cb=20221011032401" },
            new CatalogItem { CatalogRarityId = 1, CatalogWeaponId = 2, Name = "Chongyun", Region = "Liyue", Birthday = "07.09", PictureFileURL = "https://static.wikia.nocookie.net/gensin-impact/images/9/95/Character_Chongyun_Full_Wish.png/revision/latest/scale-to-width-down/1000?cb=20220507161818" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 1, Name = "Zhongli", Region = "Liyue", Birthday = "31.12", PictureFileURL = "https://static.wikia.nocookie.net/gensin-impact/images/c/c4/Character_Zhongli_Full_Wish.png/revision/latest/scale-to-width-down/1000?cb=20220507161902" },
            new CatalogItem { CatalogRarityId = 2, CatalogWeaponId = 3, Name = "Jean", Region = "Mondstadt", Birthday = "14.03", PictureFileURL = "https://static.wikia.nocookie.net/gensin-impact/images/d/d6/Character_Jean_Full_Wish.png/revision/latest/scale-to-width-down/1000?cb=20221014022733" },
            new CatalogItem { CatalogRarityId = 1, CatalogWeaponId = 5, Name = "Kujou Sara", Region = "Inazuma", Birthday = "14.07", PictureFileURL = "https://static.wikia.nocookie.net/gensin-impact/images/6/69/Character_Kujou_Sara_Full_Wish.png/revision/latest/scale-to-width-down/1000?cb=20220507110137" },
            new CatalogItem { CatalogRarityId = 1, CatalogWeaponId = 3, Name = "Layla", Region = "Sumery", Birthday = "19.12", PictureFileURL = "https://static.wikia.nocookie.net/gensin-impact/images/9/94/Character_Layla_Full_Wish.png/revision/latest/scale-to-width-down/1000?cb=20221118140902" },
        };
    }
}