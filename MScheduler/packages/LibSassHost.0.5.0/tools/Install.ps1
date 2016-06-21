param($installPath, $toolsPath, $package, $project)

$assemblyDirectoryName = "LibSassHost.Native"
$assemblyFileNames = "LibSassHost.Native-32.dll", "LibSassHost.Native-64.dll"

if ($project.Type -eq "Web Site") {
	$projectDirectoryPath = $project.Properties.Item("FullPath").Value
	$binDirectoryPath = Join-Path $projectDirectoryPath "bin"
	$assemblyDirectoryPath = Join-Path $projectDirectoryPath $assemblyDirectoryName

	if (Test-Path $assemblyDirectoryPath) {
		if (!(Test-Path $binDirectoryPath)) {
			New-Item -ItemType Directory -Force -Path $binDirectoryPath
		}

		Move-Item $assemblyDirectoryPath $binDirectoryPath -Force
	}
}
else {
	$assemblyDirectoryItem = $project.ProjectItems.Item($assemblyDirectoryName)
	$assemblyDirectoryItem.Delete()
}