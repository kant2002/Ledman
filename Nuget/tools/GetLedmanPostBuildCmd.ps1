$solutionDir = [System.IO.Path]::GetDirectoryName($dte.Solution.FullName) + "\"
$path = $installPath.Replace($solutionDir, "`$(SolutionDir)")

$NativeAssembliesDir = Join-Path $path "runtimes\win7"

$LedmanPostBuildCmd = "
xcopy /s /y `"$NativeAssembliesDir`" `"`$(TargetDir)`"
"
