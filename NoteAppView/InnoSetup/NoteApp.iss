; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "�������"
#define MyAppVersion "1.0"
#define MyAppPublisher "Lenko Studio, Inc"
#define MyAppURL "http://vk.com/"
#define MyAppExeName "NoteAppView.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{F2EFFB7A-4073-4544-BF1E-8C7DC81D4FD4}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DisableProgramGroupPage=yes
OutputBaseFilename=setup
SetupIconFile=C:\Users\User\source\repos\NoteApp\NoteAppView\NoteAppView\Properties\icon.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\User\source\repos\NoteApp\NoteAppView\InnoSetup\Release\NoteAppView.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\User\source\repos\NoteApp\NoteAppView\InnoSetup\Release\MaterialDesignColors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\User\source\repos\NoteApp\NoteAppView\InnoSetup\Release\MaterialDesignThemes.Wpf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\User\source\repos\NoteApp\NoteAppView\InnoSetup\Release\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\User\source\repos\NoteApp\NoteAppView\InnoSetup\Release\NoteAppModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\User\source\repos\NoteApp\NoteAppView\InnoSetup\Release\Realm.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\User\source\repos\NoteApp\NoteAppView\InnoSetup\Release\Remotion.Linq.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\User\source\repos\NoteApp\NoteAppView\InnoSetup\Release\System.Reflection.TypeExtensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\User\source\repos\NoteApp\NoteAppView\InnoSetup\Release\System.Runtime.CompilerServices.Unsafe.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

