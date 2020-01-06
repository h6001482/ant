using HaeDTO;
using StickerWPF.Biz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using Winforms = System.Windows.Forms;

namespace StickerWPF
{
    /// <summary>
    /// MWMain.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MWMain : Window
    {
        private StickerBiz stickerBiz = null;
        private CommonDTO stickerList = new CommonDTO();
        private Winforms.NotifyIcon notify;        

        public MWMain()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string dbPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "stickerdb.sqlite";
            stickerBiz = new StickerBiz(dbPath);

            notify = new Winforms.NotifyIcon();
            notify.Icon = Properties.Resources.sticker;
            notify.DoubleClick += Notify_DoubleClick;

            getStickerInfo();                                                
        }

        private void Notify_DoubleClick(object sender, EventArgs e)
        {
            notify.Visible = false;
            this.Visibility = Visibility.Visible;
        }

        private void txtSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                DataTable dt = stickerBiz.getStickerInfo();
                dgStickerList.ItemsSource = dt.DefaultView;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            notify.Visible = true;
            this.Visibility = Visibility.Collapsed;            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MWSticker mwSticker = new MWSticker();
            mwSticker.Show();
        }

        private void lblTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {                
                this.DragMove();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = stickerBiz.getStickerInfo();
            dgStickerList.ItemsSource = dt.DefaultView;
        }

        private void dgStickerList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            if(row == null)
            {
                return;
            }
            string id = (dgStickerList.Items[row.GetIndex()] as DataRowView).Row.ItemArray[0].ToString();
            //MWSticker mwSticker = new MWSticker(id);
            //mwSticker.Show();
        }

        private void getStickerInfo()
        {
            DataTable dt = stickerBiz.getStickerInfo();
            dgStickerList.ItemsSource = dt.DefaultView;

            string id = string.Empty;
            foreach(DataRow dr in dt.Rows)
            {
                id = dr["id"].ToString();
                if (stickerList.isExists(id))
                {
                    ((MWSticker)stickerList.get(id)).Focus();
                }
                else
                {
                    MWSticker mwSticker = new MWSticker(id);
                    mwSticker.Show();
                    stickerList.set(id, mwSticker);
                }
            }
        }
    }
}
