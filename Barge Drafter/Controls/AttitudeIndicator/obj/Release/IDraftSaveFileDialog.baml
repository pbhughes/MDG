   M S B A M L   `   `   `  ����   MDG.Visuals&     MDG.Visuals.IDraftSaveFileDialog   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f42tk)http://schemas.xceed.com/wpf/xaml/toolkit    � Title$  IDraftSaveFileDialog��$	��650��$	��500�� +� FocusedElement ��  �� ElementName$ 
txtBargeID�� � 	Resources  �� 
TargetType �� Property �� Value��%X   '��       �� " ��.���  � ��$ 5q���  � ��$ 71����  � ��$ 30��.��� $
��Auto�� � RowDefinitions �� $��50 ��� $
��500* ��� $��50 � � ColumnDefinitions �� $
��Auto �.+��� $��1q�$��0��._��� 	 �� Orientation$	 
Horizontal=�$��5q�.+��� $��Center=�.��
Hull ID:�� -   
 ��Name$
 
txtBargeID��$��Center=�$	��250�� �� CharacterCasing$ Upper=� � ToolTip$: 4Hit Enter to record the Barge ID in the Draft Ticket���� .��Hit Enter to Record Hull ID�� $��1��$��5q�._��� $
��Auto=�$
��Auto=�.���� -    ��Name$ txtPrintData��$	��500���� $	 
Horizontal=�$��2��$��center=�.+��� -   .��Save�� -   .��Print�� -   .��Email#  Xceed.Wpf.Toolkit.SplitButton  -   $��splitButton��$��1��#��Z�$��2��$��10q����� $	 
Horizontal=�.+��� .��	Publish����  �� Color$ Transparent��&   Xceed.Wpf.Toolkit.DropDownButton   DropDownContent �� .@��� .��
Commands�� -   $��5q�$��35������ $	 
Horizontal=�$��Center=�.+��� $��Center=�$
��Left=�$	��150��.��	Send Text�� -   $��5q�$��35������ $	 
Horizontal=�$��Center=�.+��� $��Center=�$
��Left=�$	��150��.��	BargeData��  �� IsCancel . .��Cancel