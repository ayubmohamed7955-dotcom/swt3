﻿using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Domain;
using System;

var startCancelButton = new Button();
var powerButton = new Button();
var timeButton = new Button();

var door = new Door();

var output = new Output();
var display = new Display(output);
var powerTube = new PowerTube(output, 1000); // Set max power to 1000W
var light = new Light(output);
var timer = new Timer();
var buzzer = new Buzzer(output);

var cooker = new CookController(timer, display, powerTube);
var microwaveController = new MicrowaveController(powerButton, timeButton, startCancelButton, door, display, light, cooker, buzzer, powerTube.MaxPower);

// Finish the double association
cooker.Microwave = microwaveController;

Console.WriteLine("Controls:");
Console.WriteLine("P = Power");
Console.WriteLine("T = Time (while cooking: add 30 seconds)");
Console.WriteLine("S = Start/Cancel");
Console.WriteLine("O = Open door");
Console.WriteLine("C = Close door");
Console.WriteLine("Q = Quit");

while (true)
{
    var key = Console.ReadKey(true).Key;

    switch (key)
    {
        case ConsoleKey.P:
            powerButton.Press();
            break;

        case ConsoleKey.T:
            timeButton.Press();
            break;

        case ConsoleKey.S:
            startCancelButton.Press();
            break;

        case ConsoleKey.O:
            door.Open();
            break;

        case ConsoleKey.C:
            door.Close();
            break;

        case ConsoleKey.Q:
            return;
    }
}