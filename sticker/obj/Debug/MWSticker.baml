   M S B A M L   `   `   `  ����E  AStickerWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:StickerWPF
StickerWPF  5          StickerWPF.MWSticker   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      *localclr-namespace:StickerWPF     5      -   5          � Title$  
MainWindow��5      $	��300��5	      $	��300��5
      $	��245��5      $	��245��5         wndBackground#���   5       � 
ResizeMode$ CanResizeWithGrip=�5       � WindowStyle$
 None=�5       � AllowsTransparency .  � Icon$ sticker.ico��5       � ShowInTaskbar .   � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ /Theme/ThemeYellow.xaml?�6%   %    5      .��5      � $��0,0,0,0q�5   	   $��Stretch=�5   	   $��Stretch=�5   	   	 � RowDefinitions	 5    
   �� 5!      $��24 �6   �� 5"      $��1 �6   �� 5#      -   5        
 ��Name$
 rowEnv��5#      $��48 �6,   �� 5$      $��1* �6   �� 5%      -   5        $
 rowEnv2��5%      $��24 �6-    � ColumnDefinitions 5'   
   �� 5(      $��24 �6   �� 5)      $��1* �6   �� 5*      $��24 �6   �� 5+      $��24 �6   �� 5,      $��24 �6   �� 5-      $��24 �6   .+�5/   
   �� -   5         ��Name$ btnNew��50      $��0��51      $��0��52      $��0,0,0,0q�53      $��Stretch=�54      $��Stretch=�55        btnDefaultStyle#���  56      .��57      �� $��18��58      $��18��59      $��1q�5:      $��Resources/btnAdd.png��5;      $
��Fill=�5<      5=      �� 5>   
   -   5         ��Name$ tbTitle��5?      $��0��5@      $��1��5A      $��0,0,0,0q�5B      $��Stretch=�5C      $��Stretch=�5D       �� MaxLines$ 1��5G        tbDefaultStyle#���  5H      $�� ��5I       �� TextWrapping$
 Wrap=�5J      $y�Hidden=�5K      �� 5L   
   -   5        $ btnEnv��5M      $��0��5N      $��2��5O      $��0,0,0,0q�5P      $��Stretch=�5Q      $��Stretch=�5R      #���  5S      .��5U      �� -   5         ��Name$ imgEditPanel��5U      $��18��6*   $��18��66   $��Resources/btnShowEdit.png��6A   5V      �� 5W   
   -   5        $ 	btnExpand��5X      $��0��5Y      $��3��5Z      $��0,0,0,0q�5[      $��Stretch=�5\      $��Stretch=�5]      #���  5_      .��5`      �� -	   5        $ imgBtnMinMax��5a      $��18��5b      $��18��5c      $��1q�5d      $��Resources/btnMinimum.png��5e      $
��Fill=�5f      5g      �� 5h   
   -
   5        $ btnFixed��5i      $��0��5j      $��4��5k      $��0,0,0,0q�5l      $��Stretch=�5m      $��Stretch=�5n      #���  5p      .��5q      �� -   5        $ imgFixed��5r      $��18��5s      $��18��5t      $��1q�5u      $��Resources/btnFix.png��5v      $
��Fill=�5w      5x      �� 5y   
   -   5        $ btnClose��5z      $��0��5{      $��5��5|      $��0,0,0,0q�5}      $��Stretch=�5~      $��Stretch=�5      #���  5�      .��5�      �� $��18��5�      $��18��5�      $��1q�5�      $��Resources/btnClose.png��5�      $
��Fill=�5�      5�      �� 5�   
   -   5         ��Name$ lblTitle��5�      $��0��5�      $��1��5�      $��0,0,0,0q�5�      $��3q�5�      $��Stretch=�5�      $��Stretch=�5�      $��스티커프로그램��5�        lblDefalutStyle#���  5�      �� 5�   
   -   5         ��Name$ 
rtbContent��5�      $��3��5�      $��0��5�      $��6��5�      $��Stretch=�5�      $��Stretch=�5�        rtbDefaultStyle#���  5�      " �� VerticalScrollBarVisibility$
 Auto=�5�      . �5�      /�  /� 
LineHeight$ 1��6   .T�5�      J� .*�6   .*�5�      �� $� ��6   5�      5�      5�      � 5�   
   $��2��5�      $��0��5�      $��6��5�      $��50��5�      $��0,0,0,0q�5�      $��Stretch=�5�      $	��Top=�5�       5�      �� 5�      $��24 �6#   �� 5�      $��24 �6#   �� 5�      $��24 �6#   �� 5�      $��24 �6#   �� 5�      $��24 �6#   �� 5�      $��24 �6#   �� 5�      $��24 �6#   �� 5�      $��24 �6#   �� 5�      $��24 �6#   �� 5�      $��24 �6#   �� 5�      $��2 �6#   �� 5�      $��24 �6#   �� 5�      $��3 �6#   	 5�      �� 5�      $��24 �6    �� 5�      $��26 �6    .+�5�      �� -   5         ��Name$ cmbFont��5�      $��0��5�      $��0��5�      $��6��5�      $��2,0,2,0q�5�      $��Stretch=�5�      $��Stretch=�5�      $
��Left=�5�      $��맑은고딕)�5�        RoundComboBox#���  5�      ��5�      ��    System.Windows.Media.Fonts   SystemFontFamilies �� Source# Z 5�      �� 5�      -   5        $ cmbFontSize��5�      $��0��5�      $��6��5�      $��2��5�      $��2,0,2,0q�5�      $��Stretch=�5�      $��Stretch=�5�      $
��Left=�5�      $��맑은고딕)�5�      #���  5�      �� 5�      -   5        $ 	btnItalic��5�      $��1��5�      $��1��5�      $��24��5�      $��24��5�      $��1q�5�      $
��Left=�5�      $	��Top=�5�      #���  5�      .��5�      �� $��18��5�      $��18��5�      $��1q�5�      $��Resources/btnItalic.png��5�      $
��Fill=�5�       J� IsHitTestVisible .  5�      5�      �� 5�      -   5        $ btnUnderLine��5�      $��1��5�      $��2��5�      $��24��5�      $��24��5�      $��1q�5�      $
��Left=�5�      $	��Top=�5�      #���  5�      .��5�      �� $��18��5�      $��18��5�      $��1q�5�      $ ��Resources/btnUnderline.png��5�      $
��Fill=�5�       .  5�      5�      �� 5�      -   5        $ 	btnStrike��5      $��1��5     $��3��5     $��24��5     $��24��5     $��1q�5     $
��Left=�5     $	��Top=�5     #���  5     .��5	     �� $��18��5
     $��18��5     $��1q�5     $��Resources/btnStrike.png��5     $
��Fill=�5      .  5     5     �� 5     -   5        $ btnBold��5     $��1��5     $��0��5     $��24��5     $��24��5     $��1q�5     $
��Left=�5     $	��Top=�5     #���  5     .��5     �� $��18��5     $��18��5     $��1q�5     $��Resources/btnBold.png��5     $
��Fill=�5       .  5!     5"     �� 5#     -   5        $ btnThemeColor��5$     $��5��5%     $��24��5&     $��24��5'     $��0,0,1,1q�5(     $��Stretch=�5)     $��Stretch=�5*       cmbBorderBrush#���  5+     #���  5,     $��1��6W   .��5-     �� -   5         ��Name$ recTxtFontColor��5.     $��21��5/     $��21��50     $��Stretch=�51     $��Stretch=�52     #���   53      . #���  55     $��2��56     57     �� 58     -   5        $ btnTxtColor��58     $��1��59     $��4��5:     $��23��5;     $��23��5<     $��0,1,2,2q�5=     $��Stretch=�5>     $��Stretch=�5?     #���  5@     .��5B     �� -   5         ��Name$ tbTxtForeColor��5B     $��0��5C     $��9��5D     $��F��5E     $
��Bold"�5F     $��Center=�5G     $��Center=�5H     $��14(�5I     ���#0000005J     5K     5L     �� 5M  
   -   5        $ btnTrash��5N     $��4��5O     $��0��5P     $��0q�5Q     $��Stretch=�5R     $��Stretch=�5S     #���  5T     .��5U     �� $��18��5V     $��18��5W     $��1q�5X     $��Resources/btnTrash.png��5Y     $
��Fill=�5Z      .  5[     5\     �� 5]  
   -   5         ��Name$ slidOpacity��5^     $��4��5_     $��1��5`     $��5,1,5,1q�5a     $��Stretch=�5b     $��Stretch=�5c      � Minimum$ 3Q�5d       slidDefaultStyle#���  5e      � Value$ 10Q�5g     5h     5i     