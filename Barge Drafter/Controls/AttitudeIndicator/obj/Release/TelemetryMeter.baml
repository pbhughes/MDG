   M S B A M L   `   `   `  ���� F  BMDG.Visuals, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MDG.VisualsMDG.Visuals  J FMahApps.Metro, Version=0.11.0.38, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls      MDG.Visuals.TelemetryMeter   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     +localclr-namespace:MDG.Visuals     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     -     � 	Resources  ,   &MDG.Visuals.PrimaryDraftMeasurementBox �� 
TargetType �� Property �� Value!  MahApps.Metro.Controls.Tile   WindowColor  ContentAreaColorLight  ContentAreaColorDark  DisabledControlLightColor  DisabledControlDarkColor  DisabledForegroundColor  SelectedBackgroundColor  SelectedUnfocusedColor  ControlLightColor 	 ControlMediumColor 
 ControlDarkColor  ControlMouseOverColor  ControlPressedColor  
GlyphColor  GlyphMouseOver  BorderLightColor  BorderMediumColor  BorderDarkColor  PressedBorderLightColor  PressedBorderDarkColor  DisabledBorderLightColor  DisabledBorderDarkColor  DefaultBorderBrushDarkColor  HeaderTopColor " DatagridCurrentCellBorderColor  SliderTrackDarkColor  NavButtonFrameColor  MenuPopupBrush �� EndPoint �� 
StartPoint � Color � Offset $  ProgressBarIndicatorAnimatedFill �� 
TargetType'  !System.Windows.VisualStateManager	   VisualStateGroups%  System.Windows.VisualStateGroup
   States   System.Windows.VisualState   
Storyboard g� Duration �� 
TargetName �� TargetProperty R� KeyTime R� Value ��Name �� CornerRadius �� Color �Name ��Name��%�  '        '  #     &	  v     &	 �     &	 �     &	 �     &	 �     &	 �     &	 �     &	 �     &	     &		     &	
 0    &	 C    &	 V    &	 i    &	 |    &	 �    &	 �    &	 �    &	 �    &	 �    &	     &	 #    &	 6    &	 E    &	 X    &	 g    &	 z    &	 �    &	 �    '� �    7  �� "  .���  � ��$ 40(��� "  .���  � ��$
 Auto����  � ��$
 Auto����  � ��$ 10q���	#FFE8EDF9����	#FFC5CBF9����	#FF7381F9����	#FFE8EDF9����	#FFC5CBF9����	#FF888888����	#FFC5CBF9����	#FFDDDDDD����	White����	#FF7381F9����	#FF211AA9����	#FF3843C4����	#FF211AA9����	#FF444444����/+sc#1, 0.004391443, 0.002428215, 0.242281124����	#FFCCCCCC����	#FF888888����	#FF444444����	#FF888888����	#FF444444����	#FFAAAAAA����	#FF888888����	Black����	#FFC5CBF9����	Black����	#FFC5CBF9����	#FF3843C4���� $ 0.5,1&�$ 0.5,0&�.��� # �  $ 0Q�� # � 	 $	 0.5Q�� # �  $ 1Q��� $	 0,0&�$	 1,0&���� � $ 	#000000FF��$ 0Q�� $ 	#600000FF��$	 0.4Q�� $ 	#600000FF��$	 0.6Q�� $ 	#000000FF��$ 1Q��� " �.���  � �� �� " �.R�� $��14��$	��200��#��z��	   CommonStates��.
   Determinate��  Indeterminate��. �� .��T� $ 00:00:00D�$ PART_Indicator��$ 
Background�..�i� $ 00:00:00�� ��Transparent��.+��� $ 
PART_Track��$ 2��$��1q����� # �  �� $ PART_Indicator��$ 2��$��1q�$
��Left=�#��z��$��0,-1,0,1q����� $	 0,0&�$	 0,1&���� � # �  $	 0.0Q�� # �  $	 1.0Q�._�� $ 	Animation��}�. .+��� $ PART_GlowRect��$	��100��$
��Left=�8��  $��
-100,0,0,0q���  � �� �� $	 0,1&�$	 0,0&�.��� # �  $ 0Q�� # � 	 $ 1Q���  � �� �� $ 0.5,1&�$ 0.5,0&�.��� # � 	 $ 0Q�� # � 
 $ 1Q�.��� $��1�� � RowDefinitions �� $��20 ��� $��50 ��� $
��Auto ��� �� $
��Auto � � ColumnDefinitions �� �� �� .+��� $��0��$��0��$��4��$��1q�
���   �
�������$ 6��._��� $��20,0,0,0q�Barge Specs:� $��5q�$
��Left=�$��1��$��0��$��4��.+��� $��Center=�.��Barge Width:�� ��.  $	��100��$��Center=�$��30������ *BargeWidthOptions+ �� SelectedValue �� *
BargeWidth+ �� StringFormat$ 00���� $��Center=�$y�	Collapsed=�.��Barge Length:�� $	��100�� �� TextAlignment$ Center=�$��Center=�$y�	Collapsed=����� *BargeLength+$ 00.00���� $��Center=�.��Barge Depth:�� -   $	��100��$��Center=���.  $��30������ *BargeDepthOptions+ �� Text �� *
BargeDepth+$ 00����  �� Orientation$ 
Horizontal=�$y�	Collapsed=�.+��� $��Center=�.��Tons Per Inch:�� $��Center=�$��Center=�$	��100������ *TonsPerInch+$ 00.000��.���� $��Center=�$��15,5,5,5q�$y�	Collapsed=� d� 	IsChecked �� *LogReadings+.��Log Readings�� -    ��Name$ cmdGo�� �� 	ClickMode$ Press=���.  $��71��$��30��$��2q�  � ToolTip  �� *ModBusError+���� *!StartModbusCommunicationCommand+$��GO���� -   $ cmdStop��$ Press=���.  $��71��$��30��$��5q�  �� *ModBusError+���� *StopTakingDataCommand+$
��Stop���� -   ! ��Name$! cboFreeboardShow��$��30��$��Center=�$��15,5,5,5q�.@��� .��Don't show Freeboard�� .��Show Freeboard�� -   $ cmdSwap��$��5q�$��30��$��71��.��Swap�� -   $ blackBoxPrimary��$��0��$��2��$��2q�$ 6��
���   �._�  -   "  Name$" blackBox��
�������
���   �#   NodeName$# Node 1��$   
RawReading$ �� *WaterSideFreeboardAft+%   
BargeDepth% �� *
BargeDepth+&   	DeckPlate& �� *BlackBoxDeckPlate+' �� Mode$' TwoWay=��� -	   $ yellowBoxPrimary��$��3��$��2��$��2q�$ 6��
��� ���._�  -
   $" 	yellowBox��
���   �
��� ���$# Node 2��$ �� *WaterSideFreeboardForward+% �� *
BargeDepth+& �� *YellowBoxDeckPlate+$' TwoWay=��� -   ( ��Name$( blackTileAngle��$��3��$��0��$��32(�$��25��$��Center=�$��Center=����� *RollAngleNode1+$ Roll:    {0:F3}���� -   $( yellowTileAngle��$��32(�$��25��$��3��$��3��$��Center=�$��Center=����� *RollAngleNode2+$ Roll:    {0:F3}���� -   $ blackBoxSecondary��$��0��$��5��$ 6��$��6q�
���   �._�$   MDG.Visuals.DraftMeasurmentBox  -   )  Name$) blackOpposite��*   
RawReading* �� *DockSideFreeboardAft++   
BargeDepth+ �� *
BargeDepth+)  #MahApps.Metro.Controls.ProgressRing  $	��100��$	��100��$��3��$��1��
����  �$��Stretch=�$��Stretch=�,   IsActive, �� *ReadInProgress+�� -   $ yellowBoxSecondary��$ 6��$��3��$��5��$��6q�
��� ���._�  -   $) yellowOpposite��* �� *DockSideFreeboardForward++ �� *
BargeDepth+