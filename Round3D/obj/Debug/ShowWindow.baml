   M S B A M L   `   `   `  ����B  >Round3D, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Round3DRound3D  5   	       Round3D.ShowWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Round3D     5   	     � Title$  
ShowWindow��5   	    � SizeToContent$ WidthAndHeight=�6    � 	Resources 5	         PlayerPanel �� 
TargetType �� Property �� Value  TextBlockStyle��%�  &	        &	 �     �� 5
   
   " ��6$   .�5      ��  � ��
 ���� 6+   �� 5       � ��$	 240��6&   �� 5       � ��$	 200��6'   �� 5       � ��$ 
5, 0, 5, 0q�6'   5      �� 5   
   " ��6'   .�5      ��  � ��$* $Noto Sans CJK JP, 源真ゴシック)�6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��
 �����6+   5      5      .��5      � -   5         �Name$ ShowWindowPanel��5      $	��450��5      $	��800��6   
���   �$
��Left=�5      $	��Top=�6&   .+�5   
   �� $��25, 25, 25, 25q�68   ��6   �� *
playerVM+��5      �� 5      .R�5      � 5       ��5"      �� 5#      .R�5$      �� #��[  6!    5%       �� BasedOn �� Mode	 �� UpdateSourceTrigger��%�   '��       7  �� 5&      8   " ��6N   .�5'   "   ��  � ��$ Center=�6H   �� 5(   "    � �� 6?   �� *	TextColor+$ OneWay=�$	 PropertyChanged=�5)      5*      .+�5,      (� $��45��6"   
 (� Stretch$

 Fill=�6.    (� StretchDirection$ DownOnly=�6=   .��5-      �� $��30(�6W   ��6(   �� *Player.TeamName+$ OneTime=�6W   5.      �� 5/      $��6��50      	 �� X1$ 0��63   	 �� Y1$ 0��6:   	 �� X2$	 240��6A   	 �� Y2$ 0��6J   ��5/      �� *	TextColor+$ OneWay=�$	 PropertyChanged=�50   J   � 51       � ColumnDefinitions 52      �� 53   "   $��* �63   6?   �� 54   "   $��* �63   6?   .+�57      �� $��0��6)   .+�58   "   (� $��30��6*   $

 Fill=�66   $ DownOnly=�6E   .��59   &   �� $��20(�6^   ��60   �� *Player.Player1+$ OneTime=�6^   5:   #   �� 5;   "   $��20(�5<   ,   $��
HGP明朝E)�6:   ��5;   ,   �� *Job1+$ OneWay=�$	 PropertyChanged=���5=   ,   �� *Job1Foreground+$ OneWay=�$	 PropertyChanged=�5>      �� 5?      $��1��6)   .+�5@   "   (� $��30��6*   $

 Fill=�66   $ DownOnly=�6E   .��5A   &   �� $��20(�6^   ��60   �� *Player.Player2+$ OneTime=�6^   5B   #   �� 5C   "   $��20(�5D   ,   $��
HGP明朝E)�6:   ��5C   ,   �� *Job2+$ OneWay=�$	 PropertyChanged=���5E   ,   �� *Job2Foreground+$ OneWay=�$	 PropertyChanged=�5F      5G      �� 5H      $��20(�5I   $   ��5H   $   �� *AP+$ OneWay=�$	 PropertyChanged=�5I   $   �� 5J      $��40(�5K   $   ��5J   $   �� *TeamP+$ OneWay=�$	 PropertyChanged=�5K   $   5L      5M      ��5O      �� 5P      5R      5T      5U      5V      