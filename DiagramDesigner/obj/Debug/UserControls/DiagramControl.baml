   M S B A M L   `   `   `  ����J  FDiagramDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DiagramDesignerDiagramDesigner  5      C&clr-namespace:DiagramDesigner.ControlsDiagramDesigner.Controls  5      $    DiagramDesigner.DiagramControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +sclr-namespace:DiagramDesigner     5      4c&clr-namespace:DiagramDesigner.Controls     5      .��5      �� 
�������$��1q�5      ._�5   
   � .+�5      �� -   5        $��DesignerScrollViewer��5      
������ $
��Auto=�5      $
��Auto=�5      .��5      �� -   5          ��Name$  _ItemsControl��5      ;   5DiagramDesigner.DesignerItemsControlItemStyleSelector   Instance#��Z 5   !   ��5   6   �� *Items+ � 	Resources 5         designerItemStyle �� 
TargetType �� Property �� Value$   DiagramDesigner.SelectionProps&    DiagramDesigner.ItemConnectProps �� DataType9   3DiagramDesigner.SelectableDesignerItemViewModelBase �Name(   "DiagramDesigner.Controls.DragThumb  Name	 � Cursor
 ��Name   DiagramDesigner.Connector   Orientation � DataContext �� Path/   )DiagramDesigner.BoolToVisibilityConverter   Instance �� 	Converter �� Triggers P� Property P� Value �� 
TargetName �� Value �� Binding �� RelativeSource3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth   Color   Opacity  connectorItemStyle ��Name �Name � Points-	   'DiagramDesigner.ConnectionPathConverter 	  Instance F�Name  �� Stretch! J� RenderTransformOrigin" �� Left
# �� Top$ �� Angle  PathItemStyle% �� 
StartPoint& �� EndPoint' @� RadiusX( @� RadiusY) @� Center* �� Mode  CircleItemStyle+ E� 
StartPoint, �� SweepDirection- �� Point. �� Size/ �� 
IsLargeArc%
   DiagramDesigner.CircleConverter0 
  Instance��%�  &	        &	 �    &	 �    &	 �    �� 5      " ��5   !   .�5      ��  �@��Top 5   %   �� *Top+�� 5       �@��Left 5   %   �� *Left+�� 5       �@ EnabledForSelection . 5   %   �� 5       �@ EnabledForConnection . 5   %   �� 5        � �� 5!   %   �� *	ItemWidth+�� 5"       � �� 5#   %   �� *
ItemHeight+�� 5$       �@J�SnapsToDevicePixels . 5%   %   �� 5&       � �� 5'   "   �� 5(   &   "  63   .R�5)   *   � $ selectedGrid��6/   .+�5*   .     $ PART_DragThumb��6:   $	 SizeAll��5+   =   �� 5,   .   $
 PART_ContentPresenter��6?   $��Stretch=�5-   A   $��Stretch=�5.   A   #��z��5/   A   � 50   .   $ PART_ConnectorDecorator��51   5   $��-5q�50   3   .+�52   2     $
 Left=�53   A   $��Center=�54   A   $
��Left=�55   A    52   >   �� *LeftConnector+y�56   A   �� $ ShowConnectors�# Z   57   2   $	 Top=�58   A   $	��Top=�59   A   $��Center=�5:   A    57   >   �� *TopConnector+y�5;   A   �� $ ShowConnectors�# Z   5<   2   $ Right=�5=   A   $��Center=�5>   A   $��Right=�5?   A    5<   >   �� *RightConnector+y�5@   A   �� $ ShowConnectors�# Z   5A   2   $ Bottom=�5B   A   $��Bottom=�5C   A   $��Center=�5D   A    5A   >   �� *BottomConnector+y�5E   A   �� $ ShowConnectors�# Z 5F   /   5G   +    5H   *   P� 5I   .    �@J�IsMouseOver . . �5K   2   �� $ PART_ConnectorDecorator��69    � y�$ Visible=�5M   9   5N   /   �� 5P   .   $
 True��6:    5Q   =   ��  �� *Self+$ IsDragConnectionOver�.a�5R   2   �� $ PART_ConnectorDecorator��69    � y�$ Visible=�5T   9   5U   /   �� 5W   .   $
 True��6:    5X   =   �� *
IsSelected+.a�5Y   2   �� $ selectedGrid��69    �@J�Effect 5[   6     5\   :   $ 30Q�6K   $ 0Q�5]   M   $ Black��5^   M   $	 1.0Q�5_   M   5a   3   5b   /   5d   '   5f      5g      �� 5i      " ��5j   !   .�5k      ��  � �� 5l   %   �� *
Area.Width+�� 5m       � �� 5n   %   �� *Area.Height+�� 5o       �@��Top 5p   %   �� *
Area.Top+�� 5q       �@��Left 5r   %   �� *	Area.Left+�� 5s       �@ EnabledForSelection . 5t   %   �� 5u       � �� 5v   "   �� 5w   &   .R�5x   *   �� $ selectedGrid��5y   1   $��0q�5x   1   $��Stretch=�5z   1   $��Stretch=�5{   1   .+�5|   .   � $
 poly��67   
�������$��2��5   9    5~   9   �� $ ConnectionPoints�# Z 5   9   F� 5�   .   $ arrow��63   "����    ��� �@KL     р�� ��� 	
�������$
��Left=�5�   5   $��10��5�   5   $
  Fill=�5�   5   
�������$	��Top=�5�   5   $��10��5�   5   $! 0.5,0.5&�5�   5   y�5�   5   �� $ IsFullConnection�# Z " 5�   5   �� *
EndPoint.X+# 5�   5   �� *
EndPoint.Y+z�5�   2   �� 5�   6   rot��6F   5�   /   5�   +    5�   *   �� 5�   .   $
 True��6:    5�   =   �� *
IsSelected+.a�5�   2   �� $
 poly��69    � ��
 �   �5�   9   �� 5�   2   $ arrow��69    � ��
 �   �5�   9   �� 5�   2   $ arrow��69    � ��
 �   �5�   9   5�   /   �� 5�   .   $
 Left��5�   =    5�   :   �� $# SinkConnectorInfo.Orientation�.a�5�   2   �� $ arrow��69    � ��$ 
-15,-5,0,0q�5�   9   �� 5�   2   $ arrow��69    � z� 5�   6   �� 5�   :   $$ 90Q�6J   5�   3   5�   /   �� 5�   .   $	 Top��5�   =    5�   :   �� $# SinkConnectorInfo.Orientation�.a�5�   2   �� $ arrow��69    � ��$ 
-5,-15,0,0q�5�   9   �� 5�   2   $ arrow��69    � z� 5�   6   �� 5�   :   $	$ 180Q�6J   5�   3   5�   /   �� 5�   .   $ Right��5�   =    5�   :   �� $# SinkConnectorInfo.Orientation�.a�5�   2   �� $ arrow��69    � ��$ 5,-5,0,0q�5�   9   �� 5�   2   $ arrow��69    � z� 5�   6   �� 5�   :   $	$ -90Q�6J   5�   3   5�   /   �� 5�   .   $ Bottom��5�   =    5�   :   �� $# SinkConnectorInfo.Orientation�.a�5�   2   �� $ arrow��69    � ��$ 	-5,10,0,0q�5�   9   �� 5�   2   $ arrow��69    � z� 5�   6   �� 5�   :   $$ 0Q�6J   5�   3   5�   /   5�   '   5�      5�      �� 5�      " ��5�   !   .�5�      ��  �@ EnabledForSelection . 5�   %   �� 5�       �@J�SnapsToDevicePixels . 5�   %   �� 5�       � �� 5�   "   �� 5�   &   .R�5�   *   �� $ selectedCanvas��61   .+�5�   .   F� $
 Line��63   
���   �$��2��6P   ��5�   2   �� 5�   6   % 6C   �� *Start.Point+& 6f   �� *	End.Point+5�   /   F� 5�   .   $ 
StartPoint��63   
���   ���5�   2   @� 5�   6   $' 5Q�6r   $( 5Q�6~   ) 6F   �� *Start.Point+$* TwoWay=�6~   5�   /   F� 5�   .   $ EndPoint��63   
���   ���5�   2   @� 5�   6   $' 5Q�6p   $( 5Q�6|   ) 6F   �� *	End.Point+$* TwoWay=�6|   5�   /   5�   +    5�   *   �� 5�   .   $
 True��6:    5�   =   �� *
IsSelected+.a�5�   2   �� $ selectedCanvas��69    �@J�Effect 5�   6     5�   :   $ 30Q�6K   $ 0Q�5�   M   $ Black��5�   M   $	 1.0Q�5�   M   5�   3   5�   /   5�   '   5     5     �� 5     " ��5  !   .�5     ��  �@ EnabledForSelection . 5  %   �� 5      �@J�SnapsToDevicePixels . 5	  %   �� 5
      � �� 5  "   �� 5  &   .R�5  *   �� $ selectedCanvas��61   .+�5  .   F� $	 Arc��63   
���   �$��2��6O   ��5  2   B� 5  6   .��5  :   E� + 6E   �� *Start.Point+.��5  >   �� $, 	Clockwise=�6}   - 6I   �� *	End.Point+. 6e   �� *Radius+/ 6�   �� *Digree+# Z0 5  ;   5  7   5  /   F� 5  .   $ 
StartPoint��63   
���   ���5  2   @� 5  6   $' 5Q�6r   $( 5Q�6~   ) 6F   �� *Start.Point+$* TwoWay=�6~   5  /   F� 5  .   $ EndPoint��63   
���   ���5  2   @� 5  6   $' 5Q�6p   $( 5Q�6|   ) 6F   �� *	End.Point+$* TwoWay=�6|   5   /   F� 5!  .   $ Center��63   
���   �{�6P   �� *
CenterFlag+��5"  2   @� 5#  6   ) 6F   �� *Center.Point+$* TwoWay=�' 6s   �� *	SetRadius+( 6�   �� *	SetRadius+5%  /   5&  +    5'  *   �� 5(  .   $
 True��6:    5)  =   �� *
IsSelected+.a�5*  2   �� $ selectedCanvas��69    �@J�Effect 5,  6     5-  :   $ 30Q�6K   $ 0Q�5.  M   $ Black��5/  M   $	 1.0Q�50  M   52  3   53  /   55  '   57     58     59     ��5;     �� 5<     .R�5=     $   DiagramDesigner.DesignerCanvas  $	��800��5>  1   $
��1000��5?  1   
�������1 J� 	AllowDrop1 . 5B     5C     5F     5G        DiagramDesigner.ZoomBox  5H     -   5        2  Name$2 zoomBox��5H     $	��180��5I     $��Right=�5J     $	��Top=�5K     $��0,5,25,0q�5M     3   ScrollViewer3 5L     �� 4 �� ElementName$4 DesignerScrollViewer��5M     5N     5O     5P     