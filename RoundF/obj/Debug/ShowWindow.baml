   M S B A M L   `   `   `  ����A  =RoundF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:RoundFRoundF  5   	       RoundF.ShowWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:RoundF     5   	     � Title$  
ShowWindow��5   	    � SizeToContent$ WidthAndHeight=�6    � 	Resources 5	         PlayerPanel �� 
TargetType �� Property �� Value  TextBlockStyle��%�  &	        &	 �     �� 5
   
   " ��6$   .�5      ��  � ��
 ���� 6+   �� 5       � ��$	 180��6&   �� 5       � ��$	 400��6'   �� 5       � ��$ 
5, 0, 5, 0q�6'   5      �� 5   
   " ��6'   .�5      ��  � ��$* $Noto Sans CJK JP, 源真ゴシック)�6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��
 �����6+   5      5      .��5      � -   5         �Name$ ShowWindowPanel��5      $	��450��5      $	��800��6   
���   �$
��Left=�5      $	��Top=�6&   .+�5   
   �� $��20, 25, 20, 25q�68   ��6   �� *
playerVM+��5      �� 5      .R�5      � 5       ��5"      �� 5#      .R�5$      �� #��[  6!    5%       �� BasedOn �� Mode	 �� UpdateSourceTrigger��%�   '��       7  �� 5&      8   " ��6N   .�5'   "   ��  � ��$ Center=�6H   �� 5(   "    � �� 6?   �� *	TextColor+$ OneWay=�$	 PropertyChanged=�5)      5*      .+�5,      (� $��60��6"   
 (� Stretch$

 Fill=�6.    (� StretchDirection$ DownOnly=�6=   $��0, 20, 0, 20q�5-   "   .��5.      �� $��45(�6W   ��6(   �� *Player.TeamName+$ OneTime=�6W   5/      �� 50      $��6��51      	 �� X1$ 0��63   	 �� Y1$ 0��6:   	 �� X2$	 180��6A   	 �� Y2$ 0��6J   ��50      �� *	TextColor+$ OneWay=�$	 PropertyChanged=�51   J   (� 53      $��45��6"   $

 Fill=�6.   $ DownOnly=�6=   $
��Left=�54   "   .��55      �� $��30(�6V   ��6(   �� *Player.Player1+$ OneTime=�6V   56      �� 57      $��20(�58   ,   $
��Left=�59   ,   ��57   $   �� *OX1+$ OneWay=�$	 PropertyChanged=�59   ,   �� 5;      $��60(�5<   $   ��5;   $   �� *TeamP+$ OneWay=�$	 PropertyChanged=�5<   $   (� 5>      $��45��6"   $

 Fill=�6.   $ DownOnly=�6=   $��Right=�5?   "   .��5@      �� $��30(�6V   ��6(   �� *Player.Player2+$ OneTime=�6V   5A      �� 5B      $��20(�5C   ,   $��Right=�5D   $   ��5B   $   �� *OX2+$ OneWay=�$	 PropertyChanged=�5D   $   5E      5F      ��5H      �� 5I      5K      5M      5N      5O      