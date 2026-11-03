using UnityEngine;
using ConditionalLog;

namespace Monet5379
{
    public static class Colors
    {
        public static readonly Color White = new Color32(255, 255, 255, 255); // #FFFFFF
        public static readonly Color FloralWhite = new Color32(255, 250, 240, 255); // #FFFAF0
        public static readonly Color GhostWhite = new Color32(248, 248, 255, 255); // #F8F8FF
        public static readonly Color AntiqueWhite = new Color32(250, 235, 215, 255); // #F5E6E0
        public static readonly Color NavajoWhite = new Color32(255, 222, 173, 255); // #FFDAB9
        public static readonly Color WhiteSmoke = new Color32(245, 245, 245, 255); // #F5F5F5
        public static readonly Color CreamIvory = new Color32(250, 250, 248, 255); // #FAFAF8

        public static readonly Color BlackGray = new Color32(40, 40, 40, 255); // #282828
        public static readonly Color DarkGray = new Color32(169, 169, 169, 255); // #A9A9A9
        public static readonly Color DarkSlateGray = new Color32(47, 79, 79, 255); // #2F4F4F
        public static readonly Color DimGray = new Color32(105, 105, 105, 255); // #696969
        public static readonly Color Gray = new Color32(128, 128, 128, 255); // #808080
        public static readonly Color LightGray = new Color32(211, 211, 211, 255); // #D3D3D3
        public static readonly Color LightSlateGray = new Color32(119, 136, 153, 255); // #778899
        public static readonly Color SlateGray = new Color32(112, 128, 144, 255); // #708090
        public static readonly Color Black = new Color32(0, 0, 0, 255); // #000000
        public static readonly Color DeepCharcoalBlack = new Color32(26, 26, 26, 255); // #1A1A1A

        public static readonly Color ReunoYellow = new Color32(255, 196, 0, 255); // #FFC400
        public static readonly Color Aqua = new Color32(0, 255, 255, 255); // #00FFFF
        public static readonly Color Aquamarine = new Color32(127, 255, 212, 255); // #7FFFDC
        public static readonly Color Azure = new Color32(240, 255, 255, 255); // #F0FFFF
        public static readonly Color Beige = new Color32(245, 245, 220, 255); // #F5F5DC
        public static readonly Color Bisque = new Color32(255, 228, 196, 255); // #FFE4C4

        public static readonly Color BlanchedAlmond = new Color32(255, 235, 205, 255); // #FFEBCD

        public static readonly Color AliceBlue = new Color32(240, 248, 255, 255); // #F0F8FF
        public static readonly Color Blue = new Color32(0, 0, 255, 255); // #0000FF
        public static readonly Color BlueViolet = new Color32(138, 43, 226, 255); // #8A2BE2
        public static readonly Color CadetBlue = new Color32(95, 158, 160, 255); // #5F9EA0
        public static readonly Color CornflowerBlue = new Color32(100, 149, 237, 255); // #6495ED
        public static readonly Color DarkBlue = new Color32(0, 0, 139, 255); // #00008B
        public static readonly Color DarkSlateBlue = new Color32(72, 61, 139, 255); // #483D8B
        public static readonly Color DeepSkyBlue = new Color32(0, 191, 255, 255); // #00BFFF
        public static readonly Color DodgerBlue = new Color32(30, 144, 255, 255); // #1e90ff
        public static readonly Color EvasionBlue = new Color32(30, 144, 255, 255); // #1E90FF
        public static readonly Color LightBlue = new Color32(173, 216, 230, 255); // #ADD8E6
        public static readonly Color LightSkyBlue = new Color32(135, 206, 250, 255); // #87CEFA
        public static readonly Color LightSteelBlue = new Color32(176, 196, 222, 255); // #B0C4DE
        public static readonly Color MediumBlue = new Color32(0, 0, 205, 255); // #0000CD
        public static readonly Color MediumSlateBlue = new Color32(123, 104, 238, 255); // #7B68EE
        public static readonly Color MidnightBlue = new Color32(25, 25, 112, 255); // #191970
        public static readonly Color PowderBlue = new Color32(176, 224, 230, 255); // #B0E0E6
        public static readonly Color RoyalBlue = new Color32(65, 105, 225, 255); // #4169E1
        public static readonly Color SkyBlue = new Color32(135, 206, 235, 255); // #87CEEB
        public static readonly Color SlateBlue = new Color32(106, 90, 205, 255); // #6A5ACD
        public static readonly Color SteelBlue = new Color32(70, 130, 180, 255); // #4682B4

        public static readonly Color CherryRed = new Color32(210, 38, 56, 255); // #D72638
        public static readonly Color DarkRed = new Color32(139, 0, 0, 255); // #8B0000
        public static readonly Color IndianRed = new Color32(205, 92, 92, 255); // #CD5C5C
        public static readonly Color MediumVioletRed = new Color32(199, 21, 133, 255); // #C71585
        public static readonly Color Red = new Color32(255, 0, 0, 255); // #FF0000
        public static readonly Color BestRed = new Color32(255, 24, 0, 255); // #FF1800
        public static readonly Color Crimson = new Color32(220, 20, 60, 255); // #DC143C
        public static readonly Color DeepPink = new Color32(255, 20, 147, 255); // #FF1493
        public static readonly Color FireBrick = new Color32(178, 34, 34, 255); // #B22222
        public static readonly Color Brown = new Color32(165, 42, 42, 255); // #A52A2A

        public static readonly Color Chartreuse = new Color32(127, 255, 0, 255); // #7FFF00
        public static readonly Color DarkGreen = new Color32(0, 100, 0, 255); // #006400
        public static readonly Color DarkOliveGreen = new Color32(85, 107, 47, 255); // #556B2F
        public static readonly Color ForestGreen = new Color32(34, 139, 34, 255); // #228B22
        public static readonly Color Green = new Color32(0, 128, 0, 255); // #008000
        public static readonly Color GreenYellow = new Color32(173, 255, 47, 255); // #ADFF2F
        public static readonly Color LawnGreen = new Color32(124, 252, 0, 255); // #7CFC00
        public static readonly Color LightGreen = new Color32(144, 238, 144, 255); // #90EE90
        public static readonly Color Lime = new Color32(0, 255, 0, 255); // #00FF00
        public static readonly Color LimeGreen = new Color32(50, 205, 50, 255); // #32CD32
        public static readonly Color MediumAquamarine = new Color32(102, 205, 170, 255); // #66CDAA
        public static readonly Color MediumSeaGreen = new Color32(60, 179, 113, 255); // #3CB371
        public static readonly Color MediumSpringGreen = new Color32(0, 250, 154, 255); // #00FA9A
        public static readonly Color MediumTurquoise = new Color32(72, 209, 204, 255); // #48D1CC
        public static readonly Color Olive = new Color32(128, 128, 0, 255); // #808000
        public static readonly Color OliveDrab = new Color32(107, 142, 35, 255); // #6B8E23
        public static readonly Color PaleGreen = new Color32(152, 251, 152, 255); // #98FB98
        public static readonly Color SeaGreen = new Color32(46, 139, 87, 255); // #2E8B57
        public static readonly Color SpringGreen = new Color32(0, 255, 127, 255); // #00FF7F
        public static readonly Color YellowGreen = new Color32(154, 205, 50, 255); // #9ACD32
        public static readonly Color DarkSeaGreen = new Color32(143, 188, 143, 255); // #8FBC8F

        public static readonly Color BurlyWood = new Color32(222, 184, 135, 255); // #DEB887
        public static readonly Color Chocolate = new Color32(210, 105, 30, 255); // #D2691E
        public static readonly Color Coral = new Color32(255, 127, 80, 255); // #FF7F50
        public static readonly Color CornSilk = new Color32(255, 248, 220, 255); // #FFF8DC
        public static readonly Color Cyan = new Color32(0, 255, 255, 255); // #00FFFF
        public static readonly Color DarkCyan = new Color32(0, 139, 139, 255); // #008B8B
        public static readonly Color DarkGoldenrod = new Color32(184, 134, 11, 255); // #B8860B
        public static readonly Color DarkKhaki = new Color32(189, 183, 107, 255); // #BDB76B
        public static readonly Color DarkMagenta = new Color32(139, 0, 139, 255); // #8B008B
        public static readonly Color DarkOrange = new Color32(255, 140, 0, 255); // #FF8C00
        public static readonly Color DarkOrchid = new Color32(153, 50, 204, 255); // #9932CC

        public static readonly Color DarkSalmon = new Color32(233, 150, 122, 255); // #E9967A
        public static readonly Color DarkTurquoise = new Color32(0, 206, 209, 255); // #00CED1
        public static readonly Color DarkViolet = new Color32(148, 0, 211, 255); // #9400D3
        public static readonly Color Fuchsia = new Color32(255, 0, 255, 255); // #FF00FF
        public static readonly Color Gainsboro = new Color32(220, 220, 220, 255); // #DCDCDC
        public static readonly Color Gold = new Color32(255, 215, 0, 255); // #FFD700
        public static readonly Color Goldenrod = new Color32(218, 165, 32, 255); // #DAA520
        public static readonly Color Honeydew = new Color32(240, 255, 240, 255); // #F0FFF0
        public static readonly Color HotPink = new Color32(255, 105, 180, 255); // #FF69B4
        public static readonly Color Indigo = new Color32(75, 0, 130, 255); // #4B0082
        public static readonly Color Ivory = new Color32(255, 255, 240, 255); // #FFFFF0
        public static readonly Color Khaki = new Color32(240, 230, 140, 255); // #F0E68C
        public static readonly Color Lavender = new Color32(230, 230, 250, 255); // #E6E6FA
        public static readonly Color LavenderBlush = new Color32(255, 240, 245, 255); // #FFF0F5
        public static readonly Color LemonChiffon = new Color32(255, 250, 205, 255); // #FFFAE8
        public static readonly Color LightCoral = new Color32(240, 128, 128, 255); // #F08080
        public static readonly Color LightCyan = new Color32(224, 255, 255, 255); // #E0FFFF
        public static readonly Color LightGoldenrodYellow = new Color32(250, 250, 210, 255); // #FAFAD2
        public static readonly Color LightPink = new Color32(255, 182, 193, 255); // #FFB6C1
        public static readonly Color LightSalmon = new Color32(255, 160, 122, 255); // #FFA07A
        public static readonly Color LightSeaGreen = new Color32(32, 178, 170, 255); // #20B2AA - Light Sea Green
        public static readonly Color LightYellow = new Color32(255, 255, 224, 255); // #FFFFE0
        public static readonly Color Linen = new Color32(250, 240, 230, 255); // #FAF0E6
        public static readonly Color Magenta = new Color32(255, 0, 255, 255); // #FF00FF
        public static readonly Color Maroon = new Color32(128, 0, 0, 255); // #800000
        public static readonly Color MediumOrchid = new Color32(186, 85, 211, 255); // #BA55D3
        public static readonly Color MediumPurple = new Color32(147, 112, 219, 255); // #9370DB
        public static readonly Color MintCream = new Color32(245, 255, 250, 255); // #F5FFFA
        public static readonly Color MistyRose = new Color32(255, 228, 225, 255); // #FFE4E1
        public static readonly Color Moccasin = new Color32(255, 228, 181, 255); // #FFE4B5

        public static readonly Color Navy = new Color32(0, 0, 128, 255); // #000080
        public static readonly Color OldLace = new Color32(253, 245, 230, 255); // #FDF5E6
        public static readonly Color Orange = new Color32(255, 165, 0, 255); // #FFA500
        public static readonly Color OrangeRed = new Color32(255, 69, 0, 255); // #FF4500
        public static readonly Color Orchid = new Color32(218, 112, 214, 255); // #DA70D6
        public static readonly Color PaleGoldenrod = new Color32(238, 232, 170, 255); // #EEE8AA
        public static readonly Color PaleTurquoise = new Color32(175, 238, 238, 255); // #AFEEEE
        public static readonly Color PaleVioletRed = new Color32(219, 112, 147, 255); // #DB7093
        public static readonly Color PapayaWhip = new Color32(255, 239, 213, 255); // #FFEFD5
        public static readonly Color PeachPuff = new Color32(255, 218, 185, 255); // #FFDAB9
        public static readonly Color Peru = new Color32(205, 133, 63, 255); // #CD853F
        public static readonly Color Pink = new Color32(255, 192, 203, 255); // #FFC0CB
        public static readonly Color Plum = new Color32(221, 160, 221, 255); // #DDA0DD
        public static readonly Color Purple = new Color32(128, 0, 128, 255); // #800080
        public static readonly Color RosyBrown = new Color32(188, 143, 143, 255); // #BC8F8F
        public static readonly Color SaddleBrown = new Color32(139, 69, 19, 255); // #8B4513
        public static readonly Color Salmon = new Color32(250, 128, 114, 255); // #FA8072
        public static readonly Color SandyBrown = new Color32(244, 164, 96, 255); // #F4A460
        public static readonly Color Seashell = new Color32(255, 245, 238, 255); // #FFF5EE
        public static readonly Color Sienna = new Color32(160, 82, 45, 255); // #A0522D
        public static readonly Color Silver = new Color32(192, 192, 192, 255); // #C0C0C0
        public static readonly Color Snow = new Color32(255, 250, 250, 255); // #FFFAFA
        public static readonly Color Tan = new Color32(210, 180, 140, 255); // #D2B48C
        public static readonly Color Teal = new Color32(0, 128, 128, 255); // #008080
        public static readonly Color Thistle = new Color32(216, 191, 216, 255); // #D8BFD8
        public static readonly Color Tomato = new Color32(255, 99, 71, 255); // #FF6347
        public static readonly Color Turquoise = new Color32(64, 224, 208, 255); // #40E0D0
        public static readonly Color Violet = new Color32(238, 130, 238, 255); // #EE82EE
        public static readonly Color Wheat = new Color32(245, 222, 179, 255); // #F5DEB3
        public static readonly Color Yellow = new Color32(255, 255, 0, 255); // #FFFF00

        public static Color EnterButtonText => new Color32(153, 160, 255, 255);

        public static Color ActivateYellow => new Color32(255, 219, 24, 255);

        public static Color Dev => IndianRed;

        public static Color Detect => LightGoldenrodYellow;

        public static Color Physical => new Color32(255, 165, 0, 255); // Orange

        public static Color Magical => new Color32(106, 90, 205, 255); // SlateBlue

        // (Elements)

        public static Color Fire => new Color32(230, 60, 0, 255); //E63E00

        public static Color Cold => new Color32(100, 209, 251, 255); //64D1FB

        public static Color Lightning => new Color32(251, 229, 80, 255); //FBE550

        public static Color Holy => new Color32(255, 255, 255, 255); //FF3700

        public static Color Darkness => new Color32(149, 54, 184, 255); //9536B8

        public static Color Bleed => new Color32(208, 23, 0, 255); //D01700

        public static Color Poison => new Color32(0, 119, 19, 255); //007713

        public static Color Vulnerable => new Color32(82, 36, 220, 255); //5325DE

        public static Color Dazed => new Color32(127, 154, 192, 255); //7F9AC0

        public static Color Paralysis => new Color32(237, 0, 252, 255); //ED00FC

        //

        public static Color Normal => new Color32(159, 159, 159, 255); // 9F9F9F
        public static Color Magic => new Color32(0, 113, 219, 255); // 0071DB
        public static Color Rare => new Color32(213, 219, 15, 255); // D5DB0F
        public static Color Legendary => new Color32(233, 139, 30, 255); // FF4F00
        public static Color Mythic => new Color32(83, 128, 80, 255); // 538050

        public static string NormalHex => "#9F9F9F";
        public static string MagicHex => "#0071DB";
        public static string RareHex => "#D5DB0F";
        public static string LegendaryHex => "#FF4F00";
        public static string MythicHex => "#538050";

        //

        public static Color MagicStone => new Color32(226, 0, 91, 255); //E2005B
        public static Color RubyKey => new Color32(254, 26, 0, 255); //FE1A00
        public static Color SoulShard => new Color32(156, 16, 44, 255); //9C102C
        public static Color OblivionCrystal => new Color32(115, 2, 221, 255); //7302DD
        public static Color RiftShard => new Color32(232, 49, 247, 255); //E831F7

        public static Color Value => new Color32(171, 106, 22, 255); //AB6A16
        public static Color MaxValue => LightSkyBlue;
        public static Color Select => new Color32(124, 252, 0, 255); //7CFC00
        public static Color Description => new Color32(168, 168, 168, 255);
        public static Color SubDescription => new Color32(128, 128, 128, 255);
        public static Color Disable => new Color32(108, 108, 108, 255);
        public static Color AdditionalLevel => SteelBlue;
        public static Color EquipmentCategory => new Color32(0, 70, 131, 255);
        public static Color Stat => ReunoYellow;
        public static Color Buff => Aquamarine;
        public static Color Character => Violet;
        public static Color ForceVelocity => LavenderBlush;
        public static Color Hitmark => Brown;
        public static Color Projectile => BurlyWood;
        public static Color Item => SkyBlue;
        public static Color Item2 => SteelBlue;
        public static Color Passive => YellowGreen;
        public static Color Stage => new Color32(82, 122, 54, 255); // 527A36
        public static Color LogStage => DarkSeaGreen;
        public static Color LogArea => LightSeaGreen;
        public static Color Skill => CornflowerBlue;
        public static Color Quest => DarkCyan;
        public static Color UI => DarkMagenta;
        public static Color AI => DarkViolet;
        public static Color Enhance => new Color32(116, 59, 167, 255); //743BA7
        public static Color Successor => new Color32(255, 1, 39, 255); //FF0127

        public static Color GetColor(int index)
        {
            if (index > 142)
            {
                index %= 142;
            }

            switch (index)
            {
                case 1: return ReunoYellow;
                case 2: return BestRed;
                case 3: return AliceBlue;
                case 4: return AntiqueWhite;
                case 5: return Aqua;
                case 6: return Aquamarine;
                case 7: return Azure;
                case 8: return Beige;
                case 9: return Bisque;
                case 10: return Black;
                case 11: return BlanchedAlmond;
                case 12: return Blue;
                case 13: return BlueViolet;
                case 14: return Brown;
                case 15: return BurlyWood;
                case 16: return CadetBlue;
                case 17: return Chartreuse;
                case 18: return Chocolate;
                case 19: return Coral;
                case 20: return CornflowerBlue;
                case 21: return CornSilk;
                case 22: return Crimson;
                case 23: return Cyan;
                case 24: return DarkBlue;
                case 25: return DarkCyan;
                case 26: return DarkGoldenrod;
                case 27: return DarkGray;
                case 28: return DarkGreen;
                case 29: return DarkKhaki;
                case 30: return DarkMagenta;
                case 31: return DarkOliveGreen;
                case 32: return DarkOrange;
                case 33: return DarkOrchid;
                case 34: return DarkRed;
                case 35: return DarkSalmon;
                case 36: return DarkSeaGreen;
                case 37: return DarkSlateBlue;
                case 38: return DarkSlateGray;
                case 39: return DarkTurquoise;
                case 40: return DarkViolet;
                case 41: return DeepPink;
                case 42: return DeepSkyBlue;
                case 43: return DimGray;
                case 44: return DodgerBlue;
                case 45: return FireBrick;
                case 46: return FloralWhite;
                case 47: return ForestGreen;
                case 48: return Fuchsia;
                case 49: return Gainsboro;
                case 50: return GhostWhite;
                case 51: return Gold;
                case 52: return Goldenrod;
                case 53: return Gray;
                case 54: return Green;
                case 55: return GreenYellow;
                case 56: return Honeydew;
                case 57: return HotPink;
                case 58: return IndianRed;
                case 59: return Indigo;
                case 60: return Ivory;
                case 61: return Khaki;
                case 62: return Lavender;
                case 63: return LavenderBlush;
                case 64: return LawnGreen;
                case 65: return LemonChiffon;
                case 66: return LightBlue;
                case 67: return LightCoral;
                case 68: return LightCyan;
                case 69: return LightGoldenrodYellow;
                case 70: return LightGray;
                case 71: return LightGreen;
                case 72: return LightPink;
                case 73: return LightSalmon;
                case 74: return LightSeaGreen;
                case 75: return LightSkyBlue;
                case 76: return LightSlateGray;
                case 77: return LightSteelBlue;
                case 78: return LightYellow;
                case 79: return Lime;
                case 80: return LimeGreen;
                case 81: return Linen;
                case 82: return Magenta;
                case 83: return Maroon;
                case 84: return MediumAquamarine;
                case 85: return MediumBlue;
                case 86: return MediumOrchid;
                case 87: return MediumPurple;
                case 88: return MediumSeaGreen;
                case 89: return MediumSlateBlue;
                case 90: return MediumSpringGreen;
                case 91: return MediumTurquoise;
                case 92: return MediumVioletRed;
                case 93: return MidnightBlue;
                case 94: return MintCream;
                case 95: return MistyRose;
                case 96: return Moccasin;
                case 97: return NavajoWhite;
                case 98: return Navy;
                case 99: return OldLace;
                case 100: return Olive;
                case 101: return OliveDrab;
                case 102: return Orange;
                case 103: return OrangeRed;
                case 104: return Orchid;
                case 105: return PaleGoldenrod;
                case 106: return PaleGreen;
                case 107: return PaleTurquoise;
                case 108: return PaleVioletRed;
                case 109: return PapayaWhip;
                case 110: return PeachPuff;
                case 111: return Peru;
                case 112: return Pink;
                case 113: return Plum;
                case 114: return PowderBlue;
                case 115: return Purple;
                case 116: return Red;
                case 117: return RosyBrown;
                case 118: return RoyalBlue;
                case 119: return SaddleBrown;
                case 120: return Salmon;
                case 121: return SandyBrown;
                case 122: return SeaGreen;
                case 123: return Seashell;
                case 124: return Sienna;
                case 125: return Silver;
                case 126: return SkyBlue;
                case 127: return SlateBlue;
                case 128: return SlateGray;
                case 129: return Snow;
                case 130: return SpringGreen;
                case 131: return SteelBlue;
                case 132: return Tan;
                case 133: return Teal;
                case 134: return Thistle;
                case 135: return Tomato;
                case 136: return Turquoise;
                case 137: return Violet;
                case 138: return Wheat;
                case 139: return CreamIvory;
                case 140: return WhiteSmoke;
                case 141: return Yellow;
                case 142: return YellowGreen;

                default:
                    return CreamIvory;
            }
        }

        public static string GetColorHex(int index)
        {
            Color color = GetColor(index);

            return ColorEx.ColorToHex(color);
        }

        //

        //

        public static void LogAllColors()
        {
            GameLog.Info($"ReunoYellow, {ColorEx.ColorToHex(ReunoYellow)}".ToColorString(ReunoYellow));
            GameLog.Info($"BestRed, {ColorEx.ColorToHex(BestRed)}".ToColorString(BestRed));
            GameLog.Info($"AliceBlue, {ColorEx.ColorToHex(AliceBlue)}".ToColorString(AliceBlue));
            GameLog.Info($"AntiqueWhite, {ColorEx.ColorToHex(AntiqueWhite)}".ToColorString(AntiqueWhite));
            GameLog.Info($"Aqua, {ColorEx.ColorToHex(Aqua)}".ToColorString(Aqua));
            GameLog.Info($"Aquamarine, {ColorEx.ColorToHex(Aquamarine)}".ToColorString(Aquamarine));
            GameLog.Info($"Azure, {ColorEx.ColorToHex(Azure)}".ToColorString(Azure));
            GameLog.Info($"Beige, {ColorEx.ColorToHex(Beige)}".ToColorString(Beige));
            GameLog.Info($"Bisque, {ColorEx.ColorToHex(Bisque)}".ToColorString(Bisque));
            GameLog.Info($"Black, {ColorEx.ColorToHex(Black)}".ToColorString(Black));
            GameLog.Info($"BlanchedAlmond, {ColorEx.ColorToHex(BlanchedAlmond)}".ToColorString(BlanchedAlmond));
            GameLog.Info($"Blue, {ColorEx.ColorToHex(Blue)}".ToColorString(Blue));
            GameLog.Info($"BlueViolet, {ColorEx.ColorToHex(BlueViolet)}".ToColorString(BlueViolet));
            GameLog.Info($"Brown, {ColorEx.ColorToHex(Brown)}".ToColorString(Brown));
            GameLog.Info($"BurlyWood, {ColorEx.ColorToHex(BurlyWood)}".ToColorString(BurlyWood));
            GameLog.Info($"CadetBlue, {ColorEx.ColorToHex(CadetBlue)}".ToColorString(CadetBlue));
            GameLog.Info($"Chartreuse, {ColorEx.ColorToHex(Chartreuse)}".ToColorString(Chartreuse));
            GameLog.Info($"Chocolate, {ColorEx.ColorToHex(Chocolate)}".ToColorString(Chocolate));
            GameLog.Info($"Coral, {ColorEx.ColorToHex(Coral)}".ToColorString(Coral));
            GameLog.Info($"CornflowerBlue, {ColorEx.ColorToHex(CornflowerBlue)}".ToColorString(CornflowerBlue));
            GameLog.Info($"CornSilk, {ColorEx.ColorToHex(CornSilk)}".ToColorString(CornSilk));
            GameLog.Info($"Crimson, {ColorEx.ColorToHex(Crimson)}".ToColorString(Crimson));
            GameLog.Info($"Cyan, {ColorEx.ColorToHex(Cyan)}".ToColorString(Cyan));
            GameLog.Info($"DarkBlue, {ColorEx.ColorToHex(DarkBlue)}".ToColorString(DarkBlue));
            GameLog.Info($"DarkCyan, {ColorEx.ColorToHex(DarkCyan)}".ToColorString(DarkCyan));
            GameLog.Info($"DarkGoldenrod, {ColorEx.ColorToHex(DarkGoldenrod)}".ToColorString(DarkGoldenrod));
            GameLog.Info($"DarkGray, {ColorEx.ColorToHex(DarkGray)}".ToColorString(DarkGray));
            GameLog.Info($"DarkGreen, {ColorEx.ColorToHex(DarkGreen)}".ToColorString(DarkGreen));
            GameLog.Info($"DarkKhaki, {ColorEx.ColorToHex(DarkKhaki)}".ToColorString(DarkKhaki));
            GameLog.Info($"DarkMagenta, {ColorEx.ColorToHex(DarkMagenta)}".ToColorString(DarkMagenta));
            GameLog.Info($"DarkOliveGreen, {ColorEx.ColorToHex(DarkOliveGreen)}".ToColorString(DarkOliveGreen));
            GameLog.Info($"DarkOrange, {ColorEx.ColorToHex(DarkOrange)}".ToColorString(DarkOrange));
            GameLog.Info($"DarkOrchid, {ColorEx.ColorToHex(DarkOrchid)}".ToColorString(DarkOrchid));
            GameLog.Info($"DarkRed, {ColorEx.ColorToHex(DarkRed)}".ToColorString(DarkRed));
            GameLog.Info($"DarkSalmon, {ColorEx.ColorToHex(DarkSalmon)}".ToColorString(DarkSalmon));
            GameLog.Info($"DarkSeaGreen, {ColorEx.ColorToHex(DarkSeaGreen)}".ToColorString(DarkSeaGreen));
            GameLog.Info($"DarkSlateBlue, {ColorEx.ColorToHex(DarkSlateBlue)}".ToColorString(DarkSlateBlue));
            GameLog.Info($"DarkSlateGray, {ColorEx.ColorToHex(DarkSlateGray)}".ToColorString(DarkSlateGray));
            GameLog.Info($"DarkTurquoise, {ColorEx.ColorToHex(DarkTurquoise)}".ToColorString(DarkTurquoise));
            GameLog.Info($"DarkViolet, {ColorEx.ColorToHex(DarkViolet)}".ToColorString(DarkViolet));
            GameLog.Info($"DeepPink, {ColorEx.ColorToHex(DeepPink)}".ToColorString(DeepPink));
            GameLog.Info($"DeepSkyBlue, {ColorEx.ColorToHex(DeepSkyBlue)}".ToColorString(DeepSkyBlue));
            GameLog.Info($"DimGray, {ColorEx.ColorToHex(DimGray)}".ToColorString(DimGray));
            GameLog.Info($"DodgerBlue, {ColorEx.ColorToHex(DodgerBlue)}".ToColorString(DodgerBlue));
            GameLog.Info($"FireBrick, {ColorEx.ColorToHex(FireBrick)}".ToColorString(FireBrick));
            GameLog.Info($"FloralWhite, {ColorEx.ColorToHex(FloralWhite)}".ToColorString(FloralWhite));
            GameLog.Info($"ForestGreen, {ColorEx.ColorToHex(ForestGreen)}".ToColorString(ForestGreen));
            GameLog.Info($"Fuchsia, {ColorEx.ColorToHex(Fuchsia)}".ToColorString(Fuchsia));
            GameLog.Info($"Gainsboro, {ColorEx.ColorToHex(Gainsboro)}".ToColorString(Gainsboro));
            GameLog.Info($"GhostWhite, {ColorEx.ColorToHex(GhostWhite)}".ToColorString(GhostWhite));
            GameLog.Info($"Gold, {ColorEx.ColorToHex(Gold)}".ToColorString(Gold));
            GameLog.Info($"Goldenrod, {ColorEx.ColorToHex(Goldenrod)}".ToColorString(Goldenrod));
            GameLog.Info($"Gray, {ColorEx.ColorToHex(Gray)}".ToColorString(Gray));
            GameLog.Info($"Green, {ColorEx.ColorToHex(Green)}".ToColorString(Green));
            GameLog.Info($"GreenYellow, {ColorEx.ColorToHex(GreenYellow)}".ToColorString(GreenYellow));
            GameLog.Info($"Honeydew, {ColorEx.ColorToHex(Honeydew)}".ToColorString(Honeydew));
            GameLog.Info($"HotPink, {ColorEx.ColorToHex(HotPink)}".ToColorString(HotPink));
            GameLog.Info($"IndianRed, {ColorEx.ColorToHex(IndianRed)}".ToColorString(IndianRed));
            GameLog.Info($"Indigo, {ColorEx.ColorToHex(Indigo)}".ToColorString(Indigo));
            GameLog.Info($"Ivory, {ColorEx.ColorToHex(Ivory)}".ToColorString(Ivory));
            GameLog.Info($"Khaki, {ColorEx.ColorToHex(Khaki)}".ToColorString(Khaki));
            GameLog.Info($"Lavender, {ColorEx.ColorToHex(Lavender)}".ToColorString(Lavender));
            GameLog.Info($"LavenderBlush, {ColorEx.ColorToHex(LavenderBlush)}".ToColorString(LavenderBlush));
            GameLog.Info($"LawnGreen, {ColorEx.ColorToHex(LawnGreen)}".ToColorString(LawnGreen));
            GameLog.Info($"LemonChiffon, {ColorEx.ColorToHex(LemonChiffon)}".ToColorString(LemonChiffon));
            GameLog.Info($"LightBlue, {ColorEx.ColorToHex(LightBlue)}".ToColorString(LightBlue));
            GameLog.Info($"LightCoral, {ColorEx.ColorToHex(LightCoral)}".ToColorString(LightCoral));
            GameLog.Info($"LightCyan, {ColorEx.ColorToHex(LightCyan)}".ToColorString(LightCyan));
            GameLog.Info($"LightGoldenrodYellow, {ColorEx.ColorToHex(LightGoldenrodYellow)}".ToColorString(LightGoldenrodYellow));
            GameLog.Info($"LightGray, {ColorEx.ColorToHex(LightGray)}".ToColorString(LightGray));
            GameLog.Info($"LightGreen, {ColorEx.ColorToHex(LightGreen)}".ToColorString(LightGreen));
            GameLog.Info($"LightPink, {ColorEx.ColorToHex(LightPink)}".ToColorString(LightPink));
            GameLog.Info($"LightSalmon, {ColorEx.ColorToHex(LightSalmon)}".ToColorString(LightSalmon));
            GameLog.Info($"LightSeaGreen, {ColorEx.ColorToHex(LightSeaGreen)}".ToColorString(LightSeaGreen));
            GameLog.Info($"LightSkyBlue, {ColorEx.ColorToHex(LightSkyBlue)}".ToColorString(LightSkyBlue));
            GameLog.Info($"LightSlateGray, {ColorEx.ColorToHex(LightSlateGray)}".ToColorString(LightSlateGray));
            GameLog.Info($"LightSteelBlue, {ColorEx.ColorToHex(LightSteelBlue)}".ToColorString(LightSteelBlue));
            GameLog.Info($"LightYellow, {ColorEx.ColorToHex(LightYellow)}".ToColorString(LightYellow));
            GameLog.Info($"Lime, {ColorEx.ColorToHex(Lime)}".ToColorString(Lime));
            GameLog.Info($"LimeGreen, {ColorEx.ColorToHex(LimeGreen)}".ToColorString(LimeGreen));
            GameLog.Info($"Linen, {ColorEx.ColorToHex(Linen)}".ToColorString(Linen));
            GameLog.Info($"Magenta, {ColorEx.ColorToHex(Magenta)}".ToColorString(Magenta));
            GameLog.Info($"Maroon, {ColorEx.ColorToHex(Maroon)}".ToColorString(Maroon));
            GameLog.Info($"MediumAquamarine, {ColorEx.ColorToHex(MediumAquamarine)}".ToColorString(MediumAquamarine));
            GameLog.Info($"MediumBlue, {ColorEx.ColorToHex(MediumBlue)}".ToColorString(MediumBlue));
            GameLog.Info($"MediumOrchid, {ColorEx.ColorToHex(MediumOrchid)}".ToColorString(MediumOrchid));
            GameLog.Info($"MediumPurple, {ColorEx.ColorToHex(MediumPurple)}".ToColorString(MediumPurple));
            GameLog.Info($"MediumSeaGreen, {ColorEx.ColorToHex(MediumSeaGreen)}".ToColorString(MediumSeaGreen));
            GameLog.Info($"MediumSlateBlue, {ColorEx.ColorToHex(MediumSlateBlue)}".ToColorString(MediumSlateBlue));
            GameLog.Info($"MediumSpringGreen, {ColorEx.ColorToHex(MediumSpringGreen)}".ToColorString(MediumSpringGreen));
            GameLog.Info($"MediumTurquoise, {ColorEx.ColorToHex(MediumTurquoise)}".ToColorString(MediumTurquoise));
            GameLog.Info($"MediumVioletRed, {ColorEx.ColorToHex(MediumVioletRed)}".ToColorString(MediumVioletRed));
            GameLog.Info($"MidnightBlue, {ColorEx.ColorToHex(MidnightBlue)}".ToColorString(MidnightBlue));
            GameLog.Info($"MintCream, {ColorEx.ColorToHex(MintCream)}".ToColorString(MintCream));
            GameLog.Info($"MistyRose, {ColorEx.ColorToHex(MistyRose)}".ToColorString(MistyRose));
            GameLog.Info($"Moccasin, {ColorEx.ColorToHex(Moccasin)}".ToColorString(Moccasin));
            GameLog.Info($"NavajoWhite, {ColorEx.ColorToHex(NavajoWhite)}".ToColorString(NavajoWhite));
            GameLog.Info($"Navy, {ColorEx.ColorToHex(Navy)}".ToColorString(Navy));
            GameLog.Info($"OldLace, {ColorEx.ColorToHex(OldLace)}".ToColorString(OldLace));
            GameLog.Info($"Olive, {ColorEx.ColorToHex(Olive)}".ToColorString(Olive));
            GameLog.Info($"OliveDrab, {ColorEx.ColorToHex(OliveDrab)}".ToColorString(OliveDrab));
            GameLog.Info($"Orange, {ColorEx.ColorToHex(Orange)}".ToColorString(Orange));
            GameLog.Info($"OrangeRed, {ColorEx.ColorToHex(OrangeRed)}".ToColorString(OrangeRed));
            GameLog.Info($"Orchid, {ColorEx.ColorToHex(Orchid)}".ToColorString(Orchid));
            GameLog.Info($"PaleGoldenrod, {ColorEx.ColorToHex(PaleGoldenrod)}".ToColorString(PaleGoldenrod));
            GameLog.Info($"PaleGreen, {ColorEx.ColorToHex(PaleGreen)}".ToColorString(PaleGreen));
            GameLog.Info($"PaleTurquoise, {ColorEx.ColorToHex(PaleTurquoise)}".ToColorString(PaleTurquoise));
            GameLog.Info($"PaleVioletRed, {ColorEx.ColorToHex(PaleVioletRed)}".ToColorString(PaleVioletRed));
            GameLog.Info($"PapayaWhip, {ColorEx.ColorToHex(PapayaWhip)}".ToColorString(PapayaWhip));
            GameLog.Info($"PeachPuff, {ColorEx.ColorToHex(PeachPuff)}".ToColorString(PeachPuff));
            GameLog.Info($"Peru, {ColorEx.ColorToHex(Peru)}".ToColorString(Peru));
            GameLog.Info($"Pink, {ColorEx.ColorToHex(Pink)}".ToColorString(Pink));
            GameLog.Info($"Plum, {ColorEx.ColorToHex(Plum)}".ToColorString(Plum));
            GameLog.Info($"PowderBlue, {ColorEx.ColorToHex(PowderBlue)}".ToColorString(PowderBlue));
            GameLog.Info($"Purple, {ColorEx.ColorToHex(Purple)}".ToColorString(Purple));
            GameLog.Info($"Red, {ColorEx.ColorToHex(Red)}".ToColorString(Red));
            GameLog.Info($"RosyBrown, {ColorEx.ColorToHex(RosyBrown)}".ToColorString(RosyBrown));
            GameLog.Info($"RoyalBlue, {ColorEx.ColorToHex(RoyalBlue)}".ToColorString(RoyalBlue));
            GameLog.Info($"SaddleBrown, {ColorEx.ColorToHex(SaddleBrown)}".ToColorString(SaddleBrown));
            GameLog.Info($"Salmon, {ColorEx.ColorToHex(Salmon)}".ToColorString(Salmon));
            GameLog.Info($"SandyBrown, {ColorEx.ColorToHex(SandyBrown)}".ToColorString(SandyBrown));
            GameLog.Info($"SeaGreen, {ColorEx.ColorToHex(SeaGreen)}".ToColorString(SeaGreen));
            GameLog.Info($"Seashell, {ColorEx.ColorToHex(Seashell)}".ToColorString(Seashell));
            GameLog.Info($"Sienna, {ColorEx.ColorToHex(Sienna)}".ToColorString(Sienna));
            GameLog.Info($"Silver, {ColorEx.ColorToHex(Silver)}".ToColorString(Silver));
            GameLog.Info($"SkyBlue, {ColorEx.ColorToHex(SkyBlue)}".ToColorString(SkyBlue));
            GameLog.Info($"SlateBlue, {ColorEx.ColorToHex(SlateBlue)}".ToColorString(SlateBlue));
            GameLog.Info($"SlateGray, {ColorEx.ColorToHex(SlateGray)}".ToColorString(SlateGray));
            GameLog.Info($"Snow, {ColorEx.ColorToHex(Snow)}".ToColorString(Snow));
            GameLog.Info($"SpringGreen, {ColorEx.ColorToHex(SpringGreen)}".ToColorString(SpringGreen));
            GameLog.Info($"SteelBlue, {ColorEx.ColorToHex(SteelBlue)}".ToColorString(SteelBlue));
            GameLog.Info($"Tan, {ColorEx.ColorToHex(Tan)}".ToColorString(Tan));
            GameLog.Info($"Teal, {ColorEx.ColorToHex(Teal)}".ToColorString(Teal));
            GameLog.Info($"Thistle, {ColorEx.ColorToHex(Thistle)}".ToColorString(Thistle));
            GameLog.Info($"Tomato, {ColorEx.ColorToHex(Tomato)}".ToColorString(Tomato));
            GameLog.Info($"Turquoise, {ColorEx.ColorToHex(Turquoise)}".ToColorString(Turquoise));
            GameLog.Info($"Violet, {ColorEx.ColorToHex(Violet)}".ToColorString(Violet));
            GameLog.Info($"Wheat, {ColorEx.ColorToHex(Wheat)}".ToColorString(Wheat));
            GameLog.Info($"White, {ColorEx.ColorToHex(CreamIvory)}".ToColorString(CreamIvory));
            GameLog.Info($"WhiteSmoke, {ColorEx.ColorToHex(WhiteSmoke)}".ToColorString(WhiteSmoke));
            GameLog.Info($"Yellow, {ColorEx.ColorToHex(Yellow)}".ToColorString(Yellow));
            GameLog.Info($"YellowGreen, {ColorEx.ColorToHex(YellowGreen)}".ToColorString(YellowGreen));
        }
    }
}