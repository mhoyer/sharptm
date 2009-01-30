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
task :test => ["xunit:runTmapiTest","xunit:runSharpTmTest","xunit:runSharpTmPersistenceTest"]

# Compile tasks
namespace :msbuild do

	# TMAPI + Tests
	Rake::MsbuildTask.new(:tmapi, msBuildTarget, msBuildConfiguration) do |msbld|
		msbld.solutions = FileList['source/SharpTM/app/TMAPI.Net/TMAPI.Net.csproj']
	end

	task :tmapiTest => [:tmapi,:sharpTM]
	Rake::MsbuildTask.new(:tmapiTest, msBuildTarget, msBuildConfiguration) do |msbld|
		msbld.solutions = FileList['source/SharpTM/test/TMAPI.Net.Tests/TMAPI.Net.Tests.csproj']
	end

	# SharpTM + Tests
	task :sharpTM => :tmapi
	Rake::MsbuildTask.new(:sharpTM, msBuildTarget, msBuildConfiguration) do |msbld|
		msbld.solutions = FileList['source/SharpTM/app/SharpTM/SharpTM.csproj']
	end

	task :sharpTmTest => [:tmapi,:sharpTM]
	Rake::MsbuildTask.new(:sharpTmTest, msBuildTarget, msBuildConfiguration) do |msbld|
		msbld.solutions = FileList['source/SharpTM/test/SharpTM.Tests/SharpTM.Tests.csproj']
	end

	# SharpTM.Persistence + Tests
	task :sharpTmPersistence => :sharpTM
	Rake::MsbuildTask.new(:sharpTmPersistence, msBuildTarget, msBuildConfiguration) do |msbld|
		msbld.solutions = FileList['source/SharpTM/app/SharpTM.Persistence/SharpTM.Persistence.csproj']
	end
	
	task :sharpTmPersistenceTest => [:tmapi,:sharpTM,:sharpTmPersistence]
	Rake::MsbuildTask.new(:sharpTmPersistenceTest, msBuildTarget, msBuildConfiguration) do |msbld|
		msbld.solutions = FileList['source/SharpTM/test/SharpTM.Persistence.Tests/SharpTM.Persistence.Tests.csproj']
		msbld.fwVersion = "v3.5"
	end
end

namespace :xunit do
	
	task :runTmapiTest => ["msbuild:tmapiTest"]
	Rake::XunitTask.new(:runTmapiTest) do |xunit|
		xunit.assemblyFile = "source/SharpTM/test/TMAPI.Net.Tests/bin/" + msBuildConfiguration + "/TMAPI.Net.Tests.dll"
	end
	
	task :runSharpTmTest => ["msbuild:sharpTmTest"]
	Rake::XunitTask.new(:runSharpTmTest) do |xunit|
		xunit.assemblyFile = "source/SharpTM/test/SharpTM.Tests/bin/" + msBuildConfiguration + "/Pixelplastic.TopicMaps.SharpTM.Tests.dll"
	end
	
	task :runSharpTmPersistenceTest => ["msbuild:sharpTmPersistenceTest"]
	Rake::XunitTask.new(:runSharpTmPersistenceTest) do |xunit|
		xunit.assemblyFile = "source/SharpTM/test/SharpTM.Persistence.Tests/bin/" + msBuildConfiguration + "/Pixelplastic.TopicMaps.SharpTM.Persistence.Tests.dll"
	end
end

# require 'tools\rake\monotask'
# namespace :mono do
	# Rake::MonoTask.new(:tmapi) do |monotask|
		# monotask.projects = FileList['source/SharpTM/app/TMAPI.Net/TMAPI.Net.csproj']
	# end
# end