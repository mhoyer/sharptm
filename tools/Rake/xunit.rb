require 'rake'
require 'rake/tasklib'

module Rake
	class XUnit < TaskLib
		attr_accessor :name, :assembly, :toolpath
		
		def initialize(name=:xunit)
			@name = name
			yield self if block_given?
			define
		end
		
		def define
			raise 'A test assembly is required to run XUnit' if @assembly.nil?
						
			Dir.glob(@assembly).each do |a|
				desc "Runs XUnit on #{File.basename(a)}"
				task @name do
					sh "#{@toolpath.escape} #{a.escape}" # /html #{File.basename(a)}.html"
				end
			end
			
			self
		end
	end
end