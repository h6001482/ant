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
    /// MWColorPicker.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PWColorPicker : UserControl
    {
        public MWSticker mwSticker;
        public MWSticker MwSticker
        {
            get { return mwSticker; }
            set { mwSticker = value; }
        }

        public PWColorPicker()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (sender as Rectangle);
            mwSticker.setTxtFontColor(rect.Fill.ToString());            
        }
    }
}
