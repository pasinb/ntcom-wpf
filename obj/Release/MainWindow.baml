   M S B A M L   `   `   `  ���� @  <NTCOM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:NTCOM_WPF	NTCOM_WPF  ;"clr-namespace:NTCOM_WPF.PropertiesNTCOM_WPF.Properties      NTCOM_WPF.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:NTCOM_WPF     0p"clr-namespace:NTCOM_WPF.Properties       � Title$  NTCOM��$	��400��$
��1100�� � 	Resources    NTCOM_WPF.RadioConverter   RadioConverter   NTCOM_WPF.MainWindow  
MainWindow$   NTCOM_WPF.NameToBrushConverter  NameToBrushConverter%   NTCOM_WPF.NameToBorderConverter  NameToBorderConverter   NTCOM_WPF.IdleConverter  IdleConverter��%K   &	        &	      &	 
     &	      &	                .���  � RowDefinitions �� �� $��30 �.+��� -   $��dateText��$��18��$
��Wrap=�$�� ��$��94��$��Right=�$	��Top=�$��0,2,5,0q� �� TextAlignment$ Right=��� -    ��Name$ mainTab��$��0��$��0q�.@��� $
��Data��.��� ���	#FFFFFFFF � ColumnDefinitions �� $��* ��� $��80 �.+�� -   $��gridWrapper��.+�&   System.Windows.Controls.DataGrid  -   $��mainGrid��   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * ���	#FFFFFFFF   AutoGenerateColumns .     
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserSortColumns
 .     HeadersVisibility$ Column=�   GridLinesVisibility$
 None=�$��Stretch=�$��Stretch=�$��0q�����  �� Source$ 	stateGrid��   RowStyle ��  �� 
TargetType)  #System.Windows.Controls.DataGridRow"  .���  �� Property �@J�IsHitTestVisible �� Value .   =	  7System.Windows.Controls.Primitives.DataGridColumnHeader �� BasedOn��%�   '	        7	 �� 8   " 	 .���  � �� �#b5b5ff��  � ��
 �   ���  � ��
 �   ���  � ��$ 1 1 1 1q���  � ��$ 	-1,-1,0,0q���  � ��$ Center=�   Columns 0
  *System.Windows.Controls.DataGridTextColumn
  ,  &System.Windows.Controls.DataGridColumn   Header$  ��   Width$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *index+   	CellStyle �� *  $System.Windows.Controls.DataGridCell"  .���  � ��
 �   ���  � ��$
 auto����  � ��$ 1q���  � ��$ 	-1,-1,0,0q�   ElementStyle �� " ��.���  � �� �#b5b5ff��  �@��TextAlignment$ Center=���  � ��$ Stretch=�
   �� #   NTCOM_WPF.Properties.Settings   Default# Z  �� Mode$ TwoWay=� �� Path$ name1� �� *	data[0]+ �� "  .���  � ��
 �   ���  � �� �� *	data[0]+ �� 	Converter# [ ��  � ��$ 	-1,-1,0,0q���  � �� �� *cellStatus[0]+# [  �� " ��.���  � ��$ Right=�
   �� # Z $ TwoWay=�$ name2� �� *	data[1]+ �� "  .���  � ��
 �   ���  � �� �� *	data[1]+# [ ��  � ��$ 	-1,-1,0,0q���  � �� �� *cellStatus[1]+# [  �� " ��.���  � ��$ Right=�
   �� # Z $ TwoWay=�$ name3� �� *	data[2]+ �� "  .���  � ��
 �   ���  � �� �� *	data[2]+# [ ��  � ��$ 	-1,-1,0,0q���  � �� �� *cellStatus[2]+# [  �� " ��.���  � ��$ Right=�
   �� # Z $ TwoWay=�$ name4� �� *	data[3]+ �� "  .���  � ��
 �   ���  � �� �� *	data[3]+# [ ��  � ��$ 	-1,-1,0,0q���  � �� �� *cellStatus[3]+# [  �� " ��.���  � ��$ Right=�
   �� # Z $ TwoWay=�$ name5� �� *	data[4]+ �� "  .���  � ��
 �   ���  � �� �� *	data[4]+# [ ��  � ��$ 	-1,-1,0,0q���  � �� �� *cellStatus[4]+# [  �� " ��.���  � ��$ Right=�
   �� # Z $ TwoWay=�$ name6� �� *	data[5]+ �� "  .���  � ��
 �   ���  � �� �� *	data[5]+# [ ��  � ��$ 	-1,-1,0,0q���  � �� �� *cellStatus[5]+# [  �� " ��.���  � ��$ Right=�
   �� # Z $ TwoWay=�$ name7� �� *	data[6]+ �� "  .���  � ��
 �   ���  � �� �� *	data[6]+# [ ��  � ��$ 	-1,-1,0,0q���  � �� �� *cellStatus[6]+# [  �� " ��.���  � ��$ Right=�
   �� # Z $ TwoWay=�$ name8� �� *	data[7]+ �� "  .���  � ��
 �   ���  � �� �� *	data[7]+# [ ��  � ��$ 	-1,-1,0,0q���  � �� �� *cellStatus[7]+# [  �� " ��.���  � ��$ Right=�
   �� # Z $ TwoWay=�$ name9� �� *	data[8]+ �� "  .���  � ��
 �   ���  � �� �� *	data[8]+# [ ��  � ��$ 	-1,-1,0,0q���  � �� �� *cellStatus[8]+# [  �� " ��.���  � ��$ Right=�
   �� # Z $ TwoWay=�$ name10� �� *	data[9]+ �� "  .���  � ��
 �   ���  � �� �� *	data[9]+# [ ��  � ��$ 	-1,-1,0,0q���  � �� �� *cellStatus[9]+# [  �� " ��.���  � ��$ Right=�
  
   �� # Z $ TwoWay=�$ name11� �� *
data[10]+ �� "  .���  � ��
 �   ���  � �� �� *
data[10]+# [ ��  � ��$ 	-1,-1,0,0q���  � �� �� *cellStatus[10]+# [  �� " ��.���  � ��$ Right=��� -   $��Set
Name��$��1��$	��Top=�$��5,5,2,0q�$��Center=�$��1q��� -   $��
Test
Reset��$��1��$	��Top=�$��5,87,2,0q�$��Center=�$��1q��� -   $��Set
Hen��$��1��$	��Top=�$��5,46,2,0q�$��Center=�$��1q��� $��Start/Stop/Save��.��� ���	#FFE5E5E5 �� $
��130* ��� $	��18* ��� $
��120* ��� $
��633* �.+��� $��CSV SAVE��$
��Left=�$��
28,100,0,0q�$	��Top=�$��26��$��86���� $
��Left=�$��27��$��	10,68,0,0q�$	��Top=�$	��816��$��4���� $��	Location:��$
��Left=�$��
91,100,0,0q�$	��Top=�$��26��$��86��$��Right=�$��3���� $��
File name:��$
��Left=�$��
91,131,0,0q�$	��Top=�$��26��$��86��$��Right=�$��3���� $
��Left=�$��23�� �� TextWrapping$
 Wrap=�$	��Top=�$	��508��$��43.5,101,0,0q�$��Center=�$��2��$��2������ *csvLocation+$ TwoWay=�# Z �� $
��Left=�$��23��$
 Wrap=�$	��Top=�$	��508��$��43.5,132,0,0q�$��Center=�$��2��$��2������ *csvFileName+$ TwoWay=�# Z �� -   $��Select��$
��Left=�$	��Top=�$��85��$��23��$��437,101,0,0q�$��3���� -	   $
��Save��$
��Left=�$	��Top=�$��85��$��23��$��437,132,0,0q�$��3���� -
    ��Name$ listenButton��$��Start��$
��Left=�$	��Top=�$��84��$��23��$��	27,20,0,0q�{���  �� ElementName$ 
isUdpRadio��$ 	IsChecked��� -   $ stopListenButton��$
��Stop��$
��Left=�$	��Top=�$��88��$��23��$��
116,20,0,0q�$��3��{��� $ 
isUdpRadio��$ 	IsChecked��� $��Print��.��� ���	#FFE5E5E5.+��� -   $��Print��$��	27,20,0,0q�$	��125��$��40��$
��Left=�$	��Top=��� $��Counter Reset��.��� ���	#FFE5E5E5 �� $	��17* ��� $	��83* �.+��� $"��Counter-Reset Password (Old)��$
��Left=�$��	27,74,0,0q�$	��Top=�$��26��$	��186��$��2���� $"��Counter-Reset Password (New)��$
��Left=�$��
27,106,0,0q�$	��Top=�$��26��$	��186��$��2���� $*��$Counter-Reset Password (New-Confirm)��$
��Left=�$��
27,136,0,0q�$	��Top=�$��26��$	��227��$��2��G� -     G�Name$  	oldPwdBox��$
��Left=�$��
110,78,0,0q�$	��Top=�$	��110��$��22��$��1��G� -   $  pwdBox��$
��Left=�$��110,108,0,0q�$	��Top=�$	��110��$��24��$��1��G� -   $  pwdBoxConfirm��$
��Left=�$��110,140,0,0q�$	��Top=�$	��110��$��22��$��1���� -   $��Save Password��$
��Left=�$	��Top=�$	��110��$��110,172,0,0q�$��24��$��1���� -   $��Counter Reset��$��	27,20,0,0q�$��Center=�$	��125��$��40��$
��Left=�$	��Top=��� -   ! ��Name$! 
setNameTab��$��Set Name��$y�Hidden=�.��� ���	#FFE5E5E5.+��� -   $
��Back��$
��Left=�$	��Top=�$��84��$��	27,20,0,0q�$��24���� $��Name 1-5��$
��Left=�$��	28,58,0,0q�$	��Top=�$��26��$��86���� $��	Name 6-10��$
��Left=�$��	28,88,0,0q�$	��Top=�$��26��$��86���� $
��Left=�$��24��$
 Wrap=�$	��Top=�$��88��$��
120,58,0,0q�$��Center=����� *name1+" �� UpdateSourceTrigger$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��24��$
 Wrap=�$	��Top=�$��88��$��
212,58,0,0q�$��Center=����� *name2+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��24��$
 Wrap=�$	��Top=�$��88��$��
306,58,0,0q�$��Center=����� *name3+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��24��$
 Wrap=�$	��Top=�$��88��$��
398,58,0,0q�$��Center=����� *name4+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��24��$
 Wrap=�$	��Top=�$��88��$��
492,58,0,0q�$��Center=����� *name5+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��22��$
 Wrap=�$	��Top=�$��88��$��
120,90,0,0q�$��Center=����� *name6+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��22��$
 Wrap=�$	��Top=�$��88��$��
212,90,0,0q�$��Center=����� *name7+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��22��$
 Wrap=�$	��Top=�$��88��$��
306,90,0,0q�$��Center=����� *name8+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��22��$
 Wrap=�$	��Top=�$��88��$��
398,90,0,0q�$��Center=����� *name9+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��22��$
 Wrap=�$	��Top=�$��88��$��
492,90,0,0q�$��Center=����� *name10+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��22��$
 Wrap=�$	��Top=�$��88��$��120,119,0,0q�$��Center=����� *name11+$" PropertyChanged=�$ TwoWay=�# Z �� $��Name 11��$
��Left=�$��
27,117,0,0q�$	��Top=�$��26��$��86���� -   $! 
setHensTab��$��Set Hen��$y�Hidden=�.��� ���	#FFE5E5E5.+��� -   $
��Back��$
��Left=�$	��Top=�$��84��$��	27,20,0,0q�$��24���� $��Hen 1-5��$
��Left=�$��	28,58,0,0q�$	��Top=�$��26��$��86���� $��Hen 6-10��$
��Left=�$��	28,88,0,0q�$	��Top=�$��26��$��86���� $
��Left=�$��24��$
 Wrap=�$	��Top=�$��88��$��
120,58,0,0q�$��Center=����� *hen1+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��24��$
 Wrap=�$	��Top=�$��88��$��
212,58,0,0q�$��Center=����� *hen2+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��24��$
 Wrap=�$	��Top=�$��88��$��
306,58,0,0q�$��Center=����� *hen3+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��24��$
 Wrap=�$	��Top=�$��88��$��
398,58,0,0q�$��Center=����� *hen4+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��24��$
 Wrap=�$	��Top=�$��88��$��
492,58,0,0q�$��Center=����� *hen5+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��22��$
 Wrap=�$	��Top=�$��88��$��
120,90,0,0q�$��Center=����� *hen6+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��22��$
 Wrap=�$	��Top=�$��88��$��
212,90,0,0q�$��Center=����� *hen7+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��22��$
 Wrap=�$	��Top=�$��88��$��
306,90,0,0q�$��Center=����� *hen8+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��22��$
 Wrap=�$	��Top=�$��88��$��
398,90,0,0q�$��Center=����� *hen9+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��22��$
 Wrap=�$	��Top=�$��88��$��
492,90,0,0q�$��Center=����� *hen10+$" PropertyChanged=�$ TwoWay=�# Z �� $
��Left=�$��22��$
 Wrap=�$	��Top=�$��88��$��120,119,0,0q�$��Center=����� *hen11+$ TwoWay=�# Z $" PropertyChanged=��� $��Hen 11��$
��Left=�$��
28,117,0,0q�$	��Top=�$��26��$��86���� $��1��# �� Orientation$# 
Horizontal=�.+��� $��Status:��$��Center=�$
��Bold"��� -   $ ��Name$$ statusLabel��$��Center=�$��Disconnected��$
��Bold"�
���  ��