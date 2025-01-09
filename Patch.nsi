!include "MUI2.nsh"

; General

  ; Name and output file
  Name "Update P Browser Builder"
  OutFile "Output\Update P Browser Builder.exe"

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
  BrandingText "MadeByPavich"

; --------------------------------
; Pages
  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "C:\Users\Pavich Komansil\Desktop\updpbb\eula.txt"
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
  
  ; Check version.txt
  FileOpen $0 "$INSTDIR\metadata\version.txt" r
  FileRead $0 $1
  FileClose $0
  StrCmp $1 "8.1.0" +3
  MessageBox MB_OK "Error! Patch is not supported in the currently installed version."
  Abort

  ; ADD YOUR OWN FILES HERE...
  File /r "C:\Users\Pavich Komansil\Desktop\updpbb\*"

  ; ExecWait '"$INSTDIR\packages\VC_redist.x64.exe"  /install /quiet /norestart'

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

  Delete "$INSTDIR\*.*"
  RmDir /r "$INSTDIR"
  Delete "$SMPROGRAMS\MadeByPavich\*.*"
  RmDir /r "$SMPROGRAMS\MadeByPavich"

  DeleteRegKey /ifempty HKCU "Software\P Browser Builder"

SectionEnd
