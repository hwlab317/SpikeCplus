===============================================
README.TXT

Sample program for the B1530A

B1530A Waveform Generator / Fast Measurement Unit

===============================================
This document contains the following:

* Introduction
* System Requirements
* Installation
* Other Documentations
* Revision History

Introduction
=================
The sample software for the B1530A WGFMU is the program to demonstrate a basic performance of the B1530A and a used as a sample code to demonstrate the usage of the instrument library of the B1530A.
For this purpose, this sample program includes source codes in addition to the executable program. This sample program is written in the Microsoft Visual C# 2005, so to see the source code of it, the Microsoft Visual C# 2005(or later) Express Edition is necessary.
The sample program of the B1530A includes following applications,

1. NBTI Test
* Supporting DC and AC stress
* Supporting various measurement, OTF, fast Id measurement, staircase sweep measurement, ramp sweep measurement and pulsed IV measurement.
* Supporting single sweep and dual sweep for sweep measurement.
* Iterative measurement after stress to evaluate a recovery effect.

2. Sampling Measurement
* Supporting multi measurement condition.
* Supporting linear and log sampling mode.

3. Sweep Measurement
* Supporting various type of sweep, staircase sweep measurement, ramp sweep measurement and pulsed IV measurement.
* Supporting single sweep and dual sweep.

4. Pulsed Sampling Measurement
* Sampling measurement with a pulsed bias.
* Supporting variable measurement condition.

5. Data Sampler
* Capturing voltage or current with a fixed sampling rate.
* The data captured by this program is used as a data source of the RTS analysis tool.

Each applications is provided as an individual program.
All application uses the two of output channels of the WGFMU to force waveform and measure voltage or current. Also another two of the WGFMU or SMU can be used as a constant bias source.

 System Requirements
=================
To use the sample program of the B1530A, Keysight B1530A instrument library and Keysight VISA library are necessary.

The required environment to run the sample program of the B1530A are,

HW:
* Keysight B1500A Semiconductor Device Analyzer with a following options
  - B1530A WGFMU x1


*Windows PC to control the B1500A
  - Windows XP, Windows Vista or Windows 7
  - Microsoft .Net Framework 2.0
  - Keysight I/O Library Suites 16.1 or later (including Keysight VISA library)
  - Keysight B1530A Instrument Library (Rev A.02.00.2011.0815)

 Optional:
 One of the following development environment is required to view or edit the source codes of the sample programs.
  - Microsoft Visual Studio 2005
  - Microsoft Visual Studio 2008
  - Microsoft Visual Studio 2010
  - Microsoft Visual C# 2005 Express Edition
  - Microsoft Visual C# 2008 Express Edition
  - Microsoft Visual C# 2010 Express Edition

* Windows, Windows XP, Windows Vista, Windows 7 are either a registered trademark or trademark of Microsoft Corporation in the United States and/or other countries

*GPIB I/F
  - Keysight 82350B PCI GPIB I/F
  - GPIB cable to connect the B1500A and the PC


Installation
=================
Copy the "SamplePrograms" directory any place you want.

The executable of the each application are included under the following directories.

* Data Sampler
\SamplePrograms\bin\DATASAMPLER\DATASAMPLER.exe

* NBTI
\SamplePrograms\bin\NBTI\NBTI.exe

* Sampling measurement
\SamplePrograms\bin\SAMPLING\SAMPLING.exe

* Sweep measurement
\SamplePrograms\bin\SWEEP\SWEEP.exe

* Pulsed sampling measurement
\SamplePrograms\bin\PULSE\PULSE.exe

* RTS Analysis Tool
\SamplePrograms\bin\RTSDataAnalysis\RTSDataAnalysis.exe

The source code (solution project) of the each application are included under the following directories.

* Data Sampler
\SamplePrograms\src\DATASAMPLER\DATASAMPLER.sln

* NBTI
\SamplePrograms\src\NBTI\NBTI.sln

* Sampling measurement
\SamplePrograms\src\SAMPLING\SAMPLING.sln

* Sweep measurement
\SamplePrograms\src\SWEEP\SWEEP.sln

* Pulsed sampling measurement
\SamplePrograms\src\PULSE\PULSE.sln

* Common functions library
\SamplePrograms\src\WGFMU_SAMPLE_Lib\WGFMU_SAMPLE_Lib.sln

* RTS Analysis Tool
\SamplePrograms\src\RTSDataAnalysis\RTSDataAnalysis.sln

Other Documentations
=================
As a reference of the each application program, the following document is provided.

P/N B1500-90503 User's manual of the B1500-87011 demo kit of the B1530A 
Å@
Revision History
=================
A.01.00.2008-10-24

=================
A.01.01.2009.01.07

New Features:
- In the pulse measurement application, PULSE.exe, the different baseline voltage can be set before or after pulse.
- "Save" dialog stays previous directory after executing "Validate Pattern", "Execute", "Save Setup" or "Load Setup".

Fixed Defects:

[ID 7396] WGFMU data recorder application does not save entire data.

[ID 7409] B1530A NBTI Sample Program, error occurs when 100 kHz AC stress is used with a stress duration over 40,000 sec.

[ID 7474] B1530A Data Sampler Applciation, dose not work at 200 M/s sampling rate.

[ID 7483] B1530A NBTI Application, error occurs when OTF measurement is selected.

[ID 7484] B1530A NBTI Sample Program, measurement timing is vaiolated when the Range Change Hold time is set not as 0s for the OTF measurement.

[ID 7485] B1530A NBTI Sample Program, measurement is not executed when the step delay time of sweep measurement is set as 0 s.

[ID 7487 ] B1530A Pulse Sample Program, fall time is set as a same value of the rise time.

=================
A.02.00.2011.0815

New Features:
- Windows 7 support is implemented.
