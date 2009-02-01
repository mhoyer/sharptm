require 'rake'
require 'rake/tasklib'

# TODO: Detect Framework locations in the registry.
module NetFramework
	Net2_0 = 2
	Net3_5 = 3.5
end

module Rake
	class MsBuild < TaskLib
		attr_accessor :name, :project, :targets, :configuration, :out_dir, :verbosity, :dependsUpon, :frameworkVersion
		
		def initialize(name=:msbuild)
			@name = name
			@verbosity = 'minimal'
			@configuration = 'Debug'
			@targets = ['Build']
			@dependsUpon = []
			@frameworkVersion = '3.5'
			yield self if block_given?
			define
		end
		
		def define
			raise 'A project is required to run MSBuild' if @project.nil? or Dir.glob(@project).length == 0
			
			# OutDir requires a trailing path separator.
			@out_dir = File.join(@out_dir, File::SEPARATOR).escape unless @out_dir.nil?
			
			Dir.glob(@project).each do |p|
				desc "Runs MSBuild on #{File.basename(p)}"
				task @name => @dependsUpon do
					MsBuildRunner.new(p, @targets, @verbosity,
						{
							:Configuration => @configuration,
							:OutDir => @out_dir,
							:OutputPath => @out_dir,
							#:TargetFrameworkVersion => @frameworkVersion
						}).run
				end
			end
			
			self
		end
	end
end

class MsBuildRunner
	def initialize(project, targets = [], verbosity = 'normal', properties = {})
		@project = project
		@properties = properties
		@targets = targets
		@verbosity = verbosity
		@working_dir = nil
	end
	
	def run
		if @working_dir
			chdir(@working_dir) do
				sh cmd
			end
		else
			sh cmd
		end
	end
	
	private
	def cmd
		"#{exe} /nologo /maxcpucount #{properties} #{verbosity} #{targets} #{@project.escape}"
	end
	
	def exe
		File.join(ENV['systemroot'].dup, 'Microsoft.NET', 'Framework', "v#{@properties.fetch(:TargetFrameworkVersion, '3.5')}", 'msbuild.exe').escape
	end
	
	def verbosity
		"/verbosity:#{@verbosity}"
	end
	
	def targets
		"/target:#{@targets.join(';')}"
	end
	
	def properties
		p = ['BuildInParallel=true']
		@properties.each { |key, value| p.push("#{key}=#{value}") unless value.nil? }
		"/property:#{p.join(';')}" 
	end
end