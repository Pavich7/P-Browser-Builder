; TEST ONLY!!!!

!include "MUI2.nsh"

; General

  ; Name and output file
  Name "P Browser Builder (Update)"
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

; --------------------------------
; Pages
  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "C:\Users\Pavich Komansil\Desktop\inspbb\eula.txt"
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
  ReadEnvStr $0 $INSTDIR\metadata\version.txt
  StrCmp $0 "8.1.0" +2
  MessageBox MB_OK "Error! Patching not supported in current installed version."
  Abort

  ; ADD YOUR OWN FILES HERE...
  File /r "C:\Users\Pavich Komansil\Desktop\inspbb\*"
  ExecWait '"$INSTDIR\packages\VC_redist.x64.exe"  /install /quiet /norestart'
  
  ; Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall P Browser Builder.exe"

  CreateDirectory "$SMPROGRAMS\MadeByPavich"
  CreateShortCut "$SMPROGRAMS\MadeByPavich\P Browser Builder.lnk" "$INSTDIR\P Browser Builder.exe"
  CreateShortCut "$SMPROGRAMS\MadeByPavich\Uninstall P Browser Builder.lnk" "$INSTDIR\Uninstall P Browser Builder.exe"

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
