using UnityEngine;

public static class ItemData
{
    public static Item CreateItem(int itemID)
    {
        string _name = "";
        string _desctiption = "";
        int _value = 0;
        int _amount = 0;
        int _damage = 0;
        int _durability = 0;
        int _armour = 0;
        int _heal = 0;
        string _icon = "";
        string _mesh = "";
        ItemType _type = ItemType.Food;
        switch (itemID)
        {
            #region default
            default:
                itemID = 0;
                _name = "apple";
                _desctiption = "nice";
                _amount = 1;
                _value = 1;
                _type = ItemType.Food;
                _icon = "Food/apple";
                _mesh = "Food/apple";
                _damage = 0;
                _armour = 0;
                _heal = 0;
                break;
            #endregion
            #region Apparrel
            case 0:
                _name = "hat";
                _desctiption = "cant see";
                _amount = 1;
                _value = 1;
                _type = ItemType.Apparrel;
                _icon = "Apparrel/hat";
                _mesh = "Apparrel/hat";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 1:
                _name = "brace";
                _desctiption = "nice";
                _amount = 1;
                _value = 1;
                _type = ItemType.Apparrel;
                _icon = "Apparrel/brace";
                _mesh = "Apparrel/brace";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 2:
                _name = "shirt";
                _desctiption = "ouch but not";
                _amount = 1;
                _value = 1;
                _type = ItemType.Apparrel;
                _icon = "Apparrel/shirt";
                _mesh = "Apparrel/shirt";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            #endregion
            #region Consumable
            case 3:
                _name = "staff of panic";
                _desctiption = "oh no";
                _amount = 1;
                _value = 1;
                _type = ItemType.Consumable;
                _icon = "Consumable/staff of panic";
                _mesh = "Consumable/staff of panic";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 4:
                _name = "flute";
                _desctiption = "charm for a bit";
                _amount = 1;
                _value = 1;
                _type = ItemType.Consumable;
                _icon = "Consumable/flute";
                _mesh = "Consumable/flute";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 5:
                _name = "lute";
                _desctiption = "charm for longer";
                _amount = 1;
                _value = 1;
                _type = ItemType.Consumable;
                _icon = "Consumable/lute";
                _mesh = "Consumable/lute";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            #endregion
            #region Weapon
            case 6:
                _name = "sword";
                _desctiption = "swing swing";
                _amount = 1;
                _value = 1;
                _type = ItemType.Weapon;
                _icon = "Weapons/sword";
                _mesh = "Weapons/sword";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 7:
                _name = "axe";
                _desctiption = "ouch";
                _amount = 1;
                _value = 1;
                _type = ItemType.Weapon;
                _icon = "Weapons/axe";
                _mesh = "Weapons/axe";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 8:
                _name = "bluesword";
                _desctiption = "ouch ouch";
                _amount = 1;
                _value = 1;
                _type = ItemType.Weapon;
                _icon = "Weapons/bluesword";
                _mesh = "Weapons/bluesword";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            #endregion
            #region Potion
            case 9:
                _name = "pis";
                _desctiption = "yucky";
                _amount = 1;
                _value = 1;
                _type = ItemType.Potion;
                _icon = "Potions/pis";
                _mesh = "Potions/pis";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 10:
                _name = "red";
                _desctiption = "heal?";
                _amount = 1;
                _value = 1;
                _type = ItemType.Potion;
                _icon = "Potions/red";
                _mesh = "Potions/red";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 11:
                _name = "purple";
                _desctiption = "oo";
                _amount = 1;
                _value = 1;
                _type = ItemType.Potion;
                _icon = "Potions/purple";
                _mesh = "Potions/purple";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            #endregion
            #region Food
            case 12:
                _name = "nut";
                _desctiption = "dont bust";
                _amount = 1;
                _value = 1;
                _type = ItemType.Food;
                _icon = "Food/nut";
                _mesh = "Food/nut";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 13:
                _name = "shroom";
                _desctiption = "+20 mouth foam";
                _amount = 1;
                _value = 1;
                _type = ItemType.Food;
                _icon = "Food/shroom";
                _mesh = "Food/shroom";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 14:
                _name = "bark";
                _desctiption = "woof";
                _amount = 1;
                _value = 1;
                _type = ItemType.Food;
                _icon = "Food/bark";
                _mesh = "Food/bark";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            #endregion
            #region Material
            case 15:
                _name = "cog";
                _desctiption = "clang";
                _amount = 1;
                _value = 1;
                _type = ItemType.Material;
                _icon = "Material/cog";
                _mesh = "Material/cog";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 16:
                _name = "cloth";
                _desctiption = "nice";
                _amount = 1;
                _value = 1;
                _type = ItemType.Material;
                _icon = "Material/cloth";
                _mesh = "Material/cloth";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 17:
                _name = "badcloth";
                _desctiption = "very bad";
                _amount = 1;
                _value = 1;
                _type = ItemType.Material;
                _icon = "Material/badcloth";
                _mesh = "Material/badcloth";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            #endregion
            #region Scroll
            case 18:
                _name = "blue";
                _desctiption = "wow";
                _amount = 1;
                _value = 1;
                _type = ItemType.Scroll;
                _icon = "Scrolls/blue";
                _mesh = "Scrolls/blue";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 19:
                _name = "red";
                _desctiption = "hot";
                _amount = 1;
                _value = 1;
                _type = ItemType.Scroll;
                _icon = "Scrolls/red";
                _mesh = "Scrolls/red";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 20:
                _name = "brown";
                _desctiption = "cool";
                _amount = 1;
                _value = 1;
                _type = ItemType.Scroll;
                _icon = "Scrolls/brown";
                _mesh = "Scrolls/brown";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            #endregion
            #region Quest
            case 21:
                _name = "Randy";
                _desctiption = "Oh no he's here";
                _amount = 1;
                _value = 1;
                _type = ItemType.Quest;
                _icon = "Quest/Randy";
                _mesh = "Quest/Randy";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 22:
                _name = "book";
                _desctiption = "magic";
                _amount = 1;
                _value = 1;
                _type = ItemType.Quest;
                _icon = "Quest/book";
                _mesh = "Quest/book";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 23:
                _name = "die";
                _desctiption = "the one you roll dummy";
                _amount = 1;
                _value = 1;
                _type = ItemType.Quest;
                _icon = "Quest/die";
                _mesh = "Quest/die";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            #endregion
            #region Money
            case 24:
                _name = "copper";
                _desctiption = "alright";
                _amount = 1;
                _value = 1;
                _type = ItemType.Money;
                _icon = "Money/copper";
                _mesh = "Money/copper";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 25:
                _name = "silver";
                _desctiption = "good";
                _amount = 1;
                _value = 1;
                _type = ItemType.Money;
                _icon = "Money/silver";
                _mesh = "Money/silver";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 26:
                _name = "gold";
                _desctiption = "very good";
                _amount = 1;
                _value = 1;
                _type = ItemType.Money;
                _icon = "Money/gold";
                _mesh = "Money/gold";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            #endregion
            #region Misc
            case 27:
                _name = "unknown";
                _desctiption = "M̫̹̰̮͇̙̞̪͙̫̬̙͎̅́ͫ͑̎̀͘͟å̸̧̪̣̩̭̻̞̗͇͉̝̪̫̓͑̾̀͘͢͡ͅy̵̡̧̝̼̠͍͖̖̱̩͎͖͙͕͍͍̞̜̼͎ͬͣ̇ͫ̀ͫͧ̉̃ͮ̅͆͜I̷̼͔͕̗̦͓̰̟͚̩̘̞̻̙͕͔̔̌̅ͫ̏͑ͮ̌͐̊̍ͨ͘͢͜͜t̷͔̲̻̮͈̺̯̋̇̄̑̅͗̓ͯ͌̏͂́͑̐̆̐͐ͨS̸̺̟̰͍ͭ̓͌̃ͦ̽ͥͦ̔̀̾͆̑̆́̄̉͜p̷͓̱͓̝ͯ͂̔ͮ̾͒ͤ̀ͩ͒ͧͩͩͯ͜a̵̫̬̹͚͈̜̠̬ͪͮ̏͗̍̏̀ͭ͡r̷̡̢̢̛̠͇̭͙̹͊ͥ̊̑̾̿̎̽̿̇ͭ̅ȩ̴̺͓̲̤̰̞̦̺͋̄̏̃ͨͮͩͩ͆ͫ̓Y̨̧̲͙̣̱̬̪͈͇̠̜̤̘͐͐̎ͨ̐ö̇ͮ͋̉̑͡҉̤̰̙̘̱̮̺̳͓͔͖͙̺̱̬̖u̜̹̩̝͙̹͇̖ͬͯ̃̽͊̏̌͗ͣ̃͗̊̓͝͡";
                _amount = 1;
                _value = 1;
                _type = ItemType.Misc;
                _icon = "Misc/unknown";
                _mesh = "Misc/unknown";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 28:
                _name = "gem";
                _desctiption = "shiny";
                _amount = 1;
                _value = 1;
                _type = ItemType.Misc;
                _icon = "Misc/gem";
                _mesh = "Misc/gem";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
            case 29:
                _name = "tool";
                _desctiption = "handy";
                _amount = 1;
                _value = 1;
                _type = ItemType.Misc;
                _icon = "Misc/tool";
                _mesh = "Misc/tool";
                _damage = 0;
                _armour = 0;
                _durability = 0;
                _heal = 0;
                break;
                #endregion
        }
        Item temp = new Item
        {
            ID = itemID,
            Desctiption = _desctiption,
            Value = _value,
            Amount = _amount,
            Damage = _damage,
            Name = _name,
            Durability = _durability,
            Armour = _armour,
            Heal = _heal,
            Type = _type,
            Icon = Resources.Load("Icons/" + _icon) as Texture,
            ItemMesh = Resources.Load("Mesh/" + _mesh) as GameObject,
        };
        return temp;
    }

}
