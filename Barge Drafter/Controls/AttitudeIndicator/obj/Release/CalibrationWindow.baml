   M S B A M L   `   `   `  ���� F  BDraftPicker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullJ4clr-namespace:DraftPicker.Views;assembly=DraftPickerDraftPicker.Views  F BMDG.Visuals, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MDG.VisualsMDG.Visuals #   MDG.Visuals.CalibrationWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Gpicker4clr-namespace:DraftPicker.Views;assembly=DraftPicker     +draftclr-namespace:MDG.Visuals       � Title$  Calibration Window��$	��300��$	��100��$	��425�� � 
ResizeMode$ CanResizeWithGrip=� � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=� � 	Resources  �� 
TargetType �� Property �� Value��%_   '��       �� " ��.���  � ��$ 35����  � ��$ 0,3,12,3q���  � ��$ 95��.��� -    �Name$ 
layoutRoot��	 � RowDefinitions	 �� �� �� $��75 �
 � ColumnDefinitions
 �� �� .+��� -    ��Name$ pnlCurrentDraft�� �� Orientation$ 
Horizontal=�$��Stretch=�$��0��$��2��.+�,  &MDG.Visuals.PrimaryDraftMeasurementBox  -     Name$ calculatedDraft��$
��Auto��   
RawReading �� *CurrentFreeboard+   
BargeDepth �� *
BargeDepth+   	DeckPlate �� *	DeckPlate+!   DraftPicker.Views.DraftView  -     Name$ actualDraft��$��1��$��2���� $��Right=�$ 
Horizontal=�$��2��$��0��$��2��.+��� -    �� 	IsDefault . .��OK�� -    �� IsCancel . .��Cancel