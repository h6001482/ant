using HaeDTO;
using StickerWPF.Biz;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MWSticker : Window
    {
        private Boolean showEnv = false;
        private Boolean folded = false;
        private Boolean bolded = false;
        private Boolean italic = false;
        private Boolean strike = false;
        private Boolean unfixed = true;
        private Boolean underline = false;
        private Window mwThemeColorPicker;
        private Window mwFontColorPicker;

        private double previousHeight;
        //private double previousWidth;

        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        StickerBiz stickerBiz = null;

        public MWSticker()
        {
            InitializeComponent();

            initControl();

            insertStickerInfo();
        }

        public MWSticker(string id)
        {
            this.id = id;

            InitializeComponent();

            initControl();

            getStickerInfo(id);
        }

        private void initControl()
        {
            string dbPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "stickerdb.sqlite";
            stickerBiz = new StickerBiz(dbPath);

            rowEnv.Height = new GridLength(0);
            rowEnv2.Height = new GridLength(0);

            cmbFontSize.ItemsSource = createComboFontSize();
            cmbFontSize.DisplayMemberPath = "NAME";
            cmbFontSize.SelectedValuePath = "VALUE";
            //mwColorPicker = new Window();

            //ThemeColorPicker 설정
            mwThemeColorPicker = new Window();
            PWThemePicker ucMWThemeColorPicker = new PWThemePicker();
            ucMWThemeColorPicker.MwSticker = (MWSticker)Window.GetWindow(this);
            mwThemeColorPicker.Content = ucMWThemeColorPicker;
            mwThemeColorPicker.Deactivated += mwThemeColorPicker_Deactivated;
            //mwThemeColorPicker.Visibility = Visibility.Hidden;
            //mwThemeColorPicker.Show();

            //FontColorPicker 설정
            mwFontColorPicker = new Window();
            PWColorPicker ucMWFontColorPicker = new PWColorPicker();
            ucMWFontColorPicker.MwSticker = (MWSticker)Window.GetWindow(this);
            mwFontColorPicker.Content = ucMWFontColorPicker;
            
            mwFontColorPicker.Deactivated += mwFontColorPicker_Deactivated;
            //mwFontColorPicker.Visibility = Visibility.Hidden;
            //mwFontColorPicker.Show();
        }

        //private string getText()
        private string getRtbData(RichTextBox rtb, string dataFormat)
        {
            FlowDocument flowDocument = rtb.Document;
            string result = string.Empty;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                textRange.Save(memoryStream, dataFormat);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (StreamReader streamReader = new StreamReader(memoryStream))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }

        private string getText(RichTextBox rtb)
        {
            return getRtbData(rtb, DataFormats.Text);            
        }

        private string getRTF(RichTextBox rtb)
        {
            return getRtbData(rtb, DataFormats.Rtf);
        }

        private void setText(RichTextBox rtb, string text)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(text));
                TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                textRange.Load(memoryStream, DataFormats.Text);
            }
            catch
            {

            }            
        }

        private void setRTF(RichTextBox rtb, string rtf)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(rtf));
                TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                textRange.Load(memoryStream, DataFormats.Rtf);
            }
            catch
            {
            }            
        }

        private MemoryStream GetMemoryStreamFromString(string s)
        {
            if (s == null || s.Length == 0)
            {
                return null;
            }                
            MemoryStream m = new MemoryStream();
            StreamWriter sw = new StreamWriter(m);
            sw.Write(s);
            sw.Flush();
            return m;
        }

        private void insertStickerInfo()
        {
            string id = string.Empty;            
            CommonDTO dto = new CommonDTO();
            dto.set("title", "제목없음");
            dto.set("contents", getText(rtbContent));
            dto.set("x", 100);
            dto.set("y", 100);
            dto.set("width", this.Width);
            dto.set("height", this.Height);
            dto.set("prev_width", this.Width);
            dto.set("prev_height", this.Height);
            dto.set("opacity", slidOpacity.Value);
            dto.set("fold_yn", folded ? "Y" : "N");
            dto.set("fix_yn", unfixed ? "N" : "Y");
            dto.set("showedit_yn", showEnv ? "Y" : "N");
            dto.set("reg_date", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));

            this.id = stickerBiz.insStickerInfo(dto);            
        }

        private void updateStickerInfo()
        {
            string id = string.Empty;
            CommonDTO dto = new CommonDTO();
            dto.set("id", this.id);
            dto.set("title", lblTitle.Content.ToString());
            dto.set("contents", getText(rtbContent));
            dto.set("contents_rtf", getRTF(rtbContent));
            dto.set("x", this.Left);
            dto.set("y", this.Top);
            dto.set("width", this.Width);
            dto.set("height", this.Height);
            dto.set("prev_width", this.Width);
            dto.set("prev_height", this.Height);
            dto.set("opacity", slidOpacity.Value);
            dto.set("fold_yn", folded ? "Y" : "N");
            dto.set("fix_yn", unfixed ? "N" : "Y");
            dto.set("showedit_yn", showEnv ? "Y" : "N");
            dto.set("reg_date", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));

            stickerBiz.updStickerInfo(dto);
        }

        public void getStickerInfo(string id)
        {
            CommonDTO dto = new CommonDTO();
            dto.set("id", id);
            DataTable dt = stickerBiz.getStickerInfoById(dto);
            if(dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.id = dr["id"].ToString();
                lblTitle.Content = dr["title"].ToString();
                tbTitle.Text = dr["title"].ToString();
                setText(rtbContent, dr["contents"].ToString());
                setRTF(rtbContent, dr["contents_rtf"].ToString());
                this.Left = Double.Parse(dr["x"].ToString());
                this.Top = Double.Parse(dr["y"].ToString());
                this.Width = Double.Parse(dr["width"].ToString());
                this.Height = Double.Parse(dr["height"].ToString());                
            }
        }

        private DataView createComboFontSize()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("VALUE", typeof(string));
            dt.Columns.Add("NAME", typeof(string));

            dt.Rows.Add(new string[] { "8", "8" });
            dt.Rows.Add(new string[] { "9", "9" });
            dt.Rows.Add(new string[] { "10", "10" });
            dt.Rows.Add(new string[] { "11", "11" });
            dt.Rows.Add(new string[] { "12", "12" });
            dt.Rows.Add(new string[] { "14", "14" });
            dt.Rows.Add(new string[] { "16", "16" });
            dt.Rows.Add(new string[] { "18", "18" });
            dt.Rows.Add(new string[] { "24", "24" });
            dt.Rows.Add(new string[] { "32", "32" });
            dt.Rows.Add(new string[] { "48", "48" });
            return dt.DefaultView;
        }

        #region  event handler

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbFont.SelectedIndex = 0;
            cmbFontSize.SelectedIndex = 2;
            rtbContent.Focus();
        }

        private void btnStrike_Click(object sender, RoutedEventArgs e)
        {
            setRtfContentFontStyle("strike");
        }

        private void btnBold_Click(object sender, RoutedEventArgs e)
        {
            setRtfContentFontStyle("bold");
        }

        private void btnUnderLine_Click(object sender, RoutedEventArgs e)
        {
            setRtfContentFontStyle("underline");
        }

        private void btnItalic_Click(object sender, RoutedEventArgs e)
        {
            setRtfContentFontStyle("italic");
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            MWSticker mwSticker = new MWSticker();
            mwSticker.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mwFontColorPicker.Close();
            mwThemeColorPicker.Close();
            Close();
        }

        private void lblTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (unfixed)
                {
                    this.DragMove();
                }
            }
        }

        private void btnEnv_Click(object sender, RoutedEventArgs e)
        {
            if (showEnv)
            {
                rowEnv.Height = new GridLength(0);
                rowEnv2.Height = new GridLength(0);
                showEnv = false;
                imgEditPanel.Source = new BitmapImage(new Uri(@"Resources/btnShowEdit.png", UriKind.Relative));
            }
            else
            {
                rowEnv.Height = new GridLength(48);
                rowEnv2.Height = new GridLength(24);
                showEnv = true;
                imgEditPanel.Source = new BitmapImage(new Uri(@"Resources/btnHideEdit.png", UriKind.Relative));
            }
        }

        private void btnFixed_Click(object sender, RoutedEventArgs e)
        {
            if (unfixed)
            {
                imgFixed.Source = new BitmapImage(new Uri(@"Resources/btnFixed.png", UriKind.Relative));
                unfixed = false;
            }
            else
            {
                imgFixed.Source = new BitmapImage(new Uri(@"Resources/btnFix.png", UriKind.Relative));
                unfixed = true;
            }
        }


        private void btnExpand_Click(object sender, RoutedEventArgs e)
        {
            if (folded)
            {
                this.tbTitle.SetValue(Grid.ColumnSpanProperty, 1);
                this.lblTitle.SetValue(Grid.ColumnSpanProperty, 1);
                this.btnEnv.Visibility = Visibility.Visible;
                this.ResizeMode = ResizeMode.CanResizeWithGrip;
                this.MinHeight = 150;
                this.Height = previousHeight;
                imgBtnMinMax.Source = new BitmapImage(new Uri(@"Resources/btnMinimum.png", UriKind.Relative));
                folded = false;                
            }
            else
            {
                this.tbTitle.SetValue(Grid.ColumnSpanProperty, 2);
                this.lblTitle.SetValue(Grid.ColumnSpanProperty, 2);
                this.btnEnv.Visibility = Visibility.Hidden;
                previousHeight = this.Height;
                this.ResizeMode = ResizeMode.NoResize;
                this.MinHeight = 24;
                this.Height = 24;
                imgBtnMinMax.Source = new BitmapImage(new Uri(@"Resources/btnMaximum.png", UriKind.Relative));
                folded = true;                
            }
        }

        private void lblTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbTitle.Visibility = Visibility.Visible;
            lblTitle.Visibility = Visibility.Hidden;
            tbTitle.Focus();
        }

        private void tbTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                lblTitle.Content = tbTitle.Text.ToString();
                tbTitle.Visibility = Visibility.Hidden;
                lblTitle.Visibility = Visibility.Visible;
            }
        }

        private void tbTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            lblTitle.Content = tbTitle.Text.ToString();
            tbTitle.Visibility = Visibility.Hidden;
            lblTitle.Visibility = Visibility.Visible;
        }

        private void rtbContent_LostFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbFont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setRtfContentFontFamilyAndSize();
        }

        private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setRtfContentFontFamilyAndSize();
        }

        private void ImgBtnBold_MouseDown(object sender, MouseButtonEventArgs e)
        {
            setRtfContentFontStyle("bold");
        }

        private void ImgBtnItalic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            setRtfContentFontStyle("italic");
        }

        private void ImgBtnUnderLine_MouseDown(object sender, MouseButtonEventArgs e)
        {
            setRtfContentFontStyle("underline");
        }

        private void ImgBtnStrikeThrough_MouseDown(object sender, MouseButtonEventArgs e)
        {
            setRtfContentFontStyle("strike");
        }

        private void rtbContent_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (rtbContent.Selection != null)
            {
                if (!rtbContent.Selection.IsEmpty)
                {
                    //btnBold.Background = (SolidColorBrush)Application.Current.FindResource("btnBackground");
                    //btnItalic.Background = (SolidColorBrush)Application.Current.FindResource("btnBackground");
                    //btnUnderLine.Background = (SolidColorBrush)Application.Current.FindResource("btnBackground");
                    //btnStrike.Background = (SolidColorBrush)Application.Current.FindResource("btnBackground");
                    btnBold.Background = (SolidColorBrush)this.FindResource("btnBackground");
                    btnItalic.Background = (SolidColorBrush)this.FindResource("btnBackground");
                    btnUnderLine.Background = (SolidColorBrush)this.FindResource("btnBackground");
                    btnStrike.Background = (SolidColorBrush)this.FindResource("btnBackground");
                    bolded = false;
                    italic = false;
                    strike = false;
                    underline = false;
                }
            }
        }

        //private void ImgBtnClose_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    Close();
        //}

        //private void ImgBtnAdd_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    MWSticker mwSticker = new MWSticker();
        //    mwSticker.Show();
        //}

        private void btnTxtFontColor_Click(object sender, RoutedEventArgs e)
        {
            //Window mwColorPicker = new Window();
            //MWColorPicker ucMWColorPicker = new MWColorPicker();
            //ucMWColorPicker.MwSticker = (MWSticker)Window.GetWindow(this);
            //mwColorPicker.Content = ucMWColorPicker;
            //var location = btnTxtFontColor.PointToScreen(new Point(0, 0));
            //mwColorPicker.Left = location.X;
            //mwColorPicker.Top = location.Y + btnTxtFontColor.Height;
            //mwColorPicker.WindowStyle = WindowStyle.None;
            //mwColorPicker.ResizeMode = ResizeMode.NoResize;
            //mwColorPicker.Width = 128;
            //mwColorPicker.Height = 48;
            //mwColorPicker.Topmost = true;
            //mwColorPicker.Show();
        }
        #endregion

        public void setTxtFontColor(string fontColor)
        {
            SolidColorBrush foreColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fontColor));
            //recTxtFontColor.Fill = foreColor;
            tbTxtForeColor.Foreground = foreColor;

            if (rtbContent.Selection != null)
            {
                if (rtbContent.Selection.IsEmpty)
                {
                    Run run = new Run(String.Empty, rtbContent.CaretPosition);
                    run.Foreground = tbTxtForeColor.Foreground;
                    //run.FontSize = double.Parse((String)cmbFontSize.Items.GetItemAt(cmbFontSize.SelectedIndex));
                    rtbContent.Focus();
                }
                else
                {
                    TextRange range = new TextRange(rtbContent.Selection.Start, rtbContent.Selection.End);
                    range.ApplyPropertyValue(ForegroundProperty, foreColor);
                }
            }
        }

        public void setThemeColor(string themeColor)
        {
            this.Resources.Source = new Uri("pack://application:,,,/Theme/" + themeColor);
            (mwThemeColorPicker.Content as PWThemePicker).Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/Theme/" + themeColor);
            (mwFontColorPicker.Content as PWColorPicker).Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/Theme/" + themeColor);

            btnBold.Background = (SolidColorBrush)FindResource("btnBackground");
            btnItalic.Background = (SolidColorBrush)FindResource("btnBackground");
            btnUnderLine.Background = (SolidColorBrush)FindResource("btnBackground");
            btnStrike.Background = (SolidColorBrush)this.FindResource("btnBackground");
        }

        private void setRtfContentFontFamilyAndSize()
        {
            if (rtbContent.Selection != null)
            {
                if (rtbContent.Selection.IsEmpty)
                {
                    Run run = new Run(String.Empty, rtbContent.CaretPosition);
                    //run.Foreground = recTxtFontColor.Fill;
                    //run.FontFamily = (FontFamily)cmbFont.SelectedItem.ToString();
                    //run.FontSize = double.Parse((String)cmbFontSize.Items.GetItemAt(cmbFontSize.SelectedIndex));
                    rtbContent.Focus();
                }
                else
                {
                    TextRange range = new TextRange(rtbContent.Selection.Start, rtbContent.Selection.End);
                    range.ApplyPropertyValue(TextElement.FontFamilyProperty, cmbFont.SelectedItem.ToString());
                    range.ApplyPropertyValue(TextElement.FontSizeProperty, ((DataRowView)cmbFontSize.Items.GetItemAt(cmbFontSize.SelectedIndex)).Row.ItemArray[0].ToString());
                }
            }
            rtbContent.Focus();
        }

        private void setRtfContentFontStyle(string style)
        {
            if (rtbContent.Selection != null)
            {
                if (rtbContent.Selection.IsEmpty)
                {
                    return;
                }
                else
                {
                    TextRange range = new TextRange(rtbContent.Selection.Start, rtbContent.Selection.End);
                    switch (style)
                    {
                        case "bold":
                            if (bolded)
                            {
                                range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                                //btnBold.Background = (SolidColorBrush)Application.Current.FindResource("btnBackground");
                                btnBold.Background = (SolidColorBrush)this.FindResource("btnBackground");
                                bolded = false;
                            }
                            else
                            {
                                range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                                //btnBold.Background = (SolidColorBrush)Application.Current.FindResource("btnMouseover");
                                btnBold.Background = (SolidColorBrush)this.FindResource("btnMouseover");
                                bolded = true;
                            }
                            break;
                        case "italic":
                            if (italic)
                            {
                                range.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                                //btnItalic.Background = (SolidColorBrush)Application.Current.FindResource("btnBackground");
                                btnItalic.Background = (SolidColorBrush)this.FindResource("btnBackground");
                                italic = false;
                            }
                            else
                            {
                                range.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                                //btnItalic.Background = (SolidColorBrush)Application.Current.FindResource("btnMouseover");
                                btnItalic.Background = (SolidColorBrush)this.FindResource("btnMouseover");
                                italic = true;
                            }
                            break;
                        case "underline":
                            if (underline)
                            {
                                TextDecorationCollection underLineDeco = (TextDecorationCollection)rtbContent.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                                if (underLineDeco.Contains(TextDecorations.Underline[0]))
                                {
                                    TextDecorationCollection noUnderlineDeco = new TextDecorationCollection(underLineDeco);
                                    noUnderlineDeco.Remove(TextDecorations.Underline[0]);  //this is a bool, and could replace Contains above
                                    rtbContent.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, noUnderlineDeco);
                                }
                                //range.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                                //btnUnderLine.Background = (SolidColorBrush)Application.Current.FindResource("btnBackground");
                                btnUnderLine.Background = (SolidColorBrush)this.FindResource("btnBackground");
                                underline = false;
                            }
                            else
                            {
                                range.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                                //btnUnderLine.Background = (SolidColorBrush)Application.Current.FindResource("btnMouseover");
                                btnUnderLine.Background = (SolidColorBrush)this.FindResource("btnMouseover");
                                underline = true;

                                //Strike 와 Underline 동시에 적용이 안되기 때문에 한쪽은 강제 해제
                                //btnStrike.Background = (SolidColorBrush)Application.Current.FindResource("btnBackground");
                                btnStrike.Background = (SolidColorBrush)this.FindResource("btnBackground");
                                strike = false;
                            }
                            break;
                        case "strike":
                            if (strike)
                            {
                                TextDecorationCollection strikeDeco = (TextDecorationCollection)rtbContent.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                                if (strikeDeco.Contains(TextDecorations.Strikethrough[0]))
                                {
                                    TextDecorationCollection noStikeDeco = new TextDecorationCollection(strikeDeco);
                                    noStikeDeco.Remove(TextDecorations.Strikethrough[0]);  //this is a bool, and could replace Contains above
                                    rtbContent.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, noStikeDeco);
                                }
                                //btnStrike.Background = (SolidColorBrush)Application.Current.FindResource("btnBackground");
                                btnStrike.Background = (SolidColorBrush)this.FindResource("btnBackground");
                                strike = false;
                            }
                            else
                            {
                                range.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);
                                //btnStrike.Background = (SolidColorBrush)Application.Current.FindResource("btnMouseover");
                                btnStrike.Background = (SolidColorBrush)this.FindResource("btnMouseover");
                                strike = true;

                                //Strike 와 Underline 동시에 적용이 안되기 때문에 한쪽은 강제 해제
                                //btnUnderLine.Background = (SolidColorBrush)Application.Current.FindResource("btnBackground");
                                btnUnderLine.Background = (SolidColorBrush)this.FindResource("btnBackground");
                                underline = false;
                            }
                            break;
                    }

                }
            }
            rtbContent.Focus();
        }

        private void btnUpDown_Click(object sender, RoutedEventArgs e)
        {
            if (showEnv)
            {
                rowEnv.Height = new GridLength(0);
                rowEnv2.Height = new GridLength(0);
                showEnv = false;
            }
            else
            {
                rowEnv.Height = new GridLength(48);
                rowEnv2.Height = new GridLength(24);
                showEnv = true;
            }
        }

        private void slidOpacity_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Opacity = slidOpacity.Value * 0.1;
        }

        private void slidOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.Opacity = slidOpacity.Value * 0.1;
        }

        //bool isOpenColorPicker = false;
        private void btnTxtColor_Click(object sender, RoutedEventArgs e)
        {
            if (mwThemeColorPicker.IsVisible)
            {
                mwThemeColorPicker.Visibility = Visibility.Hidden;
            }
            var btnTxtLocation = btnTxtColor.PointToScreen(new Point(0, 0));
            mwFontColorPicker.Left = btnTxtLocation.X;
            mwFontColorPicker.Top = btnTxtLocation.Y + btnTxtColor.Height;            
            mwFontColorPicker.WindowStyle = WindowStyle.None;
            mwFontColorPicker.ResizeMode = ResizeMode.NoResize;
            mwFontColorPicker.Width = 128;
            mwFontColorPicker.Height = 48;
            mwFontColorPicker.Topmost = true;            
            mwFontColorPicker.Visibility = Visibility.Visible;            
        }

        private void mwFontColorPicker_Deactivated(object sender, EventArgs e)
        {
            mwFontColorPicker.Visibility = Visibility.Hidden;
        }

        private void btnTrash_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnThemeColor_Click(object sender, RoutedEventArgs e)
        {
            if (mwFontColorPicker.IsVisible)
            {
                mwFontColorPicker.Visibility = Visibility.Hidden;
            }
            var btnThemeLocation = btnThemeColor.PointToScreen(new Point(0, 0));
            mwThemeColorPicker.Left = btnThemeLocation.X;
            mwThemeColorPicker.Top = btnThemeLocation.Y + btnThemeColor.Height;
            mwThemeColorPicker.Deactivated += mwThemeColorPicker_Deactivated;
            mwThemeColorPicker.WindowStyle = WindowStyle.None;
            mwThemeColorPicker.ResizeMode = ResizeMode.NoResize;
            mwThemeColorPicker.Width = 25;
            mwThemeColorPicker.Height = 200;
            mwThemeColorPicker.Topmost = true;
            mwThemeColorPicker.Visibility = Visibility.Visible;
        }

        private void mwThemeColorPicker_Deactivated(object sender, EventArgs e)
        {
            mwThemeColorPicker.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            updateStickerInfo();
        }
    }
}
