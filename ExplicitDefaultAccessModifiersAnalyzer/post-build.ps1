try {
	$version = (gi ExplicitDefaultAccessModifiersAnalyzer.dll).VersionInfo.ProductVersion
	echo "Building package with version $version."
	& '..\..\..\packages\NuGet.CommandLine.2.8.6\tools\NuGet.exe' pack ExplicitDefaultAccessModifiersAnalyzer.nuspec -Version $version -OutputDirectory .
}
catch {
	exit 1
}