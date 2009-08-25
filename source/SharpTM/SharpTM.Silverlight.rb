class SlnModifier
  def fix(target, framework)
      source = File.new(@source)

      remove_next_line = false
      project_guids = Array.new
      source.each_line do |line|

        if remove_next_line then
          remove_next_line = false
          next
        end
        
        project_guid = /^Project\("\{(.*)\}"\) = \".*\", \"(.*Tests?.csproj)\", \"(.*)\"/.match(line)
        if not project_guid.nil? then
          remove_next_line = true
          project_guids << project_guid[3]
          #puts project_guid[3]
          next
        end

        remove_next_line = false
        project_guids.each do |guid|
          if not line.index(guid).nil? then
            remove_next_line = true
            break
          end
        end

        if remove_next_line then
          remove_next_line = false
          next
        end

		target.write line.gsub(/\.csproj/, ".#{framework}.g.csproj")
      end
  end
end