# University Admission Management System

A console-based University Admission Management System 
built in C# using Three-Tier Architecture as a lab 
project for CSC-103L Object Oriented Programming 
(Spring 2026).

Supervised by Miss Rimsha Chauhdary.  
Developed by Asim Raza — Computer Engineering Student,  
UET Lahore Faisalabad Campus.

## Features

**Student Portal**
- Apply for admission (name, age, FSC, ECAT marks)
- Select program preferences
- View merit list and admission status
- Register subjects (max 9 credit hours)
- View generated fee

**Faculty Portal**
- Add and manage academic programs
- Add subjects to programs (max 20 credit hours)
- View enrolled students

**Admin Portal**
- View all applicants
- Generate and process merit list
- Process admissions based on merit and seats
- Search students
- View summary report and waitlist

## Architecture

Three-Tier Architecture:

Presentation Layer → Business Logic Layer → Data Layer

**Presentation Layer**  
Menu, StudentView, ProgramView, AdmissionView

**Business Logic Layer**  
Student, Program, Subject, AdmissionManager,
MeritCalculator, RegistrationManager, FeeCalculator

**Data Layer**  
StudentDL, ProgramDL, SubjectDL

## Academic Programs

| Program | Seats |
|---|---|
| BS Computer Engineering | 60 |
| BS Software Engineering | 50 |
| BS Electrical Engineering | 45 |
| BS Mechanical Engineering | 40 |
| BS Mechatronics Engineering | 35 |

## Business Rules

- Program cannot exceed 20 credit hours of subjects
- Student cannot register more than 9 credit hours
- Merit list based on FSC + ECAT marks
- Fee calculated from registered subjects only

## How To Run

1. Clone the repository: https://github.com/asim-raza665/university-admission-system.git

2. Open in Visual Studio Community

3. Build the solution (Ctrl+Shift+B)

4. Run the project (F5)

## OOP Concepts Used

- Classes and Objects
- Encapsulation
- Abstraction
- Association
- Lists and Collections
- Three-Tier Architecture
- UML Class Diagram Design

## Result
- 100/100

## Author

Asim Raza  
Computer Engineering Student  
UET Lahore — Faisalabad Campus  
GitHub: https://github.com/asim-raza665
