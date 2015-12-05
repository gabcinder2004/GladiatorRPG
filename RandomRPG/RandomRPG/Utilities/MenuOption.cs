namespace RandomRPG.Utilities
{
    public class MenuOption
    {
        public char Choice { get; set; }

        public string Value { get; set; }

        public MenuOption(char choice, string value)
        {
            Choice = choice;
            Value = value;
        }
    }
}