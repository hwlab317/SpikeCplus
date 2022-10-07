B1530A Instrument Library A.03.00


NOTE:	Before installing the B1530A Instrument Library A.03.00 or later,
	uninstall the revision A.02.10 or earlier and don't install them
	after installing A.03.00 or later.

Contents:

	B1530A Instrument Library deploys

		32-bit binary and files for 32-bit environment and
		32-bit and 64-bit binary and files for 64-bit environment

	as shown below.
 
	Inrder to build a native application with B1530A Instrument Library,
	specify proper include path and library path acording to the build
	environment and building application (i.e. 32-bit or 64-bit).

32-bit Environment:

	wgfmu.dll:

		C:\Windows\System32				(for 32-bit)

	wgfmu.lib:

		C:\Program Files\Agilent\B1530A\lib		(for 32-bit)

	wgfmu.h:

		C:\Program Files\Agilent\B1530A\include		(for 32-bit)


64-bit Environment:

	wgfmu.dll:

		C:\Windows\System32				(for 64-bit)
		C:\Windows\SysWOW64				(for 32-bit)

	wgfmu.lib:

		C:\Program Files\Agilent\B1530A\Lib_x64		(for 64-bit)
		C:\Program Files (x86)\Agilent\B1530A\lib	(for 32-bit)

	wgfmu.h:

		C:\Program Files\Agilent\B1530A\include		(for 64-bit)
		C:\Program Files (x86)\Agilent\B1530A\include	(for 32-bit)
