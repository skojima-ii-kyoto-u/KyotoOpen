   M S B A M L   `   `   `  ����C  ?RoundSF1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:RoundSF1RoundSF1  5   	       RoundSF1.ShowWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:RoundSF1     5   	     � Title$  
ShowWindow��5   	    � SizeToContent$ WidthAndHeight=�6    � 	Resources 5	         PlayerPanel �� 
TargetType �� Property �� Value  TextBlockStyle��%�  &	        &	 �     �� 5
   
   " ��6$   .�5      ��  � ��
 ���� 6+   �� 5       � ��$	 140��6&   �� 5       � ��$	 200��6'   �� 5       � ��$ 
5, 0, 5, 0q�6'   5      �� 5   
   " ��6'   .�5      ��  � ��$* $Noto Sans CJK JP, 源真ゴシック)�6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��
 �����6+   5      5      .��5      � -   5         �Name$ ShowWindowPanel��5      $	��450��5      $	��800��6   
���   �$
��Left=�5      $	��Top=�6&   .+�5   
   �� $��25, 25, 25, 25q�68   ��6   �� *
playerVM+��5      �� 5      .R�5      � 5       ��5"      �� 5#      .R�5$      �� #��[  6!    5%       �� BasedOn �� Mode	 �� UpdateSourceTrigger��%�   '��       7  �� 5&      8   " ��6N   .�5'   "   ��  � ��$ Center=�6H   �� 5(   "    � �� 6?   �� *	TextColor+$ OneWay=�$	 PropertyChanged=�5)      5*      .+�5,      (� $��45��6"   
 (� Stretch$

 Fill=�6.    (� StretchDirection$ DownOnly=�6=   .��5-      �� $��30(�6W   ��6(   �� *Player.TeamName+$ OneTime=�6W   5.      �� 5/      $��6��50      	 �� X1$ 0��63   	 �� Y1$ 0��6:   	 �� X2$	 140��6A   	 �� Y2$ 0��6J   ��5/      �� *	TextColor+$ OneWay=�$	 PropertyChanged=�50   J   � 52       � ColumnDefinitions 53      �� 54   "   $��* �63   6?   �� 55   "   $��* �63   6?   .+�58      �� $��0��6)   .+�59   "   (� $��30��6*   $

 Fill=�66   $ DownOnly=�6E   .��5:   &   �� $��20(�6^   ��60   �� *Player.Player1+$ OneTime=�6^   5;   #   5<      �� 5=      $��1��6)   .+�5>   "   (� $��30��6*   $

 Fill=�66   $ DownOnly=�6E   .��5?   &   �� $��20(�6^   ��60   �� *Player.Player2+$ OneTime=�6^   5@   #   5A      5B      �� 5C      $��48(�5D   $   ��5C   $   �� *TeamP+$ OneWay=�$	 PropertyChanged=�5D   $   5E      5F      ��5H      �� 5I      5K      5M      5N      5O      