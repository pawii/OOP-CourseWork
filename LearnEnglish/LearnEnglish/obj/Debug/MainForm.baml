   M S B A M L   `   `   `  ����G  CLearnEnglish, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:LearnEnglishLearnEnglish  5   	       LearnEnglish.MainForm   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:LearnEnglish     5   	   -   5          � Title$  Learn English��5   	   $	��600��6   $	��700��6,   $	��600��68   $	��700��6H   $	��600��6W   $	��700��6g   .��5	      � 
��� ��� � RowDefinitions 5
   
   �� 5      $��50 �6   �� 5      �� 5      $��30 �6    � ColumnDefinitions 5   
   �� 5      $��30 �6   �� 5      �� 5      $��30 �6   .+�5   
   c� -   5         c�Name$ toolBar��5      $��0��6#   $��9��63   
��� ���$y�Hidden=�6_   .@�5      �� -   5         ��Name$ translateTB��5       �� 	IsEnabled . $��16(�6G   $	��200��6U   
�������6b   �� 5      -   5        $��
Поиск��5      
�������$��	10,0,10,0q�68   $��16(�6K   6Y   �� 5       �� Orientation$ Vertical=�6   .+�5      �� -   5         ��Name$ countWordsLabel��5      $6��0Количество слов в словаре:��61   $��10(�6V   $��	10,0,10,0q�6d   �� 5      -   5        $ countOffersLabel��5      $G��AКоличество предложений в словаре: 22��62   $��10(�6a   $��	10,0,20,0q�6o   5      o� 5      
��� ���.@�5      m�  m� Icon 5      �� 5      $��Resources/ава.png��6    $��35,0,0,0q�6;   .@�5       m� -   5        $$��Мой словарь слов��5       69   m� 5!      -   5        $2��,Мой словарь предложений��5!      6@   m� 5"      -	   5        $��
Выход��5"      6.   5#      5$      5%      �� 5'   
   
�������$��1��6(   $��1��65   .+�5(      �� $"��Resources/MainBackground.jpg��6   $
��None=�6;   5)       � 5*   
   -
   5        	  �Name$	 Pages��5*      $��1��6   $��1��6/   
  � NavigationUIVisibility$
 Hidden=�6<   5+      5,      