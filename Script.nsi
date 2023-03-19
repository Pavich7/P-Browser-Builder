; NSIS Modern User Interface

; Include Modern UI
!include "MUI2.nsh"

; General

  ; Name and output file
  Name "P Browser Builder"
  OutFile "Output\Install P Browser Builder.exe"

  ; Default installation folder
  InstallDir "$LOCALAPPDATA\P Browser Builder"
  
  ; Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\P Browser Builder" ""

  ; Request application privileges for Windows Vista/7/8/10
  RequestExecutionLevel admin

; --------------------------------
; Interface Settings

  !define MUI_ABORTWARNING
  !define MUI_FINISHPAGE_RUN "P Browser Builder.exe"

; --------------------------------
; Pages
  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  !insertmacro MUI_PAGE_FINISH

  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  
; --------------------------------
; Languages
 
  !insertmacro MUI_LANGUAGE "English"

; --------------------------------
; Installer Sections

Section "P Browser Installer" SecDummy

  SetOutPath "$INSTDIR"
  
  ; ADD YOUR OWN FILES HERE...
  File /r "C:\Users\Pavich Komansil\Desktop\pbb\*"
  ExecWait '"$INSTDIR\packages\VC_redist.x64.exe"  /passive /norestart'

  ; Store installation folder
  WriteRegStr HKCU "Software\P Browser Builder" "" $INSTDIR
  
  ; Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall P Browser Builder.exe"

SectionEnd

; --------------------------------
; Descriptions

  ; Language strings
  LangString DESC_SecDummy ${LANG_ENGLISH} "A P Browser Builder Installer section."

  ; Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END

; --------------------------------
; Uninstaller Section

Section "Uninstall"

  ; ADD YOUR OWN FILES HERE...
  Delete "$INSTDIR\*.*"
  RmDir /r "$INSTDIR"

  DeleteRegKey /ifempty HKCU "Software\P Browser Builder"

SectionEnd
