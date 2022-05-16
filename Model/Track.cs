namespace Model;

public class Track
{
    public Track(string? name, SectionTypes[] sections)
    {
        Name = name;
        Sections = ConvertToSection(sections);
        // this.Sections = sections;
    }

    public string? Name { get; set; }
    public LinkedList<Section> Sections { get; set; }

    private static LinkedList<Section> ConvertToSection(IEnumerable<SectionTypes> sectionTypes)
    {
        var result = new LinkedList<Section>();
        foreach (var sectionType in sectionTypes) result.AddLast(new Section(sectionType));

        return result;
    }
}