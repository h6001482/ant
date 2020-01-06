using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StickerWPF
{
    /// <summary>
    /// MWThemePicker.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PWThemePicker : UserControl
    {
        public MWSticker mwSticker;
        public MWSticker MwSticker
        {
            get { return mwSticker; }
            set { mwSticker = value; }
        }

        public PWThemePicker()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (sender as Rectangle);
            string themeColor = string.Empty;
            switch (rect.Name.ToString())
            {
                case "rectRed":
                    themeColor = "ThemeRed";
                    break;
                case "rectOrange":
                    themeColor = "ThemeOrange";
                    break;
                case "rectYellow":
                    themeColor = "ThemeYellow";
                    break;
                case "rectGreen":
                    themeColor = "ThemeGreen";
                    break;
                case "rectBlue":
                    themeColor = "ThemeBlue";
                    break;
                case "rectNavy":
                    themeColor = "ThemeNavy";
                    break;
                case "rectViolet":
                    themeColor = "ThemeViolet";
                    break;
                case "rectWhite":
                    themeColor = "ThemeWhite";
                    break;
            }
            mwSticker.setThemeColor(themeColor + ".xaml");
        }
    }
}
