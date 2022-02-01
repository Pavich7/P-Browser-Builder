; #######################################################################################
; # This NSIS script was generated by Visual & Installer for MS Visual Studio           #
; # Visual & Installer (c) 2012 - 2021 unSigned, s. r. o. All Rights Reserved.          #
; # Visit http://www.visual-installer.com/ for more details.                            #
; #######################################################################################

; NSIS Modern User Interface

; Include Modern UI
!include "MUI2.nsh"

; General

  ; Name and output file
  Name "P Browser"
  OutFile "Output\P Browser Installer.exe"

  ; Default installation folder
  InstallDir "$LOCALAPPDATA\P Browser"
  
  ; Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\P Browser" ""

  ; Request application privileges for Windows Vista/7/8/10
  RequestExecutionLevel admin

; --------------------------------
; Interface Settings

  !define MUI_ABORTWARNING
  !define MUI_FINISHPAGE_RUN launchpb.exe

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
  File /r "D:\Project\P Browser 2\InsIncludeFiles\*"
  
  ; Store installation folder
  WriteRegStr HKCU "Software\P Browser" "" $INSTDIR
  
  ; Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

SectionEnd

; --------------------------------
; Descriptions

  ; Language strings
  LangString DESC_SecDummy ${LANG_ENGLISH} "A P_Browser_Installer section."

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

  DeleteRegKey /ifempty HKCU "Software\P Browser"

SectionEnd
