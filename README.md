# Carrom Game (Android)

Welcome to the Carrom Game repository! This is an Android game made in Unity that simulates the popular board game called Carrom. This README file will guide you through the installation, gameplay, and other important details about the project.

## Table of Contents

- [Installation](#installation)
- [Gameplay](#gameplay)
- [Modes](#modes)
- [Controls](#controls)
- [AI Opponent](#ai-opponent)
- [Timer](#timer)
- [Contributing](#contributing)
- [License](#license)

## Installation

To get started with the Carrom Game on your Android device, follow these steps:

1. Create a build of the project using unity (explained in [Contributing](#contributing) section).

2. Transfer the APK file to your Android device.

3. On your Android device, locate the APK file and tap on it to begin the installation process.

4. Follow the on-screen instructions to install the Carrom Game.

5. Once the installation is complete, you can find the Carrom Game icon on your Android device's home screen or app drawer. Tap on it to launch the game.

## Gameplay

The Carrom Game is a digital adaptation of the real-life Carrom board game. The objective of the game is to pot all the pucks (white or black) and the queen into the four corner pockets using the striker. The player or team who scores the maximum number of points within the given time limit wins the game.

## Modes

The Carrom Game offers two modes of gameplay:

1. Single Player Mode: In this mode, you can play against a CPU opponent. The CPU opponent is capable of taking simple shots, adding a challenging element to the game.

2. Two Player Mode: In this mode, you can play against a friend locally. Take turns with your opponent to strike the puck and attempt to score more points within the time limit.

## Controls

The Carrom Game provides intuitive controls for smooth gameplay:

- Slider: Use the slider to position the striker on the carrom board.

- Drag and Release: Drag the striker to adjust the angle and power for your shot. Release the striker to strike it.

## AI Opponent

In the Single Player Mode, the game features a CPU opponent capable of taking simple shots. The AI opponent will analyze the game state and make decisions based on a basic strategy. The CPU opponent adds a competitive element to the game, allowing you to play even when you don't have a human opponent available.

How it works -
The CPU is capable of taking very simple shots. It scans the black pucks that are in direct line of sight between the striker and the hole by using Physics2D.RaycastAll. It iterate through almost every possible starting position of striker and check in all 4 line of sights (ie. for all holes). Once it finds a suitable black puck, it take that shot. If it couldn't find any, it will take a straight shot.

## Timer

The Carrom Game incorporates a 2-minute timer to add excitement and challenge to each game. You need to pot as many pucks as possible within the time limit to achieve a higher score and win the game.

## Contributing

Contributions to the Carrom Game project are welcome! If you'd like to contribute, please follow these steps:

1. Fork the repository.

2. Create a new branch for your feature or bug fix.

3. Implement your changes and ensure the project still runs correctly.

4. Commit your changes and push your branch to your forked repository.

5. Open a pull request to the main repository, describing your changes in detail.


To get started with the Carrom Game code, please follow these steps:

1. Clone the repository to your local machine using the following command: git clone [https://github.com/yugalkishore59/Carrom-game.git] OR you can download a zip file or fork the repository.
2. Open the Unity Editor.
3. From the Unity Editor, select **File > Open Project** and navigate to the cloned repository's directory.
4. Open the project by selecting the appropriate folder.
5. Once the project is opened, you can explore the game and its assets in the Unity Editor.
6. Now you can create an apk build of the project and install in your android device.

The project maintainer(s) will review your contribution, provide feedback, and merge it if everything looks good.

## License

None

---

Thank you for your interest in the Carrom Game project! Enjoy playing the game
