# LoanFacilitiesCoverManager

Project was created as an assigment on _Intro to CS_ course at WSEI University in Kraków.


## Goal of project

Given 4 files
| File name | Contents |
| --------- | -------- |
| _loans.csv_ | Information about loans |
| _facilities.csv_ | Information about facilities that should cover the loans |
| _banks.csv_ | Information about banks the loan was given in |
| _covenants.csv_ | Information about covenants targeting some loans |

The program will output one file
| File name | Contents |
| --------- | -------- |
| _assignments.csv_ | Pairs of ids: _loan\_id_ and covering it _facility\_id_ |


## Project structure

Solution was based on **Domain Driven Domain** approach and consists of 7 projects:
| Project name | Responsibility |
| ------------ | -------------- |
| _Application_ | Contains _Application Layer_ features.  Intended to be used by end user/GUI. |
| _Domain_ | Contains _Domain Layer_ features. Domain models, interfaces and specifications. Not intended to be modified directly |
| _Output_ | Start console application. Acts as a very simple UI |
| _Application.Contracts_ | Contains contracts allowing to interact with _Application Layer_. DTOs etc. |
| _Infrastructure.CsvParser_ | Contains _Infrastructure Layer_ features realated to CsvParser. Treats .csv files as read-only databases |
| _Infrastructure.Shared_ | Contains features shared across all Infrastructure Layers. Currently empty |
| _LogParser.Core_ | Library that I have created to perform read/write operations on files with emphasis on .csv files |
