using a1;
using static System.Console;

WriteLine("Let's play Two-Player Board Game!");
Menu menu = new Menu();
menu.ShowMainMenu();


// todo re ask player move if invalid
// no need to ask for piece (notakto game)

// gamestate => save => load
// gamestate => undo => redo

// command design pattern => cmd