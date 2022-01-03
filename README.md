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
| _LogParser.Core_ | Library that I have created as a recruitment task to perform read/write operations on files with emphasis on .csv files. Original idea was the log parser |


### Required file structure
It is **crucial** that given files follow this **exact** format

#### banks.csv
| _id_ | _name_ |
| ---- | ------ |
| _integer_, _not\_null_  |	_float_, _min\_chars(3)_, _max\_chars(30)_ |

#### covenants.csv
| _facility\_id_ | _max\_default\_likelihood_ | _bank\_id_ | _banned\_state_ |
| -- | -- | -- | -- | 
| _integer_, _not\_null_ |	_float_, _not\_negative_ | _integer_, _not\_null_ | _is\_valid\_us\_state_, _not\_null_ |

#### facilities.csv
| _amount_ | _interest_rate_ | _id_ | _bank\_id_ |
| -- | -- | -- | -- | 
| _integer_, _not\_null_ |	_float_, _not\_negative_, _not\_null_ | _integer_, _not\_null_ | _integer_, _not\_null_ |

#### loans.csv
| _interest_rate_ | _amount_ | _id_ | _default\_likelihood_ | _state_ |
| -- | -- | -- | -- | -- |
| _float_, _not\_negative_, _not\_null_ |	_integer_, _not\_negative_, _not\_null_ | _integer_, _not\_null_ | _float_, _not\_negative_, _not\_null_ | _is\_valid\_us\_state_, _not\_null_ |
