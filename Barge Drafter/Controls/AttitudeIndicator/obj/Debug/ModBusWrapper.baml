   M S B A M L   `   `   `  ����F  BMDG.Visuals, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MDG.VisualsMDG.Visuals  5      I EMDG.BargeModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=/clr-namespace:MDG.Model;assembly=MDG.BargeModel	MDG.Model 5          MDG.Visuals.ModBusWrapper   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -visualsclr-namespace:MDG.Visuals     5      Amodel/clr-namespace:MDG.Model;assembly=MDG.BargeModel     5        � DataContext  5        MDG.Model.BargeModel  5	   
   -   5        	barge��   RollAngleNode1$	 0.0Q�5	   *      
PitchAngle$	 0.0Q�5
       � 	Resources 5       �� 
TargetType �� Property �� Value��%W   '��       �� 5   
   " ��6   .�5      ��  � ��$ 15(�6)   66   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      6!    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6   �� 5      $��35 �6   .+�5   
   �� -   5        	 ��Name$	 pnlModBusControl��5      $��0��5      
 �� Orientation$
 
Horizontal=�6"   $��5q�5      $
��Left=�6    .+�5#      ]� $	��300��6   .+�5*      �� $
��Left=�6   $��5q�64   ��6?   �� *TakeLightDraftReadingCommand+.��6p   Record Starting Draft6�   �� 5+      $��26��6   $	��100��6(    �� Padding$ 0,3,0,0q�64   ��6G   �� *
LightDraft+6e   �� 5,      $��Right=�6   $��5q�66   ��6A   �� *TakeHeavyDraftReadingCommand+.��6r   Record Ending Draft6�   �� 5-      $��26��6   $	��100��6)   $ 0,3,0,0q�65   $��Center=�6G   ��6b   �� *
HeavyDraft+6�   �� 5.      $��Right=�6   $��5q�65   ��6@   �� *NewBargeCommand+.��6d   Start New Barge6u   50      52      �� 54   
   $��1��6   $��4q�6    �� CornerRadius$ 5��62   
���   � �� Padding$ 5q�6W   ._�56          MDG.Visuals.TelemetryMeter  $��5q�57   %   $��Stretch=�58   %   $��Stretch=�59   %     56   %   �� 5;      5<      �� 5>   
   $
 
Horizontal=�6   $��2��6.   $��5q�6<   $��Right=�6G   .+�5?      �� $��0q�65   ��6   �� *ModbusError+6B   5@      5A      5B      