># Space Invaders

Overview
--------
>- [Game] Folder with Unity3d game and other things related to game
>- [STM_CONTROLLER] Folder with Coocox project

Description
-----------
We code simple game with Unity3D engine, which could connect with STM32f4-Discovery via
Virtual COM Port (also known as serial port on MSDN). Game will get output (moves of our player)
from controller and send simple commands back (sound to play).
Game is based on popular Space Invaders - player fight with enemy ships in space to win.

Tools
-----
>- [Coocox CoIDE] - programm for microcontroller
>- [Unity3D] - game engine
>- [Microsoft Visual Studio 2015] - C# scripts for game

How to compile
--------------
1. Without microcontroller
Game could be run without controller (but with no sounds), so everything you need is Unity3D. 
	- Open Unity and add folder **STMInvaders/PTM_2D_Game/** as new project.
	- Go to tab **"File->Build Settings..."**
	- Choose scenes (only _Scenes/level0)
	- Select platform **"PC, MAC and Linux standalone"**
	- Now you can run build (Unity should ask you for output folder)

2. With microcontroller (You need of course STM32f4-Discovery connected to PC)
	- Go to **STMInvaders/STM_CONTROLLER/**
	- Open coocox project (STM_CONTROLLER.coproj)
	- Build project and save it on microcontroller
	- If you have connected some sound output to STM (some headphones) you should hear some sound
	- Now "STM" should be detected in hardware manager (on windows) but it's still not seen as VCOM port, so you just need to remove STM on plug again
	- If you see STM as VCOMX (X is some number) we are in home :)
	- Now build game project (see 1.)

How to run
----------
- If you will run game without microcontroller - just run *exe file generated by Unity.
- Otherwise you should connect "STM" to PC. Usually first time STM load drivers so it's not recognized as VCOM port so you should remove it and plug again. After it STM should be normally detected so we can run game.

Future improvements
-------------------
See [TODO], there is list of some planned features.

Attributions
------------
>- https://github.com/xenovacivus/STM32DiscoveryVCP
>- http://www.mind-dump.net/configuring-the-stm32f4-discovery-for-audio

License
-------
MIT license - see [LICENSE]

Credits
-------

Contributors: **[Rafał Kopczyński]** && **[Artur Jędryczkowski]**


The project was conducted during the Microprocessor Lab course held by the Institute of Control and Information Engineering, Poznan University of Technology.

Supervisor: **[Tomasz Mańkowski]**

[Game]: <https://github.com/PUT-PTM/STMInvaders/tree/master/Game>
[STM_CONTROLLER]: <https://github.com/PUT-PTM/STMInvaders/tree/master/STM_CONTROLLER>
[TODO]: <https://github.com/PUT-PTM/STMInvaders/blob/master/TODO.md>
[Rafał Kopczyński]: <https://github.com/rkopczynski>
[Artur Jędryczkowski]: <https://github.com/Martsan324>
[Tomasz Mańkowski]: <https://github.com/Tomasz-Mankowski>
[Coocox CoIDE]: <http://www1.coocox.org/CoIDE/CoIDE_Updates.htm>
[Unity3D]: <www.Unity3d.com>
[Microsoft Visual Studio 2015]: <https://www.visualstudio.com/>
[LICENSE]: <https://github.com/PUT-PTM/STMInvaders/tree/master/LICENSE>
