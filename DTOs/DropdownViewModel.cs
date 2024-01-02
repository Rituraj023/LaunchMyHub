namespace LaunchMyHub.DTOs
{
    public class DropdownViewModel
    {
        public DropdownViewModel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
