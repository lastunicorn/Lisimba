; -- Lisimba --

[Setup]
#define AppVersion "1.5"
#define BinDir "..\sources\Lisimba\bin\Release"
AppName=Lisimba
AppVersion={#AppVersion}
AppPublisher=Dust in the Wind
AppCopyright=Copyright (C) 2003-2015 Dust in the Wind
AppComments=Hello.
DefaultDirName={pf}\Lisimba
DefaultGroupName=Lismba
UninstallDisplayIcon={app}\Lisimba.exe
Compression=lzma2
SolidCompression=yes
OutputBaseFilename=Lisimba {#AppVersion}


[Files]
Source: "{#BinDir}\Lisimba.exe"; DestDir: "{app}"
Source: "{#BinDir}\Lisimba.exe.config"; DestDir: "{app}"
Source: "{#BinDir}\Microsoft.Practices.Unity.dll"; DestDir: "{app}"
Source: "{#BinDir}\Egg.dll"; DestDir: "{app}"
Source: "{#BinDir}\Lisimba.ZipXmlGate.dll"; DestDir: "{app}"
Source: "{#BinDir}\ICSharpCode.SharpZipLib.dll"; DestDir: "{app}"
Source: "{#BinDir}\Lisimba.ZodiacSigns.dll"; DestDir: "{app}"

[Icons]
Name: "{group}\Lisimba"; Filename: "{app}\Lisimba.exe"
Name: "{group}\Uninstall"; Filename: "{uninstallexe}"
