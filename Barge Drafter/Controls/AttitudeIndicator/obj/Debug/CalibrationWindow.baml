   M S B A M L   `   `   `  ����F  BDraftPicker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullJ4clr-namespace:DraftPicker.Views;assembly=DraftPickerDraftPicker.Views  5   	   F BMDG.Visuals, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MDG.VisualsMDG.Visuals 5   	   #   MDG.Visuals.CalibrationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Gpicker4clr-namespace:DraftPicker.Views;assembly=DraftPicker     5   	   +draftclr-namespace:MDG.Visuals     5   	     � Title$  Calibration Window��5   	   $	��300��5   	   $	��100��5   	   $	��425��5	   	    � 
ResizeMode$ CanResizeWithGrip=�5
   	    � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5   	    � 	Resources 5       �� 
TargetType �� Property �� Value��%�   '��       �� 5   
   " ��6   .�5      ��  � ��$ 35��6'   64   �� 5       � ��$ 0,3,12,3q�6'   6:   �� 5       � ��$ 95��6&   63   5      5      .��5      � -   5         �Name$ 
layoutRoot��5      	 � RowDefinitions	 5   
   �� 5      �� 5      �� 5      $��75 �6   
 � ColumnDefinitions
 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ pnlCurrentDraft��5       �� Orientation$ 
Horizontal=�6.   $��Stretch=�6G   $��0��6f   $��2��6s   .+�5      ,  &MDG.Visuals.PrimaryDraftMeasurementBox  -   5          Name$ calculatedDraft��5       $
��Auto��5#         
RawReading 5!      �� *CurrentFreeboard+   
BargeDepth 5"      �� *
BargeDepth+5#      5$      !   DraftPicker.Views.DraftView  5%   
   -   5          Name$ actualDraft��5%      $��1��60   $��2��6=   �� 5&   
   $��Right=�6   $ 
Horizontal=�61   $��2��5'      $��0��6"   $��2��62   .+�5(      �� -   5         �� 	IsDefault . .��5(   >   OK6B   �� 5)      -   5         �� IsCancel . .��5)   A   Cancel6I   5*      5+      5,      