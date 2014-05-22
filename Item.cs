using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiddleAges
{
    public class Item : Grid
    {
        private Rectangle rectBorder;
        private Image image;
        private int weight = 0,level,strengthRequired,dexterityReqired, spiritualityReqired;
        private bool focussed = false,equiped = false;
        private MyEnum.ItemType type;
        private Affix affix;
        private Suffix suffix;
        private NpcMenu itemMenu;
        private Grid parent = null;

        public Item(NpcMenu itemMenu, MyEnum.ItemType type, int level)
        {
            this.level = level;
            this.type = type;
            this.weight = 5;
            this.itemMenu = itemMenu;

            Width = MyNumbers.itemWidth;
            Height = MyNumbers.itemHeight;

            rectBorder = new Rectangle();
            rectBorder.Width = Width;
            rectBorder.Height = Height;

            Background = Brushes.Violet;
            HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            VerticalAlignment = System.Windows.VerticalAlignment.Top;

            InitializeImage();
            InitializeAffixSuffix();
            InitializeReqirements();
            MouseRightButtonDown += new MouseButtonEventHandler(delegate(object sender, MouseButtonEventArgs args)
                {
                    InitializeMenu();
                    itemMenu.Margin = new Thickness(args.GetPosition(parent).X - itemMenu.Width / 2, args.GetPosition(parent).Y, 0, 0);
                });

            SizeChanged += new SizeChangedEventHandler(delegate(object sender, SizeChangedEventArgs args)
                {
                    rectBorder.Width = Width;
                    rectBorder.Height = Height;
                });

            this.Children.Add(image);
            this.Children.Add(rectBorder);
        }

        public void InitializeImage()
        {
            image = new Image();
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(ItemImages.GetPath(type));
            myBitmapImage.DecodePixelWidth = 100;
            myBitmapImage.EndInit();
            image.Source = myBitmapImage;
        }

        private void InitializeAffixSuffix()
        {
            if (MyRandom.Chance(90))
            {
                affix = MyRandom.RandomAffix(level);
            }

            if (MyRandom.Chance(90))
            {
                suffix = MyRandom.RandomSuffix(level);
            }
        }

        private void InitializeReqirements()
        {
            dexterityReqired=0;
            strengthRequired=0;
            spiritualityReqired=0;

            switch (type)
            {
                #region Jewelery

                case MyEnum.ItemType.Amulet:
                    if (level < MyNumbers.affixSuffix_midMagicItemLevel)
                    {
                        spiritualityReqired = 20;
                    }
                    else if (level >= MyNumbers.affixSuffix_midMagicItemLevel && level < MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        spiritualityReqired = 30;
                    }
                    else if (level >= MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        spiritualityReqired = 40;
                    }
                    break;
                case MyEnum.ItemType.Ring:
                    if (level < MyNumbers.affixSuffix_midMagicItemLevel)
                    {
                        spiritualityReqired = 15;
                    }
                    else if (level >= MyNumbers.affixSuffix_midMagicItemLevel && level < MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        spiritualityReqired = 20;
                    }
                    else if (level >= MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        spiritualityReqired = 30;
                    }
                    break;
                case MyEnum.ItemType.Book:
                    if (level < MyNumbers.affixSuffix_midMagicItemLevel)
                    {
                        spiritualityReqired = 30;
                    }
                    else if (level >= MyNumbers.affixSuffix_midMagicItemLevel && level < MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        spiritualityReqired = 60;
                    }
                    else if (level >= MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        spiritualityReqired = 200;
                    }
                    break;

                #endregion

                #region Weapons

                case MyEnum.ItemType.Axe:
                    if (level >= MyNumbers.affixSuffix_midMagicItemLevel && level < MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 40;
                    }
                    else if (level >= MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 120;
                    }
                    break;
                case MyEnum.ItemType.Sword:
                    if (level >= MyNumbers.affixSuffix_midMagicItemLevel && level < MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 40;
                        dexterityReqired = 30;
                    }
                    else if (level >= MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 90;
                        dexterityReqired = 90;
                    }
                    break;

                #endregion

                #region Armors

                case MyEnum.ItemType.Helm:
                    if (level < MyNumbers.affixSuffix_midMagicItemLevel)
                    {
                        strengthRequired = 15;
                        dexterityReqired = 15;
                    }
                    else if (level >= MyNumbers.affixSuffix_midMagicItemLevel && level < MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 40;
                        dexterityReqired = 20;
                    }
                    else if (level >= MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 75;
                        dexterityReqired = 50;
                    }
                    break;
                case MyEnum.ItemType.Chest:
                    if (level < MyNumbers.affixSuffix_midMagicItemLevel)
                    {
                        strengthRequired = 25;
                    }
                    else if (level >= MyNumbers.affixSuffix_midMagicItemLevel && level < MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 50;
                    }
                    else if (level >= MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 70;
                    }
                    break;
                case MyEnum.ItemType.Belt:
                    if (level >= MyNumbers.affixSuffix_midMagicItemLevel && level < MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 35;
                    }
                    else if (level >= MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 55;
                    }
                    break;
                case MyEnum.ItemType.Gloves:
                    if (level >= MyNumbers.affixSuffix_midMagicItemLevel && level < MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 40;
                    }
                    else if (level >= MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 65;
                    }
                    break;
                case MyEnum.ItemType.Boots:
                    if (level >= MyNumbers.affixSuffix_midMagicItemLevel && level < MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        dexterityReqired = 30;
                    }
                    else if (level >= MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        dexterityReqired = 50;
                    }
                    break;
                case MyEnum.ItemType.Shield:
                    if (level < MyNumbers.affixSuffix_midMagicItemLevel)
                    {
                        strengthRequired = 40;
                    }
                    else if (level >= MyNumbers.affixSuffix_midMagicItemLevel && level < MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 80;
                    }
                    else if (level >= MyNumbers.affixSuffix_maxMagicItemLevel)
                    {
                        strengthRequired = 200;
                    }
                    break;
                #endregion
            }
        }

        public void Focussed()
        {
            if (focussed)
            {
                rectBorder.Stroke = null;
                focussed = false;
            }
            else
            {
                rectBorder.Stroke = Brushes.Black;
                focussed = true;
            }
        }

        public void AddReqirements(MyToolTip toolTip)
        {
            if (strengthRequired > 0 || spiritualityReqired > 0 || dexterityReqired > 0)
            {
                toolTip.AddBoldText("Requirements:");

                if (strengthRequired > 0)
                {
                    if (strengthRequired > MyContainer.character.Strength)
                    {
                        toolTip.AddNormalText(strengthRequired + " strength", MyBrushes.cannotUse);
                    }
                    else
                    {
                        toolTip.AddNormalText(strengthRequired + " strength");
                    }
                }

                if (dexterityReqired > 0)
                {
                    if (dexterityReqired > MyContainer.character.Dexterity)
                    {
                        toolTip.AddNormalText(dexterityReqired + " dexterity", MyBrushes.cannotUse);
                    }
                    else
                    {
                        toolTip.AddNormalText(dexterityReqired + " dexterity");
                    }
                }

                if (spiritualityReqired > 0)
                {
                    if (spiritualityReqired > MyContainer.character.Spirituality)
                    {
                        toolTip.AddNormalText(spiritualityReqired + " spirituality", MyBrushes.cannotUse);
                    }
                    else
                    {
                        toolTip.AddNormalText(spiritualityReqired + " spirituality");
                    }
                }

                toolTip.AddNormalText("");
            }
        }

        public void InitializeMenu()
        {
            itemMenu.Clear();

            if (parent.GetType().Name == "Inventory")
            {
                if (equiped)
                {
                    itemMenu.AddItem("Unequip", delegate(object sender, MouseButtonEventArgs args)
                    {
                        if (((Inventory)parent).Unequip(this))
                        {
                            equiped = false;
                            itemMenu.Hide();
                        }
                    });
                }
                else
                {
                    itemMenu.AddItem("Equip", delegate(object sender, MouseButtonEventArgs args)
                    {
                        if (((Inventory)parent).Equip(this))
                        {
                            equiped = true;
                            itemMenu.Hide();
                        }
                    });
                }

                itemMenu.AddItem("Destroy", delegate(object sender, MouseButtonEventArgs args)
                {
                    ((Inventory)parent).RemoveFromItemStore(this);
                    itemMenu.Hide();
                });
            }
            else if (parent.GetType().Name == "Loot")
            {
                itemMenu.AddItem("Put to inventory", delegate(object sender, MouseButtonEventArgs args)
                {
                    ((Loot)parent).RemoveFromItemStore(this);
                    MyContainer.ui.AddItem(this, true);
                    itemMenu.Hide();

                    if (((Loot)parent).IsEmpty)
                    {
                        ((Loot)parent).Hide();
                    }
                });
            }
            itemMenu.Show();
        }

        public virtual void ShowToolTip()
        {

        }

        public bool Border
        {
            get
            {
                return focussed;
            }
        }

        public Grid MyParent
        {
            set
            {
                parent = value;
            }
        }

        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        public Image Image
        {
            get
            {
                return image;
            }
        }

        public bool Equiped
        {
            get
            {
                return equiped;
            }
        }

        public MyEnum.ItemType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                this.Children.Remove(image);
                InitializeImage();
                ShowToolTip();
                this.Children.Add(image);
            }
        }

        public int Level
        {
            get
            {
                return level;
            }
        }

        public int SpiritualityReqired
        {
            get
            {
                return spiritualityReqired;
            }
            set
            {
                spiritualityReqired = value;
                ShowToolTip();
            }
        }

        public int StrengthReqired
        {
            get
            {
                return strengthRequired;
            }
            set
            {
                strengthRequired = value;
                ShowToolTip();
            }
        }

        public int DexterityReqired
        {
            get
            {
                return dexterityReqired;
            }
            set
            {
                dexterityReqired = value;
                ShowToolTip();
            }
        }

        public Affix Affix
        {
            get
            {
                return affix;
            }
            set
            {
                affix = value;
                ShowToolTip();
            }
        }

        public Suffix Suffix
        {
            get
            {
                return suffix;
            }
            set
            {
                suffix = value;
                ShowToolTip();
            }
        }

        public static Item Copy(Item item, NpcMenu itemMenu)
        {
            Item ret = null;

            switch (GetItemGroup(item.Type))
            {
                case MyEnum.ItemGroup.Jewelery:
                    ret = new Jewelery(itemMenu, item.level);
                    break;
                case MyEnum.ItemGroup.Armor:
                    ret = new Armor(itemMenu, item.level);
                    ((Armor)ret).Defense = ((Armor)item).Defense;
                    break;
                case MyEnum.ItemGroup.Weapon:
                    ret = new Weapon(itemMenu, item.level);
                    ((Weapon)ret).MaxDamage = ((Weapon)item).MaxDamage;
                    ((Weapon)ret).MinDamage = ((Weapon)item).MinDamage;
                    break;
                case MyEnum.ItemGroup.Shield:
                     ret = new Shield(itemMenu, item.level);
                     ((Shield)ret).Defense = ((Shield)item).Defense;
                     ((Shield)ret).Block = ((Shield)item).Block;
                    break;
                case MyEnum.ItemGroup.Book:
                     ret = new Book(itemMenu, item.level);
                     ((Book)ret).MagicSkill = ((Book)item).MagicSkill;
                    break;
                case MyEnum.ItemGroup.Nothing:
                    ret = null;
                    break;
            }

            if (ret != null)
            {
                ret.Type = item.Type;
                ret.Weight = item.Weight;
                ret.Affix = item.Affix;
                ret.Suffix = item.Suffix;
                ret.StrengthReqired = item.StrengthReqired;
                ret.DexterityReqired = item.DexterityReqired;
                ret.SpiritualityReqired = item.SpiritualityReqired;
            }

            return ret;
        }

        public static MyEnum.ItemGroup GetItemGroup(MyEnum.ItemType type)
        {
            switch (type)
            {
                case MyEnum.ItemType.Axe:
                case MyEnum.ItemType.Sword:
                    return MyEnum.ItemGroup.Weapon;
                case MyEnum.ItemType.Belt:
                case MyEnum.ItemType.Chest:
                case MyEnum.ItemType.Helm:
                case MyEnum.ItemType.Gloves:
                case MyEnum.ItemType.Boots:
                    return MyEnum.ItemGroup.Armor;
                case MyEnum.ItemType.Shield:
                    return MyEnum.ItemGroup.Shield;
                case  MyEnum.ItemType.Amulet:
                case MyEnum.ItemType.Ring:
                    return MyEnum.ItemGroup.Jewelery;
                case MyEnum.ItemType.Book:
                    return MyEnum.ItemGroup.Book;
                case MyEnum.ItemType.Nothing:
                    return MyEnum.ItemGroup.Nothing;
            }

            return MyEnum.ItemGroup.Nothing;
        }
    }
}
