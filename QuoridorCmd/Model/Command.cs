namespace QuoridorCmd.Model
{
    class Command
    {
        public CommandAction Action { get; }
        public string Value { get; }

        public Command(CommandAction action, string value)
        {
            Action = action;
            Value = value;
        }

        public static Command ParseString(string command)
        {
            string[] strs = command.Trim().Split(' ');

            if (strs.Length == 1)
            {
                return new Command(new CommandAction(strs[0]), "");
            }
            else
            {
                string value = strs[1];

                if (value.Length == 3)
                {
                    return new Command(new CommandAction(strs[0]), value.Substring(0, 2).ToUpper() + value[2].ToString().ToLower());
                }
                else
                {
                    return new Command(new CommandAction(strs[0]), value.ToUpper());
                }
                
            }
        }

        public override string ToString()
        {
            return $"{Action} {Value}".Trim();
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Command c = (Command)obj;

                return Action.Equals(c.Action) && Value.Equals(c.Value);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
