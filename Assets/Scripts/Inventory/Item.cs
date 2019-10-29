using UnityEngine;

public class Item 
{
    #region Private Var
    string _name;
    string _desctiption;
    int _value;
    int _id;
    int _amount;
    int _damage;
    int _durability;
    int _armour;
    int _heal;
    Sprite _icon;
    GameObject _mesh;
    ItemType _type;
    #endregion
    #region Public Prop
    public string Name { get { return _name; } set { _name = value; } }
    public string Desctiption { get { return _desctiption; } set { _desctiption = value; } }
    public int ID { get { return _id; } set { _id = value; } }
    public int Value { get { return _value; } set { _value = value; } }
    public int Amount { get { return _amount; } set { _amount = value; } }
    public int Damage { get { return _damage; } set { _damage = value; } }
    public int Durability { get { return _durability; } set { _durability = value; } }
    public int Armour { get { return _armour; } set { _armour = value; } }
    public int Heal { get { return _heal; } set { _heal = value; } }
    public Sprite Icon { get { return _icon; } set { _icon = value; } }
    public GameObject ItemMesh { get { return _mesh; } set { _mesh = value; } }
    public ItemType Type { get { return _type; } set { _type = value; } }
    #endregion
}
#region Enum
public enum ItemType
{
    All,
    Apparrel,
    Consumable,
    Weapon,
    Potion,
    Food,
    Material,
    Scroll,
    Quest,
    Money,
    Misc
}
#endregion