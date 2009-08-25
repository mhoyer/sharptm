require 'rake'

class SlnModifier
	attr_reader :source
	
	def initialize(sourceFileName)
		@source = sourceFileName
	end

	def create(framework)
    target = File.new((@source.gsub /sln$/, "#{framework}.g.sln"), "w")
    #csproj_fix = @source.gsub /sln$/, "#{framework}.rb"

		source = File.new(@source)
		source.each_line do |line|
      target.write line.gsub(/\.csproj/, ".#{framework}.g.csproj")
    end
	end
end