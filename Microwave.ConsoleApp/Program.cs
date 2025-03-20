using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

var startCancelButton = new Button();
var powerButton = new Button();
var timeButton = new Button();

var door = new Door();

var output = new Output();

var display = new Display(output);

var powerTube = new PowerTube(output);

var light = new Light(output);

var timer = new Timer();

var cooker = new CookController(timer, display, powerTube);

var ui = new MicrowaveController(powerButton, timeButton, startCancelButton, door, display, light, cooker);

// Finish the double association
cooker.Microwave = ui;

// Simulate a simple sequence

powerButton.Press();

timeButton.Press();

startCancelButton.Press();

// The simple sequence should now run

System.Console.WriteLine("When you press enter, the program will stop");
// Wait for input

System.Console.ReadLine();