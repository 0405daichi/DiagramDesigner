   M S B A M L   `   `   `  ���� J  FDiagramDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DiagramDesignerDiagramDesigner  C&clr-namespace:DiagramDesigner.ControlsDiagramDesigner.Controls  $    DiagramDesigner.DiagramControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     +sclr-namespace:DiagramDesigner     4c&clr-namespace:DiagramDesigner.Controls     .���� 
�������$��1q�._�� .+��� -   $��DesignerScrollViewer��
������ $
��Auto=�$
��Auto=�.���� ;   5DiagramDesigner.DesignerItemsControlItemStyleSelector    Instance#��Z  ���� *Items+ � 	Resources    designerItemStyle �� 
TargetType �� Property �� Value$   DiagramDesigner.SelectionProps&    DiagramDesigner.ItemConnectProps �Name(   "DiagramDesigner.Controls.DragThumb  Name � Cursor ��Name   DiagramDesigner.Connector	   Orientation
 � DataContext �� Path/   )DiagramDesigner.BoolToVisibilityConverter   Instance �� 	Converter �� Triggers P� Property P� Value �� 
TargetName �� Value �� Binding �� RelativeSource3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth   Color   Opacity  connectorItemStyle ��Name �Name � Points-   'DiagramDesigner.ConnectionPathConverter   Instance F�Name �� Stretch J� RenderTransformOrigin  �� Left
! �� Top" �� Angle��%T
  &	        &	 �    �� " ��.���  �@��Top �� *Top+��  �@��Left �� *Left+��  �@ EnabledForSelection . ��  �@ EnabledForConnection . ��  � �� �� *	ItemWidth+��  � �� �� *
ItemHeight+��  �@J�SnapsToDevicePixels . ��  � �� �� .R�� $ selectedGrid��.+�  $ PART_DragThumb��$ SizeAll���� $ PART_ContentPresenter��$��Stretch=�$��Stretch=�#��z��� $ PART_ConnectorDecorator��$��-5q�.+�  $
	 Left=�$��Center=�$
��Left=�
 �� *LeftConnector+y��� $ ShowConnectors�# Z   $		 Top=�$	��Top=�$��Center=�
 �� *TopConnector+y��� $ ShowConnectors�# Z   $	 Right=�$��Center=�$��Right=�
 �� *RightConnector+y��� $ ShowConnectors�# Z   $	 Bottom=�$��Bottom=�$��Center=�
 �� *BottomConnector+y��� $ ShowConnectors�# Z  P�  �@J�IsMouseOver . . ��� $ PART_ConnectorDecorator�� � y�$ Visible=��� $
 True�� ��  �� *Self+$ IsDragConnectionOver�.a��� $ PART_ConnectorDecorator�� � y�$ Visible=��� $
 True�� �� *
IsSelected+.a��� $ selectedGrid�� �@J�Effect   $ 30Q�$ 0Q�$ Black��$	 1.0Q��� " ��.���  � �� �� *
Area.Width+��  � �� �� *Area.Height+��  �@��Top �� *
Area.Top+��  �@��Left �� *	Area.Left+��  �@ EnabledForSelection . ��  � �� �� .R��� $ selectedGrid��$��0q�$��Stretch=�$��Stretch=�.+�� $
 poly��
�������$��2�� �� $ ConnectionPoints�# Z F� $ arrow��"����    ��� �@KL     р�� ��� 	
�������$
��Left=�$��10��$
 Fill=�
�������$	��Top=�$��10��$ 0.5,0.5&�y��� $ IsFullConnection�# Z   �� *
EndPoint.X+! �� *
EndPoint.Y+z��� rot�� �� $
 True�� �� *
IsSelected+.a��� $
 poly�� � ��
 �   ��� $ arrow�� � ��
 �   ��� $ arrow�� � ��
 �   ��� $
 Left�� �� $# SinkConnectorInfo.Orientation�.a��� $ arrow�� � ��$ 
-15,-5,0,0q��� $ arrow�� � z� �� $" 90Q��� $	 Top�� �� $# SinkConnectorInfo.Orientation�.a��� $ arrow�� � ��$ 
-5,-15,0,0q��� $ arrow�� � z� �� $	" 180Q��� $ Right�� �� $# SinkConnectorInfo.Orientation�.a��� $ arrow�� � ��$ 5,-5,0,0q��� $ arrow�� � z� �� $	" -90Q��� $ Bottom�� �� $# SinkConnectorInfo.Orientation�.a��� $ arrow�� � ��$ 	-5,10,0,0q��� $ arrow�� � z� �� $" 0Q����� .R�$	   DiagramDesigner.DesignerCanvas	  $
   DiagramDesigner.DesignerCanvas# 
  Loaded# DesignerCanvas_Loaded$	��800��$
��1000��
�������$ J� 	AllowDrop$ .    DiagramDesigner.ZoomBox  -   %  Name$% zoomBox��$	��180��$��Right=�$	��Top=�$��0,5,25,0q�&   ScrollViewer& �� ' �� ElementName$' DesignerScrollViewer��