   M S B A M L   `   `   `  ����F  BMDG.Visuals, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MDG.VisualsMDG.Visuals  5      J FMahApps.Metro, Version=0.11.0.38, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5           MDG.Visuals.TelemetryMeter   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:MDG.Visuals     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5        � 	Resources  5	      ,   &MDG.Visuals.PrimaryDraftMeasurementBox �� 
TargetType �� Property �� Value!  MahApps.Metro.Controls.Tile   WindowColor  ContentAreaColorLight  ContentAreaColorDark  DisabledControlLightColor  DisabledControlDarkColor  DisabledForegroundColor  SelectedBackgroundColor  SelectedUnfocusedColor  ControlLightColor 	 ControlMediumColor 
 ControlDarkColor  ControlMouseOverColor  ControlPressedColor  
GlyphColor  GlyphMouseOver  BorderLightColor  BorderMediumColor  BorderDarkColor  PressedBorderLightColor  PressedBorderDarkColor  DisabledBorderLightColor  DisabledBorderDarkColor  DefaultBorderBrushDarkColor  HeaderTopColor " DatagridCurrentCellBorderColor  SliderTrackDarkColor  NavButtonFrameColor  MenuPopupBrush �� EndPoint �� 
StartPoint � Color � Offset $  ProgressBarIndicatorAnimatedFill �� 
TargetType'  !System.Windows.VisualStateManager	   VisualStateGroups%  System.Windows.VisualStateGroup
   States   System.Windows.VisualState   
Storyboard g� Duration �� 
TargetName �� TargetProperty R� KeyTime R� Value ��Name �� CornerRadius �� Color �Name ��Name��%�  '        '  H     &	  �     &	     &	 #    &	 D    &	 e    &	 �    &	 �    &	 �    &	 �    &		     &	
 '    &	 H    &	 i    &	 �    &	 �    &	 �    &	     &	 0    &	 Q    &	 r    &	 �    &	 �    &	 �    &	 �    &	     &	 0    &	 Q    &	 r    &	 9    '� e    7  �� 5   
   "  6   .�5      ��  � ��$ 40(�6)   5      �� 5   
   "  6   .�5      ��  � ��$
 Auto��6&   �� 5       � ��$
 Auto��6'   �� 5       � ��$ 10q�6(   65   5      ��5   
   	#FFE8EDF9��6/   ��5   
   	#FFC5CBF9��69   ��5   
   	#FF7381F9��68   ��5   
   	#FFE8EDF9��6=   ��5   
   	#FFC5CBF9��6<   ��5   
   	#FF888888��6;   ��5   
   	#FFC5CBF9��6;   ��5   
   	#FFDDDDDD��6:   ��5   
   	White��61   ��5    
   	#FF7381F9��66   ��5!   
   	#FF211AA9��64   ��5#   
   	#FF3843C4��69   ��5$   
   	#FF211AA9��67   ��5'   
   	#FF444444��6.   ��5(   
   /+sc#1, 0.004391443, 0.002428215, 0.242281124��6T   ��5+   
   	#FFCCCCCC��64   ��5,   
   	#FF888888��65   ��5-   
   	#FF444444��63   ��5/   
   	#FF888888��6;   ��50   
   	#FF444444��6:   ��52   
   	#FFAAAAAA��6<   ��53   
   	#FF888888��6;   ��55   
   	Black��6;   ��58   
   	#FFC5CBF9��62   ��59   
   	Black��6>   ��5:   
   	#FFC5CBF9��68   ��5<   
   	#FF3843C4��67   �� 5>   
   $ 0.5,1&�5?      $ 0.5,0&�5@      .��5A      � # �  6   $ 0Q�5B      � 5C      # � 	 6   $	 0.5Q�5D      � 5E      # �  6   $ 1Q�5F      5G      �� 5I   
   $	 0,0&�5J      $	 1,0&�5K      ��5M      � � 5N      $ 	#000000FF��6#   $ 0Q�5O      � 5P      $ 	#600000FF��6#   $	 0.4Q�5Q      � 5R      $ 	#600000FF��6#   $	 0.6Q�5S      � 5T      $ 	#000000FF��6#   $ 1Q�5U      5V      5X      �� 5Y   
   " �5Z      .�5[      ��  � �� 5\      �� 5]      " �6&   .R�5^      � $��14��6   $	��200��5_      #��z��5`      	 5a        5b   "   CommonStates��.
 5c   &     Determinate��62     5d   &   Indeterminate��. 5e   *   �� .��5f   .   T� $ 00:00:00D�6L   $ PART_Indicator��5g   2   $ 
Background�5h   2   ..�5i   2   i� $ 00:00:00��6I    5j   6   ��5k   :   Transparent��6W   5m   3   5n   /   5p   +   5q   '   5r   #   .+�5t      �� $ 
PART_Track��6%   $ 2��5u      $��1q�5v      ��5w   "   �� 5x   &   # �  66   5z      �� 5{      $ PART_Indicator��6%   $ 2��5|      $��1q�5}      $
��Left=�5~      #��z��5      $��0,-1,0,1q�5�      ��5�   "   �� 5�   &   $	 0,0&�6:   $	 0,1&�5�   $   ��5�   .   � � 5�   2   # �  6?   $	 0.0Q�5�   #   � 5�   2   # �  6?   $	 1.0Q�5�   #   5�   /   5�   '   ._�5�   "   � $ 	Animation��5�      }�. .+�5�   &   �� $ PART_GlowRect��60   $	��100��5�      $
��Left=�5�      8��  $��
-100,0,0,0q�5�      5�   #   5�      5�      5�      5�      �� 5�       � �� 5�      �� 5�      $	 0,1&�6*   $	 0,0&�5�      .��5�      � # �  6'   $ 0Q�5�      � 5�      # � 	 6'   $ 1Q�5�      5�      5�      �� 5�       � �� 5�      �� 5�      $ 0.5,1&�6*   $ 0.5,0&�5�      .��5�      � # � 	 6'   $ 0Q�5�      � 5�      # � 
 6'   $ 1Q�5�      5�      5�      5�      5�      .��5�   
   � $��1��6    � RowDefinitions 5�      �� 5�      $��20 �6    �� 5�      $��50 �6    �� 5�      $
��Auto �6    �� 5�      �� 5�      $
��Auto �6     � ColumnDefinitions 5�      �� 5�      �� 5�      �� 5�      .+�5�      �� $��0��6   $��0��6"   $��4��62   $��1q�6F   
���   �
�������$ 6��6�   ._�5�      �� $��20,0,0,0q�6    Barge Specs:6@   5�      � 5�      $��5q�6   $
��Left=�6#   $��1��6>   $��0��6K   $��4��6[   .+�5�      �� $��Center=�6   .��67   Barge Width:6E   �� 5�      ��.  $	��100��5�   Q   $��Center=�6`   $��30��6�   ��5�   1   �� *BargeWidthOptions+ �� SelectedValue 5�      �� *
BargeWidth+ �� StringFormat$ 00��6�   �� 5�      $��Center=�6   $y�	Collapsed=�67   .��6N   Barge Length:6]   �� 5�      $	��100��6P    �� TextAlignment$ Center=�6_   $��Center=�6v   $y�	Collapsed=�6�   ��6   �� *BargeLength+$ 00.00��6�   �� 5�      $��Center=�6   .��68   Barge Depth:6F   �� 5�      $	��100��6x   $��Center=�6�   ��.  $��30��6�   ��6   �� *BargeDepthOptions+ �� Text 6J   �� *
BargeDepth+$ 00��6�   �� 5�       �� Orientation$ 
Horizontal=�6!   $y�	Collapsed=�6:   .+�5�      �� $��Center=�6    .��6;   Tons Per Inch:6K   �� 5�      $��Center=�5�      $��Center=�5�      $	��100��5�      ��5�      �� *TonsPerInch+$ 00.000��.��5�   -   5�      5�      �� 5�      $��Center=�6   $��15,5,5,5q�6\   $y�	Collapsed=�6n    d� 	IsChecked 6:   �� *LogReadings+.��6�   Log Readings6�   �� 5�      -   5         ��Name$ cmdGo��5�   C    �� 	ClickMode$ Press=�6   ��.  $��71��5�      $��30��6&   $��2q�62     � ToolTip  6>   �� *ModBusError+��5�      �� *!StartModbusCommunicationCommand+$��GO��5�   *   5�      �� 5�      -   5        $ cmdStop��5�   C   $ Press=�6   ��.  $��71��5�      $��30��6&   $��5q�62     6>   �� *ModBusError+��5�      �� *StopTakingDataCommand+$
��Stop��5�   *   5�      �� 5�      -   5        $��71��5�      $��2q�6(   $��30��63   .��6?   	Calibrate6J   �� 5�      -   5        ! ��Name$! cboFreeboardShow��5�      $��Center=�69   $��15,5,5,5q�5�      .@�5�      �� .��6(   Don't show Freeboard6>   �� 5�      .��6(   Show Freeboard68   5�      �� 5�      -   5        $ cmdSwap��5�      $��5q�6.   $��26��69   $	��100��5�      .��6?   Swap6E   5�      �� 5�   
   $��0��6   $��2��6"   $��2q�6/   $ 6��6D   
���   �._�5�        -   5        "  Name$" blackBox��5�      
�������
���   �#   NodeName$# Node 1��5      $   
RawReading$ 5�      �� *WaterSideFreeboardAft+%   
BargeDepth% 5�      �� *
BargeDepth+&   	DeckPlate& 5�      �� *BlackBoxDeckPlate+' �� Mode$' TwoWay=�5     5     �� 5  
   $��3��6   $��2��6"   $��2q�6/   $ 6��6D   
��� ���._�5       -   5        $" 	yellowBox��5  S   
���   �
��� ���$# Node 2��5
     $ 5     �� *WaterSideFreeboardForward+% 5     �� *
BargeDepth+& 5	     �� *YellowBoxDeckPlate+$' TwoWay=�5
  1   5     �� 5  
   -   5        ( ��Name$( blackTileAngle��5     $��3��6,   $��0��69   $��32(�5     $��25��6#   $��Center=�5     $��Center=�5     ��5     �� *RollAngleNode1+$ Roll:    {0:F3}��5     �� 5     -	   5        $( yellowTileAngle��5     $��32(�5     $��25��6#   $��3��5     $��3��6%   $��Center=�5     $��Center=�5     ��5     �� *RollAngleNode2+$ Roll:    {0:F3}��5     �� 5  
   $��0��6   $��5��6!   $ 6��6.   $��6q�6?   
���   �._�5      $   MDG.Visuals.DraftMeasurmentBox  -
   5        )  Name$) blackOpposite��5   '   *   
RawReading* 5!     �� *DockSideFreeboardAft++   
BargeDepth+ 5"     �� *
BargeDepth+6:   5#     )  #MahApps.Metro.Controls.ProgressRing  5%  
   $	��100��6    $	��100��60   $��3��6?   $��1��6L   
����  �$��Stretch=�5&     $��Stretch=�63   ,   IsActive, 5%  \   �� *ReadInProgress+5&  R   �� 5(  
   $ 6��6   $��3��6#   $��5��63   $��6q�6A   
��� ���._�5)       -   5        $) yellowOpposite��5)  '   * 5*     �� *DockSideFreeboardForward++ 5+     �� *
BargeDepth+69   5,     5-     5/     