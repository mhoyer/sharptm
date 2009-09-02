require 'rake/clean'
require 'configatron'
Dir.glob(File.join(File.dirname(__FILE__), 'tools/Rake/*.rb')).each do |f|
	require f
end

task :default => [:clobber, 'compile:all', 'tests:run', :package]

desc 'Runs a quick build just compiling the libs that are not up to date'
task :quick do
	CLOBBER.clear
	
	class MSBuild
		class << self
			alias do_compile compile
		end

		def self.compile(attributes)
			artifacts = artifacts_of attributes[:project]
			do_compile attributes unless uptodate? artifacts, FileList.new("#{attributes[:project].dirname}/**/*.*")
		end
		
		def self.artifacts_of(project)
			FileList.new() \
				.include("#{configatron.dir.build}/**/#{project.dirname.name}.dll") \
				.include("#{configatron.dir.build}/**/#{project.dirname.name}.exe")
		end
		
		def self.uptodate?(new_list, old_list)
			return false if new_list.empty?
			
			new_list.each do |new|
				return false unless FileUtils.uptodate? new, old_list
			end
			
			return true
		end
	end
end

namespace :env do
	desc 'Switches the configuration to the development environment'
	task :development do
		configure_env_for 'development'
	end
	
	desc 'Switches the configuration to the test environment'
	task :test do
		configure_env_for 'test'
	end

	desc 'Switches the configuration to the production environment'
	task :production do
		configure_env_for 'production'
	end
	
	def configure_env_for(env_key)
		env_key = env_key || 'development'

		puts "Loading settings for the '#{env_key}' environment"
		configatron.configure_from_yaml 'properties.yml', :hash => 'development'
		configatron.configure_from_yaml 'properties.yml', :hash => env_key if env_key != 'development'

		if File.exists? 'local-properties.yml'
			puts "Loading local settings from 'local-properties.yml'"
			configatron.configure_from_yaml 'local-properties.yml'
		end
		
		configatron.build.number = ENV['BUILD_NUMBER']
		# configatron.database.connectionstring = "Data Source=#{configatron.database.server}; Initial Catalog=#{configatron.database.name}; #{'Integrated Security=true; ' if configatron.database.sspi} Persist Security Info=False;"
		configatron.deployment.package = "#{configatron.project}-#{configatron.build.number || '1.0.0.0'}.zip".in(configatron.dir.deploy)

		CLEAN.clear
		CLEAN.include('teamcity-info.xml')
		CLEAN.include('**/obj'.in(configatron.dir.source))
		CLEAN.include('**/*'.in(configatron.dir.test_results))
				
		CLOBBER.clear
		CLOBBER.include(configatron.dir.build)
		CLOBBER.include(configatron.dir.deploy)
		CLOBBER.include('**/bin'.in(configatron.dir.source))
		CLOBBER.include('**/*.template'.in(configatron.dir.source))
		# Clean template results.
		CLOBBER.map! do |f|
			next f.ext() if f.pathmap('%x') == '.template'
			f
		end
		
		#configatron.protect_all!

		puts configatron.inspect
	end

	# Load the default environment configuration if no environment is passed on the command line.
	Rake::Task['env:development'].invoke \
		if not Rake.application.options.show_tasks and
		   not Rake.application.options.show_prereqs and
		   not Rake.application.top_level_tasks.any? do |t|
			/^env:/.match(t)
		end
end
	
namespace :framework do
	
	desc 'Switches the framework to Silverlight 2.0'
	task :silverlight do
		configure_framework_for 'Silverlight'
	end

	desc 'Switches the framework to Mono'
	task :mono do
		configure_framework_for 'Mono'
	end

	def configure_framework_for(framework_key)
		configatron.build.framework = framework_key || ''
	end
end

namespace :generate do
	desc 'Updates the version information for the build'
	task :version do
		next if configatron.build.number.nil?

    shared_asm_info_file = 'SharedAssemblyInfo.cs'.in(configatron.dir.source)
    if not File.exist? shared_asm_info_file
      puts "Skipping shared AssemblyInfo file modification: #{shared_asm_info_file} not found."
      next
    end
		
		AssemblyInfoBuilder.new(configatron.build.number, shared_asm_info_file).patch
	end

	desc 'Updates the configuration files for the build'
	task :config do
		FileList.new("#{configatron.dir.source}/**/*.template").each do |template|
			QuickTemplate.new(template).exec(configatron)
		end
	end
	
	desc 'Creates framework spcific project files'
	task :projFiles do
    next if configatron.build.framework.nil?

    csproj = FileList.new("#{configatron.dir.source}/**/*.csproj").exclude(/.*\.g\..*/i)
    csproj.each do |csprojFile|
      CSProjModifier.new(csprojFile).create(configatron.build.framework)
    end
	end

  desc 'Creates framework spcific project files'
	task :slnFiles do
		next if configatron.build.framework.nil?

    sln = FileList.new("#{configatron.dir.source}/**/*.sln").exclude(/.*\.g\..*/i)
    sln.each do |slnFile|
      SlnModifier.new(slnFile).create(configatron.build.framework)
    end
	end

  task :all => ['generate:version', 'generate:config', 'generate:projFiles', 'generate:slnFiles']
end

namespace :compile do

	desc 'Compiles the application'
	task :app => [:clobber, 'generate:all'] do
	    configatron.build.framework.nil? ?
	      filelist = FileList.new("#{configatron.dir.app}/**/*.csproj").exclude(/.*\.g\..*/i) :
	      filelist = FileList.new("#{configatron.dir.app}/**/*#{configatron.build.framework}.g.csproj")

		filelist.each do |project|
			MSBuild.compile \
				:project => project,
				:properties => {
					:SolutionDir => configatron.dir.source.to_absolute.chomp('/').concat('/').escape,
					:Configuration => configatron.build.configuration,
					:TreatWarningsAsErrors => true,
					:Framework => configatron.build.framework
				}
		end
	end

	desc 'Compiles the tests'
	task :tests => [:clobber, 'generate:all'] do
    configatron.build.framework.nil? ?
      filelist = FileList.new("#{configatron.dir.test}/**/*.csproj").exclude(/.*\.g\..*/i) :
      filelist = FileList.new("#{configatron.dir.test}/**/*#{configatron.build.framework}.g.csproj")

    filelist.each do |project|
			MSBuild.compile \
				:project => project,
				:properties => {
					:SolutionDir => configatron.dir.source.to_absolute.chomp('/').concat('/').escape,
					:Configuration => configatron.build.configuration
				}
		end
	end
	
	task :all => [:app, :tests]
end

namespace :tests do
	desc 'Runs unit tests'
	task :run => ['compile:tests'] do
		FileList.new("#{configatron.dir.build}/#{configatron.build.configuration}.test/**/*.Tests.dll").each do |assembly|
			# Mspec.run \
				# :tool => configatron.tools.mspec,
				# :reportdirectory => configatron.dir.test_results,
				# :assembly => assembly
			Xunit.run \
				:tool => configatron.tools.xunit,
				:reportdirectory => configatron.dir.test_results,
				:assembly => assembly
		end
	end
	
	desc 'Runs CLOC to create some source code statistics'
	task :cloc do
		results = Cloc.count_loc \
			:tool => configatron.tools.cloc,
			:report_file => 'cloc.xml'.in(configatron.dir.test_results),
			:search_dir => configatron.dir.source,
			:statistics => {
				:'LOC.CS' => '/results/languages/language[@name=\'C#\']/@code',
				:'Files.CS' => '/results/languages/language[@name=\'C#\']/@files_count',
				:'LOC.Total' => '/results/languages/total/@code',
				:'Files.Total' => '/results/languages/total/@sum_files'
			} do |key, value|
				TeamCity.add_statistic key, value
			end
		
		TeamCity.append_build_status_text "#{results[:'LOC.CS']} LOC in #{results[:'Files.CS']} C# Files"
	end
	
	desc 'Runs NCover code coverage'
	task :ncover => ['compile:tests'] do
		applicationAssemblies = FileList.new() \
			.include("#{configatron.dir.build}/#{configatron.build.configuration}.test/**/#{configatron.project}*.dll") \
			.include("#{configatron.dir.build}/#{configatron.build.configuration}.test/**/#{configatron.project}*.exe") \
			.exclude(/(Tests\.dll$)|(ForTesting\.dll$)/) \
			.exclude(/\.exe$/) \
			.pathmap('%n') \
			.join(';')
			
		FileList.new("#{configatron.dir.build}/#{configatron.build.configuration}.test/**/*.Tests.dll").each do |assembly|
			NCover.run_coverage \
				:tool => configatron.tools.ncover,
				:report_dir => configatron.dir.test_results,
				:working_dir => assembly.dirname,
				:application_assemblies => applicationAssemblies,
				:program => configatron.tools.mspec,
				:assembly => assembly.to_absolute.escape,
				:args => ["#{('--teamcity ' if ENV['TEAMCITY_PROJECT_NAME']) || ''}"]
		end
		
		NCover.explore \
			:tool => configatron.tools.ncoverexplorer,
			:project => configatron.project,
			:report_dir => configatron.dir.test_results,
			:html_report => 'Coverage.html',
			:xml_report => 'Coverage.xml',
			:min_coverage => 70,
			:fail_if_under_min_coverage => true,
			:statistics => {
				:NCoverCodeCoverage => "/coverageReport/project/@functionCoverage"
			} do |key, value|
				TeamCity.add_statistic key, value
				TeamCity.append_build_status_text "Code coverage: #{Float(value.to_s).round}%"
			end
	end
	
	desc 'Runs FxCop to analyze assemblies for compliance with the coding guidelines'
	task :fxcop => [:clean, 'compile:app'] do
		results = FxCop.analyze \
			:tool => configatron.tools.fxcop,
			:project => 'Settings.FxCop'.in(configatron.dir.source),
			:report => 'FxCop.html'.in(configatron.dir.test_results),
			:apply_report_xsl => true,
			:report_xsl => 'CustomFxCopReport.xsl'.in("#{configatron.tools.fxcop.dirname}/Xml"),
			:console_output => true,
			:console_xsl => 'FxCopRichConsoleOutput.xsl'.in("#{configatron.tools.fxcop.dirname}/Xml"),
			:show_summary => true,
			:fail_on_error => false,
			:assemblies => FileList.new() \
				.include("#{configatron.dir.build}/#{configatron.build.configuration}/**/#{configatron.project}*.dll") \
				.exclude('**/*.vshost') \
			do |violations|
				TeamCity.append_build_status_text "#{violations} FxCop violation(s)"
				TeamCity.add_statistic 'FxCopViolations', violations
			end	
	end
	
	desc 'Runs StyleCop to analyze C# source code for compliance with the coding guidelines'
	task :stylecop do
		results = StyleCop.analyze \
			:tool => configatron.tools.stylecop,
			:directories => configatron.dir.app,
			:ignore_file_pattern => ['(?:Version|Solution|Assembly|FxCop)Info\.cs$', '\.Designer\.cs$', '\.hbm\.cs$', 'QueryBuilder\.cs$'],
			:settings_file => 'Settings.StyleCop'.in(configatron.dir.source),
			:report => 'StyleCop.xml'.in(configatron.dir.test_results),
			:report_xsl => 'StyleCopReport.xsl'.in(configatron.tools.stylecop.dirname) \
			do |violations|
				TeamCity.append_build_status_text "#{violations} StyleCop violation(s)"
				TeamCity.add_statistic 'StyleCopViolations', violations
			end
	end
	
	desc 'Run all code quality-related tasks'
	task :quality => [:ncover, :cloc, :fxcop, :stylecop]
end

desc 'Packages the build artifacts'
task :package => 'compile:app' do
	sz = SevenZip.new({ :tool => configatron.tools.zip,
				:zip_name => configatron.deployment.package })
	
	Dir.chdir("#{configatron.dir.build}/#{configatron.build.configuration}") do
		sz.zip :files => FileList.new() \
					.include("**/*.dll") \
					.include("**/*.pdb") \
					.include("**/*.config") \
					.include("**/*.boo") \
					.exclude("obj")
	end
end

desc 'Deploys the build artifacts to the QA system'
task :deploy => :package do
	FileUtils.rm_rf configatron.deployment.location
	
	SevenZip.unzip \
		:tool => configatron.tools.zip,
		:zip_name => configatron.deployment.package,
		:destination => configatron.deployment.location
end
