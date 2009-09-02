require 'rake'
require 'erb'

class AssemblyInfoBuilder
	attr_reader :attributes

	def initialize(build_number, template)
		@build_number = build_number
    @template = template
	end

	def patch()
    @content = ""

    File.open(@template, 'r') do |file|
      file.readlines.each { |line|
        @content << line.
          gsub(/.*assembly:\s*AssemblyFileVersion.*/, '').
          gsub(/.*assembly:\s*AssemblyVersion.*/, '')
      }
    end

    @content = @content.strip
    @content << "\n[assembly: AssemblyFileVersion(\"#{@build_number}\")]\n"
    @content << "[assembly: AssemblyVersion(\"#{@build_number}\")]\n"

		file = File.open(@template, 'w')
    file << @content
    file.close
		
		puts "modified file #{@template}"
	end
end