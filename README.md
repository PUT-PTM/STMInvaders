#Space Invaders

Overview
--------
This is game for PC with microcontroler STM32f4-Discovery as "GamePad" and audio output.

Description
-----------
We code simple game with Unity3D engine, which could connect with STM32f4-Discovery via
Virtual COM Port (also known as serial port on MSDN). Game will get output (moves of our player)
from controller and send simple commands back (sound to play).
Game is based on popular Space Invaders - player fight with enemy ships in space to win.

Tools
-----
Coocox CoIDE - programm for microcontroller
Unity3D - game engine
Microsoft Visual Studio 2015 Enterprise - C# scripts for game

How to compile
--------------
1. Without microcontroller
Game can run without controller (but with no sounds), so everything you need is Unity3D. 
1.1. Open Unity and add folder STMInvaders/PTM_2D_Game/ as new project.
1.2. Go to tab "File->Build Settings..."
1.3. Choose scenes (at this moment only _Scenes/level0)
1.4. Select platform "PC, MAC and Linux standalone"
1.5. Now you can run build (Unity should ask you for output folder)

2. With microcontroller (You need of course STM32f4-Discovery connected to PC)


How to run
----------
At this moment project isn't possible to run. However if you have Unity3D, you can
build uncompleted game in Unity (it's still nothing special, but some of game
mechanics work).

Future improvements
-------------------
See TODO.txt, there is list of some planned features:
https://github.com/PUT-PTM/STMInvaders/blob/master/TODO.txt

Attributions
------------
www.Unity3d.com
https://github.com/xenovacivus/STM32DiscoveryVCP

License
-------
Section TODO

Credits
-------

Contributors:
Rafał Kopczyński && Artur Jędryczkowski


The project was conducted during the Microprocessor Lab course held by the Institute of Control and Information Engineering, Poznan University of Technology.
Supervisor: Michał Fularz 
