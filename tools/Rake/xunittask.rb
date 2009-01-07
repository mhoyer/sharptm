require 'rake'
require 'rake/tasklib'

module Rake

  class XunitTask < TaskLib
    # Name of the main, top level task.  (default is :msbuild)
    attr_accessor :name, :assemblyFile
	EnvXUnitConsoleRunner = "XUNIT_CONSOLE_RUNNER"
    
    VERS_TMPL = { 
		:xunit => '"!xunitConsoleRunner!" "!assemblyFile!"',
	}

    # Create an MSBuild task named msbuild.  Default task name is +msbuild+.
    def initialize(name=:xunit) # :yield: self
      @name = name
	  @assemblyFile = ""
      yield self if block_given?
      define
    end
    
    # Create the tasks defined by this task lib.
    def define 
	  task name do
		fail RuntimeError, "Environment variable '" + EnvXUnitConsoleRunner + "' not specified." if ENV[EnvXUnitConsoleRunner] == nil

		cmd = VERS_TMPL[:xunit].
			sub(/!assemblyFile!/, 
				@assemblyFile.
					gsub(/\\/, '/').
					gsub(/"/, '')).
			sub(/!xunitConsoleRunner!/, 
				ENV[EnvXUnitConsoleRunner].
					gsub(/\\/, '/').
					gsub(/"/, ''))
				
        print "\n\n=====\n===== RAKE: " + @assemblyFile + "\n=====\n\n" + cmd + "\n\n"
		
		sh cmd
      end
      self
    end
  end
end
