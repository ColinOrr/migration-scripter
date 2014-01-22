require 'rake'
require 'albacore'

task :default => :build

#
# Constants

NUGET = '.nuget\NuGet.exe'

#
# Build Tasks

desc 'Builds the project to the Build\bin folder (only if changes have been made)'
msbuild :build do |msb|
    msb.targets :Build
    msb.properties = { :Configuration => 'Scripted' }
    msb.solution = 'MigrationScripter.sln'
end

#
# Packaging Tasks

desc 'Builds a NuGet package in the Build folder'
task :package => :build do

	 # Clear any existing packaged files
    folder = 'Build\package'
    sh "rmdir /q /s #{folder}" if Dir.exists? folder
    
    # Package the DLLs
    sh "xcopy /y /s /exclude:.pakignore Build\\bin\\* #{folder}\\tools\\"

    # Build the NuGet package
    sh "copy Build\\bin\\MigrationScripter.nuspec #{folder}"
    sh "#{NUGET} pack #{folder}\\MigrationScripter.nuspec -OutputDirectory Build"

end