   M S B A M L   `   `   `  ���� F  BWPFMediaKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullfBclr-namespace:WPFMediaKit.DirectShow.Controls;assembly=WPFMediaKitWPFMediaKit.DirectShow.Controls   SampleApplication   SampleApplication.Window1   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    8x,http://schemas.microsoft.com/winfx/2006/xaml    RDShowBclr-namespace:WPFMediaKit.DirectShow.Controls;assembly=WPFMediaKit    -     �Name$  
mainWindow�� � Title$ Window1��$	��900��$	��900��
���   �
������� � 	Resources 8   2WPFMediaKit.DirectShow.Controls.MediaTimeToSeconds   MediaTimeToSeconds��%   &	          .��� -    �Name$
 root��.+�� $��0,0,0,0q� � ColumnDefinitions �� $
��0.5* ��� $
��0.5* �.+�5   /WPFMediaKit.DirectShow.Controls.MediaUriElement  -     Name$ mediaPlayer��$
��Auto��$
��Auto��6   0WPFMediaKit.DirectShow.Controls.MediaElementBase   LoadedBehavior$
 Play=�   UnloadedBehavior$ Close=�9   3WPFMediaKit.DirectShow.Controls.MediaSeekingElement   PreferedPositionFormat$ Frame=�	   Volume$	 1Q�1   +WPFMediaKit.DirectShow.Controls.D3DRenderer
   Stretch$
 Uniform=�   VideoRenderer$ VideoMixingRenderer9=�   AudioRenderer ��  �� Path$ SelectedItem� �� ElementName$ audioDevices���� $
��Left=�$	��Top=����	#55ffffff.+�o� $��471.303333333333��.@�m� $��_File��.@�m� -    m�Name$ mnuOpen��$��_Open����  �� Orientation$ 
Horizontal=�.+��� $��
Deep Color����  d� 	IsChecked �� $ mediaPlayer��$ DeeperColor��� $
��Loop����  �� $
 Loop�$ mediaPlayer���� -    ��Name$ audioDevices��
���   �$��20(�$	��Top=�$��
0, 30, 0,0q�$	��400��$��80��$��Hidden=����� 4   .WPFMediaKit.DirectShow.Controls.MultimediaUtil   AudioRendererNames �� Source# Z �� -    ��Name$ 
rateSlider��$��Center=�$	��Top=� �� Orientation$ 
Horizontal=�$	��300�� � SmallChange$	 0.3Q� � LargeChange$ 1Q� � Minimum$ 0Q� � Maximum$ 10Q� � Value �� $ 
SpeedRatio�$ mediaPlayer���� $ 
Horizontal=�.+��� $��Position Format: ���� ���� $ CurrentPositionFormat�$ mediaPlayer���� $��0���� -   $ positionSlider��$��Center=�$	��Top=�$ 
Horizontal=�$	��300��$
 0.01Q�$ 1Q� �� $ MediaPosition�$ mediaPlayer�� �� $ MediaDuration�$ mediaPlayer�� � ToolTip `�  `� 	Converter# [  .1��� $ mediaPlayer���� $ mediaPlayer��$ MediaPosition��� .��`� # [  .1��� $ mediaPlayer���� $ mediaPlayer��$ MediaDuration��� $ 
Horizontal=�.+��� -    ��Name$ 
playButton��$
��Play���� -	   $ pauseButton��$��Pause���� -
   $��Don't Click��$��40��$��40���� $��1��$��0,0,0,0q�.+��� $ Vertical=�$
��Auto��$
��Auto��.+�9   3WPFMediaKit.DirectShow.Controls.VideoCaptureElement  -     Name$ videoCapElement��$
 Play=�    DesiredPixelWidth$	  320��!   DesiredPixelHeight$	! 240��$

 Fill=�
"   FPS$" 30��#   EnableSampleGrabbing# . $   VideoCaptureDevice$ �� $ SelectedItem�$ videoCapDevices���� -   % ��Name$% videoCapDevices��& �� DisplayMemberPath$
& Name��
���   �$��20(�$	��Top=�$��
0, 30, 0,0q����� '   VideoInputDevices# Z'   -   $ videoCapElement1��$
 Play=�$	  320��$	! 240��$

 Fill=�$" 30��$ �� $ SelectedItem�$ videoCapDevices1���� -   $% videoCapDevices1��$
& Name��
���   �$��20(�$	��Top=�$��
0, 30, 0,0q����� # Z' �� $��Right=�$��Stretch=�$��5��$
��Auto��$	��Top=�