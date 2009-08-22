class Xunit
	def self.run(attributes)
		tool = attributes.fetch(:tool)
		reportDirectory = attributes.fetch(:reportdirectory, '.').to_absolute
		assembly = attributes.fetch(:assembly).to_absolute
		
		reportFile = assembly.name.ext('html').in(reportDirectory).to_absolute
		FileUtils.mkdir_p reportFile.dirname
		
		xunit = tool.to_absolute
		
		Dir.chdir(assembly.dirname) do
			sh "#{xunit.escape} #{assembly.escape} #{'/teamcity ' if ENV['TEAMCITY_PROJECT_NAME']}/html #{reportFile.escape}"
		end
	end
end