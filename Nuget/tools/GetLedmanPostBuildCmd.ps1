$solutionDir = [System.IO.Path]::GetDirectoryName($dte.Solution.FullName) + "\"
$path = $installPath.Replace($solutionDir, "`$(SolutionDir)")

$NativeAssembliesDir = Join-Path $path "NativeBinaries"

$LedmanPostBuildCmd = "
xcopy /s /y `"$NativeAssembliesDir`" `"`$(TargetDir)`"
"
