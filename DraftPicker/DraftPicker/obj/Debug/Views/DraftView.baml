   M S B A M L   `   `   `  ����F  BDraftPicker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:DraftPicker.HelpersDraftPicker.Helpers  5      5clr-namespace:DraftPicker.ViewsDraftPicker.Views  5
      !    DraftPicker.Views.DraftView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4assist!clr-namespace:DraftPicker.Helpers     5      .myclr-namespace:DraftPicker.Views     5
      $��10q�5	   "     � 	Resources  5      0   *DraftPicker.Helpers.TitleAndValueConverter   converterTileAndValue��%   &	          5   
   6(   5      .��5      � -   5         �Name$ 
layoutRoot��5       � RowDefinitions 5   
   �� 5      �� 5      $��50��6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
      DraftPicker.Views.KeyPad  -   5          Name$ 
keyPadFeet��5      $��0��5      $��1��6$   $��0��65   $��Stretch=�6C      MinValue$ 0��6a      MaxValue$ 17��6n   $��Stretch=�5   )      
ValueLabel$
 Feet��6n      Value 6E   �� 	 �� Path$
	 Feet�
 �� Mode$
 TwoWay=�6n     5   
   -   5        $ keyPadInches��5      $��1��5      $��1��6$   $��0��65   $��Stretch=�6C   $ 0��6a   $ 11��6n   $��Stretch=�5   *   $ Inches��6q    6F   �� $	 Inches�$
 TwoWay=�6q   �� 5   
   $��Center=�6   $��20(�6/   $��Center=�5      $��1��5   /   $��0��6<   ��6   �� *Title+6<   �� 5    
   $��Center=�6   $��20(�6/   $��Center=�5!      $��1��5"      $��1��6&   ��5#      `� 5$       �� StringFormat$ Total Inches:  {0}��6   .1�5%      �� $	 Value�6   5&      5(      5,      5-      