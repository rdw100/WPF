# WPF
This Windows Presentation Foundation (WPF) on .NET Core demonstration project was created using Visual Studio 2019 16.7.6 and .NET Core 3.1.403.  The objective is to demonstrate .NET Core, Windows Presentation Foundation (WPF), Task Parallel Library (TPL), and Synchronous versus Asynchronous Programming.

WPF.Async.Demo demonstrates an unresponsive user interface.

WPF.TPL.Demo demonstrates a responsive user interface during long and short process execution.  The scenario waits for the first task completion before continuing by using WhenAny.  This is congruous to a group of tasks performed in sequence.  The example could be applied to extract-transform-load scenarios or interdependent activities such as income qualification, patient certification, risk assessment, food package assignment, etc.

## Demonstration
![Task Process Library Demo](https://github.com/rdw100/WPF/blob/master/WPF.TPL.Demo/assets/8c6icRnqO0.gif)
