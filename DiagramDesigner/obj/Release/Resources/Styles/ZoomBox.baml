   M S B A M L   `   `   `  ���� J  FDiagramDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DiagramDesignerDiagramDesigner  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089    DiagramDesigner.ZoomBox  �� 
TargetType �� 	Resources   Key j� Stretch j� TileMode j� ViewportUnits j� Viewport !� Visual � ColumnDefinitions � RowDefinitions	 �� Property
 �� Value �� 
TargetType ��Name  NormalBrush �� Triggers P� Property P� Value �� 
TargetName  	DarkBrush  ToggleButtonStyle d� 	IsChecked �� Path �� Mode �� RelativeSource J� SnapsToDevicePixels J� RenderTransformOrigin �� Angle � LayoutTransform �� ScaleY N� EnterActions �� 
TargetName �� TargetProperty	 W� To g� Duration g� DecelerationRatio  N� ExitActions! g� AccelerationRatio" �� CornerRadius# 9� 
IsExpanded$ �� Padding% � Cursor& �� Ticks' � Minimum( � Maximum) � SmallChange* � LargeChange+ � Value, �� IsSnapToTickEnabled- �� IsMoveToPointEnabled. �� ElementName�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     #sclr-namespace:DiagramDesigner  %�	  '         7  7  7  �� "     ��!� 
AlphaBrush  $
 None=�$
 Tile=�$ Absolute=�$ 0,0,8,8� �  �� $��4 ��� $��4 � �� $��4 ��� $��4 �.+��� ���#EEE$��0��$��0���� ���#AAA$��0��$��1���� ���#AAA$��1��$��0���� ���#EEE$��1��$��1���� ToggleButtonStyle  "  d�.��� 	 �@J�SnapsToDevicePixels
 . �� 	 �@�OverridesDefaultStyle
 . �� 	 � ��
 �� " d�.R��� $ Border��8��  ._���  P�  �@J�IsMouseOver . . ��� $ Border��8
  	 � ���� '9�       "  9�.��� 	 �@J�SnapsToDevicePixels
 . �� 	 �@�OverridesDefaultStyle
 . �� 	 � ��
 �� " 9�.R�]� .+�d� 8�� $	��Top=�$
��Left=�$��Center=� �� $ 
IsExpanded�$ TwoWay=� �� *TemplatedParent+��� $��4q� �� $��20 ��� $��* �.+�F� $��Arrow��$��0�� . #��z��#��z��$	��0.5��$ 0.5,0.5&�$��Center=�$��Center=�"����        �     z �@KL  	= 	z��� $ 0Q��� $��HeaderContent��$��1��$��Header���� $��Content�� �� $ 0Q�._��� #��z�� P�  �@9�
IsExpanded .  �� .���� .��W� $ Content��$ LayoutTransform.ScaleY�$ 1X�$ 0:0:0.3D�W� $ Content��$ Opacity�$ 1X�$ 0:0:0.3D�W� $ Arrow��$@ :(FrameworkElement.RenderTransform).(RotateTransform.Angle)�$ 0:0:0.2D�$ 90X�$ 1Q�  �� .���� .��W� $ Content��$ LayoutTransform.ScaleY�$ 0X�$ 0:0:0.3D�W� $ Content��$ Opacity�$ 0X�$ 0:0:0.3D�W� $ Arrow��$@ :(FrameworkElement.RenderTransform).(RotateTransform.Angle)�$ 0:0:0.2D�$! 1Q�.��� 	 �@J�SnapsToDevicePixels
 . �� 	 � ��
 �� "   .R��� $" 1��$��1q����#EEE
���iii�._�9� # . 
������ .���� 
���iii�$��0,1,0,0q�$$ 0q�$	��180��._�� .+��� $��PART_ZoomCanvas��$��5q���!� $ Uniform=� ��  �� *TemplatedParent+$ ScrollViewer.Content�.+�n� $��PART_ZoomThumb��$% SizeAll������ "  n�.��� 	 � ��
 �� " n�.R��� $��1��
���   �
������ ���  �� $
��Auto ��� $��* �.+��� $��PART_ZoomSlider��$��Center=�$��Center=�$��0q�$*& $25,50,75,100,125,150,200,300,400,500R�$' 25Q�$	( 500Q�$) 25Q�$* 25Q�$	+ 100Q�$	��104��$��21��, . - .  �� $��1��$��Center=�$��Right=�$��0,0,14,0q����� $. PART_ZoomSlider��$ Value��� $��%��$��1��$��Center=�$��Right=�$��1,0,2,0q�