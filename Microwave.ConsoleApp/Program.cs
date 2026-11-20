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
var powerTube = new PowerTube(output);
var light = new Light(output);
var buzzer = new Buzzer(output);
var timer = new Timer();

var cooker = new CookController(timer, display, powerTube);
var microwaveController = new MicrowaveController(powerButton, timeButton, startCancelButton, door, display, light, cooker, buzzer);

// Finish the double association
cooker.Microwave = microwaveController;

Console.WriteLine("Controls:");
Console.WriteLine("P = Power");
Console.WriteLine("T = Time");
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