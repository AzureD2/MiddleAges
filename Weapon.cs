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
    class Weapon:Item
    {
        private string name;
        private int minDamage, maxDamage;

        public Weapon(NpcMenu itemMenu, int level)
            : base(itemMenu, MyRandom.RandomItemType(MyEnum.ItemGroup.Weapon), level)
        {
            Weight = MyNumbers.itemWeaponWeight;
            InitializeDamage();
            ShowToolTip();
        }

        private void InitializeDamage()
        {
            Random rd = new Random();

            if (Level >= 1 && Level < MyNumbers.affixSuffix_midMagicItemLevel)
            {
                minDamage = rd.Next(1, 3);
                maxDamage = rd.Next(5, 7);
            }
            else if (Level >= MyNumbers.affixSuffix_midMagicItemLevel &&
                     Level < MyNumbers.affixSuffix_maxMagicItemLevel)
            {
                minDamage = rd.Next(6, 10);
                maxDamage = rd.Next(15, 20);
            }
            else
            {
                minDamage = rd.Next(25, 35);
                maxDamage = rd.Next(45, 60);
            }
        }

        public override void ShowToolTip()
        {
            MyToolTip toolTip = new MyToolTip(150);

            if (Affix != null && Suffix != null)
            {
                name = Affix.Name + Type.ToString() + Suffix.Name;
                toolTip.AddText(name, "Damage : "+minDamage+" - "+maxDamage, MyBrushes.itemMagic);
                AddReqirements(toolTip);

                if (Affix.Atribute.ToString() == Suffix.Atribute.ToString())
                {
                    toolTip.AddNormalText(Affix.ToString(Suffix), MyBrushes.itemProperty);
                }
                else
                {
                    toolTip.AddNormalText(Affix.ToString(), MyBrushes.itemProperty);
                    toolTip.AddNormalText(Suffix.ToString(), MyBrushes.itemProperty);
                }
            }
            else if (Affix != null)
            {
                name = Affix.Name + Type.ToString();
                toolTip.AddText(name, "Damage : " + minDamage + " - " + maxDamage, MyBrushes.itemMagic);
                AddReqirements(toolTip);
                toolTip.AddNormalText(Affix.ToString(), MyBrushes.itemProperty);
            }
            else if (Suffix != null)
            {
                name = Suffix.Name + Type.ToString();
                toolTip.AddText(name, "Damage : " + minDamage + " - " + maxDamage, MyBrushes.itemMagic);
                AddReqirements(toolTip);
                toolTip.AddNormalText(Suffix.ToString(), MyBrushes.itemProperty);
            }
            else
            {
                name = Type.ToString();
                toolTip.AddText(name, "Damage : " + minDamage + " - " + maxDamage);
                AddReqirements(toolTip);
            }


            ToolTip = toolTip;
            ToolTipService.SetShowDuration(this, 30000);
        }

        public int MinDamage
        {
            get
            {
                return minDamage;
            }
            set
            {
                minDamage = value;
                ShowToolTip();
            }
        }

        public int MaxDamage
        {
            get
            {
                return maxDamage;
            }
            set
            {
                maxDamage = value;
                ShowToolTip();
            }
        }
    }
}
