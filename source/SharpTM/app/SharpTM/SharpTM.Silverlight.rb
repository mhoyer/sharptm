class CSProjModifier
  def fix(doc)
    # Fix the <Import> element to load the correct targets for msbuild
	doc.elements["Project/Import"].attributes["Project"] = '$(MSBuildExtensionsPath)\\Microsoft\\Silverlight\\V3.0\\Microsoft.Silverlight.CSharp.targets'

    # Add the SILVERLIGHT constant
    doc.elements.each("Project/PropertyGroup/DefineConstants") { |element| element.add_text ";SILVERLIGHT" }

    # Remove possible settings
    doc.elements.each("Project/ItemGroup/Compile") { |element| element.parent.delete(element) if element.attributes["Include"] == "Properties\\Settings.Designer.cs" }
	doc.elements.each("Project/ItemGroup/None") { |element| element.parent.delete(element) if element.attributes["Include"] == "Properties\\Settings.settings" }

    # Add the <SilverlightApplication> element
    sl_app = Element.new("SilverlightApplication")
    sl_app.text = "false"
    doc.elements["Project/PropertyGroup"] << sl_app

    # Add the <ProjectTypeGuids> element
    sl_project_type = Element.new("ProjectTypeGuids")
    sl_project_type.text = "{A1591282-1198-4647-A2B1-27E5FF5F6F3B};{fae04ec0-301f-11d3-bf4b-00c04f79efbc}"
    doc.elements["Project/PropertyGroup"] << sl_project_type
	
	# Remove log4net reference
	doc.elements.each("Project/ItemGroup/Reference") { |element| element.parent.delete(element) if element.attributes["Include"].match(/^log4net/) }
  end
end