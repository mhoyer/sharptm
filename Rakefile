# build.cmd <(build)|test|deploy> <msBuildTarget=[(build)|clean|rebuild|*]> <msBuildConfiguration=[(Debug)|Release|*]>

require 'tools\rake\msbuildtask'
require 'tools\rake\xunittask'

msBuildTarget = "build"
msBuildConfiguration = "Debug"

$*.each do |argument|
	msBuildTarget = argument.sub(/msBuildTarget=/, '') if argument.index("msBuildTarget=") == 0
	msBuildConfiguration = argument.sub(/msBuildConfiguration=/, '') if argument.index("msBuildConfiguration=") == 0
end

puts "msBuildTarget: " + msBuildTarget
puts "msBuildConfiguration: " + msBuildConfiguration

# Dependencies
task :build => :default
task :default => ["msbuild:sharpTM"]
task :test => ["xunit:runTmapiTest","xunit:runSharpTmTest"]

# Compile tasks
namespace :msbuild do

	task :sharpTM => :tmapi
	Rake::MsbuildTask.new(:sharpTM, msBuildTarget, msBuildConfiguration) do |msbld|
		msbld.solutions = FileList['source/SharpTM/app/SharpTM/SharpTM.csproj']
	end

	Rake::MsbuildTask.new(:tmapi, msBuildTarget, msBuildConfiguration) do |msbld|
		msbld.solutions = FileList['source/SharpTM/app/TMAPI.Net/TMAPI.Net.csproj']
	end

	task :tmapiTest => [:tmapi,:sharpTM]
	Rake::MsbuildTask.new(:tmapiTest, msBuildTarget, msBuildConfiguration) do |msbld|
		msbld.solutions = FileList['source/SharpTM/test/TMAPI.Net.Tests/TMAPI.Net.Tests.csproj']
	end

	task :sharpTmTest => [:tmapi,:sharpTM,:tmapiTest]
	Rake::MsbuildTask.new(:sharpTmTest, msBuildTarget, msBuildConfiguration) do |msbld|
		msbld.solutions = FileList['source/SharpTM/test/SharpTM.Tests/SharpTM.Tests.csproj']
	end
end

namespace :xunit do
	
	task :runTmapiTest => ["msbuild:tmapiTest"]
	Rake::XunitTask.new(:runTmapiTest) do |xunit|
		xunit.assemblyFile = "source/SharpTM/test/TMAPI.Net.Tests/bin/" + msBuildConfiguration + "/TMAPI.Net.Tests.dll"
	end
	
	task :runSharpTmTest => ["msbuild:sharpTmTest"]
	Rake::XunitTask.new(:runSharpTmTest) do |xunit|
		xunit.assemblyFile = "source/SharpTM/test/SharpTM.Tests/bin/" + msBuildConfiguration + "/SharpTM.Tests.dll"
	end
end

# require 'tools\rake\monotask'
# namespace :mono do
	# Rake::MonoTask.new(:tmapi) do |monotask|
		# monotask.projects = FileList['source/SharpTM/app/TMAPI.Net/TMAPI.Net.csproj']
	# end
# end