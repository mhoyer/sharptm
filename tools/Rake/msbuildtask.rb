# source: http://hasno.info/2008/1/6/building-net-projects-with-rake

require 'rake'
require 'rake/tasklib'

module Rake

  class MsbuildTask < TaskLib
    # Name of the main, top level task.  (default is :msbuild)
    attr_accessor :name, :solutions, :config, :tool, :config, :fwVersion
    attr_accessor :cleanbefore
	EnvMsBuild = "MSBUILD"

    VERS_TMPL = { 
		:msbuild => '"!msbuild!" /nologo /v:m /t:!target! /p:Configuration=!config!;TargetFrameworkVersion=!FrameWorkVersion! "!solution!"'
	}

    # Create an MSBuild task named msbuild.  Default task name is +msbuild+.
    def initialize(name=:msbuild, target="Build", configuration="Debug") # :yield: self
      @name = name
      @tool = :msbuild
      @config = configuration
	  @target = target
	  @fwVersion = "v2.0"
      yield self if block_given?
      define
    end
    
    # Create the tasks defined by this task lib.
    def define
	  task name do
		fail RuntimeError, "Environment variable '" + EnvMsBuild + "' not specified." if ENV[EnvMsBuild] == nil

		cmd = VERS_TMPL[@tool].
			gsub("!msbuild!", 
				ENV[EnvMsBuild].
					gsub(/\\/, '/').
					gsub("\"", "")).
			gsub("!config!", @config)

        targets = []
		targets << @target

        solutions.each do |solution|
          targets.each do |target| 
			replacedCmd = cmd.gsub("!target!", target).gsub( "!solution!", solution.gsub(/\\/, '/').gsub(/\"/, '')).gsub("!FrameWorkVersion!", fwVersion)
			print "\n\n=====\n===== RAKE: " + target + " " + solution + "\n=====\n\n" + replacedCmd + "\n\n"
			sh replacedCmd
		  end
        end
      end
      self
    end
  end
end
