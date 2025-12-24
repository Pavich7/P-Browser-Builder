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
  BrandingText "MadeByPavich"

; --------------------------------
; Pages
  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "C:\Users\phavi\Desktop\inspbb\eula.txt"
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
  File /r "C:\Users\phavi\Desktop\inspbb\*"
  
  ; Check registry vc runtime
  ReadRegDWORD $R0 HKLM "SOFTWARE\Microsoft\VisualStudio\14.0\VC\Runtimes\x64" "Installed"
  StrCmp $R0 1 skip_vcredist

  ; Not installed > download and install
  inetc::get /popup /caption "Downloading VC++ Redistributable..." \
    "https://aka.ms/vc14/vc_redist.x64.exe" \
    "$TEMP\vc_redist.x64.exe"
  Pop $0

  ExecWait '"$TEMP\vc_redist.x64.exe" /quiet /norestart'

  skip_vcredist:

  ; Store installation folder
  WriteRegStr HKCU "Software\P Browser Builder" "" $INSTDIR
  
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
